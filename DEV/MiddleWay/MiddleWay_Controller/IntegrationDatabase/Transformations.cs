using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class Transformations
    {
        //[MaxLength( , ErrorMessage = "")]
        //public    { get; set; } // 
        public int TransformationUid { get; set; } // INT IDENTITY(1, 1) NOT NULL,

        public int ProcessUid { get; set; } // INT NOT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string Function { get; set; } // VARCHAR(100) NOT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string SourceColumn { get; set; } // VARCHAR(100) NOT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string DestinationColumn { get; set; } // VARCHAR(100) NULL,

        public bool Enabled { get; set; } // BIT
    }
}
