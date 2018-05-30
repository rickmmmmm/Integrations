using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class Transformations
    {
        //[MaxLength( , ErrorMessage = "")]
        //public bla   { get; set; } // 
        public bla TransformationUid INT IDENTITY(1, 1) NOT NULL,

ProcessUid        INT NOT NULL,
    Function VARCHAR(100) NOT NULL,

SourceColumn      VARCHAR(100) NOT NULL,

DestinationColumn VARCHAR(100) NULL,
    Enabled BIT
    }
}
