using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

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

    public enum MessageType
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
}
