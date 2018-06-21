using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class ImsStudentContacts
    {
        public int StudentContactId { get; set; }
        public string ContactRowId { get; set; }
        public string GivenId { get; set; }
        public string LocationGivenId { get; set; }
        public string CoreTypeId { get; set; }
        public string ContactFullName { get; set; }
        public string ContactRelation { get; set; }
        public string ContactOrder { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string CellPhone { get; set; }
        public string WorkPhone { get; set; }
        public string HomePhone { get; set; }
        public string OtherPhone { get; set; }
        public string Email { get; set; }
        public string OtherContactBearings { get; set; }
        public string Other { get; set; }
    }
}
