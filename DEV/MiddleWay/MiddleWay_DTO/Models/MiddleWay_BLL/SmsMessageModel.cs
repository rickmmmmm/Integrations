using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay_BLL
{
    public class SmsMessageModel : MessageModel
    {
        /// <summary>
        /// Alias property for the Body property of the MessageModel
        /// </summary>
        [Required]
        public string Message
        {
            get
            {
                return this.Body;
            }
            set
            {
                this.Body = value;
            }
        }
    }
}
