using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class EmailMessage : IMessage
    {
        public List<string> BlindReceivers { get; set; }
        public string Body { get; set; }
        public List<string> Receivers { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string FileAttachment { get; set; }
        public DateTime SentDate { get; set; }
    }
}
