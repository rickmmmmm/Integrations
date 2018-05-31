using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class Processes
    {
        //[MaxLength( , ErrorMessage = "")]
        //public    { get; set; } // 
        public int ProcessUid { get; set; } // INT IDENTITY(1, 1) NOT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string Client { get; set; } // VARCHAR(100) NOT NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string ProcessName { get; set; } // VARCHAR(50)  NOT NULL,

        [MaxLength(250, ErrorMessage = "")]
        public string Description { get; set; } // VARCHAR(250) NOT NULL,

        public bool Enabled { get; set; } // BIT CONSTRAINT[DF_Processes_Enabled] DEFAULT((0)) NOT NULL,

        public DateTime CreatedDate { get; set; } // DATE
    }
}
