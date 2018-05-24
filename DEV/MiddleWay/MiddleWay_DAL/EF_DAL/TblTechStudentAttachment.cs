﻿using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechStudentAttachment
    {
        public int StudentAttachmentUid { get; set; }
        public int StudentUid { get; set; }
        public string FileName { get; set; }
        public string UploadedFileName { get; set; }
        public string FilePath { get; set; }
        public string Notes { get; set; }
        public bool? MarkedForDeletion { get; set; }
        public long FileSize { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
