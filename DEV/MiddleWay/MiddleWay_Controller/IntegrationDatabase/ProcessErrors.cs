using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class ProcessErrors
    {
        public int ProcessErrorUid { get; set; } // INT IDENTITY(1, 1) NOT NULL,

        public int ProcessUid { get; set; } // INT NOT NULL,

        public int ErrorNumber { get; set; } // INT NULL,

        [MaxLength(250, ErrorMessage = "")]
        public string ErrorDescription { get; set; } // VARCHAR(250)  NOT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string ErrorField { get; set; } // VARCHAR(100) NULL,

        public DateTime CreatedDate { get; set; } // DATE
    }
}
