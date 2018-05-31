using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class Configurations
    {
        //[MaxLength( , ErrorMessage = "")]
        //public    { get; set; } // 
        public int ConfigurationUid { get; set; } // INT NOT NULL,

        public int ProcessUid { get; set; } // INT NOT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string ConfigurationName { get; set; } // VARCHAR(100) NOT NULL,

        [MaxLength(250, ErrorMessage = "")]
        public string ConfigurationValue { get; set; } // VARCHAR(250) NOT NULL,

        public bool Enabled { get; set; } // BIT NOT NULL,
    }
}
