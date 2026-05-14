using AMBus.TripManage.Application.Contracts.Interfaces.Repositories;
using AMBus.TripManage.Application.Dtos.Chat;
using AMBus.TripManage.Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AMBus.TripManage.Persistance.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IChatRepository _chatRepo;

        public ChatHub(IChatRepository chatRepo)
            => _chatRepo = chatRepo;

        // ════════════════════════════════════════════════
        //  Connection Events
        // ════════════════════════════════════════════════

        public override async Task OnConnectedAsync()
        {
            var userId = GetCurrentUserId();
            var isAdmin = IsAdmin();

            // كل Admin يدخل قناة "admins" أوتوماتيك عند الاتصال
            if (isAdmin)
                await Groups.AddToGroupAsync(
                    Context.ConnectionId, "admins");

            // كل User يدخل قناة خاصة بيه عشان نبعتله إشعارات
            await Groups.AddToGroupAsync(
                Context.ConnectionId, $"user-{userId}");

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = GetCurrentUserId();
            var isAdmin = IsAdmin();

            if (isAdmin)
                await Groups.RemoveFromGroupAsync(
                    Context.ConnectionId, "admins");

            await Groups.RemoveFromGroupAsync(
                Context.ConnectionId, $"user-{userId}");

            await base.OnDisconnectedAsync(exception);
        }

        // ════════════════════════════════════════════════
        //  Join Conversation
        //  User أو Admin يدخل غرفة المحادثة
        // ════════════════════════════════════════════════

        public async Task JoinConversation(string conversationId)
        {
            if (!Guid.TryParse(conversationId, out var convId))
            {
                await Clients.Caller.SendAsync("Error",
                    "معرف المحادثة غير صحيح.");
                return;
            }

            var userId = GetCurrentUserId();
            var isAdmin = IsAdmin();

            var conv = await _chatRepo.GetConversationAsync(convId);

            if (conv is null)
            {
                await Clients.Caller.SendAsync("Error",
                    "المحادثة غير موجودة.");
                return;
            }

            // User يدخل محادثته بس
            if (!isAdmin && conv.UserId != userId)
            {
                await Clients.Caller.SendAsync("Error",
                    "ليس لديك صلاحية.");
                return;
            }

            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                $"conv-{conversationId}");

            // إشعار بالدخول للطرف الآخر
            await Clients
                .GroupExcept(
                    $"conv-{conversationId}",
                    Context.ConnectionId)
                .SendAsync("UserJoined", new
                {
                    userId,
                    isAdmin,
                    conversationId
                });
        }

        // ════════════════════════════════════════════════
        //  Leave Conversation
        // ════════════════════════════════════════════════

        public async Task LeaveConversation(string conversationId)
        {
            await Groups.RemoveFromGroupAsync(
                Context.ConnectionId,
                $"conv-{conversationId}");
        }

        // ════════════════════════════════════════════════
        //  Send Message
        // ════════════════════════════════════════════════

        public async Task SendMessage(
            string conversationId,
            string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                await Clients.Caller.SendAsync("Error",
                    "الرسالة فارغة.");
                return;
            }

            if (content.Length > 2000)
            {
                await Clients.Caller.SendAsync("Error",
                    "الرسالة تتجاوز 2000 حرف.");
                return;
            }

            if (!Guid.TryParse(conversationId, out var convId))
            {
                await Clients.Caller.SendAsync("Error",
                    "معرف المحادثة غير صحيح.");
                return;
            }

            var userId = GetCurrentUserId();
            var isAdmin = IsAdmin();
            var senderName = GetCurrentUserName();
            var now = DateTime.UtcNow;

            var conv = await _chatRepo.GetConversationAsync(convId);

            if (conv is null)
            {
                await Clients.Caller.SendAsync("Error",
                    "المحادثة غير موجودة.");
                return;
            }

            if (!isAdmin && conv.UserId != userId)
            {
                await Clients.Caller.SendAsync("Error",
                    "ليس لديك صلاحية.");
                return;
            }

            if (conv.Status == ConversationStatus.Closed)
            {
                await Clients.Caller.SendAsync("Error",
                    "المحادثة مغلقة ولا يمكن الإرسال.");
                return;
            }

            // ── إنشاء الرسالة ─────────────────────────────
            var message = new ChatMessage
            {
                Id = Guid.NewGuid(),
                ConversationId = convId,
                SenderId = userId,
                Content = content.Trim(),
                IsRead = false,
                CreatedBy = userId.ToString(),
                CreatedDate = now,
                LastModifiedBy = userId.ToString(),
                LastModifiedDate = now
            };

            await _chatRepo.AddMessageAsync(message);

            // ── لو Admin أول ما يرد — ربطه بالمحادثة ─────
            if (isAdmin && conv.AdminId is null)
            {
                conv.AdminId = userId;
                conv.Status = ConversationStatus.InProgress;
                conv.LastModifiedBy = userId.ToString();
                conv.LastModifiedDate = now;
            }

            // ── تحديث LastModified على المحادثة ────────────
            conv.LastModifiedDate = now;

            await _chatRepo.SaveChangesAsync();

            // ── بناء الـ DTO ───────────────────────────────
            var dto = new ChatMessageDto(
                Id: message.Id,
                ConversationId: convId,
                SenderId: userId,
                SenderName: senderName,
                SenderIsAdmin: isAdmin,
                Content: content.Trim(),
                IsRead: false,
                ReadAt: null,
                CreatedDate: now
            );

            // ── إرسال لكل في المحادثة ─────────────────────
            await Clients
                .Group($"conv-{conversationId}")
                .SendAsync("ReceiveMessage", dto);

            // ── لو User بعت — بلّغ كل الـ Admins ──────────
            if (!isAdmin)
            {
                await Clients
                    .Group("admins")
                    .SendAsync("NewMessageFromUser", new
                    {
                        conversationId,
                        userId,
                        senderName,
                        preview = content.Length > 50
                            ? content[..50] + "..."
                            : content,
                        sentAt = now
                    });
            }
        }

        // ════════════════════════════════════════════════
        //  Typing Indicator
        // ════════════════════════════════════════════════

        public async Task Typing(string conversationId)
        {
            var userId = GetCurrentUserId();
            var senderName = GetCurrentUserName();
            var isAdmin = IsAdmin();

            // بعت فقط للطرف الآخر مش لنفسك
            await Clients
                .GroupExcept(
                    $"conv-{conversationId}",
                    Context.ConnectionId)
                .SendAsync("UserTyping", new
                {
                    conversationId,
                    userId,
                    senderName,
                    isAdmin
                });
        }

        // ════════════════════════════════════════════════
        //  Stop Typing
        // ════════════════════════════════════════════════

        public async Task StopTyping(string conversationId)
        {
            var userId = GetCurrentUserId();

            await Clients
                .GroupExcept(
                    $"conv-{conversationId}",
                    Context.ConnectionId)
                .SendAsync("UserStoppedTyping", new
                {
                    conversationId,
                    userId
                });
        }

        // ════════════════════════════════════════════════
        //  Mark As Read
        // ════════════════════════════════════════════════

        public async Task MarkAsRead(string conversationId)
        {
            if (!Guid.TryParse(conversationId, out var convId))
                return;

            var userId = GetCurrentUserId();

            await _chatRepo.MarkMessagesAsReadAsync(convId, userId);
            await _chatRepo.SaveChangesAsync();

            // إشعار الطرف الآخر
            await Clients
                .GroupExcept(
                    $"conv-{conversationId}",
                    Context.ConnectionId)
                .SendAsync("MessagesRead", new
                {
                    conversationId,
                    readBy = userId,
                    readAt = DateTime.UtcNow
                });
        }

        // ════════════════════════════════════════════════
        //  Close Conversation  [Admin only]
        // ════════════════════════════════════════════════

        public async Task CloseConversation(string conversationId)
        {
            if (!IsAdmin())
            {
                await Clients.Caller.SendAsync("Error",
                    "ليس لديك صلاحية إغلاق المحادثة.");
                return;
            }

            if (!Guid.TryParse(conversationId, out var convId))
                return;

            var adminId = GetCurrentUserId();
            var now = DateTime.UtcNow;

            var conv = await _chatRepo.GetConversationAsync(convId);
            if (conv is null) return;

            if (conv.Status == ConversationStatus.Closed)
            {
                await Clients.Caller.SendAsync("Error",
                    "المحادثة مغلقة بالفعل.");
                return;
            }

            conv.Status = ConversationStatus.Closed;
            conv.LastModifiedBy = adminId.ToString();
            conv.LastModifiedDate = now;

            await _chatRepo.SaveChangesAsync();

            // إشعار كل أعضاء المحادثة بالإغلاق
            await Clients
                .Group($"conv-{conversationId}")
                .SendAsync("ConversationClosed", new
                {
                    conversationId,
                    closedBy = GetCurrentUserName(),
                    closedAt = now
                });
        }

        // ════════════════════════════════════════════════
        //  Reopen Conversation  [Admin only]
        // ════════════════════════════════════════════════

        public async Task ReopenConversation(string conversationId)
        {
            if (!IsAdmin()) return;

            if (!Guid.TryParse(conversationId, out var convId))
                return;

            var adminId = GetCurrentUserId();
            var now = DateTime.UtcNow;

            var conv = await _chatRepo.GetConversationAsync(convId);
            if (conv is null) return;

            conv.Status = ConversationStatus.InProgress;
            conv.LastModifiedBy = adminId.ToString();
            conv.LastModifiedDate = now;

            await _chatRepo.SaveChangesAsync();

            await Clients
                .Group($"conv-{conversationId}")
                .SendAsync("ConversationReopened", new
                {
                    conversationId,
                    reopenedAt = now
                });
        }

        // ════════════════════════════════════════════════
        //  Helpers
        // ════════════════════════════════════════════════

        private Guid GetCurrentUserId()
            => Guid.Parse(Context.User!
                .FindFirstValue(ClaimTypes.NameIdentifier)!);

        private string GetCurrentUserName()
            => Context.User!
                .FindFirstValue(ClaimTypes.Name) ?? "Unknown";

        private bool IsAdmin()
            => Context.User!.IsInRole("Admin");
    }
}
