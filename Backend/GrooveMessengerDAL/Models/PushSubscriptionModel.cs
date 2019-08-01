using System;
using System.Collections.Generic;
using System.Text;
using WebPush;

namespace GrooveMessengerDAL.Models
{
    public class PushSubscriptionModel:PushSubscription
    {
        public Guid ConvId { get; set; }
    }
}
