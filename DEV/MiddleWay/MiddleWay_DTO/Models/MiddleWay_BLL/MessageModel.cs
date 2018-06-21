using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MiddleWay_DTO.Enumerations;

namespace MiddleWay_DTO.Models.MiddleWay_BLL
{
    public class MessageModel
    {
        [Required]
        public List<string> Recipients { get; set; }
        [Required]
        public string Body { get; set; }
        //NotificationType NotificationType { get; set; }
        TextEncoding MessageEncoding { get; set; }
    }
}
