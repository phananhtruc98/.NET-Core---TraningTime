using System.Threading.Tasks;
using GrooveMessengerAPI.Areas.Chat.Models;
using GrooveMessengerDAL.Models.CustomModel;
using GrooveMessengerDAL.Models.Notification;

namespace GrooveMessengerAPI.Hubs.Utils
{
    public interface IMessageHubClient
    {
        Task SendMessage(Message message);
        Task SendRemovedMessage(Message message);
        Task SendEditedMessage(Message message);
        Task SendMessageViewingStatus(string fromUser);
        Task SendUnreadMessagesAmount(UnreadMessageModel unreadMessage);
        Task PushNotification(NotificationModel notificationModel);
    }

    public enum MessageEventTypes
    {
        Broadcast = 0,
        Individual = 1,
        Group = 2
    }
}