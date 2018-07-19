namespace MiddleWay_DTO.Enumerations
{
    public enum ChangeTypeEnum
    {
        Error = 1,
        Activity = 2,
        RejectRecord = 3
    }

    public enum CounterTypesEnum
    {
        Tag = 1,
        Transfer = 2,
        TempAccession = 3,
        ProductNumber = 4,
        TicketNumber = 5,
        ContainerNumber = 6
    }

    public enum NotificationType
    {
        Email = 1,
        Sms = 2
    }

    public enum TextEncoding
    {
        ASCII,
        Unicode,
        UTF8,
        GSM,
        UCS2
    }

    public enum DataSourceTypes
    {
        FlatFile = 0,
        SQL = 1,
        MySQL = 2,
        API = 3,
        Other
    }

    public enum InventoryTypeEnum
    {
        Tagged = 1,
        Untagged = 2
    }

    public enum EntityTypeEnum
    {
        Site = 1,
        Room = 2,
        Staff = 3,
        Student = 4,
        Hardware = 5,
        Transfer = 6,
        Purchase = 7
    }

    public enum ImportType
    {
        PurchaseOrder,
        MobileDeviceManagement,
        InTouchPayments
    }

    public enum ApplicationCode
    {
        TIPIDCore = 0,
        TIPWebIM = 1,
        TIPWebIT = 2
    }

    public enum StatusTypes
    {
        Adjustments = 1,
        VendorOrders = 2,
        Requisitions = 3,
        Transfers = 4,
        Generic = 5,
        Room = 6,
        Student = 7,
        Staff = 8,
        Receiving = 9,
        BookOrders = 10,
        Audits = 11,
        AuditInventoryStates = 12,
        RoomAudits = 13,
        RequisitionOrders = 14
    }


    public enum AdjustmentsStatus
    {
        New = 18,
        Submitted = 19,
        Complete = 20,
        CallTag = 21,
        InProgress = 22
    }

    public enum AuditInventoryStatesStatus
    {
        Missing = 50,
        Misplaced = 51,
        Verified = 52,
        Found = 98
    }

    public enum AuditsStatus
    {
        New = 43,
        InProgress = 44,
        Submitted = 45,
        Complete = 46,
        Finalized = 62,
        InReview = 72,
        Verified = 73
    }

    public enum BookOrdersStatus
    {
        Created = 34,
        Submitted = 35,
        Denied = 36,
        Approved = 37,
        Ticketed = 38,
        Shipped = 39,
        Received = 40
    }

    public enum GenericStatus
    {
        Inactive = 0,
        Active = 1
    }

    public enum ReceivingStatus
    {
        Open = 32,
        Closed = 33,
        InTransit = 58
    }
    public enum RequisitionOrdersStatus
    {
        InProgress = 74,
        Submitted = 75
    }
    public enum RequisitionsStatus
    {
        Submitted = 13,
        InProgress = 14,
        InTransit = 15,
        Complete = 16,
        New = 17
    }

    public enum RoomStatus
    {
        Available = 26,
        InUse = 28,
        Lost = 49,
        PendingTransfer = 57,
        Disposed = 61,
        Stolen = 63,
        ReturnedtoVendor = 66,
        InRepair = 69,
        Sold = 77,
        UsedforParts = 80,
        Auctioned = 81,
        Surplus = 84,
        Recycled = 89,
        PendingLost = 99,
        PendingStolen = 102
    }

    public enum RoomAuditsStatus
    {
        New = 53,
        InProgress = 54,
        Finalized = 55,
        Closed = 56
    }

    public enum StaffStatus
    {
        InUse = 31,
        Lost = 48,
        Disposed = 60,
        Stolen = 65,
        ReturnedtoVendor = 68,
        InRepair = 71,
        Sold = 79,
        Auctioned = 83,
        Surplus = 86,
        UsedForParts = 88,
        Recycled = 91,
        PendingLost = 101,
        PendingStolen = 104
    }

    public enum StudentStatus
    {
        InUse = 30,
        Lost = 47,
        Disposed = 59,
        Stolen = 64,
        ReturnedtoVendor = 67,
        InRepair = 70,
        Sold = 78,
        Auctioned = 82,
        Surplus = 85,
        UsedForParts = 87,
        Recycled = 90,
        PendingLost = 100,
        PendingStolen = 103
    }

    public enum TransfersStatus
    {
        New = 23,
        InTransit = 24,
        Complete = 25,
        Transferred = 76,
        PendingApproval = 92,
        Submitted = 93,
        Scheduled = 94,
        Delayed = 95,
        Delivered = 96,
        Receiving = 97,
        NewRequest = 105,
        RequestSubmitted = 106,
        RequestDenied = 107
    }

    public enum VendorOrdersStatus
    {
        Complete = 2,
        InProgress = 4,
        Pending = 7
    }

    public enum TransformationFunctions
    {
        DEFAULT = 0,
        LOOKUP = 1,
        SPLIT = 2,
        TRUNCATE = 3,
        ROUNDDOWN = 4,
        ROUNDUP = 5
    }

    public enum ProcessSteps
    {
        CleanUp,
        Ingest,
        Stage,
        ProcessCommands,
        Validate,
        Upload,
        Export
    }
}
