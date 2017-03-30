using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TextMessage : IMessage
    {
        public List<string> BlindReceivers { get; set; }

        public string Body { get; set; }

        public string fileAttachment { get; set; }

        public List<string> Receivers { get; set; }

        public string Sender { get; set; }

        public string Subject { get; set; }
    }
}
