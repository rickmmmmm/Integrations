using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TIPWebContext : DbContext
    {
        public virtual DbSet<AccessionIsbnRelationships> AccessionIsbnRelationships { get; set; }
        public virtual DbSet<BookImages> BookImages { get; set; }
        public virtual DbSet<CurrentStudentList> CurrentStudentList { get; set; }
        public virtual DbSet<DownloadDataTypes> DownloadDataTypes { get; set; }
        public virtual DbSet<DownloadedData> DownloadedData { get; set; }
        public virtual DbSet<EtlActivityMonitor> EtlActivityMonitor { get; set; }
        public virtual DbSet<EtlErrors> EtlErrors { get; set; }
        public virtual DbSet<EtlImportData> EtlImportData { get; set; }
        public virtual DbSet<EtlInventory> EtlInventory { get; set; }
        public virtual DbSet<EtlItems> EtlItems { get; set; }
        public virtual DbSet<EtlPurchases> EtlPurchases { get; set; }
        public virtual DbSet<EtlRejects> EtlRejects { get; set; }
        public virtual DbSet<EtlSettings> EtlSettings { get; set; }
        public virtual DbSet<FoundAdjustmentVendorOrders> FoundAdjustmentVendorOrders { get; set; }
        public virtual DbSet<IitInventoryImportProcess> IitInventoryImportProcess { get; set; }
        public virtual DbSet<IitProductTypeImportProcess> IitProductTypeImportProcess { get; set; }
        public virtual DbSet<ImsCampusCourse> ImsCampusCourse { get; set; }
        public virtual DbSet<ImsClasses> ImsClasses { get; set; }
        public virtual DbSet<ImsLocation> ImsLocation { get; set; }
        public virtual DbSet<ImsMasterCourse> ImsMasterCourse { get; set; }
        public virtual DbSet<ImsStaff> ImsStaff { get; set; }
        public virtual DbSet<ImsStudentContacts> ImsStudentContacts { get; set; }
        public virtual DbSet<ImsStudents> ImsStudents { get; set; }
        public virtual DbSet<ImsStudentSchedule> ImsStudentSchedule { get; set; }
        public virtual DbSet<ImsTeachers> ImsTeachers { get; set; }
        public virtual DbSet<ImsTeacherSchedule> ImsTeacherSchedule { get; set; }
        public virtual DbSet<ReportPickTicket> ReportPickTicket { get; set; }
        public virtual DbSet<ReportPickTicketDetails> ReportPickTicketDetails { get; set; }
        public virtual DbSet<SettingsApiclient> SettingsApiclient { get; set; }
        public virtual DbSet<SettingsApiserver> SettingsApiserver { get; set; }
        public virtual DbSet<TblAdjustment> TblAdjustment { get; set; }
        public virtual DbSet<TblAdjustmentDetails> TblAdjustmentDetails { get; set; }
        public virtual DbSet<TblAlerts> TblAlerts { get; set; }
        public virtual DbSet<TblArchAdjustment> TblArchAdjustment { get; set; }
        public virtual DbSet<TblArchAdjustmentDetails> TblArchAdjustmentDetails { get; set; }
        public virtual DbSet<TblArchBookBins> TblArchBookBins { get; set; }
        public virtual DbSet<TblArchBookInventory> TblArchBookInventory { get; set; }
        public virtual DbSet<TblArchBookOrders> TblArchBookOrders { get; set; }
        public virtual DbSet<TblArchBookOrdersHistoryDist> TblArchBookOrdersHistoryDist { get; set; }
        public virtual DbSet<TblArchBooksCourses> TblArchBooksCourses { get; set; }
        public virtual DbSet<TblArchBooksCoursesDistrict> TblArchBooksCoursesDistrict { get; set; }
        public virtual DbSet<TblArchBooksMaterialTypes> TblArchBooksMaterialTypes { get; set; }
        public virtual DbSet<TblArchCampusApproved> TblArchCampusApproved { get; set; }
        public virtual DbSet<TblArchCampusDistribution> TblArchCampusDistribution { get; set; }
        public virtual DbSet<TblArchCampusDistributionTx> TblArchCampusDistributionTx { get; set; }
        public virtual DbSet<TblArchComponents> TblArchComponents { get; set; }
        public virtual DbSet<TblArchRequisitions> TblArchRequisitions { get; set; }
        public virtual DbSet<TblArchStudents> TblArchStudents { get; set; }
        public virtual DbSet<TblArchStudentsDistribution> TblArchStudentsDistribution { get; set; }
        public virtual DbSet<TblArchStudentsDistributionTx> TblArchStudentsDistributionTx { get; set; }
        public virtual DbSet<TblArchTeachers> TblArchTeachers { get; set; }
        public virtual DbSet<TblArchTeachersDistribution> TblArchTeachersDistribution { get; set; }
        public virtual DbSet<TblArchTeachersDistributionTx> TblArchTeachersDistributionTx { get; set; }
        public virtual DbSet<TblArchTransfer> TblArchTransfer { get; set; }
        public virtual DbSet<TblArchTransferDetails> TblArchTransferDetails { get; set; }
        public virtual DbSet<TblArchVendorBooks> TblArchVendorBooks { get; set; }
        public virtual DbSet<TblArchVendorOrders> TblArchVendorOrders { get; set; }
        public virtual DbSet<TblAuditBookInventory> TblAuditBookInventory { get; set; }
        public virtual DbSet<TblAuditDetailCounts> TblAuditDetailCounts { get; set; }
        public virtual DbSet<TblAuditDetails> TblAuditDetails { get; set; }
        public virtual DbSet<TblAuditFilterMaterialTypes> TblAuditFilterMaterialTypes { get; set; }
        public virtual DbSet<TblAuditFilters> TblAuditFilters { get; set; }
        public virtual DbSet<TblAustinIfas> TblAustinIfas { get; set; }
        public virtual DbSet<TblBookBins> TblBookBins { get; set; }
        public virtual DbSet<TblBookInventory> TblBookInventory { get; set; }
        public virtual DbSet<TblBookOrders> TblBookOrders { get; set; }
        public virtual DbSet<TblBookOrdersHistoryDist> TblBookOrdersHistoryDist { get; set; }
        public virtual DbSet<TblBookOrderTransactions> TblBookOrderTransactions { get; set; }
        public virtual DbSet<TblBooksCourses> TblBooksCourses { get; set; }
        public virtual DbSet<TblBooksCoursesDistrict> TblBooksCoursesDistrict { get; set; }
        public virtual DbSet<TblBooksMaterialTypes> TblBooksMaterialTypes { get; set; }
        public virtual DbSet<TblCampusApproved> TblCampusApproved { get; set; }
        public virtual DbSet<TblCampusBookHistory> TblCampusBookHistory { get; set; }
        public virtual DbSet<TblCampusCoursesAssigned> TblCampusCoursesAssigned { get; set; }
        public virtual DbSet<TblCampusDistribution> TblCampusDistribution { get; set; }
        public virtual DbSet<TblCampusDistributionTx> TblCampusDistributionTx { get; set; }
        public virtual DbSet<TblCampuses> TblCampuses { get; set; }
        public virtual DbSet<TblCampusInitializationTemp> TblCampusInitializationTemp { get; set; }
        public virtual DbSet<TblComponents> TblComponents { get; set; }
        public virtual DbSet<TblDigitalMaterialDetails> TblDigitalMaterialDetails { get; set; }
        public virtual DbSet<TblDistrictDistributions> TblDistrictDistributions { get; set; }
        public virtual DbSet<TblDistrictDistributionsTx> TblDistrictDistributionsTx { get; set; }
        public virtual DbSet<TblDistrictPreferences> TblDistrictPreferences { get; set; }
        public virtual DbSet<TblDownloadDataTypes> TblDownloadDataTypes { get; set; }
        public virtual DbSet<TblDownloadedData> TblDownloadedData { get; set; }
        public virtual DbSet<TblDurationIntervals> TblDurationIntervals { get; set; }
        public virtual DbSet<TblErrors> TblErrors { get; set; }
        public virtual DbSet<TblFundingSources> TblFundingSources { get; set; }
        public virtual DbSet<TblLetters> TblLetters { get; set; }
        public virtual DbSet<TblMasterBooks> TblMasterBooks { get; set; }
        public virtual DbSet<TblMasterCourse> TblMasterCourse { get; set; }
        public virtual DbSet<TblMasterPublishers> TblMasterPublishers { get; set; }
        public virtual DbSet<TblMaterialTypes> TblMaterialTypes { get; set; }
        public virtual DbSet<TblMessages> TblMessages { get; set; }
        public virtual DbSet<TblMobileErrors> TblMobileErrors { get; set; }
        public virtual DbSet<TblMobstudDistro> TblMobstudDistro { get; set; }
        public virtual DbSet<TblPaymentsCollected> TblPaymentsCollected { get; set; }
        public virtual DbSet<TblPaymentsCollectedTx> TblPaymentsCollectedTx { get; set; }
        public virtual DbSet<TblPdafileTypes> TblPdafileTypes { get; set; }
        public virtual DbSet<TblReconciledTx> TblReconciledTx { get; set; }
        public virtual DbSet<TblRegion> TblRegion { get; set; }
        public virtual DbSet<TblReportCategories> TblReportCategories { get; set; }
        public virtual DbSet<TblReportLimits> TblReportLimits { get; set; }
        public virtual DbSet<TblReports> TblReports { get; set; }
        public virtual DbSet<TblReportSorts> TblReportSorts { get; set; }
        public virtual DbSet<TblRequisitionMultiCampus> TblRequisitionMultiCampus { get; set; }
        public virtual DbSet<TblRequisitionMultiCampusDetails> TblRequisitionMultiCampusDetails { get; set; }
        public virtual DbSet<TblRequisitionMultiCampusLink> TblRequisitionMultiCampusLink { get; set; }
        public virtual DbSet<TblRequisitions> TblRequisitions { get; set; }
        public virtual DbSet<TblSettings> TblSettings { get; set; }
        public virtual DbSet<TblStates> TblStates { get; set; }
        public virtual DbSet<TblStatus> TblStatus { get; set; }
        public virtual DbSet<TblStatusTypes> TblStatusTypes { get; set; }
        public virtual DbSet<TblStudents> TblStudents { get; set; }
        public virtual DbSet<TblStudentsDistribution> TblStudentsDistribution { get; set; }
        public virtual DbSet<TblStudentsDistributionTx> TblStudentsDistributionTx { get; set; }
        public virtual DbSet<TblStudentsSchedules> TblStudentsSchedules { get; set; }
        public virtual DbSet<TblSubjectArea> TblSubjectArea { get; set; }
        public virtual DbSet<TblSubjectAreaBookLink> TblSubjectAreaBookLink { get; set; }
        public virtual DbSet<TblSyncedInventory> TblSyncedInventory { get; set; }
        public virtual DbSet<TblTeacherInformation> TblTeacherInformation { get; set; }
        public virtual DbSet<TblTeachers> TblTeachers { get; set; }
        public virtual DbSet<TblTeachersDistribution> TblTeachersDistribution { get; set; }
        public virtual DbSet<TblTeachersDistributionTx> TblTeachersDistributionTx { get; set; }
        public virtual DbSet<TblTeachersSchedules> TblTeachersSchedules { get; set; }
        public virtual DbSet<TblTeacherStatus> TblTeacherStatus { get; set; }
        public virtual DbSet<TblTechAccessories> TblTechAccessories { get; set; }
        public virtual DbSet<TblTechAccessoryCharges> TblTechAccessoryCharges { get; set; }
        public virtual DbSet<TblTechActions> TblTechActions { get; set; }
        public virtual DbSet<TblTechAssetConditions> TblTechAssetConditions { get; set; }
        public virtual DbSet<TblTechAttachments> TblTechAttachments { get; set; }
        public virtual DbSet<TblTechAttachmentScheduleLink> TblTechAttachmentScheduleLink { get; set; }
        public virtual DbSet<TblTechAuditDepartments> TblTechAuditDepartments { get; set; }
        public virtual DbSet<TblTechAuditDetailInventoryCounts> TblTechAuditDetailInventoryCounts { get; set; }
        public virtual DbSet<TblTechAuditDetails> TblTechAuditDetails { get; set; }
        public virtual DbSet<TblTechAuditEntityTypes> TblTechAuditEntityTypes { get; set; }
        public virtual DbSet<TblTechAuditGrades> TblTechAuditGrades { get; set; }
        public virtual DbSet<TblTechAuditItemTypes> TblTechAuditItemTypes { get; set; }
        public virtual DbSet<TblTechAuditRoomTypes> TblTechAuditRoomTypes { get; set; }
        public virtual DbSet<TblTechAuditStaffTypes> TblTechAuditStaffTypes { get; set; }
        public virtual DbSet<TblTechBulkEdit> TblTechBulkEdit { get; set; }
        public virtual DbSet<TblTechContainers> TblTechContainers { get; set; }
        public virtual DbSet<TblTechContainerTypes> TblTechContainerTypes { get; set; }
        public virtual DbSet<TblTechDepartments> TblTechDepartments { get; set; }
        public virtual DbSet<TblTechFundingSourceUsers> TblTechFundingSourceUsers { get; set; }
        public virtual DbSet<TblTechImages> TblTechImages { get; set; }
        public virtual DbSet<TblTechImports> TblTechImports { get; set; }
        public virtual DbSet<TblTechInventory> TblTechInventory { get; set; }
        public virtual DbSet<TblTechInventoryAccessories> TblTechInventoryAccessories { get; set; }
        public virtual DbSet<TblTechInventoryDetails> TblTechInventoryDetails { get; set; }
        public virtual DbSet<TblTechInventoryDetailsSettings> TblTechInventoryDetailsSettings { get; set; }
        public virtual DbSet<TblTechInventoryDueDates> TblTechInventoryDueDates { get; set; }
        public virtual DbSet<TblTechInventoryExt> TblTechInventoryExt { get; set; }
        public virtual DbSet<TblTechInventoryHistory> TblTechInventoryHistory { get; set; }
        public virtual DbSet<TblTechInventoryImports> TblTechInventoryImports { get; set; }
        public virtual DbSet<TblTechInventoryInstallationDetails> TblTechInventoryInstallationDetails { get; set; }
        public virtual DbSet<TblTechInventoryMeta> TblTechInventoryMeta { get; set; }
        public virtual DbSet<TblTechInventoryQuickAction> TblTechInventoryQuickAction { get; set; }
        public virtual DbSet<TblTechInventorySource> TblTechInventorySource { get; set; }
        public virtual DbSet<TblTechInventoryStatusChangeRequests> TblTechInventoryStatusChangeRequests { get; set; }
        public virtual DbSet<TblTechInventoryTypes> TblTechInventoryTypes { get; set; }
        public virtual DbSet<TblTechItemAccessories> TblTechItemAccessories { get; set; }
        public virtual DbSet<TblTechItemImages> TblTechItemImages { get; set; }
        public virtual DbSet<TblTechItems> TblTechItems { get; set; }
        public virtual DbSet<TblTechItemTypes> TblTechItemTypes { get; set; }
        public virtual DbSet<TblTechOperations> TblTechOperations { get; set; }
        public virtual DbSet<TblTechPermissionTemplate> TblTechPermissionTemplate { get; set; }
        public virtual DbSet<TblTechPurchaseAttachment> TblTechPurchaseAttachment { get; set; }
        public virtual DbSet<TblTechPurchaseInventory> TblTechPurchaseInventory { get; set; }
        public virtual DbSet<TblTechPurchaseInvoice> TblTechPurchaseInvoice { get; set; }
        public virtual DbSet<TblTechPurchaseInvoiceDetail> TblTechPurchaseInvoiceDetail { get; set; }
        public virtual DbSet<TblTechPurchaseItemDetails> TblTechPurchaseItemDetails { get; set; }
        public virtual DbSet<TblTechPurchaseItemShipments> TblTechPurchaseItemShipments { get; set; }
        public virtual DbSet<TblTechPurchases> TblTechPurchases { get; set; }
        public virtual DbSet<TblTechSavedTagSearches> TblTechSavedTagSearches { get; set; }
        public virtual DbSet<TblTechScheduleReport> TblTechScheduleReport { get; set; }
        public virtual DbSet<TblTechSignatureReceipt> TblTechSignatureReceipt { get; set; }
        public virtual DbSet<TblTechSites> TblTechSites { get; set; }
        public virtual DbSet<TblTechSiteTypes> TblTechSiteTypes { get; set; }
        public virtual DbSet<TblTechStaffAttachment> TblTechStaffAttachment { get; set; }
        public virtual DbSet<TblTechStaffCharges> TblTechStaffCharges { get; set; }
        public virtual DbSet<TblTechStaffRooms> TblTechStaffRooms { get; set; }
        public virtual DbSet<TblTechStatusApprovalSettings> TblTechStatusApprovalSettings { get; set; }
        public virtual DbSet<TblTechStatusApprovalSettingsStatusLink> TblTechStatusApprovalSettingsStatusLink { get; set; }
        public virtual DbSet<TblTechStatusChangeSettings> TblTechStatusChangeSettings { get; set; }
        public virtual DbSet<TblTechStatusChangeSettingsStatus> TblTechStatusChangeSettingsStatus { get; set; }
        public virtual DbSet<TblTechStudentAttachment> TblTechStudentAttachment { get; set; }
        public virtual DbSet<TblTechStudentCharges> TblTechStudentCharges { get; set; }
        public virtual DbSet<TblTechStudentInventory> TblTechStudentInventory { get; set; }
        public virtual DbSet<TblTechTagAttachment> TblTechTagAttachment { get; set; }
        public virtual DbSet<TblTechTagEpc> TblTechTagEpc { get; set; }
        public virtual DbSet<TblTechTagHistory> TblTechTagHistory { get; set; }
        public virtual DbSet<TblTechTemplateFunction> TblTechTemplateFunction { get; set; }
        public virtual DbSet<TblTechTransferApproverExceptionDetails> TblTechTransferApproverExceptionDetails { get; set; }
        public virtual DbSet<TblTechTransferDepartmentWorkflows> TblTechTransferDepartmentWorkflows { get; set; }
        public virtual DbSet<TblTechTransferHistory> TblTechTransferHistory { get; set; }
        public virtual DbSet<TblTechTransferInventory> TblTechTransferInventory { get; set; }
        public virtual DbSet<TblTechTransferInventoryContainerLink> TblTechTransferInventoryContainerLink { get; set; }
        public virtual DbSet<TblTechTransferRequestDetails> TblTechTransferRequestDetails { get; set; }
        public virtual DbSet<TblTechTransferRequestDetailsHistory> TblTechTransferRequestDetailsHistory { get; set; }
        public virtual DbSet<TblTechTransferRequestNotes> TblTechTransferRequestNotes { get; set; }
        public virtual DbSet<TblTechTransfers> TblTechTransfers { get; set; }
        public virtual DbSet<TblTechTransferSites> TblTechTransferSites { get; set; }
        public virtual DbSet<TblTechTransferStatusWorkflowSettings> TblTechTransferStatusWorkflowSettings { get; set; }
        public virtual DbSet<TblTechTransferTypes> TblTechTransferTypes { get; set; }
        public virtual DbSet<TblTechTransferUserWorkflows> TblTechTransferUserWorkflows { get; set; }
        public virtual DbSet<TblTechTransferWorkflowHistory> TblTechTransferWorkflowHistory { get; set; }
        public virtual DbSet<TblTechTransferWorkflowLinks> TblTechTransferWorkflowLinks { get; set; }
        public virtual DbSet<TblTechTransferWorkflows> TblTechTransferWorkflows { get; set; }
        public virtual DbSet<TblTechUntaggedInventory> TblTechUntaggedInventory { get; set; }
        public virtual DbSet<TblTechUntaggedInventoryHistory> TblTechUntaggedInventoryHistory { get; set; }
        public virtual DbSet<TblTechUserDepartments> TblTechUserDepartments { get; set; }
        public virtual DbSet<TblTechUserPermissionTemplate> TblTechUserPermissionTemplate { get; set; }
        public virtual DbSet<TblTechUserRecentTagSearches> TblTechUserRecentTagSearches { get; set; }
        public virtual DbSet<TblTechUserRoleTypes> TblTechUserRoleTypes { get; set; }
        public virtual DbSet<TblTechUserSites> TblTechUserSites { get; set; }
        public virtual DbSet<TblTechUserTypeWorkflows> TblTechUserTypeWorkflows { get; set; }
        public virtual DbSet<TblTempClosingDeleteExpired> TblTempClosingDeleteExpired { get; set; }
        public virtual DbSet<TblTipwebImport> TblTipwebImport { get; set; }
        public virtual DbSet<TblTransactionState> TblTransactionState { get; set; }
        public virtual DbSet<TblTransactionTypes> TblTransactionTypes { get; set; }
        public virtual DbSet<TblTransfer> TblTransfer { get; set; }
        public virtual DbSet<TblTransferDetails> TblTransferDetails { get; set; }
        public virtual DbSet<TblUnvAlerts> TblUnvAlerts { get; set; }
        public virtual DbSet<TblUnvAlertTypes> TblUnvAlertTypes { get; set; }
        public virtual DbSet<TblUnvAlertUser> TblUnvAlertUser { get; set; }
        public virtual DbSet<TblUnvApplications> TblUnvApplications { get; set; }
        public virtual DbSet<TblUnvArchives> TblUnvArchives { get; set; }
        public virtual DbSet<TblUnvAreas> TblUnvAreas { get; set; }
        public virtual DbSet<TblUnvAudits> TblUnvAudits { get; set; }
        public virtual DbSet<TblUnvChargePayments> TblUnvChargePayments { get; set; }
        public virtual DbSet<TblUnvCharges> TblUnvCharges { get; set; }
        public virtual DbSet<TblUnvChargeTypeCategories> TblUnvChargeTypeCategories { get; set; }
        public virtual DbSet<TblUnvChargeTypeChargeTypeCategory> TblUnvChargeTypeChargeTypeCategory { get; set; }
        public virtual DbSet<TblUnvChargeTypes> TblUnvChargeTypes { get; set; }
        public virtual DbSet<TblUnvClosings> TblUnvClosings { get; set; }
        public virtual DbSet<TblUnvClosingTypes> TblUnvClosingTypes { get; set; }
        public virtual DbSet<TblUnvCounter> TblUnvCounter { get; set; }
        public virtual DbSet<TblUnvDistrictPreferences> TblUnvDistrictPreferences { get; set; }
        public virtual DbSet<TblUnvEntityTypes> TblUnvEntityTypes { get; set; }
        public virtual DbSet<TblUnvFunctions> TblUnvFunctions { get; set; }
        public virtual DbSet<TblUnvGrades> TblUnvGrades { get; set; }
        public virtual DbSet<TblUnvImportTypes> TblUnvImportTypes { get; set; }
        public virtual DbSet<TblUnvItemTypes> TblUnvItemTypes { get; set; }
        public virtual DbSet<TblUnvLogs> TblUnvLogs { get; set; }
        public virtual DbSet<TblUnvLogTypes> TblUnvLogTypes { get; set; }
        public virtual DbSet<TblUnvManufacturers> TblUnvManufacturers { get; set; }
        public virtual DbSet<TblUnvRecipient> TblUnvRecipient { get; set; }
        public virtual DbSet<TblUnvRecipientInformation> TblUnvRecipientInformation { get; set; }
        public virtual DbSet<TblUnvRecipientType> TblUnvRecipientType { get; set; }
        public virtual DbSet<TblUnvRooms> TblUnvRooms { get; set; }
        public virtual DbSet<TblUnvRoomTypes> TblUnvRoomTypes { get; set; }
        public virtual DbSet<TblUnvSavedActivities> TblUnvSavedActivities { get; set; }
        public virtual DbSet<TblUnvSchedule> TblUnvSchedule { get; set; }
        public virtual DbSet<TblUnvScheduleDay> TblUnvScheduleDay { get; set; }
        public virtual DbSet<TblUnvScheduleDayAssigned> TblUnvScheduleDayAssigned { get; set; }
        public virtual DbSet<TblUnvScheduleReportType> TblUnvScheduleReportType { get; set; }
        public virtual DbSet<TblUnvScheduleType> TblUnvScheduleType { get; set; }
        public virtual DbSet<TblUnvSerializedObjects> TblUnvSerializedObjects { get; set; }
        public virtual DbSet<TblUnvSignature> TblUnvSignature { get; set; }
        public virtual DbSet<TblUnvStaffTypes> TblUnvStaffTypes { get; set; }
        public virtual DbSet<TblUnvSupportLinks> TblUnvSupportLinks { get; set; }
        public virtual DbSet<TblUnvUserPreferences> TblUnvUserPreferences { get; set; }
        public virtual DbSet<TblUnvUserRoles> TblUnvUserRoles { get; set; }
        public virtual DbSet<TblUnvUserTypes> TblUnvUserTypes { get; set; }
        public virtual DbSet<TblUnvViews> TblUnvViews { get; set; }
        public virtual DbSet<TblUsageTypes> TblUsageTypes { get; set; }
        public virtual DbSet<TblUser> TblUser { get; set; }
        public virtual DbSet<TblUserCampuses> TblUserCampuses { get; set; }
        public virtual DbSet<TblUserFunctions> TblUserFunctions { get; set; }
        public virtual DbSet<TblUserGroups> TblUserGroups { get; set; }
        public virtual DbSet<TblUserGroupSettings> TblUserGroupSettings { get; set; }
        public virtual DbSet<TblUserPreferences> TblUserPreferences { get; set; }
        public virtual DbSet<TblUserRoleFunctions> TblUserRoleFunctions { get; set; }
        public virtual DbSet<TblVendor> TblVendor { get; set; }
        public virtual DbSet<TblVendorBooks> TblVendorBooks { get; set; }
        public virtual DbSet<TblVendorOrderDetails> TblVendorOrderDetails { get; set; }
        public virtual DbSet<TblVendorOrderDetailsHistory> TblVendorOrderDetailsHistory { get; set; }
        public virtual DbSet<TblVendorOrders> TblVendorOrders { get; set; }
        public virtual DbSet<TblVersion> TblVersion { get; set; }

        // Unable to generate entity type for table 'dbo.tblArchDistrictDistributions_tx'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tblStagingTeacherSchedules'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tblStagingTeacherDemo'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tblStagingStudentSchedules'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tblStagingStudentDemo'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tblStagingDistrictCourses'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tblStagingCampusEnrollment'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.CurrentTeacherList'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.ErrorLoad'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tblAdjustmentTypes'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tblArchDistrictDistributions'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.LatestPickTicketNumber'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tblBins'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tblLDAPSettings'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tblSIFDeleteQueue'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.AUSTINIFAS_rowfile'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.AustinIFAS_stg'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tblTempClosingDeleteReport'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tblReportsUserDefined'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tblImportProperties'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tblTempProcessStudentReceipt'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tblSyncedInventoryDetails'. Please see the warning messages.

        private string _connectionString;

        //public string ConnectionString
        //{
        //    get
        //    {
        //        return _connectionString;
        //    }
        //    set
        //    {
        //        _connectionString = value;
        //    }
        //}

        public TIPWebContext(string connectionString) : base()
        {
            _connectionString = connectionString;
        }

        public TIPWebContext(DbContextOptions<TIPWebContext> options)
       : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (!string.IsNullOrEmpty(this._connectionString))
                {
                    optionsBuilder.UseSqlServer(this._connectionString);
                }
                else
                {
                    //    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                    //    optionsBuilder.UseSqlServer(@"Server=.;Database=HelpDesk;Trusted_Connection=True;");
                    throw new ArgumentNullException("ConnectionString", "The Connection string is null or empty");
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessionIsbnRelationships>(entity =>
            {
                entity.HasKey(e => e.AccessionIsbnRelationshipUid);

                entity.Property(e => e.AccessionIsbnRelationshipUid).HasColumnName("AccessionIsbnRelationshipUID");

                entity.Property(e => e.Accession)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BookInventoryUid).HasColumnName("BookInventoryUID");

                entity.HasOne(d => d.BookInventoryU)
                    .WithMany(p => p.AccessionIsbnRelationships)
                    .HasForeignKey(d => d.BookInventoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccessionIsbnRelationships_tblBookInventory");
            });

            modelBuilder.Entity<BookImages>(entity =>
            {
                entity.HasKey(e => e.BookImageUid);

                entity.Property(e => e.BookImageUid).HasColumnName("BookImageUID");

                entity.Property(e => e.BookImageFile).HasColumnType("image");

                entity.Property(e => e.BookInventoryUid).HasColumnName("BookInventoryUID");

                entity.Property(e => e.DefaultImage).HasDefaultValueSql("((0))");

                entity.Property(e => e.EnteredByCampus).HasDefaultValueSql("((0))");

                entity.Property(e => e.FileType).HasMaxLength(4);

                entity.Property(e => e.ImageDescription)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("(N'Front cover photo')");

                entity.HasOne(d => d.BookInventoryU)
                    .WithMany(p => p.BookImages)
                    .HasForeignKey(d => d.BookInventoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookImages_tblBookInventory");
            });

            modelBuilder.Entity<CurrentStudentList>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.StudentsUid });

                entity.HasIndex(e => e.StudentId)
                    .HasName("IDX_StudentID");

                entity.HasIndex(e => e.StudentsUid)
                    .HasName("IDX_StudentsUID");

                entity.Property(e => e.StudentId)
                    .HasColumnName("StudentID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StudentsUid).HasColumnName("StudentsUID");
            });

            modelBuilder.Entity<DownloadDataTypes>(entity =>
            {
                entity.HasKey(e => e.FileName);

                entity.Property(e => e.FileName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CampusId)
                    .IsRequired()
                    .HasColumnName("CampusID")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.DateDownloaded).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.FileType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DownloadedData>(entity =>
            {
                entity.HasKey(e => e.FileName);

                entity.Property(e => e.FileName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Accession)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Isbn)
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Message)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Sd)
                    .HasColumnName("SD")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EtlActivityMonitor>(entity =>
            {
                entity.HasKey(e => e.ActivityMonitorId);

                entity.ToTable("_ETL_ActivityMonitor");

                entity.Property(e => e.ActivityMonitorId).HasColumnName("ActivityMonitorID");

                entity.Property(e => e.ActivityDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ActivityMessage).IsUnicode(false);

                entity.Property(e => e.ActivityStep)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ImportDataId).HasColumnName("ImportDataID");
            });

            modelBuilder.Entity<EtlErrors>(entity =>
            {
                entity.HasKey(e => e.ErrorUid);

                entity.ToTable("_ETL_Errors");

                entity.Property(e => e.ErrorUid).HasColumnName("ErrorUID");

                entity.Property(e => e.ErrorDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExceptionMessage).IsUnicode(false);

                entity.Property(e => e.ImportDataId).HasColumnName("ImportDataID");

                entity.Property(e => e.InterfaceMessage).IsUnicode(false);
            });

            modelBuilder.Entity<EtlImportData>(entity =>
            {
                entity.HasKey(e => e.ImportCode);

                entity.ToTable("_ETL_ImportData");

                entity.Property(e => e.ImportCompleted).HasDefaultValueSql("('False')");

                entity.Property(e => e.ImportDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ImportUserId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");
            });

            modelBuilder.Entity<EtlInventory>(entity =>
            {
                entity.HasKey(e => e.EtlinventoryUid);

                entity.ToTable("_ETL_Inventory");

                entity.Property(e => e.EtlinventoryUid).HasColumnName("ETLInventoryUID");

                entity.Property(e => e.AccountCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Area)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AssetId)
                    .HasColumnName("AssetID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustomField1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomField1Label)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomField2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomField2Label)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomField3)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomField3Label)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomField4)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomField4Label)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EntityTypeUid).HasColumnName("EntityTypeUID");

                entity.Property(e => e.EntityUid).HasColumnName("EntityUID");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.FundingSource)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FundingSourceDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FundingSourceUid).HasColumnName("FundingSourceUID");

                entity.Property(e => e.InventoryExt1Uid).HasColumnName("InventoryExt1UID");

                entity.Property(e => e.InventoryExt2Uid).HasColumnName("InventoryExt2UID");

                entity.Property(e => e.InventoryExt3Uid).HasColumnName("InventoryExt3UID");

                entity.Property(e => e.InventoryExt4Uid).HasColumnName("InventoryExt4UID");

                entity.Property(e => e.InventoryMeta1Uid).HasColumnName("InventoryMeta1UID");

                entity.Property(e => e.InventoryMeta2Uid).HasColumnName("InventoryMeta2UID");

                entity.Property(e => e.InventoryMeta3Uid).HasColumnName("InventoryMeta3UID");

                entity.Property(e => e.InventoryMeta4Uid).HasColumnName("InventoryMeta4UID");

                entity.Property(e => e.InventoryNotes)
                    .HasMaxLength(3000)
                    .IsUnicode(false);

                entity.Property(e => e.InventorySourceUid).HasColumnName("InventorySourceUID");

                entity.Property(e => e.InventoryTypeUid).HasColumnName("InventoryTypeUID");

                entity.Property(e => e.InventoryUid).HasColumnName("InventoryUID");

                entity.Property(e => e.InvoiceDate).HasColumnType("date");

                entity.Property(e => e.InvoiceNumber)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ItemTypeUid).HasColumnName("ItemTypeUID");

                entity.Property(e => e.ItemUid).HasColumnName("ItemUID");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Model)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ParentInventoryUid).HasColumnName("ParentInventoryUID");

                entity.Property(e => e.ParentTag)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Product)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ProductType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseDate).HasColumnType("datetime");

                entity.Property(e => e.PurchaseInventoryUid).HasColumnName("PurchaseInventoryUID");

                entity.Property(e => e.PurchaseItemDetailUid).HasColumnName("PurchaseItemDetailUID");

                entity.Property(e => e.PurchaseItemShipmentUid).HasColumnName("PurchaseItemShipmentUID");

                entity.Property(e => e.PurchaseOrder)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PurchasePrice).HasColumnType("money");

                entity.Property(e => e.PurchaseUid).HasColumnName("PurchaseUID");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Site)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SiteUid).HasColumnName("SiteUID");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StatusId).HasColumnName("statusID");

                entity.Property(e => e.Tag)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TechDepartmentUid).HasColumnName("TechDepartmentUID");

                entity.Property(e => e.Vendor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.VendorId).HasColumnName("VendorID");
            });

            modelBuilder.Entity<EtlItems>(entity =>
            {
                entity.HasKey(e => e.EtlitemUid);

                entity.ToTable("_ETL_Items");

                entity.Property(e => e.EtlitemUid).HasColumnName("ETLItemUID");

                entity.Property(e => e.Area)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AreaUid).HasColumnName("AreaUID");

                entity.Property(e => e.ItemTypeUid).HasColumnName("ItemTypeUID");

                entity.Property(e => e.ItemUid).HasColumnName("ItemUID");

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ManufacturerUid).HasColumnName("ManufacturerUID");

                entity.Property(e => e.Model)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OtherField1)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.OtherField2)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.OtherField3)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Product)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductNotes)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.ProductNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductTypeDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Sku)
                    .HasColumnName("SKU")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SuggestedPrice).HasColumnType("money");
            });

            modelBuilder.Entity<EtlPurchases>(entity =>
            {
                entity.HasKey(e => e.EtlpurchaseUid);

                entity.ToTable("_ETL_Purchases");

                entity.Property(e => e.EtlpurchaseUid).HasColumnName("ETLPurchaseUID");

                entity.Property(e => e.AccountCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Area)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.Department)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FundingSource)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FundingSourceDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FundingSourceUid).HasColumnName("FundingSourceUID");

                entity.Property(e => e.ItemUid).HasColumnName("ItemUID");

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Model)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Other1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PoClose).HasColumnName("PO_Close");

                entity.Property(e => e.PolineClose).HasColumnName("POLineClose");

                entity.Property(e => e.Product)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ProductType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseDate).HasColumnType("datetime");

                entity.Property(e => e.PurchaseItemDetailUid).HasColumnName("PurchaseItemDetailUID");

                entity.Property(e => e.PurchaseItemShipmentUid).HasColumnName("PurchaseItemShipmentUID");

                entity.Property(e => e.PurchaseOrder)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PurchasePrice).HasColumnType("money");

                entity.Property(e => e.PurchaseUid).HasColumnName("PurchaseUID");

                entity.Property(e => e.ShippedToSite)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ShippedToSiteUid).HasColumnName("ShippedToSiteUID");

                entity.Property(e => e.Site)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SiteAdded)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SiteAddedSiteUid).HasColumnName("SiteAddedSiteUID");

                entity.Property(e => e.SiteUid).HasColumnName("SiteUID");

                entity.Property(e => e.TechDepartmentUid).HasColumnName("TechDepartmentUID");

                entity.Property(e => e.TicketedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TicketedByUserId).HasColumnName("TicketedByUserID");

                entity.Property(e => e.TicketedDate).HasColumnType("datetime");

                entity.Property(e => e.Vendor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.VendorAccountNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VendorUid).HasColumnName("VendorUID");
            });

            modelBuilder.Entity<EtlRejects>(entity =>
            {
                entity.HasKey(e => e.RejectUid);

                entity.ToTable("_ETL_Rejects");

                entity.Property(e => e.RejectUid).HasColumnName("RejectUID");

                entity.Property(e => e.ExceptionMessage).IsUnicode(false);

                entity.Property(e => e.Reference)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RejectDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RejectReason)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RejectedValue).IsUnicode(false);
            });

            modelBuilder.Entity<EtlSettings>(entity =>
            {
                entity.HasKey(e => e.EtlsettingUid);

                entity.ToTable("_ETL_Settings");

                entity.Property(e => e.EtlsettingUid)
                    .HasColumnName("ETLSettingUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EtlsettingName)
                    .IsRequired()
                    .HasColumnName("ETLSettingName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EtlsettingValue)
                    .IsRequired()
                    .HasColumnName("ETLSettingValue")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FoundAdjustmentVendorOrders>(entity =>
            {
                entity.HasKey(e => e.FoundAdjustmentVendorOrderUid);

                entity.Property(e => e.FoundAdjustmentVendorOrderUid).HasColumnName("FoundAdjustmentVendorOrderUID");

                entity.Property(e => e.FoundAdjustmentUid).HasColumnName("FoundAdjustmentUID");

                entity.Property(e => e.VendorOrderUid).HasColumnName("VendorOrderUID");

                entity.HasOne(d => d.FoundAdjustmentU)
                    .WithMany(p => p.FoundAdjustmentVendorOrders)
                    .HasForeignKey(d => d.FoundAdjustmentUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FoundAdjustmentVendorOrders_tblAdjustment");

                entity.HasOne(d => d.VendorOrderU)
                    .WithMany(p => p.FoundAdjustmentVendorOrders)
                    .HasForeignKey(d => d.VendorOrderUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FoundAdjustmentVendorOrders_tblVendorOrders");
            });

            modelBuilder.Entity<IitInventoryImportProcess>(entity =>
            {
                entity.HasKey(e => e.InventoryImportProcessUid);

                entity.ToTable("_IIT_InventoryImportProcess");

                entity.Property(e => e.InventoryImportProcessUid).HasColumnName("InventoryImportProcessUID");

                entity.Property(e => e.AccountCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Area)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AreaUid).HasColumnName("AreaUID");

                entity.Property(e => e.CustomField1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CustomField2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CustomField3)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CustomField4)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DepartmentUid).HasColumnName("DepartmentUID");

                entity.Property(e => e.FundingSource)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FundingSourceUid).HasColumnName("FundingSourceUID");

                entity.Property(e => e.InventoryExtUid1).HasColumnName("InventoryExtUID1");

                entity.Property(e => e.InventoryExtUid2).HasColumnName("InventoryExtUID2");

                entity.Property(e => e.InventoryExtUid3).HasColumnName("InventoryExtUID3");

                entity.Property(e => e.InventoryExtUid4).HasColumnName("InventoryExtUID4");

                entity.Property(e => e.InventoryMetaUid1).HasColumnName("InventoryMetaUID1");

                entity.Property(e => e.InventoryMetaUid2).HasColumnName("InventoryMetaUID2");

                entity.Property(e => e.InventoryMetaUid3).HasColumnName("InventoryMetaUID3");

                entity.Property(e => e.InventoryMetaUid4).HasColumnName("InventoryMetaUID4");

                entity.Property(e => e.InventorySourceUid).HasColumnName("InventorySourceUID");

                entity.Property(e => e.InventoryTypeUid).HasColumnName("InventoryTypeUID");

                entity.Property(e => e.InventoryUid).HasColumnName("InventoryUID");

                entity.Property(e => e.IsDuplicateStaffId).HasColumnName("IsDuplicateStaffID");

                entity.Property(e => e.LocationType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LocationTypeUid).HasColumnName("LocationTypeUID");

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ManufacturerUid).HasColumnName("ManufacturerUID");

                entity.Property(e => e.Model)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Other1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Other2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Other3)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductTypeUid).HasColumnName("ProductTypeUID");

                entity.Property(e => e.ProductUid).HasColumnName("ProductUID");

                entity.Property(e => e.PurchaseDate).HasColumnType("date");

                entity.Property(e => e.PurchaseInventoryUid).HasColumnName("PurchaseInventoryUID");

                entity.Property(e => e.PurchaseItemDetailUid).HasColumnName("PurchaseItemDetailUID");

                entity.Property(e => e.PurchaseItemShipmentUid).HasColumnName("PurchaseItemShipmentUID");

                entity.Property(e => e.PurchaseOrderNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PurchasePrice).HasColumnType("money");

                entity.Property(e => e.PurchaseUid).HasColumnName("PurchaseUID");

                entity.Property(e => e.RoomDescription)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RoomNumber)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.RoomOther)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RoomType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RoomTypeUid).HasColumnName("RoomTypeUID");

                entity.Property(e => e.RoomUid).HasColumnName("RoomUID");

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SiteId)
                    .IsRequired()
                    .HasColumnName("SiteID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SiteUid).HasColumnName("SiteUID");

                entity.Property(e => e.Sku)
                    .HasColumnName("SKU")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StaffOrStudentFirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StaffOrStudentId)
                    .IsRequired()
                    .HasColumnName("StaffOrStudentID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StaffOrStudentLastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StaffOrStudentMiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StaffOrStudentUid).HasColumnName("StaffOrStudentUID");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.SuggestedPrice).HasColumnType("money");

                entity.Property(e => e.Tag)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Vendor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.VendorUid).HasColumnName("VendorUID");
            });

            modelBuilder.Entity<IitProductTypeImportProcess>(entity =>
            {
                entity.HasKey(e => e.ProductTypeImportProcessUid);

                entity.ToTable("_IIT_ProductTypeImportProcess");

                entity.Property(e => e.ProductTypeImportProcessUid).HasColumnName("ProductTypeImportProcessUID");

                entity.Property(e => e.CustomFieldDataType1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomFieldDataType2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomFieldDataType3)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomFieldDataType4)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomFieldName1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomFieldName2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomFieldName3)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomFieldName4)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomFieldRequired1).HasDefaultValueSql("((0))");

                entity.Property(e => e.CustomFieldRequired2).HasDefaultValueSql("((0))");

                entity.Property(e => e.CustomFieldRequired3).HasDefaultValueSql("((0))");

                entity.Property(e => e.CustomFieldRequired4).HasDefaultValueSql("((0))");

                entity.Property(e => e.InventoryMetaUid1).HasColumnName("InventoryMetaUID1");

                entity.Property(e => e.InventoryMetaUid2).HasColumnName("InventoryMetaUID2");

                entity.Property(e => e.InventoryMetaUid3).HasColumnName("InventoryMetaUID3");

                entity.Property(e => e.InventoryMetaUid4).HasColumnName("InventoryMetaUID4");

                entity.Property(e => e.ProductType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductTypeUid).HasColumnName("ProductTypeUID");
            });

            modelBuilder.Entity<ImsCampusCourse>(entity =>
            {
                entity.ToTable("_IMS_CampusCourse");

                entity.Property(e => e.ImsCampusCourseId).HasColumnName("IMS_CampusCourseID");

                entity.Property(e => e.CampusCoursesAssignedUid).HasColumnName("CampusCoursesAssignedUID");

                entity.Property(e => e.CampusId)
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CampusUid).HasColumnName("CampusUID");

                entity.Property(e => e.CourseId)
                    .HasColumnName("CourseID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Import).HasDefaultValueSql("((1))");

                entity.Property(e => e.MasterCourseUid).HasColumnName("MasterCourseUID");

                entity.Property(e => e.RejectMessage)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.StudentCount)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TeacherCount)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ImsClasses>(entity =>
            {
                entity.HasKey(e => e.ImsClassId);

                entity.ToTable("_IMS_Classes");

                entity.Property(e => e.ImsClassId).HasColumnName("IMS_ClassID");

                entity.Property(e => e.CampusCourseAssignedUid).HasColumnName("CampusCourseAssignedUID");

                entity.Property(e => e.CampusId)
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CampusUid).HasColumnName("CampusUID");

                entity.Property(e => e.ClassId)
                    .HasColumnName("ClassID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClassName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CourseId)
                    .HasColumnName("CourseID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Import)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MasterCourseUid).HasColumnName("MasterCourseUID");

                entity.Property(e => e.Period)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RejectMessage)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.RoomLocation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Section)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ImsLocation>(entity =>
            {
                entity.ToTable("_IMS_Location");

                entity.Property(e => e.ImsLocationId).HasColumnName("IMS_LocationID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AltLocationId)
                    .HasColumnName("AltLocationID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LocationId)
                    .IsRequired()
                    .HasColumnName("LocationID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LocationName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ImsMasterCourse>(entity =>
            {
                entity.ToTable("_IMS_MasterCourse");

                entity.Property(e => e.ImsMasterCourseId).HasColumnName("IMS_MasterCourseID");

                entity.Property(e => e.CourseId)
                    .HasColumnName("CourseID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CourseName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Import)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MasterCourseUid).HasColumnName("MasterCourseUID");

                entity.Property(e => e.RejectMessage)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ImsStaff>(entity =>
            {
                entity.ToTable("_IMS_Staff");

                entity.Property(e => e.ImsStaffId).HasColumnName("IMS_StaffID");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CampusId)
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Import)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrimarySite)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Race)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RejectMessage)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.RoomUid).HasColumnName("RoomUID");

                entity.Property(e => e.StaffId)
                    .HasColumnName("StaffID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StaffStatus)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StaffType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StaffTypeUid).HasColumnName("StaffTypeUID");

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.TeachersUid).HasColumnName("TeachersUID");

                entity.Property(e => e.Zip)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ImsStudentContacts>(entity =>
            {
                entity.HasKey(e => e.StudentContactId);

                entity.ToTable("_IMS_StudentContacts");

                entity.Property(e => e.StudentContactId).HasColumnName("StudentContactID");

                entity.Property(e => e.Address1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CellPhone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactFullName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactOrder)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactRelation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactRowId)
                    .HasColumnName("ContactRowID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoreTypeId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GivenId)
                    .HasColumnName("GivenID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HomePhone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LocationGivenId)
                    .HasColumnName("LocationGivenID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Other)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OtherContactBearings)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OtherPhone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WorkPhone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Zip)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ImsStudents>(entity =>
            {
                entity.HasKey(e => e.ImsStudentId);

                entity.ToTable("_IMS_Students");

                entity.Property(e => e.ImsStudentId).HasColumnName("IMS_StudentID");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CampusId)
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Grade)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HomeRoom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Import)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ParentEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ParentPhone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Race)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RejectMessage)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.StudentId)
                    .HasColumnName("StudentID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StudentsUid).HasColumnName("StudentsUID");

                entity.Property(e => e.Zip)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ImsStudentSchedule>(entity =>
            {
                entity.ToTable("_IMS_StudentSchedule");

                entity.Property(e => e.ImsStudentScheduleId).HasColumnName("IMS_StudentScheduleID");

                entity.Property(e => e.CampusCoursesAssignedUid).HasColumnName("CampusCoursesAssignedUID");

                entity.Property(e => e.CampusId)
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClassId)
                    .HasColumnName("ClassID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CourseId)
                    .HasColumnName("CourseID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Import)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MasterCourseUid).HasColumnName("MasterCourseUID");

                entity.Property(e => e.New).HasColumnName("new");

                entity.Property(e => e.Period)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RejectMessage)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Section)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StudentId)
                    .HasColumnName("StudentID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StudentsSchedulesUid).HasColumnName("StudentsSchedulesUID");

                entity.Property(e => e.StudentsUid).HasColumnName("StudentsUID");
            });

            modelBuilder.Entity<ImsTeachers>(entity =>
            {
                entity.HasKey(e => e.ImsTeacherId);

                entity.ToTable("_IMS_Teachers");

                entity.Property(e => e.ImsTeacherId).HasColumnName("IMS_TeacherID");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CampusId)
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Grade)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HomeRoom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Import)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrimarySite)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Race)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RejectMessage)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.RoomUid).HasColumnName("RoomUID");

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.TeacherId)
                    .HasColumnName("TeacherID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TeacherStatus)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TeachersUid).HasColumnName("TeachersUID");

                entity.Property(e => e.Zip)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ImsTeacherSchedule>(entity =>
            {
                entity.HasKey(e => e.ImsTeachertScheduleId);

                entity.ToTable("_IMS_TeacherSchedule");

                entity.Property(e => e.ImsTeachertScheduleId).HasColumnName("IMS_TeachertScheduleID");

                entity.Property(e => e.CampusCoursesAssignedUid).HasColumnName("CampusCoursesAssignedUID");

                entity.Property(e => e.CampusId)
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClassId)
                    .HasColumnName("ClassID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CourseId)
                    .HasColumnName("CourseID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Import)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MasterCourseUid).HasColumnName("MasterCourseUID");

                entity.Property(e => e.New).HasColumnName("new");

                entity.Property(e => e.Period)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RejectMessage)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Section)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TeacherId)
                    .HasColumnName("TeacherID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TeachersSchedulesUid).HasColumnName("TeachersSchedulesUID");

                entity.Property(e => e.TeachersUid).HasColumnName("TeachersUID");
            });

            modelBuilder.Entity<ReportPickTicket>(entity =>
            {
                entity.HasKey(e => e.PickTicketUid);

                entity.ToTable("reportPickTicket");

                entity.Property(e => e.PickTicketUid).HasColumnName("PickTicketUID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PickTicketNumber)
                    .IsRequired()
                    .HasMaxLength(325)
                    .IsUnicode(false);

                entity.Property(e => e.RequisitionUid).HasColumnName("RequisitionUID");
            });

            modelBuilder.Entity<ReportPickTicketDetails>(entity =>
            {
                entity.HasKey(e => e.PickTicketDetailUid);

                entity.ToTable("reportPickTicketDetails");

                entity.Property(e => e.PickTicketDetailUid).HasColumnName("PickTicketDetailUID");

                entity.Property(e => e.BinDescription)
                    .HasMaxLength(7500)
                    .IsUnicode(false);

                entity.Property(e => e.BinId)
                    .HasColumnName("BinID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CampusContact)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CampusName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PickTicketUid).HasColumnName("PickTicketUID");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.RandomReq)
                    .HasMaxLength(325)
                    .IsUnicode(false);

                entity.Property(e => e.RealName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RequisitionId)
                    .IsRequired()
                    .HasColumnName("RequisitionID")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.RequisitionUid).HasColumnName("RequisitionUID");

                entity.Property(e => e.ShippingAddress)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ShippingCityStateZip)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.Slc)
                    .HasColumnName("SLC")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.TotalCampus)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TotalValue).HasColumnType("money");
            });

            modelBuilder.Entity<SettingsApiclient>(entity =>
            {
                entity.HasKey(e => e.SettingsApiclientUid);

                entity.ToTable("SettingsAPIClient");

                entity.Property(e => e.SettingsApiclientUid).HasColumnName("SettingsAPIClientUID");

                entity.Property(e => e.BaseUrl)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Passphrase).IsUnicode(false);

                entity.Property(e => e.SecretKey).IsUnicode(false);

                entity.Property(e => e.Token).IsUnicode(false);

                entity.Property(e => e.TokenCreationDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SettingsApiserver>(entity =>
            {
                entity.HasKey(e => e.SettingsApiserverUid);

                entity.ToTable("SettingsAPIServer");

                entity.Property(e => e.SettingsApiserverUid).HasColumnName("SettingsAPIServerUID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Passphrase).IsUnicode(false);

                entity.Property(e => e.SecretKey).IsUnicode(false);

                entity.Property(e => e.SettingsApirolesId).HasColumnName("SettingsAPIRolesID");

                entity.Property(e => e.ValidFromDate).HasColumnType("datetime");

                entity.Property(e => e.ValidToDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblAdjustment>(entity =>
            {
                entity.HasKey(e => e.AdjustmentId);

                entity.ToTable("tblAdjustment");

                entity.HasIndex(e => new { e.CampusLocalCreated, e.AdjustmentStatus, e.CampusId })
                    .HasName("_dta_index_tblAdjustment_8_816878127__K7_K8_9");

                entity.Property(e => e.AdjustmentId).HasColumnName("AdjustmentID");

                entity.Property(e => e.AdjustmentName)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.AdjustmentStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CampusId)
                    .IsRequired()
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Description1)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblAdjustmentDetails>(entity =>
            {
                entity.HasKey(e => e.AdjustmentDetailsUid);

                entity.ToTable("tblAdjustmentDetails");

                entity.HasIndex(e => new { e.CopiesToAdjust, e.AdjustmentDetailsUid, e.Posted, e.AdjustmentId, e.Isbn })
                    .HasName("_dta_index_tblAdjustmentDetails_8_1264879723__K1_K2_3_4_8");

                entity.Property(e => e.AdjustmentDetailsUid).HasColumnName("AdjustmentDetailsUID");

                entity.Property(e => e.AdjustmentId).HasColumnName("AdjustmentID");

                entity.Property(e => e.CopiesToAdjust).HasDefaultValueSql("((0))");

                entity.Property(e => e.DatePosted)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Posted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Pending')");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TblAlerts>(entity =>
            {
                entity.HasKey(e => e.AlertId);

                entity.ToTable("tblAlerts");

                entity.Property(e => e.AlertId).HasColumnName("AlertID");

                entity.Property(e => e.Alert)
                    .HasMaxLength(7000)
                    .IsUnicode(false);

                entity.Property(e => e.AlertDate).HasColumnType("datetime");

                entity.Property(e => e.AlertRead).HasDefaultValueSql("((0))");

                entity.Property(e => e.ToUserId).HasColumnName("ToUserID");
            });

            modelBuilder.Entity<TblArchAdjustment>(entity =>
            {
                entity.HasKey(e => e.ArchiveId);

                entity.ToTable("tblArchAdjustment");

                entity.Property(e => e.ArchiveId).HasColumnName("ArchiveID");

                entity.Property(e => e.AdjustmentId).HasColumnName("AdjustmentID");

                entity.Property(e => e.AdjustmentName)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.AdjustmentStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ArchivedDate).HasColumnType("datetime");

                entity.Property(e => e.ArchivedUserId).HasColumnName("ArchivedUserID");

                entity.Property(e => e.CampusId)
                    .IsRequired()
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Description1)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblArchAdjustmentDetails>(entity =>
            {
                entity.HasKey(e => e.ArchiveId);

                entity.ToTable("tblArchAdjustmentDetails");

                entity.Property(e => e.ArchiveId).HasColumnName("ArchiveID");

                entity.Property(e => e.AdjustmentId).HasColumnName("AdjustmentID");

                entity.Property(e => e.ArchivedDate).HasColumnType("datetime");

                entity.Property(e => e.ArchivedUserId).HasColumnName("ArchivedUserID");

                entity.Property(e => e.DatePosted).HasColumnType("datetime");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TblArchBookBins>(entity =>
            {
                entity.HasKey(e => e.ArchiveId);

                entity.ToTable("tblArchBookBins");

                entity.Property(e => e.ArchiveId).HasColumnName("ArchiveID");

                entity.Property(e => e.ArchivedDate).HasColumnType("datetime");

                entity.Property(e => e.ArchiveduserId).HasColumnName("ArchiveduserID");

                entity.Property(e => e.BinId)
                    .IsRequired()
                    .HasColumnName("BinID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RecordId).HasColumnName("RecordID");
            });

            modelBuilder.Entity<TblArchBookInventory>(entity =>
            {
                entity.HasKey(e => e.ArchiveId);

                entity.ToTable("tblArchBookInventory");

                entity.Property(e => e.ArchiveId).HasColumnName("ArchiveID");

                entity.Property(e => e.Adopt)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ArchiveDate).HasColumnType("datetime");

                entity.Property(e => e.ArchiveUserId).HasColumnName("ArchiveUserID");

                entity.Property(e => e.BinId)
                    .HasColumnName("BinID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Copyright)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DistReqCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Expire)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Grade)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GroupCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.SetIsbn).HasColumnName("SetISBN");

                entity.Property(e => e.Slc)
                    .HasColumnName("SLC")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Udf)
                    .HasColumnName("UDF")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TblArchBookOrders>(entity =>
            {
                entity.HasKey(e => e.ArchiveId);

                entity.ToTable("tblArchBookOrders");

                entity.Property(e => e.ArchiveId).HasColumnName("ArchiveID");

                entity.Property(e => e.ArchivedDate).HasColumnType("datetime");

                entity.Property(e => e.ArchivedUserId).HasColumnName("ArchivedUserID");

                entity.Property(e => e.ArrivalDate).HasColumnType("datetime");

                entity.Property(e => e.CampusId)
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DateReceived).HasColumnType("datetime");

                entity.Property(e => e.DateSent).HasColumnType("datetime");

                entity.Property(e => e.Denied)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FundingSource)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.RandomReq)
                    .HasMaxLength(325)
                    .IsUnicode(false);

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.RequisitionId)
                    .IsRequired()
                    .HasColumnName("RequisitionID")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TicketDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TblArchBookOrdersHistoryDist>(entity =>
            {
                entity.HasKey(e => e.ArchiveId);

                entity.ToTable("tblArchBookOrdersHistoryDist");

                entity.Property(e => e.ArchiveId).HasColumnName("ArchiveID");

                entity.Property(e => e.ArchivedDate).HasColumnType("datetime");

                entity.Property(e => e.ArchivedUserId).HasColumnName("ArchivedUserID");

                entity.Property(e => e.CampusId)
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCopiesSent).HasColumnType("datetime");

                entity.Property(e => e.Isbn)
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.RequisitionId)
                    .HasColumnName("RequisitionID")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.TicketDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TblArchBooksCourses>(entity =>
            {
                entity.HasKey(e => e.ArchiveId);

                entity.ToTable("tblArchBooksCourses");

                entity.Property(e => e.ArchiveId).HasColumnName("ArchiveID");

                entity.Property(e => e.ArchivedDate).HasColumnType("datetime");

                entity.Property(e => e.ArchivedUserId).HasColumnName("ArchivedUserID");

                entity.Property(e => e.CampusId)
                    .IsRequired()
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CourseId)
                    .IsRequired()
                    .HasColumnName("CourseID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblArchBooksCoursesDistrict>(entity =>
            {
                entity.HasKey(e => e.ArchiveId);

                entity.ToTable("tblArchBooksCoursesDistrict");

                entity.Property(e => e.ArchiveId).HasColumnName("ArchiveID");

                entity.Property(e => e.ArchivedDate).HasColumnType("datetime");

                entity.Property(e => e.ArchivedUserId).HasColumnName("ArchivedUserID");

                entity.Property(e => e.CourseId)
                    .IsRequired()
                    .HasColumnName("CourseID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblArchBooksMaterialTypes>(entity =>
            {
                entity.HasKey(e => e.ArchiveId);

                entity.ToTable("tblArchBooksMaterialTypes");

                entity.Property(e => e.ArchiveId).HasColumnName("ArchiveID");

                entity.Property(e => e.ArchivedDate).HasColumnType("datetime");

                entity.Property(e => e.ArchivedUserId).HasColumnName("ArchivedUserID");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MaterialTypeId)
                    .IsRequired()
                    .HasColumnName("MaterialTypeID")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblArchCampusApproved>(entity =>
            {
                entity.HasKey(e => e.ArchiveId);

                entity.ToTable("tblArchCampusApproved");

                entity.Property(e => e.ArchiveId).HasColumnName("ArchiveID");

                entity.Property(e => e.ArchivedDate).HasColumnType("datetime");

                entity.Property(e => e.ArchivedUserId).HasColumnName("ArchivedUserID");

                entity.Property(e => e.CampusId)
                    .IsRequired()
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TblArchCampusDistribution>(entity =>
            {
                entity.HasKey(e => e.ArchiveDistributionId);

                entity.ToTable("tblArchCampusDistribution");

                entity.Property(e => e.ArchiveDistributionId).HasColumnName("ArchiveDistributionID");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.ArchiveDate).HasColumnType("datetime");

                entity.Property(e => e.ArchiveId).HasColumnName("ArchiveID");

                entity.Property(e => e.ArchiveUserId).HasColumnName("ArchiveUserID");

                entity.Property(e => e.CampusId)
                    .IsRequired()
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DistributionId).HasColumnName("DistributionID");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Reference)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TblArchCampusDistributionTx>(entity =>
            {
                entity.HasKey(e => e.ArchiveTransactionId);

                entity.ToTable("tblArchCampusDistribution_tx");

                entity.Property(e => e.ArchiveTransactionId).HasColumnName("ArchiveTransactionID");

                entity.Property(e => e.ActionTaken)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.ArchiveDate).HasColumnType("datetime");

                entity.Property(e => e.ArchiveId).HasColumnName("ArchiveID");

                entity.Property(e => e.ArchiveUserId).HasColumnName("ArchiveUserID");

                entity.Property(e => e.CampusId)
                    .IsRequired()
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DistributionId).HasColumnName("DistributionID");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Reference)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TblArchComponents>(entity =>
            {
                entity.HasKey(e => e.ArchiveId);

                entity.ToTable("tblArchComponents");

                entity.Property(e => e.ArchiveId).HasColumnName("ArchiveID");

                entity.Property(e => e.ArchivedDate).HasColumnType("datetime");

                entity.Property(e => e.ArchivedUserId).HasColumnName("ArchivedUserID");

                entity.Property(e => e.Bin)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BinAlt1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BinAlt2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BinAlt3)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BinAlt4)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ComponentIsbn)
                    .IsRequired()
                    .HasColumnName("ComponentISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MasterIsbn)
                    .IsRequired()
                    .HasColumnName("MasterISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.Title)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblArchRequisitions>(entity =>
            {
                entity.HasKey(e => e.ArchiveRequisitionId);

                entity.ToTable("tblArchRequisitions");

                entity.Property(e => e.ArchiveRequisitionId).HasColumnName("ArchiveRequisitionID");

                entity.Property(e => e.ArchiveDate).HasColumnType("datetime");

                entity.Property(e => e.ArchiveUserId).HasColumnName("ArchiveUserID");

                entity.Property(e => e.CampusId)
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreatedOrSent).HasColumnType("datetime");

                entity.Property(e => e.DateReqApproved).HasColumnType("datetime");

                entity.Property(e => e.DateSubmittedByCampus).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.ReqStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RequisitionId)
                    .IsRequired()
                    .HasColumnName("RequisitionID")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.RetrievedFromDistrict)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.VerifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VerifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblArchStudents>(entity =>
            {
                entity.HasKey(e => e.ArchiveStudentId);

                entity.ToTable("tblArchStudents");

                entity.Property(e => e.ArchiveStudentId).HasColumnName("ArchiveStudentID");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ArchiveDate).HasColumnType("datetime");

                entity.Property(e => e.ArchiveUserId).HasColumnName("ArchiveUserID");

                entity.Property(e => e.CampusId)
                    .IsRequired()
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Grade)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Homeroom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.ParentEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Race)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasColumnName("StudentID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StudentsUid).HasColumnName("StudentsUID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Zip)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblArchStudentsDistribution>(entity =>
            {
                entity.HasKey(e => e.ArchiveDistributionId);

                entity.ToTable("tblArchStudentsDistribution");

                entity.Property(e => e.ArchiveDistributionId).HasColumnName("ArchiveDistributionID");

                entity.Property(e => e.Accession)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ArchiveDate).HasColumnType("datetime");

                entity.Property(e => e.ArchiveId).HasColumnName("ArchiveID");

                entity.Property(e => e.ArchiveStudentId).HasColumnName("ArchiveStudentID");

                entity.Property(e => e.ArchiveUserId).HasColumnName("ArchiveUserID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DistributionId).HasColumnName("DistributionID");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SourceType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasColumnName("StudentID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StudentsUid).HasColumnName("StudentsUID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TblArchStudentsDistributionTx>(entity =>
            {
                entity.HasKey(e => e.ArchiveTransactionId);

                entity.ToTable("tblArchStudentsDistribution_tx");

                entity.Property(e => e.ArchiveTransactionId).HasColumnName("ArchiveTransactionID");

                entity.Property(e => e.Accession)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ActionTaken)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ArchiveDate).HasColumnType("datetime");

                entity.Property(e => e.ArchiveId).HasColumnName("ArchiveID");

                entity.Property(e => e.ArchiveStudentId).HasColumnName("ArchiveStudentID");

                entity.Property(e => e.ArchiveUserId).HasColumnName("ArchiveUserID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DistributionId).HasColumnName("DistributionID");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SourceType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasColumnName("StudentID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StudentsUid).HasColumnName("StudentsUID");

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TblArchTeachers>(entity =>
            {
                entity.HasKey(e => e.ArchiveTeacherId);

                entity.ToTable("tblArchTeachers");

                entity.Property(e => e.ArchiveTeacherId).HasColumnName("ArchiveTeacherID");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ArchiveDate).HasColumnType("datetime");

                entity.Property(e => e.ArchiveUserId).HasColumnName("ArchiveUserID");

                entity.Property(e => e.CampusId)
                    .IsRequired()
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Grade)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HomeRoom)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Race)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.TeacherId)
                    .IsRequired()
                    .HasColumnName("TeacherID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Zip)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblArchTeachersDistribution>(entity =>
            {
                entity.HasKey(e => e.ArchiveDistributionId);

                entity.ToTable("tblArchTeachersDistribution");

                entity.Property(e => e.ArchiveDistributionId).HasColumnName("ArchiveDistributionID");

                entity.Property(e => e.Accession)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ArchiveDate).HasColumnType("datetime");

                entity.Property(e => e.ArchiveId).HasColumnName("ArchiveID");

                entity.Property(e => e.ArchiveTeacherId).HasColumnName("ArchiveTeacherID");

                entity.Property(e => e.ArchiveUserId).HasColumnName("ArchiveUserID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DistributionId).HasColumnName("DistributionID");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SourceType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TeacherId)
                    .IsRequired()
                    .HasColumnName("TeacherID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TblArchTeachersDistributionTx>(entity =>
            {
                entity.HasKey(e => e.ArchiveTransactionId);

                entity.ToTable("tblArchTeachersDistribution_tx");

                entity.Property(e => e.ArchiveTransactionId).HasColumnName("ArchiveTransactionID");

                entity.Property(e => e.Accession)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ActionTaken)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ArchiveDate).HasColumnType("datetime");

                entity.Property(e => e.ArchiveId).HasColumnName("ArchiveID");

                entity.Property(e => e.ArchiveTeacherId).HasColumnName("ArchiveTeacherID");

                entity.Property(e => e.ArchiveUserId).HasColumnName("ArchiveUserID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DistributionId).HasColumnName("DistributionID");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SourceType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TeacherId)
                    .IsRequired()
                    .HasColumnName("TeacherID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TblArchTransfer>(entity =>
            {
                entity.HasKey(e => e.ArchiveId);

                entity.ToTable("tblArchTransfer");

                entity.Property(e => e.ArchiveId).HasColumnName("ArchiveID");

                entity.Property(e => e.ArchivedDate).HasColumnType("datetime");

                entity.Property(e => e.ArchivedUserId).HasColumnName("ArchivedUserID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TargetCampus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TransferId).HasColumnName("TransferID");

                entity.Property(e => e.TransferName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblArchTransferDetails>(entity =>
            {
                entity.HasKey(e => e.ArchiveId);

                entity.ToTable("tblArchTransferDetails");

                entity.Property(e => e.ArchiveId).HasColumnName("ArchiveID");

                entity.Property(e => e.ArchivedDate).HasColumnType("datetime");

                entity.Property(e => e.ArchivedUserId).HasColumnName("ArchivedUserID");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.SourceCampus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TransferId).HasColumnName("TransferID");
            });

            modelBuilder.Entity<TblArchVendorBooks>(entity =>
            {
                entity.HasKey(e => e.ArchiveId);

                entity.ToTable("tblArchVendorBooks");

                entity.Property(e => e.ArchiveId).HasColumnName("ArchiveID");

                entity.Property(e => e.ArchivedDate).HasColumnType("datetime");

                entity.Property(e => e.ArchivedUserId).HasColumnName("ArchivedUserID");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");
            });

            modelBuilder.Entity<TblArchVendorOrders>(entity =>
            {
                entity.HasKey(e => e.ArchiveId);

                entity.ToTable("tblArchVendorOrders");

                entity.Property(e => e.ArchiveId).HasColumnName("ArchiveID");

                entity.Property(e => e.ArchivedDate).HasColumnType("datetime");

                entity.Property(e => e.ArrivalDate).HasColumnType("datetime");

                entity.Property(e => e.CampusId)
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateSubmitted).HasColumnType("datetime");

                entity.Property(e => e.OrderStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseOrder)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.SpecialInstructions)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.Property(e => e.VendorOrderId)
                    .HasColumnName("VendorOrderID")
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblAuditBookInventory>(entity =>
            {
                entity.HasKey(e => e.AuditBookInventoryUid);

                entity.ToTable("tblAuditBookInventory");

                entity.Property(e => e.AuditBookInventoryUid).HasColumnName("AuditBookInventoryUID");

                entity.Property(e => e.AuditUid).HasColumnName("AuditUID");

                entity.Property(e => e.BookInventoryUid).HasColumnName("BookInventoryUID");

                entity.HasOne(d => d.AuditU)
                    .WithMany(p => p.TblAuditBookInventory)
                    .HasForeignKey(d => d.AuditUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblAuditBookInventory_tblUnvAudits");

                entity.HasOne(d => d.BookInventoryU)
                    .WithMany(p => p.TblAuditBookInventory)
                    .HasForeignKey(d => d.BookInventoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblAuditBookInventory_tblBookInventory");
            });

            modelBuilder.Entity<TblAuditDetailCounts>(entity =>
            {
                entity.HasKey(e => e.AuditDetailCountUid);

                entity.ToTable("tblAuditDetailCounts");

                entity.HasIndex(e => new { e.AuditDetailCountUid, e.Count, e.AuditDetailUid, e.BookInventoryUid })
                    .HasName("IDX_AuditDetailBookInventory");

                entity.Property(e => e.AuditDetailCountUid).HasColumnName("AuditDetailCountUID");

                entity.Property(e => e.AuditDetailUid).HasColumnName("AuditDetailUID");

                entity.Property(e => e.BookInventoryUid).HasColumnName("BookInventoryUID");

                entity.Property(e => e.Notes)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.AuditDetailU)
                    .WithMany(p => p.TblAuditDetailCounts)
                    .HasForeignKey(d => d.AuditDetailUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblAuditDetailCounts_tblAuditDetails");

                entity.HasOne(d => d.BookInventoryU)
                    .WithMany(p => p.TblAuditDetailCounts)
                    .HasForeignKey(d => d.BookInventoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblAuditDetailCounts_tblBookInventory");
            });

            modelBuilder.Entity<TblAuditDetails>(entity =>
            {
                entity.HasKey(e => e.AuditDetailUid);

                entity.ToTable("tblAuditDetails");

                entity.Property(e => e.AuditDetailUid).HasColumnName("AuditDetailUID");

                entity.Property(e => e.AuditDetailNotes)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.AuditUid).HasColumnName("AuditUID");

                entity.Property(e => e.CampusUid).HasColumnName("CampusUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.FinalizeSignatureDateTime).HasColumnType("datetime");

                entity.Property(e => e.FinalizeSignatureFullName).HasMaxLength(100);

                entity.Property(e => e.FinalizeSignatureInitials).HasMaxLength(4);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.SubmitSignatureDateTime).HasColumnType("datetime");

                entity.Property(e => e.SubmitSignatureFullName).HasMaxLength(100);

                entity.Property(e => e.SubmitSignatureInitials).HasMaxLength(4);

                entity.Property(e => e.SubmittedByUserId).HasColumnName("SubmittedByUserID");

                entity.HasOne(d => d.AuditU)
                    .WithMany(p => p.TblAuditDetails)
                    .HasForeignKey(d => d.AuditUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblAuditDetails_tblUnvAudits");

                entity.HasOne(d => d.CampusU)
                    .WithMany(p => p.TblAuditDetails)
                    .HasForeignKey(d => d.CampusUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblAuditDetails_tblCampuses");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TblAuditDetails)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblAuditDetails_tblStatus");
            });

            modelBuilder.Entity<TblAuditFilterMaterialTypes>(entity =>
            {
                entity.HasKey(e => e.AuditFilterMaterialTypesUid);

                entity.ToTable("tblAuditFilterMaterialTypes");

                entity.Property(e => e.AuditFilterMaterialTypesUid).HasColumnName("AuditFilterMaterialTypesUID");

                entity.Property(e => e.AuditUid).HasColumnName("AuditUID");

                entity.Property(e => e.MaterialTypeId).HasColumnName("MaterialTypeID");

                entity.HasOne(d => d.AuditU)
                    .WithMany(p => p.TblAuditFilterMaterialTypes)
                    .HasForeignKey(d => d.AuditUid)
                    .HasConstraintName("FK_tblAuditFilterMaterialTypes_tblUnvAudits");
            });

            modelBuilder.Entity<TblAuditFilters>(entity =>
            {
                entity.HasKey(e => e.AuditFilterUid);

                entity.ToTable("tblAuditFilters");

                entity.Property(e => e.AuditFilterUid).HasColumnName("AuditFilterUID");

                entity.Property(e => e.AuditUid).HasColumnName("AuditUID");

                entity.Property(e => e.FilterType).HasMaxLength(50);

                entity.Property(e => e.FilterValue).HasMaxLength(50);

                entity.HasOne(d => d.AuditU)
                    .WithMany(p => p.TblAuditFilters)
                    .HasForeignKey(d => d.AuditUid)
                    .HasConstraintName("FK_tblAuditFilters_tblUnvAudits");
            });

            modelBuilder.Entity<TblAustinIfas>(entity =>
            {
                entity.ToTable("tblAustinIFAS");

                entity.Property(e => e.AtmetaUid).HasColumnName("ATMetaUID");

                entity.Property(e => e.Cond)
                    .HasColumnName("cond")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExpnsAcct)
                    .HasColumnName("expns_acct")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FDesc)
                    .HasColumnName("f_desc")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Faid)
                    .HasColumnName("faid")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FaidmetaUid).HasColumnName("FAIDMetaUID");

                entity.Property(e => e.FundingSourceUid).HasColumnName("FundingSourceUID");

                entity.Property(e => e.Inservdt)
                    .HasColumnName("inservdt")
                    .HasColumnType("datetime");

                entity.Property(e => e.Invdt)
                    .HasColumnName("invdt")
                    .HasColumnType("datetime");

                entity.Property(e => e.InventoryUid).HasColumnName("InventoryUID");

                entity.Property(e => e.Invoice)
                    .HasColumnName("invoice")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsdmetaUid).HasColumnName("ISDMetaUID");

                entity.Property(e => e.IssuedDate).HasColumnType("datetime");

                entity.Property(e => e.ItemTypeUid).HasColumnName("ItemTypeUID");

                entity.Property(e => e.ItemUid).HasColumnName("ItemUID");

                entity.Property(e => e.Itemamt)
                    .HasColumnName("itemamt")
                    .HasColumnType("money");

                entity.Property(e => e.Key)
                    .HasColumnName("key")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lctn)
                    .HasColumnName("lctn")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LongTag)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ManufacturerUid).HasColumnName("ManufacturerUID");

                entity.Property(e => e.Mfctid)
                    .HasColumnName("mfctid")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Model)
                    .HasColumnName("model")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Obj)
                    .HasColumnName("obj")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OldAtmetaUid).HasColumnName("oldATMetaUID");

                entity.Property(e => e.OldFaidmetaUid).HasColumnName("oldFAIDMetaUID");

                entity.Property(e => e.OldIsdmetaUid).HasColumnName("oldISDMetaUID");

                entity.Property(e => e.Pc)
                    .HasColumnName("pc")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Po)
                    .HasColumnName("po")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Purchamt)
                    .HasColumnName("purchamt")
                    .HasColumnType("money");

                entity.Property(e => e.PurchaseInventoryUid).HasColumnName("PurchaseInventoryUID");

                entity.Property(e => e.PurchaseItemDetailUid).HasColumnName("PurchaseItemDetailUID");

                entity.Property(e => e.PurchaseItemShipmentUid).HasColumnName("PurchaseItemShipmentUID");

                entity.Property(e => e.PurchaseUid).HasColumnName("PurchaseUID");

                entity.Property(e => e.Sc)
                    .HasColumnName("sc")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Serialno)
                    .HasColumnName("serialno")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SiteUid).HasColumnName("SiteUID");

                entity.Property(e => e.Stat)
                    .HasColumnName("stat")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tag)
                    .HasColumnName("tag")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TeacherId)
                    .HasColumnName("TeacherID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VendorUid).HasColumnName("VendorUID");
            });

            modelBuilder.Entity<TblBookBins>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.ToTable("tblBookBins");

                entity.HasIndex(e => e.BinId)
                    .HasName("IDX_BinID");

                entity.HasIndex(e => e.Isbn)
                    .HasName("IDX_ISBN");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.BinId)
                    .IsRequired()
                    .HasColumnName("BinID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblBookInventory>(entity =>
            {
                entity.HasKey(e => e.BookInventoryUid);

                entity.ToTable("tblBookInventory");

                entity.HasIndex(e => e.Grade)
                    .HasName("IDX_Grade");

                entity.HasIndex(e => e.Isbn)
                    .HasName("IDX_ISBN")
                    .IsUnique();

                entity.HasIndex(e => e.Publisher)
                    .HasName("IDX_Publisher");

                entity.HasIndex(e => e.Slc)
                    .HasName("IDX_SLC");

                entity.HasIndex(e => new { e.Price, e.Isbn, e.BookInventoryUid })
                    .HasName("_dta_index_tblBookInventory_8_672877614__K1_K26_5");

                entity.HasIndex(e => new { e.Title, e.Isbn, e.BookInventoryUid })
                    .HasName("_dta_index_tblBookInventory_8_672877614__K1_K26_4");

                entity.HasIndex(e => new { e.Expire, e.Grade, e.Slc, e.Price, e.Publisher, e.Active, e.CampusAdded, e.Title, e.Isbn, e.BookInventoryUid })
                    .HasName("_dta_index_tblBookInventory_8_672877614__K1_K26_2_4_5_6_7_10_22_25");

                entity.HasIndex(e => new { e.CampusAdded, e.Adopt, e.BookSet, e.BinId, e.Expire, e.DistReqCode, e.DistrictOnOrder, e.GroupCode, e.Grade, e.Copyright, e.Active, e.Publisher, e.OnOrder, e.Price, e.ModifiedDate, e.LeftInStorage, e.Notes, e.SetIsbn, e.ShowOnReports, e.Slc, e.Udf, e.UserId, e.Title, e.StateOnOrder, e.Isbn, e.BookInventoryUid })
                    .HasName("_dta_index_tblBookInventory_8_672877614__K1_K26_2_3_4_5_6_7_8_9")
                    .IsUnique();

                entity.Property(e => e.BookInventoryUid).HasColumnName("BookInventoryUID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Adopt)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BinId)
                    .HasColumnName("BinID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BookSet).HasDefaultValueSql("((0))");

                entity.Property(e => e.CampusAdded).HasDefaultValueSql("((0))");

                entity.Property(e => e.Copyright)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DistReqCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Expire)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Grade)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GroupCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Publisher).HasDefaultValueSql("((0))");

                entity.Property(e => e.SetIsbn).HasColumnName("SetISBN");

                entity.Property(e => e.ShowOnReports).HasDefaultValueSql("((0))");

                entity.Property(e => e.Slc)
                    .HasColumnName("SLC")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Udf)
                    .HasColumnName("UDF")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TblBookOrders>(entity =>
            {
                entity.HasKey(e => e.BookOrdersUid);

                entity.ToTable("tblBookOrders");

                entity.HasIndex(e => e.BookOrdersUid)
                    .HasName("idx_BookOrdersUID");

                entity.HasIndex(e => e.Isbn)
                    .HasName("idx_ISBN");

                entity.HasIndex(e => e.RandomReq)
                    .HasName("IDX_RandomReq");

                entity.HasIndex(e => e.RequisitionUid)
                    .HasName("idx_requid");

                entity.HasIndex(e => e.Status)
                    .HasName("IDX_Status");

                entity.HasIndex(e => new { e.VendorOrder, e.Isbn, e.BackOrders, e.RequisitionUid, e.Status, e.BookOrdersUid })
                    .HasName("_dta_index_tblBookOrders_8_1442976367__K2_K16_K1_3_18_25");

                entity.HasIndex(e => new { e.CopiesApproved, e.Received, e.CopiesToSend, e.Ordered, e.Isbn, e.RequisitionUid, e.BookOrdersUid })
                    .HasName("_dta_index_tblBookOrders_8_1442976367__K3_K2_K1_5_8_20_23");

                entity.HasIndex(e => new { e.Ordered, e.RequisitionUid, e.BookOrdersUid, e.Isbn, e.VendorOrder, e.FundingSource, e.OrderNumber })
                    .HasName("_dta_index_tblBookOrders_8_1442976367__K2_K1_K3_K25_K4_K10_5");

                entity.HasIndex(e => new { e.CopiesOnHand, e.CopiesApproved, e.BackOrders, e.Ordered, e.CopiesSent, e.CopiesToSend, e.Denied, e.RequisitionUid, e.Status, e.Isbn, e.BookOrdersUid })
                    .HasName("_dta_index_tblBookOrders_8_1442976367__K2_K16_K3_K1_5_18_19_20_21_23_24");

                entity.HasIndex(e => new { e.VendorOrder, e.Received, e.Ordered, e.CopiesToSend, e.CopiesSent, e.CopiesApproved, e.CopiesOnHand, e.BackOrders, e.Denied, e.Status, e.Isbn, e.RequisitionUid, e.BookOrdersUid })
                    .HasName("_dta_index_tblBookOrders_8_1442976367__K16_K3_K2_K1_5_8_18_19_20_21_23_24_25");

                entity.Property(e => e.BookOrdersUid).HasColumnName("BookOrdersUID");

                entity.Property(e => e.ArrivalDate).HasColumnType("datetime");

                entity.Property(e => e.CopiesApproved).HasDefaultValueSql("((0))");

                entity.Property(e => e.CopiesSent).HasDefaultValueSql("((0))");

                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DateReceived).HasColumnType("datetime");

                entity.Property(e => e.DateSent).HasColumnType("datetime");

                entity.Property(e => e.Denied)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeniedNote)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.FundingSource)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.RandomReq)
                    .HasMaxLength(325)
                    .IsUnicode(false);

                entity.Property(e => e.Received).HasDefaultValueSql("((0))");

                entity.Property(e => e.RequisitionUid).HasColumnName("RequisitionUID");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TicketDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.RequisitionU)
                    .WithMany(p => p.TblBookOrders)
                    .HasForeignKey(d => d.RequisitionUid)
                    .HasConstraintName("FK_tblBookOrders_tblRequisitions");
            });

            modelBuilder.Entity<TblBookOrdersHistoryDist>(entity =>
            {
                entity.HasKey(e => e.BookOrdersHistoryDistUid);

                entity.ToTable("tblBookOrdersHistoryDist");

                entity.HasIndex(e => e.BookOrdersHistoryDistUid)
                    .HasName("IDX_RecordID");

                entity.HasIndex(e => e.Isbn)
                    .HasName("IDX_ISBN");

                entity.HasIndex(e => e.RandomReq)
                    .HasName("IDX_RandomReq");

                entity.HasIndex(e => e.RequisitionUid)
                    .HasName("idx_RequisitionUID");

                entity.HasIndex(e => new { e.CopiesSent, e.CopiesToShip, e.CopiesReceived, e.TicketDate, e.DateCopiesSent, e.Isbn, e.RequisitionUid, e.BookOrdersHistoryDistUid })
                    .HasName("_dta_index_tblBookOrdersHistoryDist_8_1586976880__K3_K2_K1_4_5_6_7_8");

                entity.HasIndex(e => new { e.UserId, e.ModifiedDate, e.RandomReq, e.TicketDate, e.CopiesToShip, e.DateCopiesSent, e.RequisitionUid, e.Isbn, e.BookOrdersHistoryDistUid, e.CopiesReceived, e.CopiesSent })
                    .HasName("_dta_index_tblBookOrdersHistoryDist_8_1586976880__K2_K3_K1_K8_K4_5_6_7_9_10_11");

                entity.Property(e => e.BookOrdersHistoryDistUid).HasColumnName("BookOrdersHistoryDistUID");

                entity.Property(e => e.DateCopiesSent).HasColumnType("datetime");

                entity.Property(e => e.Isbn)
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RandomReq)
                    .HasMaxLength(325)
                    .IsUnicode(false);

                entity.Property(e => e.RequisitionUid).HasColumnName("RequisitionUID");

                entity.Property(e => e.TicketDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.RequisitionU)
                    .WithMany(p => p.TblBookOrdersHistoryDist)
                    .HasForeignKey(d => d.RequisitionUid)
                    .HasConstraintName("FK_tblBookOrdersHistoryDist_tblRequisitions");
            });

            modelBuilder.Entity<TblBookOrderTransactions>(entity =>
            {
                entity.HasKey(e => e.BookOrderTransactionUid);

                entity.ToTable("tblBookOrderTransactions");

                entity.Property(e => e.BookOrderTransactionUid).HasColumnName("BookOrderTransactionUID");

                entity.Property(e => e.BookOrdersUid).HasColumnName("BookOrdersUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.EventDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.BookOrdersU)
                    .WithMany(p => p.TblBookOrderTransactions)
                    .HasForeignKey(d => d.BookOrdersUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBookOrderTransactions_tblBookOrders");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TblBookOrderTransactions)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBookOrderTransactions_tblStatus");
            });

            modelBuilder.Entity<TblBooksCourses>(entity =>
            {
                entity.HasKey(e => e.BooksCoursesUid);

                entity.ToTable("tblBooksCourses");

                entity.HasIndex(e => new { e.MasterCourseUid, e.Teachers, e.Students, e.BookInventoryUid, e.CampusUid })
                    .HasName("_dta_index_tblBooksCourses_8_429400749__K3_K2_4_5_6");

                entity.Property(e => e.BooksCoursesUid).HasColumnName("BooksCoursesUID");

                entity.Property(e => e.BookInventoryUid).HasColumnName("BookInventoryUID");

                entity.Property(e => e.CampusUid).HasColumnName("CampusUID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MasterCourseUid).HasColumnName("MasterCourseUID");

                entity.Property(e => e.Students).HasDefaultValueSql("((0))");

                entity.Property(e => e.Teachers).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.BookInventoryU)
                    .WithMany(p => p.TblBooksCourses)
                    .HasForeignKey(d => d.BookInventoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBooksCourses_tblBookInventory");

                entity.HasOne(d => d.CampusU)
                    .WithMany(p => p.TblBooksCourses)
                    .HasForeignKey(d => d.CampusUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBooksCourses_tblCampuses");

                entity.HasOne(d => d.MasterCourseU)
                    .WithMany(p => p.TblBooksCourses)
                    .HasForeignKey(d => d.MasterCourseUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBooksCourses_tblMasterCourse");
            });

            modelBuilder.Entity<TblBooksCoursesDistrict>(entity =>
            {
                entity.HasKey(e => e.BooksCoursesDistrictUid);

                entity.ToTable("tblBooksCoursesDistrict");

                entity.Property(e => e.BooksCoursesDistrictUid).HasColumnName("BooksCoursesDistrictUID");

                entity.Property(e => e.BookInventoryUid).HasColumnName("BookInventoryUID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MasterCourseUid).HasColumnName("MasterCourseUID");

                entity.HasOne(d => d.BookInventoryU)
                    .WithMany(p => p.TblBooksCoursesDistrict)
                    .HasForeignKey(d => d.BookInventoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBooksCoursesDistrict_tblBookInventory");

                entity.HasOne(d => d.MasterCourseU)
                    .WithMany(p => p.TblBooksCoursesDistrict)
                    .HasForeignKey(d => d.MasterCourseUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBooksCoursesDistrict_tblMasterCourse");
            });

            modelBuilder.Entity<TblBooksMaterialTypes>(entity =>
            {
                entity.HasKey(e => e.BooksMaterialTypeId);

                entity.ToTable("tblBooksMaterialTypes");

                entity.Property(e => e.BooksMaterialTypeId).HasColumnName("BooksMaterialTypeID");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MaterialTypeId)
                    .IsRequired()
                    .HasColumnName("MaterialTypeID")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblCampusApproved>(entity =>
            {
                entity.HasKey(e => e.CampusApprovedId);

                entity.ToTable("tblCampusApproved");

                entity.HasIndex(e => new { e.CampusId, e.Isbn })
                    .HasName("_dta_index_tblCampusApproved_8_365400521__K1_K2");

                entity.Property(e => e.CampusApprovedId).HasColumnName("CampusApprovedID");

                entity.Property(e => e.CampusId)
                    .IsRequired()
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StudentQuota).HasDefaultValueSql("((100))");

                entity.Property(e => e.TeacherQuota).HasDefaultValueSql("((100))");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TblCampusBookHistory>(entity =>
            {
                entity.HasKey(e => e.Isbn);

                entity.ToTable("tblCampusBookHistory");

                entity.Property(e => e.Isbn)
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Accession)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CourseCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Publisher)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblCampusCoursesAssigned>(entity =>
            {
                entity.HasKey(e => e.CampusCoursesAssignedUid);

                entity.ToTable("tblCampusCoursesAssigned");

                entity.HasIndex(e => e.CampusUid)
                    .HasName("IDX_CampusUID");

                entity.HasIndex(e => e.MasterCourseUid)
                    .HasName("MasterCoursesAssigned_Index");

                entity.HasIndex(e => new { e.MaxTeacherEnrollment, e.StudentEnrollment, e.MaxStudentEnrollment, e.TeacherEnrollment, e.MasterCourseUid, e.CampusUid, e.CampusCoursesAssignedUid })
                    .HasName("_dta_index_tblCampusCoursesAssigned_8_1552880749__K3_K2_K1_4_5_6_7");

                entity.HasIndex(e => new { e.TeacherEnrollment, e.StudentEnrollment, e.MaxTeacherEnrollment, e.MaxStudentEnrollment, e.CampusUid, e.CampusCoursesAssignedUid, e.MasterCourseUid })
                    .HasName("_dta_index_tblCampusCoursesAssigned_8_1552880749__K2_K1_K3_4_5_6_7");

                entity.HasIndex(e => new { e.MaxTeacherEnrollment, e.StudentEnrollment, e.MaxStudentEnrollment, e.TeacherEnrollment, e.CourseSifguid, e.MasterCourseUid, e.CampusUid, e.CampusCoursesAssignedUid })
                    .HasName("_dta_index_tblCampusCoursesAssigned_8_1552880749__K3_K2_K1_4_5_6_7_8");

                entity.Property(e => e.CampusCoursesAssignedUid).HasColumnName("CampusCoursesAssignedUID");

                entity.Property(e => e.CampusUid).HasColumnName("CampusUID");

                entity.Property(e => e.CourseSifguid).HasColumnName("CourseSIFGuid");

                entity.Property(e => e.MasterCourseUid).HasColumnName("MasterCourseUID");

                entity.Property(e => e.StudentEnrollment).HasDefaultValueSql("((0))");

                entity.Property(e => e.TeacherEnrollment).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.CampusU)
                    .WithMany(p => p.TblCampusCoursesAssigned)
                    .HasForeignKey(d => d.CampusUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblCampusCoursesAssigned_tblCampuses");

                entity.HasOne(d => d.MasterCourseU)
                    .WithMany(p => p.TblCampusCoursesAssigned)
                    .HasForeignKey(d => d.MasterCourseUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblCampusCoursesAssigned_tblMasterCourse");
            });

            modelBuilder.Entity<TblCampusDistribution>(entity =>
            {
                entity.HasKey(e => e.DistributionId);

                entity.ToTable("tblCampusDistribution");

                entity.HasIndex(e => e.CampusId)
                    .HasName("IDX_CAMPUSID");

                entity.HasIndex(e => e.Code)
                    .HasName("IDX_CODE");

                entity.HasIndex(e => e.DistributionId)
                    .HasName("IDX_DistributionID");

                entity.HasIndex(e => e.Isbn)
                    .HasName("IDX_ISBN");

                entity.HasIndex(e => new { e.Copies, e.Code, e.Isbn, e.DistributionId })
                    .HasName("_dta_index_tblCampusDistribution_8_532353111__K4_K3_K1_7");

                entity.HasIndex(e => new { e.Copies, e.Isbn, e.Code, e.DistributionId })
                    .HasName("_dta_index_tblCampusDistribution_8_532353111__K3_K4_K1_7");

                entity.HasIndex(e => new { e.Copies, e.CampusId, e.DistributionId, e.Isbn, e.Code })
                    .HasName("_dta_index_tblCampusDistribution_8_532353111__K2_K1_K3_K4_7");

                entity.HasIndex(e => new { e.Copies, e.Code, e.CampusId, e.Isbn, e.DistributionId })
                    .HasName("_dta_index_tblCampusDistribution_8_532353111__K4_K2_K3_K1_7");

                entity.HasIndex(e => new { e.DistributionId, e.Copies, e.CampusId, e.Isbn, e.ModifiedDate, e.Code, e.Reference })
                    .HasName("_dta_index_tblCampusDistribution_8_532353111__K2_K3_K10_K4_K11_1_7");

                entity.Property(e => e.DistributionId).HasColumnName("DistributionID");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.CampusId)
                    .IsRequired()
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Reference)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TblCampusDistributionTx>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.ToTable("tblCampusDistribution_tx");

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.ActionTaken)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.CampusId)
                    .IsRequired()
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DistributionId).HasColumnName("DistributionID");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Reference)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Distribution)
                    .WithMany(p => p.TblCampusDistributionTx)
                    .HasForeignKey(d => d.DistributionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tblCampusDistribution_tx_tblCampusDistribution");
            });

            modelBuilder.Entity<TblCampuses>(entity =>
            {
                entity.HasKey(e => e.CampusUid);

                entity.ToTable("tblCampuses");

                entity.HasIndex(e => e.CampusId)
                    .HasName("IDX_CampusID");

                entity.HasIndex(e => e.CampusUid)
                    .HasName("IDX_CampusUID");

                entity.Property(e => e.CampusUid).HasColumnName("CampusUID");

                entity.Property(e => e.BillAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BillAddress2)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BillCity)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BillState)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.BillZip)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.CampusContact)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CampusId)
                    .IsRequired()
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CampusName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CampusSifguid).HasColumnName("CampusSIFGuid");

                entity.Property(e => e.CampusType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Notes)
                    .HasMaxLength(4200)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShipAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ShipAddress2)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ShipCity)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ShipInstructions)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ShipState)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ShipZip)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TblCampusInitializationTemp>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.ToTable("tblCampusInitializationTemp");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.CampusId)
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Isbn)
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblComponents>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.ToTable("tblComponents");

                entity.HasIndex(e => e.MasterIsbn)
                    .HasName("IDX_MasterISBN");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.Bin)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BinAlt1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BinAlt2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BinAlt3)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BinAlt4)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ComponentIsbn)
                    .IsRequired()
                    .HasColumnName("ComponentISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MasterIsbn)
                    .IsRequired()
                    .HasColumnName("MasterISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Title)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblDigitalMaterialDetails>(entity =>
            {
                entity.HasKey(e => e.DigitalMaterialDetailUid);

                entity.ToTable("tblDigitalMaterialDetails");

                entity.Property(e => e.DigitalMaterialDetailUid).HasColumnName("DigitalMaterialDetailUID");

                entity.Property(e => e.BookInventoryUid).HasColumnName("BookInventoryUID");

                entity.Property(e => e.DurationIntervalUid).HasColumnName("DurationIntervalUID");

                entity.Property(e => e.MaterialTypeId).HasColumnName("MaterialTypeID");

                entity.Property(e => e.Product)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UsageTypeUid).HasColumnName("UsageTypeUID");

                entity.Property(e => e.Website)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.BookInventoryU)
                    .WithMany(p => p.TblDigitalMaterialDetails)
                    .HasForeignKey(d => d.BookInventoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblDigitalMaterialDetails_tblBookInventory");

                entity.HasOne(d => d.DurationIntervalU)
                    .WithMany(p => p.TblDigitalMaterialDetails)
                    .HasForeignKey(d => d.DurationIntervalUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblDigitalMaterialDetails_tblDurationIntervals");

                entity.HasOne(d => d.UsageTypeU)
                    .WithMany(p => p.TblDigitalMaterialDetails)
                    .HasForeignKey(d => d.UsageTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblDigitalMaterialDetails_tblUsageTypes");
            });

            modelBuilder.Entity<TblDistrictDistributions>(entity =>
            {
                entity.HasKey(e => e.DistributionId);

                entity.ToTable("tblDistrictDistributions");

                entity.HasIndex(e => e.Code)
                    .HasName("IDX_Code");

                entity.HasIndex(e => e.DistributionId)
                    .HasName("IDX_DistributionID");

                entity.HasIndex(e => e.Isbn)
                    .HasName("IDX_ISBN");

                entity.HasIndex(e => new { e.Copies, e.Isbn, e.Code, e.DistributionId })
                    .HasName("_dta_index_tblDistrictDistributions_8_900354422__K2_K3_K1_6");

                entity.Property(e => e.DistributionId).HasColumnName("DistributionID");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TblDistrictDistributionsTx>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.ToTable("tblDistrictDistributions_tx");

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.ActionTaken)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DistributionId).HasColumnName("DistributionID");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Distribution)
                    .WithMany(p => p.TblDistrictDistributionsTx)
                    .HasForeignKey(d => d.DistributionId)
                    .HasConstraintName("FK_tblDistrictDistributions_tx_tblDistrictDistributions");
            });

            modelBuilder.Entity<TblDistrictPreferences>(entity =>
            {
                entity.HasKey(e => e.DistrictPreferenceId);

                entity.ToTable("tblDistrictPreferences");

                entity.Property(e => e.DistrictPreferenceId).HasColumnName("DistrictPreferenceID");

                entity.Property(e => e.AccLimitMax).HasDefaultValueSql("((100))");

                entity.Property(e => e.AccLimitMin).HasDefaultValueSql("((1))");

                entity.Property(e => e.AllowSound)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DistrictLogo).HasColumnType("image");

                entity.Property(e => e.IsbnlimitMax)
                    .HasColumnName("ISBNLimitMax")
                    .HasDefaultValueSql("((100))");

                entity.Property(e => e.IsbnlimitMin)
                    .HasColumnName("ISBNLimitMin")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PickTicketExpirationDays).HasDefaultValueSql("((60))");

                entity.Property(e => e.PickTicketMessage)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.RowsDisplayed).HasDefaultValueSql("((100))");

                entity.Property(e => e.ShowId)
                    .IsRequired()
                    .HasColumnName("ShowID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.StudIdlimitMax)
                    .HasColumnName("StudIDLimitMax")
                    .HasDefaultValueSql("((100))");

                entity.Property(e => e.StudIdlimitMin)
                    .HasColumnName("StudIDLimitMin")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TeachIdlimitMax)
                    .HasColumnName("TeachIDLimitMax")
                    .HasDefaultValueSql("((100))");

                entity.Property(e => e.TeachIdlimitMin)
                    .HasColumnName("TeachIDLimitMin")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TransferOptions).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TblDownloadDataTypes>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.ToTable("tblDownloadDataTypes");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.CampusId)
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FileType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblDownloadedData>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.ToTable("tblDownloadedData");

                entity.HasIndex(e => e.TypeRecordId)
                    .HasName("_dta_index_tblDownloadedData_8_708353738__K8");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.Accession)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Isbn)
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Message)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.TypeRecordId).HasColumnName("TypeRecordID");
            });

            modelBuilder.Entity<TblDurationIntervals>(entity =>
            {
                entity.HasKey(e => e.DurationIntervalUid);

                entity.ToTable("tblDurationIntervals");

                entity.Property(e => e.DurationIntervalUid)
                    .HasColumnName("DurationIntervalUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DurationInterval)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblErrors>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.ToTable("tblErrors");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.CampusId)
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DistrictId)
                    .HasColumnName("DistrictID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExceptionType)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Message)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PageErrorOccured)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.StackTrace)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblFundingSources>(entity =>
            {
                entity.HasKey(e => e.FundingSourceUid);

                entity.ToTable("tblFundingSources");

                entity.Property(e => e.FundingSourceUid).HasColumnName("FundingSourceUID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ApplicationUid)
                    .HasColumnName("ApplicationUID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FundingDesc)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FundingSource)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StatusNotificationEmail).IsUnicode(false);

                entity.Property(e => e.TransferNotificationEmail).IsUnicode(false);

                entity.HasOne(d => d.ApplicationU)
                    .WithMany(p => p.TblFundingSources)
                    .HasForeignKey(d => d.ApplicationUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblFundingSources_tblUnvApplications");
            });

            modelBuilder.Entity<TblLetters>(entity =>
            {
                entity.HasKey(e => e.LetterId);

                entity.ToTable("tblLetters");

                entity.Property(e => e.LetterId).HasColumnName("LetterID");

                entity.Property(e => e.CampusId)
                    .IsRequired()
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LetterBody)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.LetterDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LetterName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ReportId)
                    .HasColumnName("ReportID")
                    .HasDefaultValueSql("((21))");
            });

            modelBuilder.Entity<TblMasterBooks>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.ToTable("tblMasterBooks");

                entity.HasIndex(e => e.Isbn)
                    .HasName("_dta_index_tblMasterBooks_8_1854121846__K2");

                entity.HasIndex(e => e.RecordId)
                    .HasName("IX_tblMasterBooks");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.Aid).HasDefaultValueSql("((0))");

                entity.Property(e => e.Consumable).HasDefaultValueSql("((0))");

                entity.Property(e => e.Copyright)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Expire)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Grade)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MemCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Slc)
                    .HasColumnName("SLC")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.StudentPercent).HasColumnName("Student_Percent");

                entity.Property(e => e.TeacherPercent).HasColumnName("Teacher_Percent");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.PublisherNavigation)
                    .WithMany(p => p.TblMasterBooks)
                    .HasForeignKey(d => d.Publisher)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tblMasterBooks_tblMasterPublishers");
            });

            modelBuilder.Entity<TblMasterCourse>(entity =>
            {
                entity.HasKey(e => e.MasterCourseUid);

                entity.ToTable("tblMasterCourse");

                entity.HasIndex(e => e.CourseId)
                    .HasName("IDX_CourseID");

                entity.Property(e => e.MasterCourseUid).HasColumnName("MasterCourseUID");

                entity.Property(e => e.CourseId)
                    .IsRequired()
                    .HasColumnName("CourseID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CourseName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblMasterPublishers>(entity =>
            {
                entity.HasKey(e => e.PublisherId);

                entity.ToTable("tblMasterPublishers");

                entity.Property(e => e.PublisherId)
                    .HasColumnName("PublisherID")
                    .ValueGeneratedNever();

                entity.Property(e => e.PublisherName)
                    .IsRequired()
                    .HasMaxLength(7000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblMaterialTypes>(entity =>
            {
                entity.HasKey(e => e.MaterialTypeId);

                entity.ToTable("tblMaterialTypes");

                entity.Property(e => e.MaterialTypeId).HasColumnName("MaterialTypeID");

                entity.Property(e => e.MaterialType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MaterialTypeDescription)
                    .HasMaxLength(7000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblMessages>(entity =>
            {
                entity.HasKey(e => e.MessageId);

                entity.ToTable("tblMessages");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.FromUserId).HasColumnName("FromUserID");

                entity.Property(e => e.Message)
                    .HasMaxLength(7000)
                    .IsUnicode(false);

                entity.Property(e => e.MessageRead).HasDefaultValueSql("((0))");

                entity.Property(e => e.ReadDate).HasColumnType("datetime");

                entity.Property(e => e.SentDate).HasColumnType("datetime");

                entity.Property(e => e.ToUserId).HasColumnName("ToUserID");
            });

            modelBuilder.Entity<TblMobileErrors>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.ToTable("tblMobileErrors");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.Accession)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CampusId)
                    .IsRequired()
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ErrorMsg)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Isbn)
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SyncType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TblMobstudDistro>(entity =>
            {
                entity.HasKey(e => e.Uid);

                entity.ToTable("tblMOBStudDistro");

                entity.Property(e => e.Uid).HasColumnName("UID");

                entity.Property(e => e.CampusId)
                    .HasColumnName("CampusID")
                    .HasMaxLength(50);

                entity.Property(e => e.Isbn)
                    .HasColumnName("ISBN")
                    .HasMaxLength(50);

                entity.Property(e => e.Quantity).HasMaxLength(50);

                entity.Property(e => e.StudentId)
                    .HasColumnName("StudentID")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblPaymentsCollected>(entity =>
            {
                entity.HasKey(e => e.PaymentId);

                entity.ToTable("tblPaymentsCollected");

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.Accession)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(7500)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.TotalOwed).HasColumnType("money");

                entity.Property(e => e.TotalPaid).HasColumnType("money");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TblPaymentsCollectedTx>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.ToTable("tblPaymentsCollected_tx");

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.Accession)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ActionTaken)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(7500)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.TotalOwed).HasColumnType("money");

                entity.Property(e => e.TotalPaid).HasColumnType("money");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TblPdafileTypes>(entity =>
            {
                entity.HasKey(e => e.FileTypeId);

                entity.ToTable("tblPDAFileTypes");

                entity.Property(e => e.FileTypeId).HasColumnName("FileTypeID");

                entity.Property(e => e.FileType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblReconciledTx>(entity =>
            {
                entity.HasKey(e => e.RecondiledUid);

                entity.ToTable("tblReconciled_tx");

                entity.Property(e => e.RecondiledUid).HasColumnName("recondiledUID");

                entity.Property(e => e.Accession)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DistributionDate)
                    .HasColumnName("distributionDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.FkAdjustmentId).HasColumnName("fk_AdjustmentID");

                entity.Property(e => e.FkAreaUid).HasColumnName("fk_AreaUID");

                entity.Property(e => e.FkBookUid).HasColumnName("fk_BookUID");

                entity.Property(e => e.FkEntityId).HasColumnName("fk_EntityID");

                entity.Property(e => e.FkUserUid).HasColumnName("fk_UserUID");
            });

            modelBuilder.Entity<TblRegion>(entity =>
            {
                entity.HasKey(e => e.RegionId);

                entity.ToTable("tblRegion");

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.RegionDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.RegionName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblReportCategories>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.ToTable("tblReportCategories");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.CampusDistrict)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryDescription)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            });

            modelBuilder.Entity<TblReportLimits>(entity =>
            {
                entity.HasKey(e => new { e.ReportId, e.LimitBy });

                entity.ToTable("tblReportLimits");

                entity.Property(e => e.ReportId).HasColumnName("ReportID");

                entity.Property(e => e.LimitBy)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.DatabaseTranslation)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblReports>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.ToTable("tblReports");

                entity.Property(e => e.RecordId)
                    .HasColumnName("RecordID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CampusDistrict)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.QuickReportsPage)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.ReportDescription)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ReportId).HasColumnName("ReportID");

                entity.Property(e => e.ReportName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ReportQuery)
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Scope)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SortQuery)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.UserNotes)
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblReportSorts>(entity =>
            {
                entity.HasKey(e => new { e.ReportId, e.SortedBy });

                entity.ToTable("tblReportSorts");

                entity.Property(e => e.ReportId).HasColumnName("ReportID");

                entity.Property(e => e.SortedBy)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.DatabaseTranslation)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblRequisitionMultiCampus>(entity =>
            {
                entity.HasKey(e => e.RequisitionMultiCampusUid);

                entity.ToTable("tblRequisitionMultiCampus");

                entity.Property(e => e.RequisitionMultiCampusUid).HasColumnName("RequisitionMultiCampusUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Notes).HasMaxLength(1000);

                entity.Property(e => e.StatusUid).HasColumnName("StatusUID");

                entity.HasOne(d => d.StatusU)
                    .WithMany(p => p.TblRequisitionMultiCampus)
                    .HasForeignKey(d => d.StatusUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblRequisitionMultiCampus_tblStatus");
            });

            modelBuilder.Entity<TblRequisitionMultiCampusDetails>(entity =>
            {
                entity.HasKey(e => e.RequisitionMultiCampusDetailUid);

                entity.ToTable("tblRequisitionMultiCampusDetails");

                entity.Property(e => e.RequisitionMultiCampusDetailUid).HasColumnName("RequisitionMultiCampusDetailUID");

                entity.Property(e => e.BookInventoryUid).HasColumnName("BookInventoryUID");

                entity.Property(e => e.CampusUid).HasColumnName("CampusUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RequisitionMultiCampusUid).HasColumnName("RequisitionMultiCampusUID");

                entity.HasOne(d => d.RequisitionMultiCampusU)
                    .WithMany(p => p.TblRequisitionMultiCampusDetails)
                    .HasForeignKey(d => d.RequisitionMultiCampusUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblRequisitionMultiCampusDetails_tblRequisitionMultiCampus");
            });

            modelBuilder.Entity<TblRequisitionMultiCampusLink>(entity =>
            {
                entity.HasKey(e => e.RequisitionToMultiCampusRequisitionUid);

                entity.ToTable("tblRequisitionMultiCampusLink");

                entity.Property(e => e.RequisitionToMultiCampusRequisitionUid).HasColumnName("RequisitionToMultiCampusRequisitionUID");

                entity.Property(e => e.RequisitionMultiCampusUid).HasColumnName("RequisitionMultiCampusUID");

                entity.Property(e => e.RequisitionUid).HasColumnName("RequisitionUID");

                entity.HasOne(d => d.RequisitionMultiCampusU)
                    .WithMany(p => p.TblRequisitionMultiCampusLink)
                    .HasForeignKey(d => d.RequisitionMultiCampusUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblRequisitionMultiCampusLink_tblRequisitionMultiCampus");

                entity.HasOne(d => d.RequisitionU)
                    .WithMany(p => p.TblRequisitionMultiCampusLink)
                    .HasForeignKey(d => d.RequisitionUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblRequisitionMultiCampusLink_tblRequisitions");
            });

            modelBuilder.Entity<TblRequisitions>(entity =>
            {
                entity.HasKey(e => e.RequisitionUid);

                entity.ToTable("tblRequisitions");

                entity.HasIndex(e => e.RequisitionId)
                    .HasName("IDX_RequisitionID");

                entity.HasIndex(e => new { e.CampusId, e.ReqStatus })
                    .HasName("_dta_index_tblRequisitions_8_1330975968__K10_K4");

                entity.HasIndex(e => new { e.RequisitionUid, e.ReqStatus, e.RequisitionId, e.CampusId, e.DateCreatedOrSent })
                    .HasName("_dta_index_tblRequisitions_8_1330975968__K13D_1_2_4_10");

                entity.Property(e => e.RequisitionUid).HasColumnName("RequisitionUID");

                entity.Property(e => e.Approved).HasDefaultValueSql("((0))");

                entity.Property(e => e.CampusId)
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CampusReq).HasDefaultValueSql("((0))");

                entity.Property(e => e.DateCreatedOrSent).HasColumnType("datetime");

                entity.Property(e => e.DateReqApproved).HasColumnType("datetime");

                entity.Property(e => e.DateSubmittedByCampus).HasColumnType("datetime");

                entity.Property(e => e.DistrictCreatedCampusReq).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.ReqStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RequisitionId)
                    .IsRequired()
                    .HasColumnName("RequisitionID")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.RetrievedFromDistrict)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.VerifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VerifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblSettings>(entity =>
            {
                entity.HasKey(e => e.DistrictId);

                entity.ToTable("tblSettings");

                entity.Property(e => e.DistrictId)
                    .HasColumnName("DistrictID")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.BillingAddress1)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BillingAddress2)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BillingCity)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BillingContact)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BillingFax)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BillingNotes)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.BillingPhone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BillingState)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.BillingTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BillingZip)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DistrictName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ShippingAddress1)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ShippingAddress2)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ShippingCity)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ShippingContact)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ShippingFax)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShippingNotes)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ShippingPhone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShippingState)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ShippingTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShippingZip)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblStates>(entity =>
            {
                entity.HasKey(e => e.StateId);

                entity.ToTable("tblStates");

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.Property(e => e.StateAbbr)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.StateDesc)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("tblStatus");

                entity.Property(e => e.StatusId)
                    .HasColumnName("statusID")
                    .ValueGeneratedNever();

                entity.Property(e => e.StatusCharacter)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StatusDesc)
                    .IsRequired()
                    .HasColumnName("statusDesc")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StatusDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.StatusTypeUid).HasColumnName("StatusTypeUID");

                entity.HasOne(d => d.StatusTypeU)
                    .WithMany(p => p.TblStatus)
                    .HasForeignKey(d => d.StatusTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblStatus_tblStatusTypes");
            });

            modelBuilder.Entity<TblStatusTypes>(entity =>
            {
                entity.HasKey(e => e.StatusTypeUid);

                entity.ToTable("tblStatusTypes");

                entity.Property(e => e.StatusTypeUid).HasColumnName("StatusTypeUID");

                entity.Property(e => e.StatusType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblStudents>(entity =>
            {
                entity.HasKey(e => e.StudentsUid);

                entity.ToTable("tblStudents");

                entity.HasIndex(e => e.CampusId)
                    .HasName("CampusID_Index");

                entity.HasIndex(e => e.StudentId)
                    .HasName("StudentID_Index");

                entity.HasIndex(e => e.StudentSifguid)
                    .HasName("IDX_SIF_StudentSIFGuid");

                entity.HasIndex(e => new { e.Grade, e.StudentsUid })
                    .HasName("IX_tblStudents_StudentUID_iGrade");

                entity.HasIndex(e => new { e.StudentId, e.FullName })
                    .HasName("tblStudents_ID_FullName");

                entity.HasIndex(e => new { e.CampusId, e.StudentsUid, e.LastName, e.StudentId, e.FullName })
                    .HasName("_dta_index_tblStudents_8_1360880065__K2_K22_K5_K1_K6");

                entity.HasIndex(e => new { e.CampusId, e.StudentsUid, e.LastName, e.StudentId, e.FullName, e.Grade, e.HomeRoom, e.FirstName })
                    .HasName("_dta_index_tblStudents_8_1360880065__K2_K22_K5_K1_K6_K7_K8_K3");

                entity.HasIndex(e => new { e.Grade, e.FullName, e.HomeRoom, e.StudentId, e.CampusId, e.Status, e.StudentsUid, e.LastName })
                    .HasName("_dta_index_tblStudents_8_1360880065__K2_K21_K22_K5_1_6_7_8");

                entity.HasIndex(e => new { e.StudentsUid, e.CampusId, e.LastName, e.StudentId, e.FullName, e.Grade, e.HomeRoom, e.FirstName })
                    .HasName("_dta_index_tblStudents_8_1360880065__K2_K5_K1_K6_K7_K8_K3_22");

                entity.HasIndex(e => new { e.FirstName, e.FullName, e.Grade, e.HomeRoom, e.Gender, e.City, e.Address, e.Address2, e.ModifiedDate, e.LastName, e.Notes, e.MiddleName, e.StudentSifguid, e.Zip, e.UserId, e.State, e.ParentEmail, e.Phone, e.Race, e.StudentId, e.StudentsUid, e.CampusId })
                    .HasName("_dta_index_tblStudents_8_1360880065__K1_K22_K2_3_4_5_6_7_8_9_10_11_12_13_14_15_16_17_18_19_20_23");

                entity.HasIndex(e => new { e.HomeRoom, e.Address2, e.Address, e.City, e.Gender, e.FullName, e.StudentsUid, e.ModifiedDate, e.MiddleName, e.Notes, e.State, e.UserId, e.Zip, e.StudentSifguid, e.Race, e.Phone, e.ParentEmail, e.CampusId, e.StudentId, e.FirstName, e.LastName, e.Grade })
                    .HasName("_dta_index_tblStudents_8_1360880065__K2_K1_K3_K5_K7_4_6_8_9_10_11_12_13_14_15_16_17_18_19_20_22_23");

                entity.Property(e => e.StudentsUid).HasColumnName("StudentsUID");

                entity.Property(e => e.ActiveIt)
                    .HasColumnName("ActiveIT")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CampusId)
                    .IsRequired()
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Grade)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HomeRoom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.New)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.NotesIt)
                    .HasColumnName("NotesIT")
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.ParentEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Race)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.StudentEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasColumnName("StudentID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StudentSifguid).HasColumnName("StudentSIFGuid");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Zip)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblStudentsDistribution>(entity =>
            {
                entity.HasKey(e => e.DistributionId);

                entity.ToTable("tblStudentsDistribution");

                entity.HasIndex(e => e.Accession)
                    .HasName("tblStudentsDistribution_Accession");

                entity.HasIndex(e => e.Code)
                    .HasName("Code_Index");

                entity.HasIndex(e => e.Isbn)
                    .HasName("IDX_ISBN");

                entity.HasIndex(e => e.StudentsUid)
                    .HasName("tblStudentsDistribution_ID");

                entity.HasIndex(e => new { e.Code, e.Reconciled, e.Isbn, e.DistributionId, e.StudentsUid })
                    .HasName("_dta_index_tblStudentsDistribution_8_1216879552__K5_K13_K3_K1_K2");

                entity.HasIndex(e => new { e.Code, e.StudentsUid, e.Isbn, e.Accession, e.ModifiedDate })
                    .HasName("_dta_index_tblStudentsDistribution_8_1216879552__K5_K2_K3_K4_K12");

                entity.HasIndex(e => new { e.Isbn, e.StudentsUid, e.Accession, e.Code, e.ModifiedDate })
                    .HasName("_dta_index_tblStudentsDistribution_8_1216879552__K3_K2_K4_K5_K12");

                entity.HasIndex(e => new { e.Copies, e.Reconciled, e.Code, e.DistributionId, e.Isbn, e.StudentsUid })
                    .HasName("_dta_index_tblStudentsDistribution_8_1216879552__K13_K5_K1_K3_K2_9");

                entity.HasIndex(e => new { e.ModifiedDate, e.Reconciled, e.Accession, e.DistributionId, e.Copies, e.Isbn, e.Code, e.StudentsUid })
                    .HasName("_dta_index_tblStudentsDistribution_8_1216879552__K3_K5_K2_1_4_9_12_13");

                entity.Property(e => e.DistributionId).HasColumnName("DistributionID");

                entity.Property(e => e.Accession)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SourceType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StudentsUid).HasColumnName("StudentsUID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TblStudentsDistributionTx>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.ToTable("tblStudentsDistribution_tx");

                entity.HasIndex(e => e.DistributionId)
                    .HasName("IDX_StudentsDistribution_ForClosing");

                entity.HasIndex(e => new { e.Code, e.TransactionId, e.StudentsUid, e.Isbn, e.Accession, e.ModifiedDate })
                    .HasName("_dta_index_tblStudentsDistribution_tx_8_772353966__K3_K4_K5_K13_1_6");

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.Accession)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ActionTaken)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DistributionId).HasColumnName("DistributionID");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SourceType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StudentsUid).HasColumnName("StudentsUID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Distribution)
                    .WithMany(p => p.TblStudentsDistributionTx)
                    .HasForeignKey(d => d.DistributionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tblStudentsDistribution_tx_tblStudentsDistribution");
            });

            modelBuilder.Entity<TblStudentsSchedules>(entity =>
            {
                entity.HasKey(e => e.StudentsSchedulesUid);

                entity.ToTable("tblStudentsSchedules");

                entity.HasIndex(e => e.EnrollmentSifguid)
                    .HasName("IDX_SIF_EnrollmentSIFGuid");

                entity.HasIndex(e => e.MasterCourseUid)
                    .HasName("IDX_MasterCourseUID");

                entity.HasIndex(e => e.StudentsUid)
                    .HasName("IDX_StudentsUID");

                entity.HasIndex(e => new { e.SectionId, e.MasterCourseUid, e.StudentsUid })
                    .HasName("_dta_index_tblStudentsSchedules_8_644353510__K5_K3_K2");

                entity.HasIndex(e => new { e.StudentsUid, e.MasterCourseUid, e.SectionId })
                    .HasName("_dta_index_tblStudentsSchedules_8_644353510__K2_K3_K5");

                entity.Property(e => e.StudentsSchedulesUid).HasColumnName("StudentsSchedulesUID");

                entity.Property(e => e.EnrollmentSifguid).HasColumnName("EnrollmentSIFGuid");

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.ExitDate).HasColumnType("datetime");

                entity.Property(e => e.MasterCourseUid).HasColumnName("MasterCourseUID");

                entity.Property(e => e.Period)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SectionId)
                    .HasColumnName("SectionID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StudentsUid).HasColumnName("StudentsUID");

                entity.HasOne(d => d.MasterCourseU)
                    .WithMany(p => p.TblStudentsSchedules)
                    .HasForeignKey(d => d.MasterCourseUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblStudentsSchedules_tblMasterCourse");

                entity.HasOne(d => d.StudentsU)
                    .WithMany(p => p.TblStudentsSchedules)
                    .HasForeignKey(d => d.StudentsUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblStudentsSchedules_tblStudents");
            });

            modelBuilder.Entity<TblSubjectArea>(entity =>
            {
                entity.HasKey(e => e.SubjectAreaUid);

                entity.ToTable("tblSubjectArea");

                entity.Property(e => e.SubjectAreaUid).HasColumnName("SubjectAreaUID");

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<TblSubjectAreaBookLink>(entity =>
            {
                entity.HasKey(e => e.SubjectAreaBookUid);

                entity.ToTable("tblSubjectAreaBookLINK");

                entity.Property(e => e.SubjectAreaBookUid).HasColumnName("SubjectAreaBookUID");

                entity.Property(e => e.BookInventoryUid).HasColumnName("BookInventoryUID");

                entity.Property(e => e.SubjectAreaUid).HasColumnName("SubjectAreaUID");
            });

            modelBuilder.Entity<TblSyncedInventory>(entity =>
            {
                entity.HasKey(e => e.FileId);

                entity.ToTable("tblSyncedInventory");

                entity.Property(e => e.FileId).HasColumnName("FileID");

                entity.Property(e => e.CampusId)
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.FileName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FileType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTeacherInformation>(entity =>
            {
                entity.HasKey(e => e.TeacherInformationUid);

                entity.ToTable("tblTeacherInformation");

                entity.HasIndex(e => e.TeacherId)
                    .HasName("UQ_tblTeacherID")
                    .IsUnique();

                entity.Property(e => e.TeacherInformationUid).HasColumnName("TeacherInformationUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentCampusId)
                    .IsRequired()
                    .HasColumnName("CurrentCampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TeacherId)
                    .IsRequired()
                    .HasColumnName("TeacherID")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTeachers>(entity =>
            {
                entity.HasKey(e => e.TeachersUid);

                entity.ToTable("tblTeachers");

                entity.HasIndex(e => e.CampusId)
                    .HasName("CampusID_Index");

                entity.HasIndex(e => new { e.TeacherId, e.CampusId })
                    .HasName("TeacherID_Index");

                entity.HasIndex(e => new { e.StaffTypeUid, e.TeachersUid, e.Grade })
                    .HasName("IX_tblTeachers_StaffTypeUID");

                entity.HasIndex(e => new { e.FullName, e.TeacherId, e.CampusId, e.LastName, e.Status, e.TeachersUid })
                    .HasName("_dta_index_tblTeachers_8_740353852__K2_K5_K23_K22_1_6");

                entity.Property(e => e.TeachersUid).HasColumnName("TeachersUID");

                entity.Property(e => e.ActiveIt)
                    .HasColumnName("ActiveIT")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CampusId)
                    .IsRequired()
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Grade)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HomeRoom)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.NotesIt)
                    .HasColumnName("NotesIT")
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Race)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StaffTypeUid).HasColumnName("StaffTypeUID");

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.TeacherId)
                    .IsRequired()
                    .HasColumnName("TeacherID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TeacherSifguid).HasColumnName("TeacherSIFGuid");

                entity.Property(e => e.TeacherStatusUid).HasColumnName("TeacherStatusUID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Zip)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.StaffTypeU)
                    .WithMany(p => p.TblTeachers)
                    .HasForeignKey(d => d.StaffTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTeachers_tblUnvStaffTypes");

                entity.HasOne(d => d.TeacherStatusU)
                    .WithMany(p => p.TblTeachers)
                    .HasForeignKey(d => d.TeacherStatusUid)
                    .HasConstraintName("FK_tblTeachers_tblTeacherStatus");
            });

            modelBuilder.Entity<TblTeachersDistribution>(entity =>
            {
                entity.HasKey(e => e.DistributionId);

                entity.ToTable("tblTeachersDistribution");

                entity.HasIndex(e => e.Code)
                    .HasName("Code_Index");

                entity.HasIndex(e => e.Isbn)
                    .HasName("IDX_ISBN");

                entity.HasIndex(e => e.TeachersUid)
                    .HasName("IDX_TeachersUID");

                entity.HasIndex(e => new { e.Accession, e.Code, e.TeachersUid })
                    .HasName("_dta_index_tblTeachersDistribution_8_624877443__K4_K5_K2");

                entity.HasIndex(e => new { e.TeachersUid, e.DistributionId, e.Isbn })
                    .HasName("_dta_index_tblTeachersDistribution_8_624877443__K2_K1_K3");

                entity.HasIndex(e => new { e.Copies, e.Isbn, e.Code, e.TeachersUid })
                    .HasName("_dta_index_tblTeachersDistribution_8_624877443__K3_K5_K2_9");

                entity.HasIndex(e => new { e.ModifiedDate, e.Accession, e.Copies, e.Code, e.Reconciled, e.Isbn, e.DistributionId, e.TeachersUid })
                    .HasName("_dta_index_tblTeachersDistribution_8_624877443__K5_K13_K3_K1_K2_4_9_12");

                entity.Property(e => e.DistributionId).HasColumnName("DistributionID");

                entity.Property(e => e.Accession)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SourceType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TeachersUid).HasColumnName("TeachersUID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TblTeachersDistributionTx>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.ToTable("tblTeachersDistribution_tx");

                entity.HasIndex(e => e.DistributionId)
                    .HasName("IDX_TeachersDistribution_ForClosing");

                entity.HasIndex(e => new { e.Code, e.TeachersUid, e.Isbn, e.Source, e.SourceType })
                    .HasName("_dta_index_tblTeachersDistribution_tx_8_1300355847__K6_K3_K4_K8_K7");

                entity.HasIndex(e => new { e.ModifiedDate, e.Copies, e.TeachersUid, e.Isbn, e.Code, e.Source })
                    .HasName("_dta_index_tblTeachersDistribution_tx_8_1300355847__K3_K4_K6_K8_10_13");

                entity.HasIndex(e => new { e.SourceType, e.Copies, e.ModifiedDate, e.Source, e.Isbn, e.Code, e.TeachersUid })
                    .HasName("_dta_index_tblTeachersDistribution_tx_8_1300355847__K8_K4_K6_K3_7_10_13");

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.Accession)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ActionTaken)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DistributionId).HasColumnName("DistributionID");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SourceType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TeachersUid).HasColumnName("TeachersUID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Distribution)
                    .WithMany(p => p.TblTeachersDistributionTx)
                    .HasForeignKey(d => d.DistributionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tblTeachersDistribution_tx_tblTeachersDistribution");
            });

            modelBuilder.Entity<TblTeachersSchedules>(entity =>
            {
                entity.HasKey(e => e.TeachersSchedulesUid);

                entity.ToTable("tblTeachersSchedules");

                entity.HasIndex(e => new { e.Period, e.TeachersUid, e.SectionId, e.MasterCourseUid })
                    .HasName("_dta_index_tblTeachersSchedules_8_612353396__K2_K4_K3_5");

                entity.Property(e => e.TeachersSchedulesUid).HasColumnName("TeachersSchedulesUID");

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.ExitDate).HasColumnType("datetime");

                entity.Property(e => e.MasterCourseUid).HasColumnName("MasterCourseUID");

                entity.Property(e => e.Period)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SectionId)
                    .HasColumnName("SectionID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SectionSifguid).HasColumnName("SectionSIFGuid");

                entity.Property(e => e.TeachersUid).HasColumnName("TeachersUID");

                entity.HasOne(d => d.MasterCourseU)
                    .WithMany(p => p.TblTeachersSchedules)
                    .HasForeignKey(d => d.MasterCourseUid)
                    .HasConstraintName("FK_tblTeachersSchedules_tblMasterCourse");

                entity.HasOne(d => d.TeachersU)
                    .WithMany(p => p.TblTeachersSchedules)
                    .HasForeignKey(d => d.TeachersUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTeachersSchedules_tblTeachers");
            });

            modelBuilder.Entity<TblTeacherStatus>(entity =>
            {
                entity.HasKey(e => e.TeacherStatusUid);

                entity.ToTable("tblTeacherStatus");

                entity.HasIndex(e => e.TeacherStatus)
                    .HasName("UQ_tblTeacherStatus")
                    .IsUnique();

                entity.HasIndex(e => e.TeacherStatusId)
                    .HasName("UQ_tblTeacherStatusID")
                    .IsUnique();

                entity.Property(e => e.TeacherStatusUid).HasColumnName("TeacherStatusUID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TeacherStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TeacherStatusId)
                    .IsRequired()
                    .HasColumnName("TeacherStatusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTechAccessories>(entity =>
            {
                entity.HasKey(e => e.AccessoryUid);

                entity.ToTable("tblTechAccessories");

                entity.Property(e => e.AccessoryUid).HasColumnName("AccessoryUID");

                entity.Property(e => e.AccessoryDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.AccessoryName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AccessoryPrice).HasColumnType("money");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblTechAccessoryCharges>(entity =>
            {
                entity.HasKey(e => e.AccessoryChargeUid);

                entity.ToTable("tblTechAccessoryCharges");

                entity.Property(e => e.AccessoryChargeUid).HasColumnName("AccessoryChargeUID");

                entity.Property(e => e.AccessoryUid).HasColumnName("AccessoryUID");

                entity.Property(e => e.ChargeUid).HasColumnName("ChargeUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.InventoryHistoryUid).HasColumnName("InventoryHistoryUID");

                entity.HasOne(d => d.AccessoryU)
                    .WithMany(p => p.TblTechAccessoryCharges)
                    .HasForeignKey(d => d.AccessoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechAccessoryCharges_tblTechAccessories");

                entity.HasOne(d => d.ChargeU)
                    .WithMany(p => p.TblTechAccessoryCharges)
                    .HasForeignKey(d => d.ChargeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechAccessoryCharges_tblUnvCharges");

                entity.HasOne(d => d.InventoryHistoryU)
                    .WithMany(p => p.TblTechAccessoryCharges)
                    .HasForeignKey(d => d.InventoryHistoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechAccessoryCharges_tblTechInventoryHistory");
            });

            modelBuilder.Entity<TblTechActions>(entity =>
            {
                entity.HasKey(e => e.ActionUid);

                entity.ToTable("tblTechActions");

                entity.Property(e => e.ActionUid).HasColumnName("ActionUID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTechAssetConditions>(entity =>
            {
                entity.HasKey(e => e.AssetConditionUid);

                entity.ToTable("tblTechAssetConditions");

                entity.Property(e => e.AssetConditionUid).HasColumnName("AssetConditionUID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTechAttachments>(entity =>
            {
                entity.HasKey(e => e.AttachmentUid);

                entity.ToTable("tblTechAttachments");

                entity.Property(e => e.AttachmentUid).HasColumnName("AttachmentUID");

                entity.Property(e => e.ApplicationArea).HasMaxLength(100);

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FileName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FilePath)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UploadedFileName)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTechAttachmentScheduleLink>(entity =>
            {
                entity.HasKey(e => e.AttachmentScheduleLinkUid);

                entity.ToTable("tblTechAttachmentScheduleLink");

                entity.Property(e => e.AttachmentScheduleLinkUid).HasColumnName("AttachmentScheduleLinkUID");

                entity.Property(e => e.AttachmentUid).HasColumnName("AttachmentUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ScheduleUid).HasColumnName("ScheduleUID");

                entity.HasOne(d => d.AttachmentU)
                    .WithMany(p => p.TblTechAttachmentScheduleLink)
                    .HasForeignKey(d => d.AttachmentUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechAttachmentScheduleLink_tblTechAttachments");

                entity.HasOne(d => d.ScheduleU)
                    .WithMany(p => p.TblTechAttachmentScheduleLink)
                    .HasForeignKey(d => d.ScheduleUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechAttachmentScheduleLink_tblUnvSchedule");
            });

            modelBuilder.Entity<TblTechAuditDepartments>(entity =>
            {
                entity.HasKey(e => e.AuditDepartmentUid);

                entity.ToTable("tblTechAuditDepartments");

                entity.HasIndex(e => new { e.AuditUid, e.TechDepartmentUid });

                entity.Property(e => e.AuditDepartmentUid).HasColumnName("AuditDepartmentUID");

                entity.Property(e => e.AuditUid).HasColumnName("AuditUID");

                entity.Property(e => e.TechDepartmentUid).HasColumnName("TechDepartmentUID");

                entity.HasOne(d => d.AuditU)
                    .WithMany(p => p.TblTechAuditDepartments)
                    .HasForeignKey(d => d.AuditUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechAuditsDepartments_tblUnvAudits");

                entity.HasOne(d => d.TechDepartmentU)
                    .WithMany(p => p.TblTechAuditDepartments)
                    .HasForeignKey(d => d.TechDepartmentUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechAuditsDepartments_tblTechDepartments");
            });

            modelBuilder.Entity<TblTechAuditDetailInventoryCounts>(entity =>
            {
                entity.HasKey(e => e.AuditDetailInventoryCountUid);

                entity.ToTable("tblTechAuditDetailInventoryCounts");

                entity.HasIndex(e => e.InventoryUid)
                    .HasName("IX_InventoryUID");

                entity.HasIndex(e => e.LastModifiedByUserId)
                    .HasName("IX_LastModifiedByUserID");

                entity.HasIndex(e => e.LastModifiedDate)
                    .HasName("IX_LastModifiedDate");

                entity.HasIndex(e => e.StatusId)
                    .HasName("IX_AuditInvStatusID");

                entity.HasIndex(e => new { e.AuditDetailInventoryCountUid, e.LastModifiedDate })
                    .HasName("IX_AuditDetailInvCntUID_LModDate");

                entity.HasIndex(e => new { e.AuditDetailUid, e.StatusId })
                    .HasName("IX_AuditInvCounts");

                entity.HasIndex(e => new { e.AuditDetailUid, e.ItemUid, e.InventoryUid, e.StatusId })
                    .HasName("IX_AuditDetailItemsInventoryStatus");

                entity.Property(e => e.AuditDetailInventoryCountUid).HasColumnName("AuditDetailInventoryCountUID");

                entity.Property(e => e.ActionUid).HasColumnName("ActionUID");

                entity.Property(e => e.AuditDetailUid).HasColumnName("AuditDetailUID");

                entity.Property(e => e.AuditEntityTypeUid).HasColumnName("AuditEntityTypeUID");

                entity.Property(e => e.AuditEntityUid).HasColumnName("AuditEntityUID");

                entity.Property(e => e.AuditSiteUid).HasColumnName("AuditSiteUID");

                entity.Property(e => e.CreatedByEntityTypeUid).HasColumnName("CreatedByEntityTypeUID");

                entity.Property(e => e.CreatedByEntityUid).HasColumnName("CreatedByEntityUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FoundReferenceAuditDetailInventoryUid).HasColumnName("FoundReferenceAuditDetailInventoryUID");

                entity.Property(e => e.InventoryUid).HasColumnName("InventoryUID");

                entity.Property(e => e.ItemUid).HasColumnName("ItemUID");

                entity.Property(e => e.LastModifiedByEntityTypeUid).HasColumnName("LastModifiedByEntityTypeUID");

                entity.Property(e => e.LastModifiedByEntityUid).HasColumnName("LastModifiedByEntityUID");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalStatusId).HasColumnName("OriginalStatusID");

                entity.Property(e => e.ScanByEntityTypeUid).HasColumnName("ScanByEntityTypeUID");

                entity.Property(e => e.ScanByEntityUid).HasColumnName("ScanByEntityUID");

                entity.Property(e => e.ScanDate).HasColumnType("datetime");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.ActionU)
                    .WithMany(p => p.TblTechAuditDetailInventoryCounts)
                    .HasForeignKey(d => d.ActionUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechAuditDetailInventoryCounts_tblTechActions");

                entity.HasOne(d => d.AuditDetailU)
                    .WithMany(p => p.TblTechAuditDetailInventoryCounts)
                    .HasForeignKey(d => d.AuditDetailUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechAuditDetailInventoryCounts_tblTechAuditDetails");

                entity.HasOne(d => d.AuditSiteU)
                    .WithMany(p => p.TblTechAuditDetailInventoryCounts)
                    .HasForeignKey(d => d.AuditSiteUid)
                    .HasConstraintName("FK_tblTechAuditDetailInventoryCounts_tblTechSites");

                entity.HasOne(d => d.FoundReferenceAuditDetailInventoryU)
                    .WithMany(p => p.InverseFoundReferenceAuditDetailInventoryU)
                    .HasForeignKey(d => d.FoundReferenceAuditDetailInventoryUid)
                    .HasConstraintName("FK_tblTechAuditDetailInventoryCounts_tblTechAuditDetailInventoryCounts");

                entity.HasOne(d => d.InventoryU)
                    .WithMany(p => p.TblTechAuditDetailInventoryCounts)
                    .HasForeignKey(d => d.InventoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechAuditDetailInventoryCounts_tblTechInventory");

                entity.HasOne(d => d.ItemU)
                    .WithMany(p => p.TblTechAuditDetailInventoryCounts)
                    .HasForeignKey(d => d.ItemUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechAuditDetailInventoryCounts_tblTechItems");

                entity.HasOne(d => d.OriginalStatus)
                    .WithMany(p => p.TblTechAuditDetailInventoryCountsOriginalStatus)
                    .HasForeignKey(d => d.OriginalStatusId)
                    .HasConstraintName("FK_OriginalStatus_tblTechAuditDetailInventoryCounts_tblStatus");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TblTechAuditDetailInventoryCountsStatus)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechAuditDetailInventoryCounts_tblStatus");
            });

            modelBuilder.Entity<TblTechAuditDetails>(entity =>
            {
                entity.HasKey(e => e.AuditDetailUid);

                entity.ToTable("tblTechAuditDetails");

                entity.HasIndex(e => new { e.EntityTypeUid, e.EntityUid, e.StatusId, e.AuditUid })
                    .HasName("IX_AuditDetails");

                entity.Property(e => e.AuditDetailUid).HasColumnName("AuditDetailUID");

                entity.Property(e => e.AuditDetailNotes)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.AuditUid).HasColumnName("AuditUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.EntityTypeUid).HasColumnName("EntityTypeUID");

                entity.Property(e => e.EntityUid).HasColumnName("EntityUID");

                entity.Property(e => e.FinalizedByEntityTypeUid).HasColumnName("FinalizedByEntityTypeUID");

                entity.Property(e => e.FinalizedByEntityUid).HasColumnName("FinalizedByEntityUID");

                entity.Property(e => e.FinalizedByUserId).HasColumnName("FinalizedByUserID");

                entity.Property(e => e.FinalizedDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedByEntityTypeUid).HasColumnName("LastModifiedByEntityTypeUID");

                entity.Property(e => e.LastModifiedByEntityUid).HasColumnName("LastModifiedByEntityUID");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.SiteUid).HasColumnName("SiteUID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.AuditU)
                    .WithMany(p => p.TblTechAuditDetails)
                    .HasForeignKey(d => d.AuditUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechAuditDetails_tblUnvAudits");

                entity.HasOne(d => d.SiteU)
                    .WithMany(p => p.TblTechAuditDetails)
                    .HasForeignKey(d => d.SiteUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechAuditDetails_tblTechSites");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TblTechAuditDetails)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechAuditDetails_tblStatus");
            });

            modelBuilder.Entity<TblTechAuditEntityTypes>(entity =>
            {
                entity.HasKey(e => e.AuditEntityTypeUid);

                entity.ToTable("tblTechAuditEntityTypes");

                entity.Property(e => e.AuditEntityTypeUid).HasColumnName("AuditEntityTypeUID");

                entity.Property(e => e.AuditUid).HasColumnName("AuditUID");

                entity.Property(e => e.EntityTypeUid).HasColumnName("EntityTypeUID");

                entity.HasOne(d => d.AuditU)
                    .WithMany(p => p.TblTechAuditEntityTypes)
                    .HasForeignKey(d => d.AuditUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechAuditEntityTypes_tblUnvAudits");

                entity.HasOne(d => d.EntityTypeU)
                    .WithMany(p => p.TblTechAuditEntityTypes)
                    .HasForeignKey(d => d.EntityTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechAuditEntityTypes_tblUnvEntityTypes");
            });

            modelBuilder.Entity<TblTechAuditGrades>(entity =>
            {
                entity.HasKey(e => e.AuditGradesUid);

                entity.ToTable("tblTechAuditGrades");

                entity.Property(e => e.AuditGradesUid).HasColumnName("AuditGradesUID");

                entity.Property(e => e.AuditUid).HasColumnName("AuditUID");

                entity.Property(e => e.GradeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.AuditU)
                    .WithMany(p => p.TblTechAuditGrades)
                    .HasForeignKey(d => d.AuditUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechAuditGrades_tblUnvAudits");
            });

            modelBuilder.Entity<TblTechAuditItemTypes>(entity =>
            {
                entity.HasKey(e => e.AuditItemTypeUid);

                entity.ToTable("tblTechAuditItemTypes");

                entity.Property(e => e.AuditItemTypeUid).HasColumnName("AuditItemTypeUID");

                entity.Property(e => e.AuditUid).HasColumnName("AuditUID");

                entity.Property(e => e.ItemTypeUid).HasColumnName("ItemTypeUID");

                entity.HasOne(d => d.AuditU)
                    .WithMany(p => p.TblTechAuditItemTypes)
                    .HasForeignKey(d => d.AuditUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechAuditItemTypes_tblUnvAudits");

                entity.HasOne(d => d.ItemTypeU)
                    .WithMany(p => p.TblTechAuditItemTypes)
                    .HasForeignKey(d => d.ItemTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechAuditItemTypes_tblTechItemTypes");
            });

            modelBuilder.Entity<TblTechAuditRoomTypes>(entity =>
            {
                entity.HasKey(e => e.AuditRoomTypeUid);

                entity.ToTable("tblTechAuditRoomTypes");

                entity.Property(e => e.AuditRoomTypeUid).HasColumnName("AuditRoomTypeUID");

                entity.Property(e => e.AuditUid).HasColumnName("AuditUID");

                entity.Property(e => e.RoomTypeUid).HasColumnName("RoomTypeUID");

                entity.HasOne(d => d.AuditU)
                    .WithMany(p => p.TblTechAuditRoomTypes)
                    .HasForeignKey(d => d.AuditUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechAuditRoomTypes_tblUnvAudits");

                entity.HasOne(d => d.RoomTypeU)
                    .WithMany(p => p.TblTechAuditRoomTypes)
                    .HasForeignKey(d => d.RoomTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechAuditRoomTypes_tblUnvRoomTypes");
            });

            modelBuilder.Entity<TblTechAuditStaffTypes>(entity =>
            {
                entity.HasKey(e => e.AuditStaffTypesUid);

                entity.ToTable("tblTechAuditStaffTypes");

                entity.Property(e => e.AuditStaffTypesUid).HasColumnName("AuditStaffTypesUID");

                entity.Property(e => e.AuditUid).HasColumnName("AuditUID");

                entity.Property(e => e.StaffTypeUid).HasColumnName("StaffTypeUID");

                entity.HasOne(d => d.AuditU)
                    .WithMany(p => p.TblTechAuditStaffTypes)
                    .HasForeignKey(d => d.AuditUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechAuditStaffTypes_tblUnvAudits");

                entity.HasOne(d => d.StaffTypeU)
                    .WithMany(p => p.TblTechAuditStaffTypes)
                    .HasForeignKey(d => d.StaffTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechAuditStaffTypes_tblUnvStaffTypes");
            });

            modelBuilder.Entity<TblTechBulkEdit>(entity =>
            {
                entity.HasKey(e => e.BulkEditUid);

                entity.ToTable("tblTechBulkEdit");

                entity.Property(e => e.BulkEditUid).HasColumnName("BulkEditUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Field)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTechContainers>(entity =>
            {
                entity.HasKey(e => e.ContainerUid);

                entity.ToTable("tblTechContainers");

                entity.Property(e => e.ContainerUid).HasColumnName("ContainerUID");

                entity.Property(e => e.ContainerDescription)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ContainerNotes)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ContainerNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContainerTypeUid).HasColumnName("ContainerTypeUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EntityTypeUid).HasColumnName("EntityTypeUID");

                entity.Property(e => e.EntityUid).HasColumnName("EntityUID");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SiteUid).HasColumnName("SiteUID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.ContainerTypeU)
                    .WithMany(p => p.TblTechContainers)
                    .HasForeignKey(d => d.ContainerTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechContainer_tblTechContainerTypes");

                entity.HasOne(d => d.EntityTypeU)
                    .WithMany(p => p.TblTechContainers)
                    .HasForeignKey(d => d.EntityTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechContainer_tblUnvEntityTypes");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TblTechContainers)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechContainer_tblStatus");
            });

            modelBuilder.Entity<TblTechContainerTypes>(entity =>
            {
                entity.HasKey(e => e.ContainerTypeUid);

                entity.ToTable("tblTechContainerTypes");

                entity.Property(e => e.ContainerTypeUid).HasColumnName("ContainerTypeUID");

                entity.Property(e => e.ContainerTypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblTechDepartments>(entity =>
            {
                entity.HasKey(e => e.TechDepartmentUid);

                entity.ToTable("tblTechDepartments");

                entity.Property(e => e.TechDepartmentUid).HasColumnName("TechDepartmentUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("DepartmentID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PrimaryContactUserId).HasColumnName("PrimaryContactUserID");

                entity.Property(e => e.TransferCompleteNotificationRecipientUid).HasColumnName("TransferCompleteNotificationRecipientUID");

                entity.HasOne(d => d.PrimaryContactUser)
                    .WithMany(p => p.TblTechDepartments)
                    .HasForeignKey(d => d.PrimaryContactUserId)
                    .HasConstraintName("FK_tblTechDepartments_tblUser");

                entity.HasOne(d => d.TransferCompleteNotificationRecipientU)
                    .WithMany(p => p.TblTechDepartments)
                    .HasForeignKey(d => d.TransferCompleteNotificationRecipientUid)
                    .HasConstraintName("FK_tblTechDepartments_tblUnvRecipient");
            });

            modelBuilder.Entity<TblTechFundingSourceUsers>(entity =>
            {
                entity.HasKey(e => e.FundingSourceUserUid);

                entity.ToTable("tblTechFundingSourceUsers");

                entity.Property(e => e.FundingSourceUserUid).HasColumnName("FundingSourceUserUID");

                entity.Property(e => e.ApplicationUid).HasColumnName("ApplicationUID");

                entity.Property(e => e.FundingSourceUid).HasColumnName("FundingSourceUID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.ApplicationU)
                    .WithMany(p => p.TblTechFundingSourceUsers)
                    .HasForeignKey(d => d.ApplicationUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechFundingSourceUsers_tblUnvApplications");

                entity.HasOne(d => d.FundingSourceU)
                    .WithMany(p => p.TblTechFundingSourceUsers)
                    .HasForeignKey(d => d.FundingSourceUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechFundingSourceUsers_tblFundingSources");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblTechFundingSourceUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechFundingSourceUsers_tblUser");
            });

            modelBuilder.Entity<TblTechImages>(entity =>
            {
                entity.HasKey(e => e.ImageUid);

                entity.ToTable("tblTechImages");

                entity.HasIndex(e => e.ImageFileName)
                    .HasName("UQ_tblTechImages_ImageFileName")
                    .IsUnique();

                entity.HasIndex(e => e.ImageName)
                    .HasName("UQ_tblTechImages_ImageName")
                    .IsUnique();

                entity.Property(e => e.ImageUid).HasColumnName("ImageUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ImageContent).IsRequired();

                entity.Property(e => e.ImageDescription)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ImageFileName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ImageName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblTechImports>(entity =>
            {
                entity.HasKey(e => e.ImportUid);

                entity.ToTable("tblTechImports");

                entity.Property(e => e.ImportUid).HasColumnName("ImportUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ImportFileName)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.ImportName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ImportTypeUid)
                    .HasColumnName("ImportTypeUID")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ImportTypeU)
                    .WithMany(p => p.TblTechImports)
                    .HasForeignKey(d => d.ImportTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechImports_tblUnvImportTypes");
            });

            modelBuilder.Entity<TblTechInventory>(entity =>
            {
                entity.HasKey(e => e.InventoryUid);

                entity.ToTable("tblTechInventory");

                entity.HasIndex(e => e.ParentInventoryUid);

                entity.HasIndex(e => e.Serial)
                    .HasName("IX_tblTechInventorySerial");

                entity.HasIndex(e => e.Tag)
                    .HasName("UQ_tblTechInventoryTag")
                    .IsUnique();

                entity.HasIndex(e => new { e.ArchiveUid, e.ItemUid })
                    .HasName("_dta_index_tblTechInventory_8_1122259203__K20_K3");

                entity.HasIndex(e => new { e.ItemUid, e.InventoryUid });

                entity.HasIndex(e => new { e.EntityUid, e.EntityTypeUid, e.InventoryUid })
                    .HasName("IX_tblTechInventory_EUID_ETUID_IUID");

                entity.HasIndex(e => new { e.Serial, e.PurchasePrice, e.PurchaseDate, e.ExpirationDate, e.InventoryNotes, e.TechDepartmentUid, e.ArchiveUid, e.StatusUid, e.InventoryUid, e.EntityTypeUid, e.InventoryTypeUid, e.SiteUid, e.FundingSourceUid, e.ItemUid, e.EntityUid, e.Tag })
                    .HasName("_dta_index_tblTechInventory_8_1122259203__K8_K20_K7_K1_K6_K2_K4_K11_K3_K5_K9_10_12_13_14_15");

                entity.Property(e => e.InventoryUid).HasColumnName("InventoryUID");

                entity.Property(e => e.ArchiveUid).HasColumnName("ArchiveUID");

                entity.Property(e => e.AssetId)
                    .HasColumnName("AssetID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ContainerUid).HasColumnName("ContainerUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EntityTypeUid).HasColumnName("EntityTypeUID");

                entity.Property(e => e.EntityUid).HasColumnName("EntityUID");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.FundingSourceUid).HasColumnName("FundingSourceUID");

                entity.Property(e => e.InventoryNotes)
                    .HasMaxLength(3000)
                    .IsUnicode(false);

                entity.Property(e => e.InventorySourceUid).HasColumnName("InventorySourceUID");

                entity.Property(e => e.InventoryTypeUid).HasColumnName("InventoryTypeUID");

                entity.Property(e => e.ItemUid).HasColumnName("ItemUID");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ParentInventoryUid).HasColumnName("ParentInventoryUID");

                entity.Property(e => e.PurchaseDate).HasColumnType("datetime");

                entity.Property(e => e.PurchasePrice).HasColumnType("money");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SiteUid).HasColumnName("SiteUID");

                entity.Property(e => e.StatusUid).HasColumnName("StatusUID");

                entity.Property(e => e.Tag)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TechDepartmentUid).HasColumnName("TechDepartmentUID");

                entity.HasOne(d => d.ArchiveU)
                    .WithMany(p => p.TblTechInventory)
                    .HasForeignKey(d => d.ArchiveUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventory_tblUnvArchives");

                entity.HasOne(d => d.ContainerU)
                    .WithMany(p => p.TblTechInventory)
                    .HasForeignKey(d => d.ContainerUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventory_tblTechContainer");

                entity.HasOne(d => d.EntityTypeU)
                    .WithMany(p => p.TblTechInventory)
                    .HasForeignKey(d => d.EntityTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventory_tblUnvEntityTypes");

                entity.HasOne(d => d.FundingSourceU)
                    .WithMany(p => p.TblTechInventory)
                    .HasForeignKey(d => d.FundingSourceUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventory_tblFundingSources");

                entity.HasOne(d => d.InventorySourceU)
                    .WithMany(p => p.TblTechInventory)
                    .HasForeignKey(d => d.InventorySourceUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventory_tblTechInventorySource");

                entity.HasOne(d => d.InventoryTypeU)
                    .WithMany(p => p.TblTechInventory)
                    .HasForeignKey(d => d.InventoryTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventory_tblTechInventoryTypes");

                entity.HasOne(d => d.ItemU)
                    .WithMany(p => p.TblTechInventory)
                    .HasForeignKey(d => d.ItemUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventory_tblTechItems");

                entity.HasOne(d => d.SiteU)
                    .WithMany(p => p.TblTechInventory)
                    .HasForeignKey(d => d.SiteUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventory_tblTechSites");

                entity.HasOne(d => d.StatusU)
                    .WithMany(p => p.TblTechInventory)
                    .HasForeignKey(d => d.StatusUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventory_tblStatus");

                entity.HasOne(d => d.TechDepartmentU)
                    .WithMany(p => p.TblTechInventory)
                    .HasForeignKey(d => d.TechDepartmentUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventory_tblTechDepartments");
            });

            modelBuilder.Entity<TblTechInventoryAccessories>(entity =>
            {
                entity.HasKey(e => e.InventoryAccessoryUid);

                entity.ToTable("tblTechInventoryAccessories");

                entity.Property(e => e.InventoryAccessoryUid).HasColumnName("InventoryAccessoryUID");

                entity.Property(e => e.AccessoryUid).HasColumnName("AccessoryUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.InventoryHistoryUid).HasColumnName("InventoryHistoryUID");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.AccessoryU)
                    .WithMany(p => p.TblTechInventoryAccessories)
                    .HasForeignKey(d => d.AccessoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryAccessories_tblTechAccessories");

                entity.HasOne(d => d.InventoryHistoryU)
                    .WithMany(p => p.TblTechInventoryAccessories)
                    .HasForeignKey(d => d.InventoryHistoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryAccessories_tblTechInventoryHistory");
            });

            modelBuilder.Entity<TblTechInventoryDetails>(entity =>
            {
                entity.HasKey(e => e.InventoryDetailUid);

                entity.ToTable("tblTechInventoryDetails");

                entity.HasIndex(e => e.InventoryUid)
                    .HasName("UQ_tblTechInventoryDetails")
                    .IsUnique();

                entity.Property(e => e.InventoryDetailUid).HasColumnName("InventoryDetailUID");

                entity.Property(e => e.AssetConditionUid).HasColumnName("AssetConditionUID");

                entity.Property(e => e.Cfda)
                    .HasColumnName("CFDA")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.InventoryUid).HasColumnName("InventoryUID");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PoliceReportNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.AssetConditionU)
                    .WithMany(p => p.TblTechInventoryDetails)
                    .HasForeignKey(d => d.AssetConditionUid)
                    .HasConstraintName("FK_tblTechInventoryDetails_tblTechAssetConditions");

                entity.HasOne(d => d.InventoryU)
                    .WithOne(p => p.TblTechInventoryDetails)
                    .HasForeignKey<TblTechInventoryDetails>(d => d.InventoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryDetails_tblTechInventory");
            });

            modelBuilder.Entity<TblTechInventoryDetailsSettings>(entity =>
            {
                entity.HasKey(e => e.InventoryDetailSettingUid);

                entity.ToTable("tblTechInventoryDetailsSettings");

                entity.HasIndex(e => e.InventoryDetailField)
                    .HasName("UQ_tblTechInventoryDetailsSettings_InventoryDetailField")
                    .IsUnique();

                entity.Property(e => e.InventoryDetailSettingUid).HasColumnName("InventoryDetailSettingUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InventoryDetailField)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ShowField)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TblTechInventoryDueDates>(entity =>
            {
                entity.HasKey(e => e.InventoryDueDateUid);

                entity.ToTable("tblTechInventoryDueDates");

                entity.Property(e => e.InventoryDueDateUid).HasColumnName("InventoryDueDateUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DueDate).HasColumnType("date");

                entity.Property(e => e.InventoryHistoryUid).HasColumnName("InventoryHistoryUID");

                entity.Property(e => e.InventoryUid).HasColumnName("InventoryUID");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ReturnedDate).HasColumnType("date");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblTechInventoryDueDatesCreatedByUser)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryDueDates_tblUser1");

                entity.HasOne(d => d.InventoryHistoryU)
                    .WithMany(p => p.TblTechInventoryDueDates)
                    .HasForeignKey(d => d.InventoryHistoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryDueDates_tblTechInventoryHistory");

                entity.HasOne(d => d.InventoryU)
                    .WithMany(p => p.TblTechInventoryDueDates)
                    .HasForeignKey(d => d.InventoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryDueDates_tblTechInventory");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TblTechInventoryDueDatesLastModifiedByUser)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryDueDates_tblUser2");
            });

            modelBuilder.Entity<TblTechInventoryExt>(entity =>
            {
                entity.HasKey(e => e.InventoryExtUid);

                entity.ToTable("tblTechInventoryExt");

                entity.HasIndex(e => e.InventoryMetaUid)
                    .HasName("IX_tblTechInventoryMetaInventoryMetaUID");

                entity.HasIndex(e => new { e.InventoryExtUid, e.InventoryUid })
                    .HasName("IX_tblTechInventoryExt_InventoryUID");

                entity.HasIndex(e => new { e.InventoryUid, e.InventoryMetaUid })
                    .IsUnique();

                entity.Property(e => e.InventoryExtUid).HasColumnName("InventoryExtUID");

                entity.Property(e => e.InventoryExtValue)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InventoryMetaUid).HasColumnName("InventoryMetaUID");

                entity.Property(e => e.InventoryUid).HasColumnName("InventoryUID");

                entity.HasOne(d => d.InventoryMetaU)
                    .WithMany(p => p.TblTechInventoryExt)
                    .HasForeignKey(d => d.InventoryMetaUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryExt_tblTechInventoryMeta");

                entity.HasOne(d => d.InventoryU)
                    .WithMany(p => p.TblTechInventoryExt)
                    .HasForeignKey(d => d.InventoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryExt_tblTechInventory");
            });

            modelBuilder.Entity<TblTechInventoryHistory>(entity =>
            {
                entity.HasKey(e => e.InventoryHistoryUid);

                entity.ToTable("tblTechInventoryHistory");

                entity.HasIndex(e => new { e.InventoryHistoryUid, e.InventoryUid, e.CreatedDate })
                    .HasName("IX_tblTechInventoryHistory_CD_IUID_iIHUID");

                entity.HasIndex(e => new { e.InventoryHistoryNotes, e.CreatedDate, e.InventoryHistoryUid, e.InventoryUid, e.CreatedByUserId })
                    .HasName("_dta_index_tblTechInventoryHistory_8_2062018477__K1_K2_K13_12_14");

                entity.HasIndex(e => new { e.InventoryHistoryNotes, e.CreatedDate, e.InventoryUid, e.CreatedByUserId, e.InventoryHistoryUid })
                    .HasName("_dta_index_tblTechInventoryHistory_8_2062018477__K2_K13_K1_12_14");

                entity.Property(e => e.InventoryHistoryUid).HasColumnName("InventoryHistoryUID");

                entity.Property(e => e.ArchiveUid).HasColumnName("ArchiveUID");

                entity.Property(e => e.BulkEditUid).HasColumnName("BulkEditUID");

                entity.Property(e => e.ContainerUid).HasColumnName("ContainerUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EntityTypeUid).HasColumnName("EntityTypeUID");

                entity.Property(e => e.EntityUid).HasColumnName("EntityUID");

                entity.Property(e => e.InventoryHistoryNotes)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.InventorySourceUid).HasColumnName("InventorySourceUID");

                entity.Property(e => e.InventoryTypeUid).HasColumnName("InventoryTypeUID");

                entity.Property(e => e.InventoryUid).HasColumnName("InventoryUID");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OperationUid).HasColumnName("OperationUID");

                entity.Property(e => e.OriginArchiveUid).HasColumnName("OriginArchiveUID");

                entity.Property(e => e.OriginContainerUid).HasColumnName("OriginContainerUID");

                entity.Property(e => e.OriginEntityTypeUid).HasColumnName("OriginEntityTypeUID");

                entity.Property(e => e.OriginEntityUid).HasColumnName("OriginEntityUID");

                entity.Property(e => e.OriginParentInventoryUid).HasColumnName("OriginParentInventoryUID");

                entity.Property(e => e.OriginSiteUid).HasColumnName("OriginSiteUID");

                entity.Property(e => e.OriginStatusUid).HasColumnName("OriginStatusUID");

                entity.Property(e => e.ParentInventoryUid).HasColumnName("ParentInventoryUID");

                entity.Property(e => e.SiteUid).HasColumnName("SiteUID");

                entity.Property(e => e.StatusUid).HasColumnName("StatusUID");

                entity.HasOne(d => d.ContainerU)
                    .WithMany(p => p.TblTechInventoryHistoryContainerU)
                    .HasForeignKey(d => d.ContainerUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryHistory_tblTechContainer");

                entity.HasOne(d => d.EntityTypeU)
                    .WithMany(p => p.TblTechInventoryHistory)
                    .HasForeignKey(d => d.EntityTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryHistory_tblUnvEntityTypes");

                entity.HasOne(d => d.InventorySourceU)
                    .WithMany(p => p.TblTechInventoryHistory)
                    .HasForeignKey(d => d.InventorySourceUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryHistory_tblTechInventorySource");

                entity.HasOne(d => d.InventoryTypeU)
                    .WithMany(p => p.TblTechInventoryHistory)
                    .HasForeignKey(d => d.InventoryTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryHistory_tblTechInventoryTypes");

                entity.HasOne(d => d.InventoryU)
                    .WithMany(p => p.TblTechInventoryHistory)
                    .HasForeignKey(d => d.InventoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryHistory_tblTechInventory");

                entity.HasOne(d => d.OperationU)
                    .WithMany(p => p.TblTechInventoryHistory)
                    .HasForeignKey(d => d.OperationUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryHistory_tblTechOperations");

                entity.HasOne(d => d.OriginContainerU)
                    .WithMany(p => p.TblTechInventoryHistoryOriginContainerU)
                    .HasForeignKey(d => d.OriginContainerUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryHistory_tblTechContainer1");

                entity.HasOne(d => d.StatusU)
                    .WithMany(p => p.TblTechInventoryHistory)
                    .HasForeignKey(d => d.StatusUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryHistory_tblStatus");
            });

            modelBuilder.Entity<TblTechInventoryImports>(entity =>
            {
                entity.HasKey(e => e.InventoryImportUid);

                entity.ToTable("tblTechInventoryImports");

                entity.Property(e => e.InventoryImportUid).HasColumnName("InventoryImportUID");

                entity.Property(e => e.ImportUid).HasColumnName("ImportUID");

                entity.Property(e => e.InventoryUid).HasColumnName("InventoryUID");

                entity.HasOne(d => d.ImportU)
                    .WithMany(p => p.TblTechInventoryImports)
                    .HasForeignKey(d => d.ImportUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryImports_tblTechImports");

                entity.HasOne(d => d.InventoryU)
                    .WithMany(p => p.TblTechInventoryImports)
                    .HasForeignKey(d => d.InventoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTagImport_tblTechInventory");
            });

            modelBuilder.Entity<TblTechInventoryInstallationDetails>(entity =>
            {
                entity.HasKey(e => e.InstallationDetailUid);

                entity.ToTable("tblTechInventoryInstallationDetails");

                entity.Property(e => e.InstallationDetailUid).HasColumnName("InstallationDetailUID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.InstallationDate).HasColumnType("date");

                entity.Property(e => e.InventoryUid).HasColumnName("InventoryUID");

                entity.Property(e => e.RoomUid).HasColumnName("RoomUID");

                entity.Property(e => e.SiteUid).HasColumnName("SiteUID");

                entity.HasOne(d => d.InventoryU)
                    .WithMany(p => p.TblTechInventoryInstallationDetails)
                    .HasForeignKey(d => d.InventoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryInstallationDetails_tblTechInventory1");

                entity.HasOne(d => d.RoomU)
                    .WithMany(p => p.TblTechInventoryInstallationDetails)
                    .HasForeignKey(d => d.RoomUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryInstallationDetails_tblUnvRooms");

                entity.HasOne(d => d.SiteU)
                    .WithMany(p => p.TblTechInventoryInstallationDetails)
                    .HasForeignKey(d => d.SiteUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryInstallationDetails_tblTechSites");
            });

            modelBuilder.Entity<TblTechInventoryMeta>(entity =>
            {
                entity.HasKey(e => e.InventoryMetaUid);

                entity.ToTable("tblTechInventoryMeta");

                entity.Property(e => e.InventoryMetaUid).HasColumnName("InventoryMetaUID");

                entity.Property(e => e.InventoryMetaLabel)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InventoryMetaType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ItemTypeUid).HasColumnName("ItemTypeUID");

                entity.HasOne(d => d.ItemTypeU)
                    .WithMany(p => p.TblTechInventoryMeta)
                    .HasForeignKey(d => d.ItemTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryMeta_tblTechItemTypes");
            });

            modelBuilder.Entity<TblTechInventoryQuickAction>(entity =>
            {
                entity.HasKey(e => e.InventoryQuickActionUid);

                entity.ToTable("tblTechInventoryQuickAction");

                entity.Property(e => e.InventoryQuickActionUid).HasColumnName("InventoryQuickActionUID");

                entity.Property(e => e.ActionUid).HasColumnName("ActionUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EntityTypeUid).HasColumnName("EntityTypeUID");

                entity.Property(e => e.EntityUid).HasColumnName("EntityUID");

                entity.Property(e => e.InventoryUid).HasColumnName("InventoryUID");

                entity.Property(e => e.SiteUid).HasColumnName("SiteUID");

                entity.HasOne(d => d.ActionU)
                    .WithMany(p => p.TblTechInventoryQuickAction)
                    .HasForeignKey(d => d.ActionUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryQuickAction_tblTechActions");

                entity.HasOne(d => d.InventoryU)
                    .WithMany(p => p.TblTechInventoryQuickAction)
                    .HasForeignKey(d => d.InventoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryQuickAction_tblTechInventory");

                entity.HasOne(d => d.SiteU)
                    .WithMany(p => p.TblTechInventoryQuickAction)
                    .HasForeignKey(d => d.SiteUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryQuickAction_tblTechSites");
            });

            modelBuilder.Entity<TblTechInventorySource>(entity =>
            {
                entity.HasKey(e => e.InventorySourceUid);

                entity.ToTable("tblTechInventorySource");

                entity.Property(e => e.InventorySourceUid).HasColumnName("InventorySourceUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InventorySourceName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblTechInventoryStatusChangeRequests>(entity =>
            {
                entity.HasKey(e => e.StatusChangeRequestUid);

                entity.ToTable("tblTechInventoryStatusChangeRequests");

                entity.Property(e => e.StatusChangeRequestUid).HasColumnName("StatusChangeRequestUID");

                entity.Property(e => e.ApprovedDeniedInventoryHistoryUid).HasColumnName("ApprovedDeniedInventoryHistoryUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.InventoryHistoryUid).HasColumnName("InventoryHistoryUID");

                entity.Property(e => e.InventoryUid).HasColumnName("InventoryUID");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TagAttachmentUid).HasColumnName("TagAttachmentUID");

                entity.HasOne(d => d.ApprovedDeniedInventoryHistoryU)
                    .WithMany(p => p.TblTechInventoryStatusChangeRequestsApprovedDeniedInventoryHistoryU)
                    .HasForeignKey(d => d.ApprovedDeniedInventoryHistoryUid)
                    .HasConstraintName("FK_tblTechInventoryStatusChangeRequests_tblTechInventoryHistory1");

                entity.HasOne(d => d.InventoryHistoryU)
                    .WithMany(p => p.TblTechInventoryStatusChangeRequestsInventoryHistoryU)
                    .HasForeignKey(d => d.InventoryHistoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryStatusChangeRequests_tblTechInventoryHistory");

                entity.HasOne(d => d.InventoryU)
                    .WithMany(p => p.TblTechInventoryStatusChangeRequests)
                    .HasForeignKey(d => d.InventoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechInventoryStatusChangeRequests_tblTechInventory");

                entity.HasOne(d => d.TagAttachmentU)
                    .WithMany(p => p.TblTechInventoryStatusChangeRequests)
                    .HasForeignKey(d => d.TagAttachmentUid)
                    .HasConstraintName("FK_tblTechInventoryStatusChangeRequests_tblTechTagAttachment");
            });

            modelBuilder.Entity<TblTechInventoryTypes>(entity =>
            {
                entity.HasKey(e => e.InventoryTypeUid);

                entity.ToTable("tblTechInventoryTypes");

                entity.Property(e => e.InventoryTypeUid).HasColumnName("InventoryTypeUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InventoryTypeName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblTechItemAccessories>(entity =>
            {
                entity.HasKey(e => e.ItemAccessoryUid);

                entity.ToTable("tblTechItemAccessories");

                entity.Property(e => e.ItemAccessoryUid).HasColumnName("ItemAccessoryUID");

                entity.Property(e => e.AccessoryUid).HasColumnName("AccessoryUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ItemUid).HasColumnName("ItemUID");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.AccessoryU)
                    .WithMany(p => p.TblTechItemAccessories)
                    .HasForeignKey(d => d.AccessoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechItemAccessories_tblTechAccessories");

                entity.HasOne(d => d.ItemU)
                    .WithMany(p => p.TblTechItemAccessories)
                    .HasForeignKey(d => d.ItemUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechItemAccessories_tblTechItems");
            });

            modelBuilder.Entity<TblTechItemImages>(entity =>
            {
                entity.HasKey(e => e.ItemImageUid);

                entity.ToTable("tblTechItemImages");

                entity.Property(e => e.ItemImageUid).HasColumnName("ItemImageUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ImageUid).HasColumnName("ImageUID");

                entity.Property(e => e.IsPrimary)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ItemUid).HasColumnName("ItemUID");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ImageU)
                    .WithMany(p => p.TblTechItemImages)
                    .HasForeignKey(d => d.ImageUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechItemImages_tblTechImages");

                entity.HasOne(d => d.ItemU)
                    .WithMany(p => p.TblTechItemImages)
                    .HasForeignKey(d => d.ItemUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechItemImages_tblTechItems");
            });

            modelBuilder.Entity<TblTechItems>(entity =>
            {
                entity.HasKey(e => e.ItemUid);

                entity.ToTable("tblTechItems");

                entity.HasIndex(e => e.ItemNumber)
                    .HasName("UQ_tblTechItemsItemNumber")
                    .IsUnique();

                entity.HasIndex(e => e.ItemTypeUid)
                    .HasName("_dta_index_tblTechItems_8_254012036__K5");

                entity.Property(e => e.ItemUid).HasColumnName("ItemUID");

                entity.Property(e => e.AreaUid).HasColumnName("AreaUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CustomField1)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.CustomField2)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.CustomField3)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ItemDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ItemNotes)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.ItemNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ItemSuggestedPrice)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.ItemTypeUid).HasColumnName("ItemTypeUID");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ManufacturerUid).HasColumnName("ManufacturerUID");

                entity.Property(e => e.ModelNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Sku)
                    .HasColumnName("SKU")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.AreaU)
                    .WithMany(p => p.TblTechItems)
                    .HasForeignKey(d => d.AreaUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechItems_tblUnvAreas");

                entity.HasOne(d => d.ItemTypeU)
                    .WithMany(p => p.TblTechItems)
                    .HasForeignKey(d => d.ItemTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechItems_tblTechItemTypes");

                entity.HasOne(d => d.ManufacturerU)
                    .WithMany(p => p.TblTechItems)
                    .HasForeignKey(d => d.ManufacturerUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechItems_tblUnvManufacturers");
            });

            modelBuilder.Entity<TblTechItemTypes>(entity =>
            {
                entity.HasKey(e => e.ItemTypeUid);

                entity.ToTable("tblTechItemTypes");

                entity.Property(e => e.ItemTypeUid).HasColumnName("ItemTypeUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ItemTypeDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ItemTypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblTechOperations>(entity =>
            {
                entity.HasKey(e => e.OperationUid);

                entity.ToTable("tblTechOperations");

                entity.Property(e => e.OperationUid).HasColumnName("OperationUID");

                entity.Property(e => e.CreatedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OperationName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTechPermissionTemplate>(entity =>
            {
                entity.HasKey(e => e.PermissionTemplateUid);

                entity.ToTable("tblTechPermissionTemplate");

                entity.Property(e => e.PermissionTemplateUid).HasColumnName("PermissionTemplateUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TemplateName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserRoleUid).HasColumnName("UserRoleUID");

                entity.HasOne(d => d.UserRoleU)
                    .WithMany(p => p.TblTechPermissionTemplate)
                    .HasForeignKey(d => d.UserRoleUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechPermissionTemplate_tblTechPermissionTemplate");
            });

            modelBuilder.Entity<TblTechPurchaseAttachment>(entity =>
            {
                entity.HasKey(e => e.PurchaseAttachmentUid);

                entity.ToTable("tblTechPurchaseAttachment");

                entity.Property(e => e.PurchaseAttachmentUid).HasColumnName("PurchaseAttachmentUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FilePath)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MarkedForDeletion).HasDefaultValueSql("((0))");

                entity.Property(e => e.Notes)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseUid).HasColumnName("PurchaseUID");

                entity.Property(e => e.UploadedFileName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTechPurchaseInventory>(entity =>
            {
                entity.HasKey(e => e.PurchaseInventoryUid);

                entity.ToTable("tblTechPurchaseInventory");

                entity.HasIndex(e => e.InventoryUid)
                    .HasName("UQ_tblTechPurchaseInventoryInventoryUID")
                    .IsUnique();

                entity.HasIndex(e => e.PurchaseItemShipmentUid)
                    .HasName("IX_PurchaseItemShipmentUID");

                entity.HasIndex(e => new { e.PurchaseItemShipmentUid, e.InventoryUid })
                    .HasName("_dta_index_tblTechPurchaseInventory_8_228299973__K2_3");

                entity.Property(e => e.PurchaseInventoryUid).HasColumnName("PurchaseInventoryUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InventoryUid).HasColumnName("InventoryUID");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PurchaseItemShipmentUid).HasColumnName("PurchaseItemShipmentUID");

                entity.HasOne(d => d.InventoryU)
                    .WithOne(p => p.TblTechPurchaseInventory)
                    .HasForeignKey<TblTechPurchaseInventory>(d => d.InventoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechPurchaseInventory_tblTechInventory");

                entity.HasOne(d => d.PurchaseItemShipmentU)
                    .WithMany(p => p.TblTechPurchaseInventory)
                    .HasForeignKey(d => d.PurchaseItemShipmentUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechPurchaseInventory_tblTechPurchaseItemShipments");
            });

            modelBuilder.Entity<TblTechPurchaseInvoice>(entity =>
            {
                entity.HasKey(e => e.PurchaseInvoiceUid);

                entity.ToTable("tblTechPurchaseInvoice");

                entity.Property(e => e.PurchaseInvoiceUid).HasColumnName("PurchaseInvoiceUID");

                entity.Property(e => e.AccountingDate)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AuthorizationStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InvoiceDate)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InvoiceNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.InvoiceStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedDate)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseUid).HasColumnName("PurchaseUID");

                entity.HasOne(d => d.PurchaseU)
                    .WithMany(p => p.TblTechPurchaseInvoice)
                    .HasForeignKey(d => d.PurchaseUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechPurchaseInvoice_tblTechPurchaseInvoice");
            });

            modelBuilder.Entity<TblTechPurchaseInvoiceDetail>(entity =>
            {
                entity.HasKey(e => e.PurchaseInvoiceDetailUid);

                entity.ToTable("tblTechPurchaseInvoiceDetail");

                entity.Property(e => e.PurchaseInvoiceDetailUid).HasColumnName("PurchaseInvoiceDetailUID");

                entity.Property(e => e.AssetPrice)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InvoicePrice)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LineAmount)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LineDescription).IsUnicode(false);

                entity.Property(e => e.LineNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseInvoiceUid).HasColumnName("PurchaseInvoiceUID");

                entity.Property(e => e.Quantity)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.PurchaseInvoiceU)
                    .WithMany(p => p.TblTechPurchaseInvoiceDetail)
                    .HasForeignKey(d => d.PurchaseInvoiceUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechPurchaseInvoiceDetail_tblTechPurchaseInvoice");
            });

            modelBuilder.Entity<TblTechPurchaseItemDetails>(entity =>
            {
                entity.HasKey(e => e.PurchaseItemDetailUid);

                entity.ToTable("tblTechPurchaseItemDetails");

                entity.HasIndex(e => new { e.PurchaseItemDetailUid, e.PurchaseUid })
                    .HasName("IX_PurchaseUID");

                entity.Property(e => e.PurchaseItemDetailUid).HasColumnName("PurchaseItemDetailUID");

                entity.Property(e => e.AccountCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Cfda)
                    .HasColumnName("CFDA")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FundingSourceUid).HasColumnName("FundingSourceUID");

                entity.Property(e => e.ItemUid).HasColumnName("ItemUID");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PurchasePrice).HasDefaultValueSql("((0.00))");

                entity.Property(e => e.PurchaseUid).HasColumnName("PurchaseUID");

                entity.Property(e => e.SiteAddedSiteUid)
                    .HasColumnName("SiteAddedSiteUID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.StatusUid).HasColumnName("StatusUID");

                entity.Property(e => e.TechDepartmentUid).HasColumnName("TechDepartmentUID");

                entity.HasOne(d => d.FundingSourceU)
                    .WithMany(p => p.TblTechPurchaseItemDetails)
                    .HasForeignKey(d => d.FundingSourceUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechPurchaseItemDetails_tblFundingSources");

                entity.HasOne(d => d.ItemU)
                    .WithMany(p => p.TblTechPurchaseItemDetails)
                    .HasForeignKey(d => d.ItemUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechPurchaseItemDetails_tblTechItems");

                entity.HasOne(d => d.PurchaseU)
                    .WithMany(p => p.TblTechPurchaseItemDetails)
                    .HasForeignKey(d => d.PurchaseUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechPurchaseItemDetails_tblTechPurchases");

                entity.HasOne(d => d.SiteAddedSiteU)
                    .WithMany(p => p.TblTechPurchaseItemDetails)
                    .HasForeignKey(d => d.SiteAddedSiteUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechPurchaseItemDetails_tblTechSites");

                entity.HasOne(d => d.StatusU)
                    .WithMany(p => p.TblTechPurchaseItemDetails)
                    .HasForeignKey(d => d.StatusUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechPurchaseItemDetails_tblStatus");

                entity.HasOne(d => d.TechDepartmentU)
                    .WithMany(p => p.TblTechPurchaseItemDetails)
                    .HasForeignKey(d => d.TechDepartmentUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechPurchaseItemDetails_tblTechDepartments");
            });

            modelBuilder.Entity<TblTechPurchaseItemShipments>(entity =>
            {
                entity.HasKey(e => e.PurchaseItemShipmentUid);

                entity.ToTable("tblTechPurchaseItemShipments");

                entity.HasIndex(e => e.PurchaseItemDetailUid)
                    .HasName("IX_PurchaseItemDetailUID");

                entity.Property(e => e.PurchaseItemShipmentUid).HasColumnName("PurchaseItemShipmentUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InvoiceDate).HasColumnType("date");

                entity.Property(e => e.InvoiceNumber)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PurchaseItemDetailUid).HasColumnName("PurchaseItemDetailUID");

                entity.Property(e => e.ShippedToSiteUid).HasColumnName("ShippedToSiteUID");

                entity.Property(e => e.StatusUid).HasColumnName("StatusUID");

                entity.Property(e => e.TicketedByUserId).HasColumnName("TicketedByUserID");

                entity.Property(e => e.TicketedDate).HasColumnType("datetime");

                entity.HasOne(d => d.PurchaseItemDetailU)
                    .WithMany(p => p.TblTechPurchaseItemShipments)
                    .HasForeignKey(d => d.PurchaseItemDetailUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechPurchaseItemShipments_tblTechPurchaseItemDetails");

                entity.HasOne(d => d.ShippedToSiteU)
                    .WithMany(p => p.TblTechPurchaseItemShipments)
                    .HasForeignKey(d => d.ShippedToSiteUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechPurchaseItemShipments_tblTechSites");

                entity.HasOne(d => d.StatusU)
                    .WithMany(p => p.TblTechPurchaseItemShipments)
                    .HasForeignKey(d => d.StatusUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechPurchaseItemShipments_tblStatus");
            });

            modelBuilder.Entity<TblTechPurchases>(entity =>
            {
                entity.HasKey(e => e.PurchaseUid);

                entity.ToTable("tblTechPurchases");

                entity.HasIndex(e => e.OrderNumber)
                    .HasName("UQ_tblTechPurchasesOrderNumber")
                    .IsUnique();

                entity.Property(e => e.PurchaseUid).HasColumnName("PurchaseUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EstimatedDeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.FederalFunding).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Frn)
                    .HasColumnName("FRN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Notes)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Other1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SiteUid)
                    .HasColumnName("SiteUID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.StateFunding).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.StatusUid).HasColumnName("StatusUID");

                entity.Property(e => e.VendorUid).HasColumnName("VendorUID");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblTechPurchases)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechPurchases_tblUser");

                entity.HasOne(d => d.SiteU)
                    .WithMany(p => p.TblTechPurchases)
                    .HasForeignKey(d => d.SiteUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechPurchases_tblTechSites");

                entity.HasOne(d => d.StatusU)
                    .WithMany(p => p.TblTechPurchases)
                    .HasForeignKey(d => d.StatusUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechPurchases_tblStatus");

                entity.HasOne(d => d.VendorU)
                    .WithMany(p => p.TblTechPurchases)
                    .HasForeignKey(d => d.VendorUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechPurchases_tblVendor");
            });

            modelBuilder.Entity<TblTechSavedTagSearches>(entity =>
            {
                entity.HasKey(e => new { e.TagSearchUid, e.SearchName });

                entity.ToTable("tblTechSavedTagSearches");

                entity.Property(e => e.TagSearchUid)
                    .HasColumnName("TagSearchUID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.SearchName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AdvancedFilters)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.BasicFilters)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.GridSettings)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.SiteUid).HasColumnName("SiteUID");
            });

            modelBuilder.Entity<TblTechScheduleReport>(entity =>
            {
                entity.HasKey(e => e.ScheduleReportUid);

                entity.ToTable("tblTechScheduleReport");

                entity.Property(e => e.ScheduleReportUid).HasColumnName("ScheduleReportUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ScheduleReportTypeUid).HasColumnName("ScheduleReportTypeUID");

                entity.Property(e => e.ScheduleUid).HasColumnName("ScheduleUID");

                entity.Property(e => e.SearchUid).HasColumnName("SearchUID");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblTechScheduleReportCreatedByUser)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechScheduleReport_CreatedByUser");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TblTechScheduleReportLastModifiedByUser)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .HasConstraintName("FK_tblTechScheduleReport_LastModifiedByUserID");

                entity.HasOne(d => d.ScheduleReportTypeU)
                    .WithMany(p => p.TblTechScheduleReport)
                    .HasForeignKey(d => d.ScheduleReportTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechScheduleReport_tblUnvScheduleReportType");

                entity.HasOne(d => d.ScheduleU)
                    .WithMany(p => p.TblTechScheduleReport)
                    .HasForeignKey(d => d.ScheduleUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechScheduleReport_tblTechSchedule");
            });

            modelBuilder.Entity<TblTechSignatureReceipt>(entity =>
            {
                entity.HasKey(e => e.SignatureReceiptUid);

                entity.ToTable("tblTechSignatureReceipt");

                entity.Property(e => e.SignatureReceiptUid).HasColumnName("SignatureReceiptUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EntityTypeUid).HasColumnName("EntityTypeUID");

                entity.Property(e => e.EntityUid).HasColumnName("EntityUID");

                entity.Property(e => e.InventoryHistoryUid).HasColumnName("InventoryHistoryUID");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblTechSites>(entity =>
            {
                entity.HasKey(e => e.SiteUid);

                entity.ToTable("tblTechSites");

                entity.HasIndex(e => e.SiteId)
                    .HasName("UQ_tblTechSites_SiteID")
                    .IsUnique();

                entity.Property(e => e.SiteUid).HasColumnName("SiteUID");

                entity.Property(e => e.BillingAddress1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BillingAddress2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BillingCity)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BillingInstructions)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.BillingStateId).HasColumnName("BillingStateID");

                entity.Property(e => e.BillingZip)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Contact)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FacilityId)
                    .HasColumnName("FacilityID")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.IsDesignatedTransferSite)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.ShippingAddress1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ShippingAddress2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ShippingCity)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShippingInstructions)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ShippingStateId).HasColumnName("ShippingStateID");

                entity.Property(e => e.ShippingZip)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SiteId)
                    .IsRequired()
                    .HasColumnName("SiteID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SiteName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SiteTypeUid).HasColumnName("SiteTypeUID");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.TblTechSites)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK_tblTechSites_tblRegion");

                entity.HasOne(d => d.SiteTypeU)
                    .WithMany(p => p.TblTechSites)
                    .HasForeignKey(d => d.SiteTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechSites_tblTechSiteTypes");
            });

            modelBuilder.Entity<TblTechSiteTypes>(entity =>
            {
                entity.HasKey(e => e.SiteTypeUid);

                entity.ToTable("tblTechSiteTypes");

                entity.Property(e => e.SiteTypeUid).HasColumnName("SiteTypeUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTechStaffAttachment>(entity =>
            {
                entity.HasKey(e => e.StaffAttachmentUid);

                entity.ToTable("tblTechStaffAttachment");

                entity.Property(e => e.StaffAttachmentUid).HasColumnName("StaffAttachmentUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FilePath)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MarkedForDeletion).HasDefaultValueSql("((0))");

                entity.Property(e => e.Notes)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.StaffUid).HasColumnName("StaffUID");

                entity.Property(e => e.UploadedFileName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTechStaffCharges>(entity =>
            {
                entity.HasKey(e => e.StaffChargeUid);

                entity.ToTable("tblTechStaffCharges");

                entity.Property(e => e.StaffChargeUid).HasColumnName("StaffChargeUID");

                entity.Property(e => e.ChargeUid).HasColumnName("ChargeUID");

                entity.Property(e => e.StaffId)
                    .IsRequired()
                    .HasColumnName("StaffID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ChargeU)
                    .WithMany(p => p.TblTechStaffCharges)
                    .HasForeignKey(d => d.ChargeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechStaffCharges_tblUnvCharges");
            });

            modelBuilder.Entity<TblTechStaffRooms>(entity =>
            {
                entity.HasKey(e => e.StaffRoomUid);

                entity.ToTable("tblTechStaffRooms");

                entity.Property(e => e.StaffRoomUid).HasColumnName("StaffRoomUID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RoomUid).HasColumnName("RoomUID");

                entity.Property(e => e.TeacherUid).HasColumnName("TeacherUID");

                entity.HasOne(d => d.RoomU)
                    .WithMany(p => p.TblTechStaffRooms)
                    .HasForeignKey(d => d.RoomUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechStaffRooms_tblUnvRooms");

                entity.HasOne(d => d.TeacherU)
                    .WithMany(p => p.TblTechStaffRooms)
                    .HasForeignKey(d => d.TeacherUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechStaffRooms_tblTeachers");
            });

            modelBuilder.Entity<TblTechStatusApprovalSettings>(entity =>
            {
                entity.HasKey(e => e.StatusApprovalSettingsUid);

                entity.ToTable("tblTechStatusApprovalSettings");

                entity.Property(e => e.StatusApprovalSettingsUid).HasColumnName("StatusApprovalSettingsUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RecipientUid).HasColumnName("RecipientUID");

                entity.HasOne(d => d.RecipientU)
                    .WithMany(p => p.TblTechStatusApprovalSettings)
                    .HasForeignKey(d => d.RecipientUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechStatusApprovalSettings_tblTechStatusApprovalSettings");
            });

            modelBuilder.Entity<TblTechStatusApprovalSettingsStatusLink>(entity =>
            {
                entity.HasKey(e => e.StatusApprovalSettingStatusUid);

                entity.ToTable("tblTechStatusApprovalSettingsStatusLink");

                entity.Property(e => e.StatusApprovalSettingStatusUid).HasColumnName("StatusApprovalSettingStatusUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.StatusApprovalSettingsUid).HasColumnName("StatusApprovalSettingsUID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.StatusApprovalSettingsU)
                    .WithMany(p => p.TblTechStatusApprovalSettingsStatusLink)
                    .HasForeignKey(d => d.StatusApprovalSettingsUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechStatusApprovalSettingsStatusLink_tblTechStatusApprovalSettings");
            });

            modelBuilder.Entity<TblTechStatusChangeSettings>(entity =>
            {
                entity.HasKey(e => e.StatusChangeSettingsUid);

                entity.ToTable("tblTechStatusChangeSettings");

                entity.Property(e => e.StatusChangeSettingsUid).HasColumnName("StatusChangeSettingsUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RecipientUid).HasColumnName("RecipientUID");

                entity.HasOne(d => d.RecipientU)
                    .WithMany(p => p.TblTechStatusChangeSettings)
                    .HasForeignKey(d => d.RecipientUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblStatusChangeSettings_tblStatusChangeSettings");
            });

            modelBuilder.Entity<TblTechStatusChangeSettingsStatus>(entity =>
            {
                entity.HasKey(e => e.StatusChangeSettingStatusUid);

                entity.ToTable("tblTechStatusChangeSettingsStatus");

                entity.Property(e => e.StatusChangeSettingStatusUid).HasColumnName("StatusChangeSettingStatusUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.StatusChangeSettingsUid).HasColumnName("StatusChangeSettingsUID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.StatusChangeSettingsU)
                    .WithMany(p => p.TblTechStatusChangeSettingsStatus)
                    .HasForeignKey(d => d.StatusChangeSettingsUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechStatusChangeSettingsStatus_tblTechStatusChangeSettings");
            });

            modelBuilder.Entity<TblTechStudentAttachment>(entity =>
            {
                entity.HasKey(e => e.StudentAttachmentUid);

                entity.ToTable("tblTechStudentAttachment");

                entity.Property(e => e.StudentAttachmentUid).HasColumnName("StudentAttachmentUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FilePath)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MarkedForDeletion).HasDefaultValueSql("((0))");

                entity.Property(e => e.Notes)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.StudentUid).HasColumnName("StudentUID");

                entity.Property(e => e.UploadedFileName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTechStudentCharges>(entity =>
            {
                entity.HasKey(e => e.StudentChargeUid);

                entity.ToTable("tblTechStudentCharges");

                entity.Property(e => e.StudentChargeUid).HasColumnName("StudentChargeUID");

                entity.Property(e => e.ChargeUid).HasColumnName("ChargeUID");

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasColumnName("StudentID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ChargeU)
                    .WithMany(p => p.TblTechStudentCharges)
                    .HasForeignKey(d => d.ChargeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechStudentCharges_tblUnvCharges");
            });

            modelBuilder.Entity<TblTechStudentInventory>(entity =>
            {
                entity.HasKey(e => e.StudentInventoryUid);

                entity.ToTable("tblTechStudentInventory");

                entity.Property(e => e.StudentInventoryUid).HasColumnName("StudentInventoryUID");

                entity.Property(e => e.InventoryHistoryUid).HasColumnName("InventoryHistoryUID");

                entity.Property(e => e.InventoryUid).HasColumnName("InventoryUID");

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasColumnName("StudentID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.InventoryHistoryU)
                    .WithMany(p => p.TblTechStudentInventory)
                    .HasForeignKey(d => d.InventoryHistoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechStudentInventory_tblTechInventoryHistory");

                entity.HasOne(d => d.InventoryU)
                    .WithMany(p => p.TblTechStudentInventory)
                    .HasForeignKey(d => d.InventoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechStudentInventory_tblTechInventory");
            });

            modelBuilder.Entity<TblTechTagAttachment>(entity =>
            {
                entity.HasKey(e => e.TagAttachmentUid);

                entity.ToTable("tblTechTagAttachment");

                entity.Property(e => e.TagAttachmentUid).HasColumnName("TagAttachmentUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FilePath)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.InventoryUid).HasColumnName("InventoryUID");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .HasMaxLength(3000)
                    .IsUnicode(false);

                entity.Property(e => e.UploadedFileName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTechTagEpc>(entity =>
            {
                entity.HasKey(e => e.TagEpcUid);

                entity.ToTable("tblTechTagEpc");

                entity.HasIndex(e => new { e.Tag, e.Epc })
                    .HasName("UQ_tblTechTagEpc")
                    .IsUnique();

                entity.Property(e => e.TagEpcUid).HasColumnName("TagEpcUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Epc)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Tag)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblTechTagHistory>(entity =>
            {
                entity.HasKey(e => e.TagHistoryUid);

                entity.ToTable("tblTechTagHistory");

                entity.Property(e => e.TagHistoryUid).HasColumnName("TagHistoryUID");

                entity.Property(e => e.AssetConditionUid).HasColumnName("AssetConditionUID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.DueDate).HasColumnType("date");

                entity.Property(e => e.InventoryUid).HasColumnName("InventoryUID");

                entity.Property(e => e.OriginAssetConditionUid).HasColumnName("OriginAssetConditionUID");

                entity.Property(e => e.OriginDueDate).HasColumnType("date");

                entity.Property(e => e.OriginSerial)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OriginTag)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductChangeNote).IsUnicode(false);

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tag)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.AssetConditionU)
                    .WithMany(p => p.TblTechTagHistoryAssetConditionU)
                    .HasForeignKey(d => d.AssetConditionUid)
                    .HasConstraintName("FK_tblTechTagHistory_tblTechAssetConditions1");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblTechTagHistory)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTagHistory_tblUser");

                entity.HasOne(d => d.InventoryU)
                    .WithMany(p => p.TblTechTagHistory)
                    .HasForeignKey(d => d.InventoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTagHistory_tblTechInventory");

                entity.HasOne(d => d.OriginAssetConditionU)
                    .WithMany(p => p.TblTechTagHistoryOriginAssetConditionU)
                    .HasForeignKey(d => d.OriginAssetConditionUid)
                    .HasConstraintName("FK_tblTechTagHistory_tblTechAssetConditions");
            });

            modelBuilder.Entity<TblTechTemplateFunction>(entity =>
            {
                entity.HasKey(e => e.TemplateFunctionUid);

                entity.ToTable("tblTechTemplateFunction");

                entity.Property(e => e.TemplateFunctionUid).HasColumnName("TemplateFunctionUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FunctionUid).HasColumnName("FunctionUID");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PermissionTemplateUid).HasColumnName("PermissionTemplateUID");

                entity.HasOne(d => d.PermissionTemplateU)
                    .WithMany(p => p.TblTechTemplateFunction)
                    .HasForeignKey(d => d.PermissionTemplateUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTemplateFunction_tblUnvFunctions");
            });

            modelBuilder.Entity<TblTechTransferApproverExceptionDetails>(entity =>
            {
                entity.HasKey(e => e.TransferApproverExceptionDetailsId);

                entity.ToTable("tblTechTransferApproverExceptionDetails");

                entity.Property(e => e.ApproverEmail)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ApproverFlag)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ApproverName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CcnotificationRecipientUid).HasColumnName("CCNotificationRecipientUID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CcnotificationRecipientU)
                    .WithMany(p => p.TblTechTransferApproverExceptionDetails)
                    .HasForeignKey(d => d.CcnotificationRecipientUid)
                    .HasConstraintName("FK_tblTechTransferApproverExceptionDetails_tblUnvRecipient");
            });

            modelBuilder.Entity<TblTechTransferDepartmentWorkflows>(entity =>
            {
                entity.HasKey(e => e.TransferDepartmentWorkflowUid);

                entity.ToTable("tblTechTransferDepartmentWorkflows");

                entity.Property(e => e.TransferDepartmentWorkflowUid).HasColumnName("TransferDepartmentWorkflowUID");

                entity.Property(e => e.CcnotificationRecipientUid).HasColumnName("CCNotificationRecipientUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TechDepartmentUid).HasColumnName("TechDepartmentUID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.CcnotificationRecipientU)
                    .WithMany(p => p.TblTechTransferDepartmentWorkflows)
                    .HasForeignKey(d => d.CcnotificationRecipientUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferDepartmentWorkflows_tblUnvRecipient");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblTechTransferDepartmentWorkflowsCreatedByUser)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferDepartmentWorkflowApprovals_tblUser1");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TblTechTransferDepartmentWorkflowsLastModifiedByUser)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferDepartmentWorkflowApprovals_tblUser2");

                entity.HasOne(d => d.TechDepartmentU)
                    .WithMany(p => p.TblTechTransferDepartmentWorkflows)
                    .HasForeignKey(d => d.TechDepartmentUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferDepartmentWorkflowApprovals_tblTechDepartments");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblTechTransferDepartmentWorkflowsUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferDepartmentWorkflowApprovals_tblUser");
            });

            modelBuilder.Entity<TblTechTransferHistory>(entity =>
            {
                entity.HasKey(e => e.TransferHistoryUid);

                entity.ToTable("tblTechTransferHistory");

                entity.Property(e => e.TransferHistoryUid).HasColumnName("TransferHistoryUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.DriverId).HasColumnName("DriverID");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OriginStatusUid).HasColumnName("OriginStatusUID");

                entity.Property(e => e.SignatureUid).HasColumnName("SignatureUID");

                entity.Property(e => e.StatusUid).HasColumnName("StatusUID");

                entity.Property(e => e.TransferUid).HasColumnName("TransferUID");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.TblTechTransferHistory)
                    .HasForeignKey(d => d.DriverId)
                    .HasConstraintName("FK_tblTechTransferHistory_tblUser");

                entity.HasOne(d => d.SignatureU)
                    .WithMany(p => p.TblTechTransferHistory)
                    .HasForeignKey(d => d.SignatureUid)
                    .HasConstraintName("FK_tblTechTransferHistory_tblUnvSignature");

                entity.HasOne(d => d.StatusU)
                    .WithMany(p => p.TblTechTransferHistory)
                    .HasForeignKey(d => d.StatusUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferHistory_tblStatus");

                entity.HasOne(d => d.TransferU)
                    .WithMany(p => p.TblTechTransferHistory)
                    .HasForeignKey(d => d.TransferUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferHistory_tblTechTransfer");
            });

            modelBuilder.Entity<TblTechTransferInventory>(entity =>
            {
                entity.HasKey(e => e.TransferInventoryUid);

                entity.ToTable("tblTechTransferInventory");

                entity.Property(e => e.TransferInventoryUid).HasColumnName("TransferInventoryUID");

                entity.Property(e => e.ContainerUid).HasColumnName("ContainerUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FundingSourceApproved).HasDefaultValueSql("((1))");

                entity.Property(e => e.InventoryUid).HasColumnName("InventoryUID");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.OriginStatusUid)
                    .HasColumnName("OriginStatusUID")
                    .HasDefaultValueSql("((26))");

                entity.Property(e => e.TransferUid).HasColumnName("TransferUID");

                entity.Property(e => e.UntaggedInventoryUid).HasColumnName("UntaggedInventoryUID");

                entity.HasOne(d => d.ContainerU)
                    .WithMany(p => p.TblTechTransferInventory)
                    .HasForeignKey(d => d.ContainerUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferInventory_tblTechContainers");

                entity.HasOne(d => d.InventoryU)
                    .WithMany(p => p.TblTechTransferInventory)
                    .HasForeignKey(d => d.InventoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferInventory_tblTechInventory");

                entity.HasOne(d => d.OriginStatusU)
                    .WithMany(p => p.TblTechTransferInventory)
                    .HasForeignKey(d => d.OriginStatusUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferInventory_tblStatus");

                entity.HasOne(d => d.TransferU)
                    .WithMany(p => p.TblTechTransferInventory)
                    .HasForeignKey(d => d.TransferUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferInventory_tblTechTransfers");

                entity.HasOne(d => d.UntaggedInventoryU)
                    .WithMany(p => p.TblTechTransferInventory)
                    .HasForeignKey(d => d.UntaggedInventoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferInventory_tblTechUntaggedInventory");
            });

            modelBuilder.Entity<TblTechTransferInventoryContainerLink>(entity =>
            {
                entity.HasKey(e => e.TransferInventoryContainerUid);

                entity.ToTable("tblTechTransferInventoryContainerLink");

                entity.Property(e => e.TransferInventoryContainerUid).HasColumnName("TransferInventoryContainerUID");

                entity.Property(e => e.ContainerUid).HasColumnName("ContainerUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TransferInventoryUid).HasColumnName("TransferInventoryUID");

                entity.HasOne(d => d.ContainerU)
                    .WithMany(p => p.TblTechTransferInventoryContainerLink)
                    .HasForeignKey(d => d.ContainerUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferInventoryContainerLink_tblTechContainers");

                entity.HasOne(d => d.TransferInventoryU)
                    .WithMany(p => p.TblTechTransferInventoryContainerLink)
                    .HasForeignKey(d => d.TransferInventoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferInventoryContainerLink_tblTechTransferInventory");
            });

            modelBuilder.Entity<TblTechTransferRequestDetails>(entity =>
            {
                entity.HasKey(e => e.TransferRequestDetailsUid);

                entity.ToTable("tblTechTransferRequestDetails");

                entity.Property(e => e.TransferRequestDetailsUid).HasColumnName("TransferRequestDetailsUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.InventoryTypeUid).HasColumnName("InventoryTypeUID");

                entity.Property(e => e.ItemUid).HasColumnName("ItemUID");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TransferUid).HasColumnName("TransferUID");

                entity.HasOne(d => d.InventoryTypeU)
                    .WithMany(p => p.TblTechTransferRequestDetails)
                    .HasForeignKey(d => d.InventoryTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferRequestDetails_tblTechInventoryTypes");

                entity.HasOne(d => d.ItemU)
                    .WithMany(p => p.TblTechTransferRequestDetails)
                    .HasForeignKey(d => d.ItemUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferRequestDetails_tblTechItems");

                entity.HasOne(d => d.TransferU)
                    .WithMany(p => p.TblTechTransferRequestDetails)
                    .HasForeignKey(d => d.TransferUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferRequestDetails_tblTechTransfers");
            });

            modelBuilder.Entity<TblTechTransferRequestDetailsHistory>(entity =>
            {
                entity.HasKey(e => e.TransferRequestDetailsHistoryUid);

                entity.ToTable("tblTechTransferRequestDetailsHistory");

                entity.Property(e => e.TransferRequestDetailsHistoryUid).HasColumnName("TransferRequestDetailsHistoryUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.TransferRequestDetailsUid).HasColumnName("TransferRequestDetailsUID");

                entity.HasOne(d => d.TransferRequestDetailsU)
                    .WithMany(p => p.TblTechTransferRequestDetailsHistory)
                    .HasForeignKey(d => d.TransferRequestDetailsUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferRequestDetailsHistory_tblTechTransferRequestDetails");
            });

            modelBuilder.Entity<TblTechTransferRequestNotes>(entity =>
            {
                entity.HasKey(e => e.TransferRequestNotesUid);

                entity.ToTable("tblTechTransferRequestNotes");

                entity.Property(e => e.TransferRequestNotesUid).HasColumnName("TransferRequestNotesUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.StatusUid).HasColumnName("StatusUID");

                entity.Property(e => e.TransferUid).HasColumnName("TransferUID");

                entity.HasOne(d => d.StatusU)
                    .WithMany(p => p.TblTechTransferRequestNotes)
                    .HasForeignKey(d => d.StatusUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferRequest_tblStatus");

                entity.HasOne(d => d.TransferU)
                    .WithMany(p => p.TblTechTransferRequestNotes)
                    .HasForeignKey(d => d.TransferUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferRequest_tblTechTransfers1");
            });

            modelBuilder.Entity<TblTechTransfers>(entity =>
            {
                entity.HasKey(e => e.TransferUid);

                entity.ToTable("tblTechTransfers");

                entity.Property(e => e.TransferUid).HasColumnName("TransferUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.DestinationSiteUid).HasColumnName("DestinationSiteUID");

                entity.Property(e => e.DriverId).HasColumnName("DriverID");

                entity.Property(e => e.DriverRequired)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastEditByUserId).HasColumnName("LastEditByUserID");

                entity.Property(e => e.LastEditDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiveApprovedByUserId).HasColumnName("ReceiveApprovedByUserID");

                entity.Property(e => e.ReceiveApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.SiteUid).HasColumnName("SiteUID");

                entity.Property(e => e.StatusUid)
                    .HasColumnName("StatusUID")
                    .HasDefaultValueSql("((23))");

                entity.Property(e => e.TransferNotes)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.TransferTypeUid)
                    .HasColumnName("TransferTypeUID")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.TblTechTransfers)
                    .HasForeignKey(d => d.DriverId)
                    .HasConstraintName("FK_tblTechTransfers_tblUser");

                entity.HasOne(d => d.StatusU)
                    .WithMany(p => p.TblTechTransfers)
                    .HasForeignKey(d => d.StatusUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransfers_tblStatus");

                entity.HasOne(d => d.TransferTypeU)
                    .WithMany(p => p.TblTechTransfers)
                    .HasForeignKey(d => d.TransferTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransfers_tblTechTransferTypes");
            });

            modelBuilder.Entity<TblTechTransferSites>(entity =>
            {
                entity.HasKey(e => e.TransferSiteUid);

                entity.ToTable("tblTechTransferSites");

                entity.Property(e => e.TransferSiteUid).HasColumnName("TransferSiteUID");

                entity.Property(e => e.CreatedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TransferSiteName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTechTransferStatusWorkflowSettings>(entity =>
            {
                entity.HasKey(e => e.TransferStatusWorkflowSettingUid);

                entity.ToTable("tblTechTransferStatusWorkflowSettings");

                entity.Property(e => e.TransferStatusWorkflowSettingUid).HasColumnName("TransferStatusWorkflowSettingUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NotifyRecipientUid).HasColumnName("NotifyRecipientUID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblTechTransferStatusWorkflowSettingsCreatedByUser)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferStatusWorkflowSettings_tblUser");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TblTechTransferStatusWorkflowSettingsLastModifiedByUser)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferStatusWorkflowSettings_tblUser1");

                entity.HasOne(d => d.NotifyRecipientU)
                    .WithMany(p => p.TblTechTransferStatusWorkflowSettings)
                    .HasForeignKey(d => d.NotifyRecipientUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferStatusWorkflowSettings_tblUnvRecipient");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TblTechTransferStatusWorkflowSettings)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferStatusWorkflowSettings_tblStatus");
            });

            modelBuilder.Entity<TblTechTransferTypes>(entity =>
            {
                entity.HasKey(e => e.TransferTypeUid);

                entity.ToTable("tblTechTransferTypes");

                entity.Property(e => e.TransferTypeUid)
                    .HasColumnName("TransferTypeUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTechTransferUserWorkflows>(entity =>
            {
                entity.HasKey(e => e.TransferUserWorkflowUid);

                entity.ToTable("tblTechTransferUserWorkflows");

                entity.Property(e => e.TransferUserWorkflowUid).HasColumnName("TransferUserWorkflowUID");

                entity.Property(e => e.ApprovalLevel).HasDefaultValueSql("((1))");

                entity.Property(e => e.ApproverEmail).IsUnicode(false);

                entity.Property(e => e.ApproverFlag)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ApproverFullName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ApproverUserId).HasColumnName("ApproverUserID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.ApproverUser)
                    .WithMany(p => p.TblTechTransferUserWorkflowsApproverUser)
                    .HasForeignKey(d => d.ApproverUserId)
                    .HasConstraintName("FK_tblTechTransferUserWorkflows_tblUser1");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblTechTransferUserWorkflowsCreatedByUser)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferUserWorkflows_tblUser2");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TblTechTransferUserWorkflowsLastModifiedByUser)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferUserWorkflows_tblUser3");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblTechTransferUserWorkflowsUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferUserWorkflows_tblUser");
            });

            modelBuilder.Entity<TblTechTransferWorkflowHistory>(entity =>
            {
                entity.HasKey(e => e.TransferWorkflowHistoryUid);

                entity.ToTable("tblTechTransferWorkflowHistory");

                entity.Property(e => e.TransferWorkflowHistoryUid).HasColumnName("TransferWorkflowHistoryUID");

                entity.Property(e => e.AdditionalEmail).IsUnicode(false);

                entity.Property(e => e.ApprovalUserTypeUid).HasColumnName("ApprovalUserTypeUID");

                entity.Property(e => e.ApprovedByName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ApprovedByUserId).HasColumnName("ApprovedByUserID");

                entity.Property(e => e.ApprovedByUserType)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ApproverFlag)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CcnotificationRecipientUid).HasColumnName("CCNotificationRecipientUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeniedByName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DeniedByUserId).HasColumnName("DeniedByUserID");

                entity.Property(e => e.DeniedByUserType)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OutsideUserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TransferSiteUid).HasColumnName("TransferSiteUID");

                entity.Property(e => e.TransferUid).HasColumnName("TransferUID");

                entity.Property(e => e.TransferWorkflowUid).HasColumnName("TransferWorkflowUID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserRoleTypeUid).HasColumnName("UserRoleTypeUID");

                entity.HasOne(d => d.CcnotificationRecipientU)
                    .WithMany(p => p.TblTechTransferWorkflowHistory)
                    .HasForeignKey(d => d.CcnotificationRecipientUid)
                    .HasConstraintName("FK_tblTechTransferWorkflowHistory_tblUnvRecipient");

                entity.HasOne(d => d.TransferSiteU)
                    .WithMany(p => p.TblTechTransferWorkflowHistory)
                    .HasForeignKey(d => d.TransferSiteUid)
                    .HasConstraintName("FK_tblTechTransferWorkflowHistory_tblTechTransferSites");

                entity.HasOne(d => d.TransferU)
                    .WithMany(p => p.TblTechTransferWorkflowHistory)
                    .HasForeignKey(d => d.TransferUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferWorkflowHistory_tblTechTransfer");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblTechTransferWorkflowHistory)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferWorkflowHistory_tblUser");

                entity.HasOne(d => d.UserRoleTypeU)
                    .WithMany(p => p.TblTechTransferWorkflowHistory)
                    .HasForeignKey(d => d.UserRoleTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferWorkflowHistory_tblTechUserRoleTypes");
            });

            modelBuilder.Entity<TblTechTransferWorkflowLinks>(entity =>
            {
                entity.HasKey(e => e.TransferWorkflowLinkUid);

                entity.ToTable("tblTechTransferWorkflowLinks");

                entity.Property(e => e.TransferWorkflowLinkUid).HasColumnName("TransferWorkflowLinkUID");

                entity.Property(e => e.EncryptedLink)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.TransferWorkflowUid).HasColumnName("TransferWorkflowUID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.TransferWorkflowU)
                    .WithMany(p => p.TblTechTransferWorkflowLinks)
                    .HasForeignKey(d => d.TransferWorkflowUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferWorkflowLinks_tblTechTransferWorkflows");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblTechTransferWorkflowLinks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferWorkflowLinks_tblUser");
            });

            modelBuilder.Entity<TblTechTransferWorkflows>(entity =>
            {
                entity.HasKey(e => e.TransferWorkflowUid);

                entity.ToTable("tblTechTransferWorkflows");

                entity.Property(e => e.TransferWorkflowUid).HasColumnName("TransferWorkflowUID");

                entity.Property(e => e.AdditionalEmail).IsUnicode(false);

                entity.Property(e => e.ApprovalUserTypeUid).HasColumnName("ApprovalUserTypeUID");

                entity.Property(e => e.ApprovedByName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ApprovedByUserId).HasColumnName("ApprovedByUserID");

                entity.Property(e => e.ApprovedByUserType)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ApproverFlag)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CcnotificationRecipientUid).HasColumnName("CCNotificationRecipientUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeniedByName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DeniedByUserId).HasColumnName("DeniedByUserID");

                entity.Property(e => e.DeniedByUserType)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OutsideUserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TransferSiteUid).HasColumnName("TransferSiteUID");

                entity.Property(e => e.TransferUid).HasColumnName("TransferUID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserRoleTypeUid).HasColumnName("UserRoleTypeUID");

                entity.HasOne(d => d.CcnotificationRecipientU)
                    .WithMany(p => p.TblTechTransferWorkflows)
                    .HasForeignKey(d => d.CcnotificationRecipientUid)
                    .HasConstraintName("FK_tblTechTransferWorkflows_tblUnvRecipient");

                entity.HasOne(d => d.TransferSiteU)
                    .WithMany(p => p.TblTechTransferWorkflows)
                    .HasForeignKey(d => d.TransferSiteUid)
                    .HasConstraintName("FK_tblTechTransferWorkflows_tblTechTransferSites");

                entity.HasOne(d => d.TransferU)
                    .WithMany(p => p.TblTechTransferWorkflows)
                    .HasForeignKey(d => d.TransferUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferWorkflows_tblTechTransfers");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblTechTransferWorkflows)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferWorkflows_tblUser");

                entity.HasOne(d => d.UserRoleTypeU)
                    .WithMany(p => p.TblTechTransferWorkflows)
                    .HasForeignKey(d => d.UserRoleTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechTransferWorkflows_tblTechUserRoleTypes");
            });

            modelBuilder.Entity<TblTechUntaggedInventory>(entity =>
            {
                entity.HasKey(e => e.UntaggedInventoryUid);

                entity.ToTable("tblTechUntaggedInventory");

                entity.Property(e => e.UntaggedInventoryUid).HasColumnName("UntaggedInventoryUID");

                entity.Property(e => e.ContainerUid).HasColumnName("ContainerUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Identifier)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.InventorySourceUid)
                    .HasColumnName("InventorySourceUID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ItemUid).HasColumnName("ItemUID");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ContainerU)
                    .WithMany(p => p.TblTechUntaggedInventory)
                    .HasForeignKey(d => d.ContainerUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechUntaggedInventory_tblTechContainer");

                entity.HasOne(d => d.InventorySourceU)
                    .WithMany(p => p.TblTechUntaggedInventory)
                    .HasForeignKey(d => d.InventorySourceUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechUntaggedInventory_tblInventorySource");

                entity.HasOne(d => d.ItemU)
                    .WithMany(p => p.TblTechUntaggedInventory)
                    .HasForeignKey(d => d.ItemUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechUntaggedInventory_tblTechItems");
            });

            modelBuilder.Entity<TblTechUntaggedInventoryHistory>(entity =>
            {
                entity.HasKey(e => e.UntaggedInventoryHistoryUid);

                entity.ToTable("tblTechUntaggedInventoryHistory");

                entity.Property(e => e.UntaggedInventoryHistoryUid).HasColumnName("UntaggedInventoryHistoryUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.UntaggedInventoryUid).HasColumnName("UntaggedInventoryUID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TblTechUntaggedInventoryHistory)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechUntaggedInventoryHistory_tblStatus");

                entity.HasOne(d => d.UntaggedInventoryU)
                    .WithMany(p => p.TblTechUntaggedInventoryHistory)
                    .HasForeignKey(d => d.UntaggedInventoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechUntaggedInventoryHistory_tblTechUntaggedInventory");
            });

            modelBuilder.Entity<TblTechUserDepartments>(entity =>
            {
                entity.HasKey(e => e.UserDepartmentUid);

                entity.ToTable("tblTechUserDepartments");

                entity.Property(e => e.UserDepartmentUid).HasColumnName("UserDepartmentUID");

                entity.Property(e => e.TechDepartmentUid).HasColumnName("TechDepartmentUID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ViewActive)
                    .IsRequired()
                    .HasDefaultValueSql("('1')");

                entity.HasOne(d => d.TechDepartmentU)
                    .WithMany(p => p.TblTechUserDepartments)
                    .HasForeignKey(d => d.TechDepartmentUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechUserDepartments_tblTechDepartments");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblTechUserDepartments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechUserDepartments_tblUser");
            });

            modelBuilder.Entity<TblTechUserPermissionTemplate>(entity =>
            {
                entity.HasKey(e => e.UserPermissionTemplateId);

                entity.ToTable("tblTechUserPermissionTemplate");

                entity.Property(e => e.UserPermissionTemplateId).HasColumnName("UserPermissionTemplateID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PermissionTemplateUid).HasColumnName("PermissionTemplateUID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.PermissionTemplateU)
                    .WithMany(p => p.TblTechUserPermissionTemplate)
                    .HasForeignKey(d => d.PermissionTemplateUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechUserPermissionTemplate_tblTechPermissionTemplate");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblTechUserPermissionTemplate)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechUserPermissionTemplate_tblUser");
            });

            modelBuilder.Entity<TblTechUserRecentTagSearches>(entity =>
            {
                entity.HasKey(e => e.RecentSearchUid);

                entity.ToTable("tblTechUserRecentTagSearches");

                entity.Property(e => e.RecentSearchUid).HasColumnName("RecentSearchUId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.SiteUid).HasColumnName("SiteUId");

                entity.Property(e => e.TagSearchUid).HasColumnName("TagSearchUId");
            });

            modelBuilder.Entity<TblTechUserRoleTypes>(entity =>
            {
                entity.HasKey(e => e.UserRoleTypeUid);

                entity.ToTable("tblTechUserRoleTypes");

                entity.HasIndex(e => e.UserRoleTypeName)
                    .HasName("IX_tblTechUserRoleTypes")
                    .IsUnique();

                entity.Property(e => e.UserRoleTypeUid).HasColumnName("UserRoleTypeUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserRoleTypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTechUserSites>(entity =>
            {
                entity.HasKey(e => e.UserSiteUid);

                entity.ToTable("tblTechUserSites");

                entity.Property(e => e.UserSiteUid).HasColumnName("UserSiteUID");

                entity.Property(e => e.SiteUid).HasColumnName("SiteUID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.SiteU)
                    .WithMany(p => p.TblTechUserSites)
                    .HasForeignKey(d => d.SiteUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechUserSites_tblTechSites");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblTechUserSites)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechUserSites_tblUser");
            });

            modelBuilder.Entity<TblTechUserTypeWorkflows>(entity =>
            {
                entity.HasKey(e => e.UserTypeWorkflowUid);

                entity.ToTable("tblTechUserTypeWorkflows");

                entity.Property(e => e.UserTypeWorkflowUid).HasColumnName("UserTypeWorkflowUID");

                entity.Property(e => e.AdditionalEmail).IsUnicode(false);

                entity.Property(e => e.ApprovalUserTypeUid).HasColumnName("ApprovalUserTypeUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TransferSiteUid).HasColumnName("TransferSiteUID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserRoleTypeUid).HasColumnName("UserRoleTypeUID");

                entity.Property(e => e.UserTypeUid).HasColumnName("UserTypeUID");

                entity.HasOne(d => d.TransferSiteU)
                    .WithMany(p => p.TblTechUserTypeWorkflows)
                    .HasForeignKey(d => d.TransferSiteUid)
                    .HasConstraintName("FK_tblTechUserTypeWorkflows_tblTechTransferSites");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblTechUserTypeWorkflows)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechUserTypeWorkflows_tblUser");

                entity.HasOne(d => d.UserRoleTypeU)
                    .WithMany(p => p.TblTechUserTypeWorkflows)
                    .HasForeignKey(d => d.UserRoleTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechUserTypeWorkflows_tblTechUserRoleTypes");

                entity.HasOne(d => d.UserTypeU)
                    .WithMany(p => p.TblTechUserTypeWorkflows)
                    .HasForeignKey(d => d.UserTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTechUserTypeWorkflows_tblUnvUserTypes");
            });

            modelBuilder.Entity<TblTempClosingDeleteExpired>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.ToTable("tblTempClosingDeleteExpired");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.Expire)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Isbn)
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Slc)
                    .HasColumnName("SLC")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TblTipwebImport>(entity =>
            {
                entity.HasKey(e => e.TipwebImportId);

                entity.ToTable("tblTIPWebImport");

                entity.Property(e => e.TipwebImportId).HasColumnName("TIPWebImportID");

                entity.Property(e => e.ImportStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Not Imported')");

                entity.Property(e => e.ImportType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Notes)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.RowsImported).HasDefaultValueSql("((0))");

                entity.Property(e => e.StepName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTransactionState>(entity =>
            {
                entity.ToTable("tblTransactionState");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ApplicationUid).HasColumnName("ApplicationUID");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.TransactionName).HasMaxLength(50);
            });

            modelBuilder.Entity<TblTransactionTypes>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.ToTable("tblTransactionTypes");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTransfer>(entity =>
            {
                entity.HasKey(e => e.TransferId);

                entity.ToTable("tblTransfer");

                entity.Property(e => e.TransferId).HasColumnName("TransferID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TargetCampus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TransferName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTransferDetails>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.ToTable("tblTransferDetails");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.SourceCampus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TransferId).HasColumnName("TransferID");

                entity.HasOne(d => d.Transfer)
                    .WithMany(p => p.TblTransferDetails)
                    .HasForeignKey(d => d.TransferId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTransferDetails_tblTransfer");
            });

            modelBuilder.Entity<TblUnvAlerts>(entity =>
            {
                entity.HasKey(e => e.AlertUid);

                entity.ToTable("tblUnvAlerts");

                entity.Property(e => e.AlertUid).HasColumnName("AlertUID");

                entity.Property(e => e.AlertBeginDate).HasColumnType("datetime");

                entity.Property(e => e.AlertExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.AlertMessage)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.AlertTitle)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.AlertTypeUid).HasColumnName("AlertTypeUID");

                entity.Property(e => e.ApplicationUid).HasColumnName("ApplicationUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Enabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.AlertTypeU)
                    .WithMany(p => p.TblUnvAlerts)
                    .HasForeignKey(d => d.AlertTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvAlerts_tblUnvAlertTypes");

                entity.HasOne(d => d.ApplicationU)
                    .WithMany(p => p.TblUnvAlerts)
                    .HasForeignKey(d => d.ApplicationUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvAlerts_tblUnvApplications");
            });

            modelBuilder.Entity<TblUnvAlertTypes>(entity =>
            {
                entity.HasKey(e => e.AlertTypeUid);

                entity.ToTable("tblUnvAlertTypes");

                entity.Property(e => e.AlertTypeUid).HasColumnName("AlertTypeUID");

                entity.Property(e => e.AlertTypeDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.AlertTypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApplicationUid).HasColumnName("ApplicationUID");

                entity.HasOne(d => d.ApplicationU)
                    .WithMany(p => p.TblUnvAlertTypes)
                    .HasForeignKey(d => d.ApplicationUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvAlertTypes_tblUnvApplications");
            });

            modelBuilder.Entity<TblUnvAlertUser>(entity =>
            {
                entity.HasKey(e => e.AlertUserUid);

                entity.ToTable("tblUnvAlertUser");

                entity.Property(e => e.AlertUserUid).HasColumnName("AlertUserUID");

                entity.Property(e => e.AlertUid).HasColumnName("AlertUID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.AlertU)
                    .WithMany(p => p.TblUnvAlertUser)
                    .HasForeignKey(d => d.AlertUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvAlertUser_tblUnvAlerts");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblUnvAlertUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvAlertUser_tblUser");
            });

            modelBuilder.Entity<TblUnvApplications>(entity =>
            {
                entity.HasKey(e => e.ApplicationUid);

                entity.ToTable("tblUnvApplications");

                entity.Property(e => e.ApplicationUid).HasColumnName("ApplicationUID");

                entity.Property(e => e.ApplicationDescription)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ApplicationName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblUnvArchives>(entity =>
            {
                entity.HasKey(e => e.ArchiveUid);

                entity.ToTable("tblUnvArchives");

                entity.Property(e => e.ArchiveUid).HasColumnName("ArchiveUID");

                entity.Property(e => e.ApplicationUid)
                    .HasColumnName("ApplicationUID")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.ArchiveDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ArchiveDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ArchiveNotes)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ArchiveUserId).HasColumnName("ArchiveUserID");
            });

            modelBuilder.Entity<TblUnvAreas>(entity =>
            {
                entity.HasKey(e => e.AreaUid);

                entity.ToTable("tblUnvAreas");

                entity.Property(e => e.AreaUid).HasColumnName("AreaUID");

                entity.Property(e => e.AreaName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblUnvAudits>(entity =>
            {
                entity.HasKey(e => e.AuditUid);

                entity.ToTable("tblUnvAudits");

                entity.HasIndex(e => new { e.AuditName, e.AuditUid })
                    .HasName("IX_tblUnvAudits_AuditName");

                entity.Property(e => e.AuditUid).HasColumnName("AuditUID");

                entity.Property(e => e.ApplicationUid).HasColumnName("ApplicationUID");

                entity.Property(e => e.ApprovedByUserId).HasColumnName("ApprovedByUserID");

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.AuditDueDate).HasColumnType("date");

                entity.Property(e => e.AuditName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AuditNotes)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExcludeScannedAfterDate).HasColumnType("datetime");

                entity.Property(e => e.LastEmailSendDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SiteUid).HasColumnName("SiteUID");

                entity.HasOne(d => d.ApplicationU)
                    .WithMany(p => p.TblUnvAudits)
                    .HasForeignKey(d => d.ApplicationUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvAudits_tblUnvApplications");
            });

            modelBuilder.Entity<TblUnvChargePayments>(entity =>
            {
                entity.HasKey(e => e.ChargePaymentUid);

                entity.ToTable("tblUnvChargePayments");

                entity.Property(e => e.ChargePaymentUid).HasColumnName("ChargePaymentUID");

                entity.Property(e => e.ApplicationUid).HasColumnName("ApplicationUID");

                entity.Property(e => e.ChargeAmount).HasColumnType("money");

                entity.Property(e => e.ChargeUid).HasColumnName("ChargeUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.PaymentSiteUid).HasColumnName("PaymentSiteUID");

                entity.HasOne(d => d.ChargeU)
                    .WithMany(p => p.TblUnvChargePayments)
                    .HasForeignKey(d => d.ChargeUid)
                    .HasConstraintName("FK_tblUnvChargePayments_tblUnvCharges");
            });

            modelBuilder.Entity<TblUnvCharges>(entity =>
            {
                entity.HasKey(e => e.ChargeUid);

                entity.ToTable("tblUnvCharges");

                entity.Property(e => e.ChargeUid).HasColumnName("ChargeUID");

                entity.Property(e => e.ApplicationUid).HasColumnName("ApplicationUID");

                entity.Property(e => e.ChargeAmount).HasColumnType("money");

                entity.Property(e => e.ChargeSiteUid).HasColumnName("ChargeSiteUID");

                entity.Property(e => e.ChargeTypeUid).HasColumnName("ChargeTypeUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateSatisfied).HasColumnType("datetime");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.EntityTypeUid).HasColumnName("EntityTypeUID");

                entity.Property(e => e.EntityUid).HasColumnName("EntityUID");

                entity.Property(e => e.ItemTypeUid).HasColumnName("ItemTypeUID");

                entity.Property(e => e.ItemUid).HasColumnName("ItemUID");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.UniversalId)
                    .HasColumnName("UniversalID")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.ChargeTypeU)
                    .WithMany(p => p.TblUnvCharges)
                    .HasForeignKey(d => d.ChargeTypeUid)
                    .HasConstraintName("FK_tblUnvCharges_tblUnvChargeTypes");
            });

            modelBuilder.Entity<TblUnvChargeTypeCategories>(entity =>
            {
                entity.HasKey(e => e.ChargeTypeCategoryUid);

                entity.ToTable("tblUnvChargeTypeCategories");

                entity.Property(e => e.ChargeTypeCategoryUid).HasColumnName("ChargeTypeCategoryUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblUnvChargeTypeChargeTypeCategory>(entity =>
            {
                entity.HasKey(e => e.ChargeTypeChargeTypeCategoryUid);

                entity.ToTable("tblUnvChargeTypeChargeTypeCategory");

                entity.Property(e => e.ChargeTypeChargeTypeCategoryUid).HasColumnName("ChargeTypeChargeTypeCategoryUID");

                entity.Property(e => e.ChargeTypeCategoryUid).HasColumnName("ChargeTypeCategoryUID");

                entity.Property(e => e.ChargeTypeUid).HasColumnName("ChargeTypeUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.ChargeTypeCategoryU)
                    .WithMany(p => p.TblUnvChargeTypeChargeTypeCategory)
                    .HasForeignKey(d => d.ChargeTypeCategoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvChargeTypeChargeTypeCategory_tblUnvChargeTypeCategories");

                entity.HasOne(d => d.ChargeTypeU)
                    .WithMany(p => p.TblUnvChargeTypeChargeTypeCategory)
                    .HasForeignKey(d => d.ChargeTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvChargeTypeChargeTypeCategory_tblUnvChargeTypes");
            });

            modelBuilder.Entity<TblUnvChargeTypes>(entity =>
            {
                entity.HasKey(e => e.ChargeTypeUid);

                entity.ToTable("tblUnvChargeTypes");

                entity.Property(e => e.ChargeTypeUid).HasColumnName("ChargeTypeUID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ApplicationUid).HasColumnName("ApplicationUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('No Name Assigned')");
            });

            modelBuilder.Entity<TblUnvClosings>(entity =>
            {
                entity.HasKey(e => e.ClosingUid);

                entity.ToTable("tblUnvClosings");

                entity.Property(e => e.ClosingUid).HasColumnName("ClosingUID");

                entity.Property(e => e.ApplicationUid).HasColumnName("ApplicationUID");

                entity.Property(e => e.ClosingCampusUid).HasColumnName("ClosingCampusUID");

                entity.Property(e => e.ClosingDate).HasColumnType("datetime");

                entity.Property(e => e.ClosingDescription)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ClosingNote)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ClosingTypeUid).HasColumnName("ClosingTypeUID");

                entity.Property(e => e.ClosingUserId).HasColumnName("ClosingUserID");

                entity.HasOne(d => d.ApplicationU)
                    .WithMany(p => p.TblUnvClosings)
                    .HasForeignKey(d => d.ApplicationUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvClosing_tblUnvApplications");

                entity.HasOne(d => d.ClosingTypeU)
                    .WithMany(p => p.TblUnvClosings)
                    .HasForeignKey(d => d.ClosingTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvClosing_tblUnvClosingTypes");

                entity.HasOne(d => d.ClosingUser)
                    .WithMany(p => p.TblUnvClosings)
                    .HasForeignKey(d => d.ClosingUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvClosing_tblUser");
            });

            modelBuilder.Entity<TblUnvClosingTypes>(entity =>
            {
                entity.HasKey(e => e.ClosingTypeUid);

                entity.ToTable("tblUnvClosingTypes");

                entity.Property(e => e.ClosingTypeUid)
                    .HasColumnName("ClosingTypeUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ClosingType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblUnvCounter>(entity =>
            {
                entity.HasKey(e => e.CounterUid);

                entity.ToTable("tblUnvCounter");

                entity.Property(e => e.CounterUid).HasColumnName("CounterUID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblUnvDistrictPreferences>(entity =>
            {
                entity.HasKey(e => e.DistrictPreferenceUid);

                entity.ToTable("tblUnvDistrictPreferences");

                entity.Property(e => e.DistrictPreferenceUid).HasColumnName("DistrictPreferenceUID");

                entity.Property(e => e.ApplicationUid).HasColumnName("ApplicationUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DistrictPreferenceDescription).IsUnicode(false);

                entity.Property(e => e.DistrictPreferenceName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DistrictPreferenceValue)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('No Value Set')");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ApplicationU)
                    .WithMany(p => p.TblUnvDistrictPreferences)
                    .HasForeignKey(d => d.ApplicationUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvDistrictPreferences_tblUnvApplications");
            });

            modelBuilder.Entity<TblUnvEntityTypes>(entity =>
            {
                entity.HasKey(e => e.EntityTypeUid);

                entity.ToTable("tblUnvEntityTypes");

                entity.Property(e => e.EntityTypeUid).HasColumnName("EntityTypeUID");

                entity.Property(e => e.ApplicationUid).HasColumnName("ApplicationUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.IdentityColumn)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblUnvFunctions>(entity =>
            {
                entity.HasKey(e => e.FunctionUid);

                entity.ToTable("tblUnvFunctions");

                entity.HasIndex(e => e.FunctionName)
                    .HasName("UQ_tblUnvFunctions")
                    .IsUnique();

                entity.Property(e => e.FunctionUid).HasColumnName("FunctionUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FunctionDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.FunctionName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblUnvGrades>(entity =>
            {
                entity.HasKey(e => e.GradeUid);

                entity.ToTable("tblUnvGrades");

                entity.HasIndex(e => e.GradeName);

                entity.Property(e => e.GradeUid).HasColumnName("GradeUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.GradeDescription)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GradeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblUnvImportTypes>(entity =>
            {
                entity.HasKey(e => e.ImportTypeUid);

                entity.ToTable("tblUnvImportTypes");

                entity.Property(e => e.ImportTypeUid).HasColumnName("ImportTypeUID");

                entity.Property(e => e.ApplicationUid).HasColumnName("ApplicationUID");

                entity.Property(e => e.ImportTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<TblUnvItemTypes>(entity =>
            {
                entity.HasKey(e => e.ItemTypeUid);

                entity.ToTable("tblUnvItemTypes");

                entity.Property(e => e.ItemTypeUid).HasColumnName("ItemTypeUID");

                entity.Property(e => e.ApplicationUid).HasColumnName("ApplicationUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdentityColumn)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblUnvLogs>(entity =>
            {
                entity.HasKey(e => e.LogUid);

                entity.ToTable("tblUnvLogs");

                entity.Property(e => e.LogUid).HasColumnName("LogUID");

                entity.Property(e => e.ApplicationUid).HasColumnName("ApplicationUID");

                entity.Property(e => e.CreatedByUserUid).HasColumnName("CreatedByUserUID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedByUserUid).HasColumnName("LastModifiedByUserUID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.LogEntry)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.LogTypeUid).HasColumnName("LogTypeUID");
            });

            modelBuilder.Entity<TblUnvLogTypes>(entity =>
            {
                entity.HasKey(e => e.LogTypeUid);

                entity.ToTable("tblUnvLogTypes");

                entity.Property(e => e.LogTypeUid).HasColumnName("LogTypeUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.LogTypeDescription)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LogTypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblUnvManufacturers>(entity =>
            {
                entity.HasKey(e => e.ManufacturerUid);

                entity.ToTable("tblUnvManufacturers");

                entity.Property(e => e.ManufacturerUid).HasColumnName("ManufacturerUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ManufacturerName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblUnvRecipient>(entity =>
            {
                entity.HasKey(e => e.RecipientUid);

                entity.ToTable("tblUnvRecipient");

                entity.Property(e => e.RecipientUid).HasColumnName("RecipientUID");

                entity.Property(e => e.ApplicationUid).HasColumnName("ApplicationUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RecipientTypeUid).HasColumnName("RecipientTypeUID");

                entity.HasOne(d => d.ApplicationU)
                    .WithMany(p => p.TblUnvRecipient)
                    .HasForeignKey(d => d.ApplicationUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvRecipient_tblUnvApplications");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblUnvRecipient)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvRecipient_CreatedByUser");

                entity.HasOne(d => d.RecipientTypeU)
                    .WithMany(p => p.TblUnvRecipient)
                    .HasForeignKey(d => d.RecipientTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvRecipient_tblUnvRecipientType");
            });

            modelBuilder.Entity<TblUnvRecipientInformation>(entity =>
            {
                entity.HasKey(e => e.RecipientInformationUid);

                entity.ToTable("tblUnvRecipientInformation");

                entity.Property(e => e.RecipientInformationUid).HasColumnName("RecipientInformationUID");

                entity.Property(e => e.ApplicationUid).HasColumnName("ApplicationUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailAddresses).IsUnicode(false);

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RecipientUid).HasColumnName("RecipientUID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.ApplicationU)
                    .WithMany(p => p.TblUnvRecipientInformation)
                    .HasForeignKey(d => d.ApplicationUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvRecipientInformation_tblUnvApplications");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblUnvRecipientInformationCreatedByUser)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvRecipientInformation_CreatedByUser");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TblUnvRecipientInformationLastModifiedByUser)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .HasConstraintName("FK_tblUnvRecipientInformation_LastModifiedByUser");

                entity.HasOne(d => d.RecipientU)
                    .WithMany(p => p.TblUnvRecipientInformation)
                    .HasForeignKey(d => d.RecipientUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvRecipientInformation_tblUnvRecipient");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblUnvRecipientInformationUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_tblUnvRecipientInformation_User");
            });

            modelBuilder.Entity<TblUnvRecipientType>(entity =>
            {
                entity.HasKey(e => e.RecipientTypeUid);

                entity.ToTable("tblUnvRecipientType");

                entity.Property(e => e.RecipientTypeUid).HasColumnName("RecipientTypeUID");

                entity.Property(e => e.ApplicationUid).HasColumnName("ApplicationUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RecipientTypeName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.ApplicationU)
                    .WithMany(p => p.TblUnvRecipientType)
                    .HasForeignKey(d => d.ApplicationUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvRecipientType_tblUnvApplications");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblUnvRecipientType)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvRecipientType_CreatedByUser");
            });

            modelBuilder.Entity<TblUnvRooms>(entity =>
            {
                entity.HasKey(e => e.RoomUid);

                entity.ToTable("tblUnvRooms");

                entity.HasIndex(e => new { e.RoomUid, e.Active, e.SiteUid, e.RoomTypeUid })
                    .HasName("IX_tblUnvRooms_SUID_RTUID_iRoomUID_iActive");

                entity.Property(e => e.RoomUid).HasColumnName("RoomUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RoomDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.RoomNotes)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.RoomNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoomOther)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.RoomTypeUid).HasColumnName("RoomTypeUID");

                entity.Property(e => e.SiteUid).HasColumnName("SiteUID");

                entity.HasOne(d => d.RoomTypeU)
                    .WithMany(p => p.TblUnvRooms)
                    .HasForeignKey(d => d.RoomTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvRooms_tblUnvRoomTypes");
            });

            modelBuilder.Entity<TblUnvRoomTypes>(entity =>
            {
                entity.HasKey(e => e.RoomTypeUid);

                entity.ToTable("tblUnvRoomTypes");

                entity.Property(e => e.RoomTypeUid).HasColumnName("RoomTypeUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RoomTypeDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.RoomTypeName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblUnvSavedActivities>(entity =>
            {
                entity.HasKey(e => e.SavedActivityUid);

                entity.ToTable("tblUnvSavedActivities");

                entity.Property(e => e.SavedActivityUid).HasColumnName("SavedActivityUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DisplayText)
                    .HasMaxLength(110)
                    .IsUnicode(false);

                entity.Property(e => e.EntityTypeUid).HasColumnName("EntityTypeUID");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Parameter)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ParameterTypeUid).HasColumnName("ParameterTypeUID");

                entity.HasOne(d => d.EntityTypeU)
                    .WithMany(p => p.TblUnvSavedActivities)
                    .HasForeignKey(d => d.EntityTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvSavedActivities_tblUnvEntityTypes");
            });

            modelBuilder.Entity<TblUnvSchedule>(entity =>
            {
                entity.HasKey(e => e.ScheduleUid);

                entity.ToTable("tblUnvSchedule");

                entity.Property(e => e.ScheduleUid).HasColumnName("ScheduleUID");

                entity.Property(e => e.ApplicationUid).HasColumnName("ApplicationUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.LastRunTime).HasColumnType("datetime");

                entity.Property(e => e.RecipientUid).HasColumnName("RecipientUID");

                entity.Property(e => e.ScheduleReportTypeUid).HasColumnName("ScheduleReportTypeUID");

                entity.Property(e => e.ScheduleTypeUid).HasColumnName("ScheduleTypeUID");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblUnvScheduleCreatedByUser)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvSchedule_CreatedByUser");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TblUnvScheduleLastModifiedByUser)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .HasConstraintName("FK_tblUnvSchedule_LastModifiedByUser");

                entity.HasOne(d => d.RecipientU)
                    .WithMany(p => p.TblUnvSchedule)
                    .HasForeignKey(d => d.RecipientUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvSchedule_RecipientUID");

                entity.HasOne(d => d.ScheduleReportTypeU)
                    .WithMany(p => p.TblUnvSchedule)
                    .HasForeignKey(d => d.ScheduleReportTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvSchedule_ScheduleReportTypeUID");

                entity.HasOne(d => d.ScheduleTypeU)
                    .WithMany(p => p.TblUnvSchedule)
                    .HasForeignKey(d => d.ScheduleTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvSchedule_ScheduleTypeUID");
            });

            modelBuilder.Entity<TblUnvScheduleDay>(entity =>
            {
                entity.HasKey(e => e.ScheduleDayUid);

                entity.ToTable("tblUnvScheduleDay");

                entity.Property(e => e.ScheduleDayUid).HasColumnName("ScheduleDayUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ScheduleDayName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ScheduleTypeUid).HasColumnName("ScheduleTypeUID");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblUnvScheduleDay)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvScheduleDay_CreatedByUser");

                entity.HasOne(d => d.ScheduleTypeU)
                    .WithMany(p => p.TblUnvScheduleDay)
                    .HasForeignKey(d => d.ScheduleTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvScheduleDay_tblUnvScheduleType");
            });

            modelBuilder.Entity<TblUnvScheduleDayAssigned>(entity =>
            {
                entity.HasKey(e => e.ScheduleDayAssignedUid);

                entity.ToTable("tblUnvScheduleDayAssigned");

                entity.Property(e => e.ScheduleDayAssignedUid).HasColumnName("ScheduleDayAssignedUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ScheduleDayUid).HasColumnName("ScheduleDayUID");

                entity.Property(e => e.ScheduleTypeUid).HasColumnName("ScheduleTypeUID");

                entity.Property(e => e.ScheduleUid).HasColumnName("ScheduleUID");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblUnvScheduleDayAssignedCreatedByUser)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvScheduleDayAssigned_CreatedByUser");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TblUnvScheduleDayAssignedLastModifiedByUser)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .HasConstraintName("FK_tblUnvScheduleDayAssigned_LastModifiedByUser");

                entity.HasOne(d => d.ScheduleDayU)
                    .WithMany(p => p.TblUnvScheduleDayAssigned)
                    .HasForeignKey(d => d.ScheduleDayUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvScheduleDayAssigned_tblUnvScheduleDay");

                entity.HasOne(d => d.ScheduleTypeU)
                    .WithMany(p => p.TblUnvScheduleDayAssigned)
                    .HasForeignKey(d => d.ScheduleTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvScheduleDayAssigned_tblUnvScheduleType");

                entity.HasOne(d => d.ScheduleU)
                    .WithMany(p => p.TblUnvScheduleDayAssigned)
                    .HasForeignKey(d => d.ScheduleUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvScheduleDayAssigned_tblUnvSchedule");
            });

            modelBuilder.Entity<TblUnvScheduleReportType>(entity =>
            {
                entity.HasKey(e => e.ScheduleReportTypeUid);

                entity.ToTable("tblUnvScheduleReportType");

                entity.Property(e => e.ScheduleReportTypeUid).HasColumnName("ScheduleReportTypeUID");

                entity.Property(e => e.ApplicationUid).HasColumnName("ApplicationUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ScheduleReportTypeName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.ApplicationU)
                    .WithMany(p => p.TblUnvScheduleReportType)
                    .HasForeignKey(d => d.ApplicationUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvScheduleReportType_tblUnvApplications");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblUnvScheduleReportType)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvScheduleReportType_CreatedByUser");
            });

            modelBuilder.Entity<TblUnvScheduleType>(entity =>
            {
                entity.HasKey(e => e.ScheduleTypeUid);

                entity.ToTable("tblUnvScheduleType");

                entity.Property(e => e.ScheduleTypeUid).HasColumnName("ScheduleTypeUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ScheduleTypeName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblUnvScheduleType)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvScheduleType_CreatedByUser");
            });

            modelBuilder.Entity<TblUnvSerializedObjects>(entity =>
            {
                entity.ToTable("tblUnvSerializedObjects");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApplicationUid).HasColumnName("ApplicationUID");

                entity.Property(e => e.CreatedByUserUid).HasColumnName("CreatedByUserUID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedByUserUid).HasColumnName("LastModifiedByUserUID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ObjectTypeUid).HasColumnName("ObjectTypeUID");

                entity.Property(e => e.SerilalizedObj)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.SiteUid).HasColumnName("SiteUID");

                entity.Property(e => e.UserUid).HasColumnName("UserUID");
            });

            modelBuilder.Entity<TblUnvSignature>(entity =>
            {
                entity.HasKey(e => e.SignatureUid);

                entity.ToTable("tblUnvSignature");

                entity.Property(e => e.SignatureUid).HasColumnName("SignatureUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PrintedName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Signature).IsRequired();
            });

            modelBuilder.Entity<TblUnvStaffTypes>(entity =>
            {
                entity.HasKey(e => e.StaffTypeUid);

                entity.ToTable("tblUnvStaffTypes");

                entity.Property(e => e.StaffTypeUid).HasColumnName("StaffTypeUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StaffTypeDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.StaffTypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblUnvSupportLinks>(entity =>
            {
                entity.HasKey(e => e.SupportLinkUid);

                entity.ToTable("tblUnvSupportLinks");

                entity.Property(e => e.SupportLinkUid)
                    .HasColumnName("SupportLinkUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ApplicationUid).HasColumnName("ApplicationUID");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StepByStepUrl)
                    .HasColumnName("StepByStepURL")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.VideoUrl)
                    .HasColumnName("VideoURL")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.ApplicationU)
                    .WithMany(p => p.TblUnvSupportLinks)
                    .HasForeignKey(d => d.ApplicationUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvSupportLinks_tblUnvApplications");
            });

            modelBuilder.Entity<TblUnvUserPreferences>(entity =>
            {
                entity.HasKey(e => e.UserPreferenceUid);

                entity.ToTable("tblUnvUserPreferences");

                entity.Property(e => e.UserPreferenceUid).HasColumnName("UserPreferenceUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserPreferenceDescription)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UserPreferenceName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UserPreferenceValue)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblUnvUserPreferences)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvUserPreferences_tblUser");
            });

            modelBuilder.Entity<TblUnvUserRoles>(entity =>
            {
                entity.HasKey(e => e.UserRoleUid);

                entity.ToTable("tblUnvUserRoles");

                entity.Property(e => e.UserRoleUid).HasColumnName("UserRoleUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserRoleDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.UserRoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ViewUid).HasColumnName("ViewUID");

                entity.HasOne(d => d.ViewU)
                    .WithMany(p => p.TblUnvUserRoles)
                    .HasForeignKey(d => d.ViewUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUnvUserRoles_tblUnvViews");
            });

            modelBuilder.Entity<TblUnvUserTypes>(entity =>
            {
                entity.HasKey(e => e.UserTypeUid);

                entity.ToTable("tblUnvUserTypes");

                entity.HasIndex(e => e.UserTypeName)
                    .HasName("IX_tblUnvUserTypes")
                    .IsUnique();

                entity.Property(e => e.UserTypeUid).HasColumnName("UserTypeUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.UserTypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblUnvViews>(entity =>
            {
                entity.HasKey(e => e.ViewUid);

                entity.ToTable("tblUnvViews");

                entity.Property(e => e.ViewUid).HasColumnName("ViewUID");

                entity.Property(e => e.ApplicationUid).HasColumnName("ApplicationUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ViewDescription)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ViewName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblUsageTypes>(entity =>
            {
                entity.HasKey(e => e.UsageTypeUid);

                entity.ToTable("tblUsageTypes");

                entity.Property(e => e.UsageTypeUid)
                    .HasColumnName("UsageTypeUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.UsageType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("tblUser");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ApplicationUid)
                    .HasColumnName("ApplicationUID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CampusId)
                    .IsRequired()
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ldapid).HasColumnName("LDAPID");

                entity.Property(e => e.Ldapuser).HasColumnName("LDAPUser");

                entity.Property(e => e.LoginName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.ParentUserId).HasColumnName("ParentUserID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Race)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RealName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SessionExpires).HasColumnType("datetime");

                entity.Property(e => e.SessionId).HasColumnName("SessionID");

                entity.Property(e => e.SiteUid)
                    .HasColumnName("SiteUID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.UserRoleUid)
                    .HasColumnName("UserRoleUID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UserTypeUid).HasColumnName("UserTypeUID");

                entity.Property(e => e.Zip)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.ApplicationU)
                    .WithMany(p => p.TblUser)
                    .HasForeignKey(d => d.ApplicationUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUser_tblUnvApplications");

                entity.HasOne(d => d.SiteU)
                    .WithMany(p => p.TblUser)
                    .HasForeignKey(d => d.SiteUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUser_tblTechSites");

                entity.HasOne(d => d.UserRoleU)
                    .WithMany(p => p.TblUser)
                    .HasForeignKey(d => d.UserRoleUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUser_tblUnvUserRoles");

                entity.HasOne(d => d.UserTypeU)
                    .WithMany(p => p.TblUser)
                    .HasForeignKey(d => d.UserTypeUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUser_tblUnvUserTypes");
            });

            modelBuilder.Entity<TblUserCampuses>(entity =>
            {
                entity.HasKey(e => e.UserCampusUid);

                entity.ToTable("tblUserCampuses");

                entity.Property(e => e.UserCampusUid).HasColumnName("UserCampusUID");

                entity.Property(e => e.CampusUid).HasColumnName("CampusUID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.CampusU)
                    .WithMany(p => p.TblUserCampuses)
                    .HasForeignKey(d => d.CampusUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUserCampuses_tblCampuses");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblUserCampuses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUserCampuses_tblUser");
            });

            modelBuilder.Entity<TblUserFunctions>(entity =>
            {
                entity.HasKey(e => e.UserFunctionUid);

                entity.ToTable("tblUserFunctions");

                entity.HasIndex(e => new { e.UserId, e.FunctionUid })
                    .HasName("UQ_tblUserFunctions")
                    .IsUnique();

                entity.Property(e => e.UserFunctionUid).HasColumnName("UserFunctionUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FunctionUid).HasColumnName("FunctionUID");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.FunctionU)
                    .WithMany(p => p.TblUserFunctions)
                    .HasForeignKey(d => d.FunctionUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUserFunctions_tblUnvFunctions");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblUserFunctions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUserFunctions_tblUser");
            });

            modelBuilder.Entity<TblUserGroups>(entity =>
            {
                entity.HasKey(e => e.GroupId);

                entity.ToTable("tblUserGroups");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.AccessCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblUserGroupSettings>(entity =>
            {
                entity.HasKey(e => e.GroupId);

                entity.ToTable("tblUserGroupSettings");

                entity.Property(e => e.GroupId)
                    .HasColumnName("GroupID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ChangeIsbn).HasColumnName("ChangeISBN");

                entity.Property(e => e.ChangeStudentId).HasColumnName("ChangeStudentID");

                entity.Property(e => e.FindAbook).HasColumnName("FindABook");

                entity.Property(e => e.MasterTitlesDb).HasColumnName("MasterTitlesDB");
            });

            modelBuilder.Entity<TblUserPreferences>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("tblUserPreferences");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AdjustmentPrefix)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BarCodeText)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DistributeSearch1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DistributeSearch2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Enrollment)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FindAbook)
                    .HasColumnName("FindABook")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GcodeDefault).HasColumnName("GCodeDefault");

                entity.Property(e => e.Host)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastAdjNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastReqNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LeftMargin)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Maintenance).HasColumnType("datetime");

                entity.Property(e => e.NetworkPath)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Other1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Other2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PickTicket)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RepairDay).HasColumnType("datetime");

                entity.Property(e => e.RepairTime).HasColumnType("datetime");

                entity.Property(e => e.SearchRelate1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SearchRelate2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SpecifyAdjNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SpecifyReqNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TopMargin)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblUserRoleFunctions>(entity =>
            {
                entity.HasKey(e => e.UserRoleFunctionUid);

                entity.ToTable("tblUserRoleFunctions");

                entity.HasIndex(e => new { e.UserRoleUid, e.FunctionUid })
                    .HasName("UQ_tblUserRoleFunctions")
                    .IsUnique();

                entity.Property(e => e.UserRoleFunctionUid).HasColumnName("UserRoleFunctionUID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FunctionUid).HasColumnName("FunctionUID");

                entity.Property(e => e.LastModifiedByUserId).HasColumnName("LastModifiedByUserID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserRoleUid).HasColumnName("UserRoleUID");

                entity.HasOne(d => d.FunctionU)
                    .WithMany(p => p.TblUserRoleFunctions)
                    .HasForeignKey(d => d.FunctionUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUserRoleFunctions_tblUnvFunctions");

                entity.HasOne(d => d.UserRoleU)
                    .WithMany(p => p.TblUserRoleFunctions)
                    .HasForeignKey(d => d.UserRoleUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUserRoleFunctions_tblUnvUserRoles");
            });

            modelBuilder.Entity<TblVendor>(entity =>
            {
                entity.HasKey(e => e.VendorId);

                entity.ToTable("tblVendor");

                entity.HasIndex(e => e.VendorName)
                    .HasName("IDX_VendorName");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.Property(e => e.AccountNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ApplicationUid)
                    .HasColumnName("ApplicationUID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CampusId)
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Contact)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.VendorName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Zip)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.ApplicationU)
                    .WithMany(p => p.TblVendor)
                    .HasForeignKey(d => d.ApplicationUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblVendor_tblUnvApplications");
            });

            modelBuilder.Entity<TblVendorBooks>(entity =>
            {
                entity.HasKey(e => new { e.VendorId, e.Isbn });

                entity.ToTable("tblVendorBooks");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.Property(e => e.Isbn)
                    .HasColumnName("ISBN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.TblVendorBooks)
                    .HasForeignKey(d => d.VendorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblVendorBooks_tblVendor");
            });

            modelBuilder.Entity<TblVendorOrderDetails>(entity =>
            {
                entity.HasKey(e => e.VendorOrderDetailsUid);

                entity.ToTable("tblVendorOrderDetails");

                entity.HasIndex(e => new { e.Received, e.Price, e.Ordered, e.BookInventoryUid, e.StatusId, e.VendorOrderUid })
                    .HasName("_dta_index_tblVendorOrderDetails_8_1140355277__K3_K9_K2_5_7_10");

                entity.Property(e => e.VendorOrderDetailsUid).HasColumnName("VendorOrderDetailsUID");

                entity.Property(e => e.BookInventoryUid).HasColumnName("BookInventoryUID");

                entity.Property(e => e.DateArriving).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.DateOrdered).HasColumnType("datetime");

                entity.Property(e => e.DateReceived).HasColumnType("datetime");

                entity.Property(e => e.FundingSourceUid).HasColumnName("FundingSourceUID");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.VendorOrderUid).HasColumnName("VendorOrderUID");

                entity.HasOne(d => d.BookInventoryU)
                    .WithMany(p => p.TblVendorOrderDetails)
                    .HasForeignKey(d => d.BookInventoryUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblVendorOrderDetails_tblBookInventory");

                entity.HasOne(d => d.VendorOrderU)
                    .WithMany(p => p.TblVendorOrderDetails)
                    .HasForeignKey(d => d.VendorOrderUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblVendorOrderDetails_tblVendorOrders");
            });

            modelBuilder.Entity<TblVendorOrderDetailsHistory>(entity =>
            {
                entity.HasKey(e => e.VendorOrderDetailsHistoryUid);

                entity.ToTable("tblVendorOrderDetailsHistory");

                entity.Property(e => e.VendorOrderDetailsHistoryUid).HasColumnName("VendorOrderDetailsHistoryUID");

                entity.Property(e => e.DateReceived).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.VendorOrderDetailsUid).HasColumnName("VendorOrderDetailsUID");
            });

            modelBuilder.Entity<TblVendorOrders>(entity =>
            {
                entity.HasKey(e => e.VendorOrderUid);

                entity.ToTable("tblVendorOrders");

                entity.Property(e => e.VendorOrderUid).HasColumnName("VendorOrderUID");

                entity.Property(e => e.ArrivalDate).HasColumnType("datetime");

                entity.Property(e => e.CampusId)
                    .HasColumnName("CampusID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateSubmitted).HasColumnType("datetime");

                entity.Property(e => e.DistrictCreated).HasDefaultValueSql("((1))");

                entity.Property(e => e.FundingSourceUid).HasColumnName("FundingSourceUID");

                entity.Property(e => e.PurchaseOrder)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SpecialInstructions)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.Property(e => e.VendorOrderId)
                    .HasColumnName("VendorOrderID")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.HasOne(d => d.FundingSourceU)
                    .WithMany(p => p.TblVendorOrders)
                    .HasForeignKey(d => d.FundingSourceUid)
                    .HasConstraintName("FK_tblVendorOrders_tblFundingSources");
            });

            modelBuilder.Entity<TblVersion>(entity =>
            {
                entity.HasKey(e => e.VersionUid);

                entity.ToTable("tblVersion");

                entity.Property(e => e.VersionUid).HasColumnName("VersionUID");

                entity.Property(e => e.ApplicationUid).HasColumnName("ApplicationUID");

                entity.Property(e => e.CombinedVersion)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.DateReleased).HasColumnType("datetime");

                entity.Property(e => e.Informational)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Version)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
