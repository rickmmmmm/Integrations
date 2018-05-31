using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class Mappings
    {
        public int MappingsUid { get; set; } // INT IDENTITY(1, 1) NOT NULL,

        public int ProcessUid { get; set; } // INT NOT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string SourceColumn { get; set; } // VARCHAR(100) NOT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string DestinationColumn { get; set; } // VARCHAR(100) NOT NULL,

        public bool Enabled { get; set; } // BIT
    }
}
