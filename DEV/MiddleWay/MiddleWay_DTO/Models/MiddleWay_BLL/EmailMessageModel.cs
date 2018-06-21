using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWay_DTO.Models.MiddleWay_BLL
{
    public class EmailMessageModel : MessageModel
    {
        public string Sender { get; set; }
        public List<string> CopyRecipients { get; set; }
        public List<string> BlindCopyRecipients { get; set; }
        public string Subject { get; set; }
        public string FileAttachment { get; set; }
        public DateTime SentDate { get; set; }
    }
}
