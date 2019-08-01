using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Models.Notification
{
    public class NotificationModel
    {
        public Guid ConvId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
    }
}
