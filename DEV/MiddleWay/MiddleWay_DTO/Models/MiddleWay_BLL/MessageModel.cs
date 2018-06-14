using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MiddleWay_DTO.Enumerations;

namespace MiddleWay_DTO.Models
{
    public class MessageModel
    {
        [Required]
        public List<string> Recipients { get; set; }
        [Required]
        public string Body { get; set; }
        MessageType MessageType { get; set; }
        TextEncoding MessageEncoding { get; set; }
    }
}
