using GrooveMessengerDAL.Models;
using GrooveMessengerDAL.Models.Notification;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using WebPush;

namespace GrooveMessengerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        public static List<PushSubscriptionModel> Subscriptions { get; set; } = new List<PushSubscriptionModel>();

        [HttpPost("subscribe")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public void Subscribe([FromBody] PushSubscriptionModel sub)
        {
            Subscriptions.Add(sub);
        }

        [HttpPost("unsubscribe")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public void Unsubscribe([FromBody] PushSubscriptionModel sub)
        {
            var item = Subscriptions.FirstOrDefault(s => s.Endpoint == sub.Endpoint);
            if (item != null)
            {
                Subscriptions.Remove(item);
            }
        }

        //[HttpPost("broadcast")]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //public void Broadcast([FromBody] NotificationModel message, [FromServices] VapidDetails vapidDetails)
        //{
        //    var client = new WebPushClient();
        //    var serializedMessage = JsonConvert.SerializeObject(message);
        //    foreach (var PushSubscriptionModel in Subscriptions.Where(m => m.ConvId == message.ConvId))
        //    {
        //        client.SendNotification(PushSubscriptionModel, serializedMessage, vapidDetails);
        //    }

        //}
        [HttpPost("broadcast")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public void Broadcast([FromBody] PushSubscription message, [FromServices] VapidDetails vapidDetails)
        {
            var client = new WebPushClient();
            var serializedMessage = JsonConvert.SerializeObject(message);
            client.SendNotification(message, serializedMessage, vapidDetails);
        }
    }
}