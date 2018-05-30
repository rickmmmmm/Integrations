using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class ProcessErrors
    {
        //[MaxLength( , ErrorMessage = "")]
        //public bla   { get; set; } // 
        public bla ProcessErrorUid INT IDENTITY(1, 1) NOT NULL,

public bla ProcessUid       INT NOT NULL,
    public bla ErrorNumber
        INT NULL,
    public bla ErrorDescription VARCHAR(250)  NOT NULL,

   public bla ErrorField       NVARCHAR(100) NULL,
    public bla CreatedDate DATE
    }
}
