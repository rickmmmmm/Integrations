using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class Mappings
    {
        //[MaxLength( , ErrorMessage = "")]
        //public bla   { get; set; } // 
        public bla MappingsUid INT IDENTITY(1, 1) NOT NULL,

public bla ProcessUid        INT NOT NULL,
    public bla SourceColumn VARCHAR(100) NOT NULL,

public bla DestinationColumn VARCHAR(100) NOT NULL,

public bla Enabled           BIT
    }
}
