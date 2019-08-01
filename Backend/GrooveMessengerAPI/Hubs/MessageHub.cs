using GrooveMessengerAPI.Constants;
using GrooveMessengerAPI.Hubs.Utils;
using GrooveMessengerAPI.PushNotification;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace GrooveMessengerAPI.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MessageHub : HubBase<IMessageHubClient>
    {
        public MessageHub(
            HubConnectionStorage connectionStore,
             NotificationConnectionStorage notificationConnectionStorage
        ) : base(connectionStore, notificationConnectionStorage, HubConstant.MessageHubTopic)
        {
        }

        public override Task OnConnectedAsync()
        {
            // Do something just related to message hub
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            // Do something just related to message hub            
            return base.OnDisconnectedAsync(exception);
        }
    }
}