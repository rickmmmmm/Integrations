using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IMessage
    {
        string Sender { get; set; }
        List<string> Receivers { get; set; }
        List<string> BlindReceivers { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
    }
}
