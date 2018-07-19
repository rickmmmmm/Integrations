using MiddleWay_DTO.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace MiddleWay_DTO.Models.MiddleWay_BLL
{
    public class MessageModel
    {
        [Required]
        public string Recipients { get; set; }
        [Required]
        public string Body { get; set; }
        //NotificationType NotificationType { get; set; }
        TextEncoding MessageEncoding { get; set; }
    }
}
