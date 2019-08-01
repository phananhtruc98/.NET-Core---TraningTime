using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPush;

namespace GrooveMessengerAPI.PushNotification
{
    public class NotificationConnectionStorage
    {
        public static List<PushSubscription> Subscriptions { get; set; } = new List<PushSubscription>();
        //[HttpPost("subscribe")]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        public void Subscribe( PushSubscription sub)
        {
            Subscriptions.Add(sub);
        }

        //[HttpPost("unsubscribe")]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        public void Unsubscribe( PushSubscription sub)
        {
            var item = Subscriptions.FirstOrDefault(s => s.Endpoint == sub.Endpoint);
            if (item != null)
            {
                Subscriptions.Remove(item);
            }
        }
    }
}
