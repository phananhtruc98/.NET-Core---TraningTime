using System;
using System.Linq;
using System.Threading.Tasks;
using GrooveMessengerAPI.Hubs.Utils;
using GrooveMessengerAPI.PushNotification;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using WebPush;

namespace GrooveMessengerAPI.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HubBase<T> : Hub<T> where T : class
    {
        protected HubConnectionStorage ConnectionStore;
        protected NotificationConnectionStorage NotificationConnectionStorage;
        protected string Topic;


        public HubBase(HubConnectionStorage connectionStore, NotificationConnectionStorage notificationConnectionStorage, string topic)
        {
            this.NotificationConnectionStorage = notificationConnectionStorage;
            this.ConnectionStore = connectionStore;
            this.Topic = topic;
        }

        public override Task OnConnectedAsync()
        {
            var name = Context.User.Identity.Name;

            if (!ConnectionStore.GetConnections(Topic, name).Contains(Context.ConnectionId))
            {
                var conn = Context.ConnectionId;
                ConnectionStore.Add(Topic, name, conn);
                PushSubscription sub = new PushSubscription()
                {
                    //Auth=123,
                    //Endpoint=123,
                    //P256DH=
                };
                NotificationConnectionStorage.Subscribe(sub);
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var name = Context.User.Identity.Name;
            ConnectionStore.Remove(Topic, name, Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}