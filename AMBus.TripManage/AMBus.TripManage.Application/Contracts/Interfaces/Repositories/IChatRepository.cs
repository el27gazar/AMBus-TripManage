using AMBus.TripManage.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Contracts.Interfaces.Repositories
{
    public interface IChatRepository
    {
        // Conversations
        Task<ChatConversation?> GetConversationAsync(Guid id);
        Task<ChatConversation?> GetConversationWithMessagesAsync(Guid id);
        Task<IEnumerable<ChatConversation>> GetUserConversationsAsync(Guid userId);
        Task<IEnumerable<ChatConversation>> GetAllConversationsAsync(
            string? status, int page, int pageSize);
        Task<int> GetTotalConversationsCountAsync(string? status);
        Task AddConversationAsync(ChatConversation conversation);

        // Messages
        Task<IEnumerable<ChatMessage>> GetMessagesAsync(
            Guid conversationId, int page, int pageSize);
        Task<int> GetMessagesCountAsync(Guid conversationId);
        Task AddMessageAsync(ChatMessage message);
        Task MarkMessagesAsReadAsync(Guid conversationId, Guid readByUserId);
        Task<ChatConversation?> GetActiveConversationByUserAsync(Guid userId);

        Task<int> SaveChangesAsync();
    }
}
