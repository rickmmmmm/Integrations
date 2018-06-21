using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblArchStudentsDistribution
    {
        public int ArchiveDistributionId { get; set; }
        public int ArchiveStudentId { get; set; }
        public int ArchiveId { get; set; }
        public int DistributionId { get; set; }
        public string StudentId { get; set; }
        public string Isbn { get; set; }
        public string Accession { get; set; }
        public string Code { get; set; }
        public string SourceType { get; set; }
        public string Source { get; set; }
        public int? Amount { get; set; }
        public int? Copies { get; set; }
        public string Notes { get; set; }
        public int UserId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ArchiveUserId { get; set; }
        public DateTime ArchiveDate { get; set; }
        public int StudentsUid { get; set; }
    }
}
