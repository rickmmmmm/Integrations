using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblUserGroupSettings
    {
        public int GroupId { get; set; }
        public bool FindAbook { get; set; }
        public bool BookOrders { get; set; }
        public bool StatePubReq { get; set; }
        public bool CampusReq { get; set; }
        public bool QuickShip { get; set; }
        public bool LostDamagedPaid { get; set; }
        public bool TextbookAdjustments { get; set; }
        public bool Membership { get; set; }
        public bool MasterTitlesDb { get; set; }
        public bool ChangeIsbn { get; set; }
        public bool CampusMembership { get; set; }
        public bool CampusCourseEnrollment { get; set; }
        public bool ExportLostBookData { get; set; }
        public bool InitializeCampus { get; set; }
        public bool CampusReplacements { get; set; }
        public bool LetterEditor { get; set; }
        public bool UserAccounts { get; set; }
        public bool Preferences { get; set; }
        public bool SchoolDiscipline { get; set; }
        public bool ChangeStudentId { get; set; }
        public bool UseAttachedReader { get; set; }
        public bool DownloadEditDataFiles { get; set; }
        public bool PrintBarcodeLabels { get; set; }
        public bool ConnectReader { get; set; }
        public bool OrderBarcodeLabels { get; set; }
        public bool Closing { get; set; }
        public bool Reindex { get; set; }
        public bool TextbookDetails { get; set; }
        public bool Relationships { get; set; }
        public bool AddNewRecords { get; set; }
        public bool BinEditor { get; set; }
        public bool Distribute { get; set; }
        public bool Reassign { get; set; }
        public bool GenerateReports { get; set; }
    }
}
