using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblUser
    {
        public TblUser()
        {
            TblTechDepartments = new HashSet<TblTechDepartments>();
            TblTechFundingSourceUsers = new HashSet<TblTechFundingSourceUsers>();
            TblTechInventoryDueDatesCreatedByUser = new HashSet<TblTechInventoryDueDates>();
            TblTechInventoryDueDatesLastModifiedByUser = new HashSet<TblTechInventoryDueDates>();
            TblTechPurchases = new HashSet<TblTechPurchases>();
            TblTechScheduleReportCreatedByUser = new HashSet<TblTechScheduleReport>();
            TblTechScheduleReportLastModifiedByUser = new HashSet<TblTechScheduleReport>();
            TblTechTagHistory = new HashSet<TblTechTagHistory>();
            TblTechTransferDepartmentWorkflowsCreatedByUser = new HashSet<TblTechTransferDepartmentWorkflows>();
            TblTechTransferDepartmentWorkflowsLastModifiedByUser = new HashSet<TblTechTransferDepartmentWorkflows>();
            TblTechTransferDepartmentWorkflowsUser = new HashSet<TblTechTransferDepartmentWorkflows>();
            TblTechTransferHistory = new HashSet<TblTechTransferHistory>();
            TblTechTransferStatusWorkflowSettingsCreatedByUser = new HashSet<TblTechTransferStatusWorkflowSettings>();
            TblTechTransferStatusWorkflowSettingsLastModifiedByUser = new HashSet<TblTechTransferStatusWorkflowSettings>();
            TblTechTransferUserWorkflowsApproverUser = new HashSet<TblTechTransferUserWorkflows>();
            TblTechTransferUserWorkflowsCreatedByUser = new HashSet<TblTechTransferUserWorkflows>();
            TblTechTransferUserWorkflowsLastModifiedByUser = new HashSet<TblTechTransferUserWorkflows>();
            TblTechTransferUserWorkflowsUser = new HashSet<TblTechTransferUserWorkflows>();
            TblTechTransferWorkflowHistory = new HashSet<TblTechTransferWorkflowHistory>();
            TblTechTransferWorkflowLinks = new HashSet<TblTechTransferWorkflowLinks>();
            TblTechTransferWorkflows = new HashSet<TblTechTransferWorkflows>();
            TblTechTransfers = new HashSet<TblTechTransfers>();
            TblTechUserDepartments = new HashSet<TblTechUserDepartments>();
            TblTechUserPermissionTemplate = new HashSet<TblTechUserPermissionTemplate>();
            TblTechUserSites = new HashSet<TblTechUserSites>();
            TblTechUserTypeWorkflows = new HashSet<TblTechUserTypeWorkflows>();
            TblUnvAlertUser = new HashSet<TblUnvAlertUser>();
            TblUnvClosings = new HashSet<TblUnvClosings>();
            TblUnvRecipient = new HashSet<TblUnvRecipient>();
            TblUnvRecipientInformationCreatedByUser = new HashSet<TblUnvRecipientInformation>();
            TblUnvRecipientInformationLastModifiedByUser = new HashSet<TblUnvRecipientInformation>();
            TblUnvRecipientInformationUser = new HashSet<TblUnvRecipientInformation>();
            TblUnvRecipientType = new HashSet<TblUnvRecipientType>();
            TblUnvScheduleCreatedByUser = new HashSet<TblUnvSchedule>();
            TblUnvScheduleDay = new HashSet<TblUnvScheduleDay>();
            TblUnvScheduleDayAssignedCreatedByUser = new HashSet<TblUnvScheduleDayAssigned>();
            TblUnvScheduleDayAssignedLastModifiedByUser = new HashSet<TblUnvScheduleDayAssigned>();
            TblUnvScheduleLastModifiedByUser = new HashSet<TblUnvSchedule>();
            TblUnvScheduleReportType = new HashSet<TblUnvScheduleReportType>();
            TblUnvScheduleType = new HashSet<TblUnvScheduleType>();
            TblUnvUserPreferences = new HashSet<TblUnvUserPreferences>();
            TblUserCampuses = new HashSet<TblUserCampuses>();
            TblUserFunctions = new HashSet<TblUserFunctions>();
        }

        public int UserId { get; set; }
        public int ApplicationUid { get; set; }
        public int UserRoleUid { get; set; }
        public int SiteUid { get; set; }
        public int GroupId { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string RealName { get; set; }
        public bool DistrictUser { get; set; }
        public string CampusId { get; set; }
        public string Email { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Notes { get; set; }
        public bool? Active { get; set; }
        public Guid? Ldapid { get; set; }
        public bool Ldapuser { get; set; }
        public Guid? SessionId { get; set; }
        public DateTime? SessionExpires { get; set; }
        public bool Impersonator { get; set; }
        public int? ParentUserId { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsDriver { get; set; }
        public int UserTypeUid { get; set; }

        public TblUnvApplications ApplicationU { get; set; }
        public TblTechSites SiteU { get; set; }
        public TblUnvUserRoles UserRoleU { get; set; }
        public TblUnvUserTypes UserTypeU { get; set; }
        public ICollection<TblTechDepartments> TblTechDepartments { get; set; }
        public ICollection<TblTechFundingSourceUsers> TblTechFundingSourceUsers { get; set; }
        public ICollection<TblTechInventoryDueDates> TblTechInventoryDueDatesCreatedByUser { get; set; }
        public ICollection<TblTechInventoryDueDates> TblTechInventoryDueDatesLastModifiedByUser { get; set; }
        public ICollection<TblTechPurchases> TblTechPurchases { get; set; }
        public ICollection<TblTechScheduleReport> TblTechScheduleReportCreatedByUser { get; set; }
        public ICollection<TblTechScheduleReport> TblTechScheduleReportLastModifiedByUser { get; set; }
        public ICollection<TblTechTagHistory> TblTechTagHistory { get; set; }
        public ICollection<TblTechTransferDepartmentWorkflows> TblTechTransferDepartmentWorkflowsCreatedByUser { get; set; }
        public ICollection<TblTechTransferDepartmentWorkflows> TblTechTransferDepartmentWorkflowsLastModifiedByUser { get; set; }
        public ICollection<TblTechTransferDepartmentWorkflows> TblTechTransferDepartmentWorkflowsUser { get; set; }
        public ICollection<TblTechTransferHistory> TblTechTransferHistory { get; set; }
        public ICollection<TblTechTransferStatusWorkflowSettings> TblTechTransferStatusWorkflowSettingsCreatedByUser { get; set; }
        public ICollection<TblTechTransferStatusWorkflowSettings> TblTechTransferStatusWorkflowSettingsLastModifiedByUser { get; set; }
        public ICollection<TblTechTransferUserWorkflows> TblTechTransferUserWorkflowsApproverUser { get; set; }
        public ICollection<TblTechTransferUserWorkflows> TblTechTransferUserWorkflowsCreatedByUser { get; set; }
        public ICollection<TblTechTransferUserWorkflows> TblTechTransferUserWorkflowsLastModifiedByUser { get; set; }
        public ICollection<TblTechTransferUserWorkflows> TblTechTransferUserWorkflowsUser { get; set; }
        public ICollection<TblTechTransferWorkflowHistory> TblTechTransferWorkflowHistory { get; set; }
        public ICollection<TblTechTransferWorkflowLinks> TblTechTransferWorkflowLinks { get; set; }
        public ICollection<TblTechTransferWorkflows> TblTechTransferWorkflows { get; set; }
        public ICollection<TblTechTransfers> TblTechTransfers { get; set; }
        public ICollection<TblTechUserDepartments> TblTechUserDepartments { get; set; }
        public ICollection<TblTechUserPermissionTemplate> TblTechUserPermissionTemplate { get; set; }
        public ICollection<TblTechUserSites> TblTechUserSites { get; set; }
        public ICollection<TblTechUserTypeWorkflows> TblTechUserTypeWorkflows { get; set; }
        public ICollection<TblUnvAlertUser> TblUnvAlertUser { get; set; }
        public ICollection<TblUnvClosings> TblUnvClosings { get; set; }
        public ICollection<TblUnvRecipient> TblUnvRecipient { get; set; }
        public ICollection<TblUnvRecipientInformation> TblUnvRecipientInformationCreatedByUser { get; set; }
        public ICollection<TblUnvRecipientInformation> TblUnvRecipientInformationLastModifiedByUser { get; set; }
        public ICollection<TblUnvRecipientInformation> TblUnvRecipientInformationUser { get; set; }
        public ICollection<TblUnvRecipientType> TblUnvRecipientType { get; set; }
        public ICollection<TblUnvSchedule> TblUnvScheduleCreatedByUser { get; set; }
        public ICollection<TblUnvScheduleDay> TblUnvScheduleDay { get; set; }
        public ICollection<TblUnvScheduleDayAssigned> TblUnvScheduleDayAssignedCreatedByUser { get; set; }
        public ICollection<TblUnvScheduleDayAssigned> TblUnvScheduleDayAssignedLastModifiedByUser { get; set; }
        public ICollection<TblUnvSchedule> TblUnvScheduleLastModifiedByUser { get; set; }
        public ICollection<TblUnvScheduleReportType> TblUnvScheduleReportType { get; set; }
        public ICollection<TblUnvScheduleType> TblUnvScheduleType { get; set; }
        public ICollection<TblUnvUserPreferences> TblUnvUserPreferences { get; set; }
        public ICollection<TblUserCampuses> TblUserCampuses { get; set; }
        public ICollection<TblUserFunctions> TblUserFunctions { get; set; }
    }
}
