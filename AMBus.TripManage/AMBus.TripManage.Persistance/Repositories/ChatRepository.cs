using AMBus.TripManage.Application.Contracts.Interfaces.Repositories;
using AMBus.TripManage.Domain.Entites;
using AMBus.TripManage.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Persistance.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext _ctx;

        public ChatRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task<ChatConversation?> GetConversationAsync(Guid id)
            => await _ctx.ChatConversations
                .Include(c => c.User)
                .Include(c => c.Admin)
                .FirstOrDefaultAsync(c => c.Id == id);
      
        public async Task<ChatConversation?> GetConversationWithMessagesAsync(Guid id)
            => await _ctx.ChatConversations
                .Include(c => c.User)
                .Include(c => c.Admin)
                .Include(c => c.Messages
                    .OrderBy(m => m.CreatedDate))
                    .ThenInclude(m => m.Sender)
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<IEnumerable<ChatConversation>> GetUserConversationsAsync(
            Guid userId)
            => await _ctx.ChatConversations
                .Include(c => c.Admin)
                .Include(c => c.Messages
                    .OrderByDescending(m => m.CreatedDate)
                    .Take(1))
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.LastModifiedDate)
                .ToListAsync();

        public async Task<IEnumerable<ChatConversation>> GetAllConversationsAsync(
            string? status, int page, int pageSize)
        {
            var query = _ctx.ChatConversations
                .Include(c => c.User)
                .Include(c => c.Admin)
                .Include(c => c.Messages
                    .OrderByDescending(m => m.CreatedDate)
                    .Take(1))
                .AsQueryable();

            if (!string.IsNullOrEmpty(status) &&
                Enum.TryParse<ConversationStatus>(status, out var s))
                query = query.Where(c => c.Status == s);

            return await query
                .OrderByDescending(c => c.LastModifiedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalConversationsCountAsync(string? status)
        {
            var query = _ctx.ChatConversations.AsQueryable();

            if (!string.IsNullOrEmpty(status) &&
                Enum.TryParse<ConversationStatus>(status, out var s))
                query = query.Where(c => c.Status == s);

            return await query.CountAsync();
        }

        public async Task AddConversationAsync(ChatConversation conv)
            => await _ctx.ChatConversations.AddAsync(conv);

        public async Task<IEnumerable<ChatMessage>> GetMessagesAsync(
            Guid conversationId, int page, int pageSize)
            => await _ctx.ChatMessages
                .Include(m => m.Sender)
                .Where(m => m.ConversationId == conversationId)
                .OrderByDescending(m => m.CreatedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(m => m.CreatedDate) // ترتيب تصاعدي للعرض
                .ToListAsync();

        public async Task<int> GetMessagesCountAsync(Guid conversationId)
            => await _ctx.ChatMessages
                .CountAsync(m => m.ConversationId == conversationId);

        public async Task AddMessageAsync(ChatMessage message)
            => await _ctx.ChatMessages.AddAsync(message);

        public async Task MarkMessagesAsReadAsync(
            Guid conversationId, Guid readByUserId)
        {
            // نعلم بالرسائل اللي مش بعتها الـ user الحالي كمقروءة
            var unread = await _ctx.ChatMessages
                .Where(m =>
                    m.ConversationId == conversationId &&
                    m.SenderId != readByUserId &&
                    !m.IsRead)
                .ToListAsync();

            foreach (var msg in unread)
            {
                msg.IsRead = true;
                msg.ReadAt = DateTime.UtcNow;
            }
        }
        public async Task<ChatConversation?> GetActiveConversationByUserAsync(Guid userId)
                 => await _ctx.ChatConversations
                     .Where(c => c.UserId == userId &&
                               (c.Status == ConversationStatus.Open ||
                                  c.Status == ConversationStatus.InProgress))
                     .OrderByDescending(c => c.CreatedDate)
                     .FirstOrDefaultAsync();
        public async Task<int> SaveChangesAsync()
            => await _ctx.SaveChangesAsync();
    }
}
