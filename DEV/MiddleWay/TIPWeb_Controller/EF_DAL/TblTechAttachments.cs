using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechAttachments
    {
        public TblTechAttachments()
        {
            TblTechAttachmentScheduleLink = new HashSet<TblTechAttachmentScheduleLink>();
        }

        public int AttachmentUid { get; set; }
        public string Name { get; set; }
        public string ApplicationArea { get; set; }
        public string Type { get; set; }
        public byte[] Data { get; set; }
        public string FileName { get; set; }
        public string UploadedFileName { get; set; }
        public string FilePath { get; set; }
        public bool MarkForDeletion { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<TblTechAttachmentScheduleLink> TblTechAttachmentScheduleLink { get; set; }
    }
}
