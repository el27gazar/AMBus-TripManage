export interface ChatMessageDto {
    id: string;
  conversationId: string;
  senderId: string;
  senderName: string;
  senderIsAdmin: boolean;
  content: string;
  isRead: boolean;
  readAt?: Date;
  createdDate: Date;
}
