using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblMessages
    {
        public int MessageId { get; set; }
        public int ToUserId { get; set; }
        public int FromUserId { get; set; }
        public string Message { get; set; }
        public DateTime? SentDate { get; set; }
        public bool? MessageRead { get; set; }
        public DateTime? ReadDate { get; set; }
    }
}
