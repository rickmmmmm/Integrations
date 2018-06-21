using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay
{
    public class ProcessesModel
    {
        public int ProcessUid { get; set; }
        public string Client { get; set; }
        public string ProcessName { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
