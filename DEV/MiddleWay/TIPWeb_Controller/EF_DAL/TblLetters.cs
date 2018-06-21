using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblLetters
    {
        public int LetterId { get; set; }
        public string CampusId { get; set; }
        public string LetterName { get; set; }
        public string LetterDescription { get; set; }
        public string LetterBody { get; set; }
        public bool UserCreated { get; set; }
        public int ReportId { get; set; }
    }
}
