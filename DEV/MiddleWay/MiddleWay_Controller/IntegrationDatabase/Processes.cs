using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class Processes
    {
        //[MaxLength( , ErrorMessage = "")]
        //public bla   { get; set; } // 
        public bla ProcessUid INT IDENTITY(1, 1) NOT NULL,

public bla Client      VARCHAR(100) NOT NULL,

public bla ProcessName VARCHAR(50)  NOT NULL,

public bla Description VARCHAR(250) NOT NULL,

public bla Enabled     BIT CONSTRAINT[DF_Processes_Enabled] DEFAULT((0)) NOT NULL,

public bla CreatedDate DATE
    }
}
