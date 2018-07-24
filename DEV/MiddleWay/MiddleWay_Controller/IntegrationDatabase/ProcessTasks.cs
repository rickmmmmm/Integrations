using System;
using System.Collections.Generic;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public partial class ProcessTasks
    {
        public ProcessTasks()
        {
            EtlDetails = new HashSet<EtlDetails>();
            EtlHeaders = new HashSet<EtlHeaders>();
            EtlInventory = new HashSet<EtlInventory>();
            EtlProducts = new HashSet<EtlProducts>();
            EtlRawFile = new HashSet<EtlRawFile>();
            EtlShipments = new HashSet<EtlShipments>();
            InventoryFlatData = new HashSet<InventoryFlatData>();
            ProcessTaskSteps = new HashSet<ProcessTaskSteps>();
            ProcessTasksErrors = new HashSet<ProcessTasksErrors>();
            ProductsFlatData = new HashSet<ProductsFlatData>();
            PurchaseInvoiceFlatData = new HashSet<PurchaseInvoiceFlatData>();
            PurchaseOrderDetailShipmentFlatData = new HashSet<PurchaseOrderDetailShipmentFlatData>();
            PurchaseOrderFlatData = new HashSet<PurchaseOrderFlatData>();
            PurchaseOrderShellFlatData = new HashSet<PurchaseOrderShellFlatData>();
            PurchaseShipmentFlatData = new HashSet<PurchaseShipmentFlatData>();
        }

        public int ProcessTaskUid { get; set; }
        public int ProcessUid { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Parameters { get; set; }
        public bool Successful { get; set; }

        public Processes ProcessU { get; set; }
        public ICollection<EtlDetails> EtlDetails { get; set; }
        public ICollection<EtlHeaders> EtlHeaders { get; set; }
        public ICollection<EtlInventory> EtlInventory { get; set; }
        public ICollection<EtlProducts> EtlProducts { get; set; }
        public ICollection<EtlRawFile> EtlRawFile { get; set; }
        public ICollection<EtlShipments> EtlShipments { get; set; }
        public ICollection<InventoryFlatData> InventoryFlatData { get; set; }
        public ICollection<ProcessTaskSteps> ProcessTaskSteps { get; set; }
        public ICollection<ProcessTasksErrors> ProcessTasksErrors { get; set; }
        public ICollection<ProductsFlatData> ProductsFlatData { get; set; }
        public ICollection<PurchaseInvoiceFlatData> PurchaseInvoiceFlatData { get; set; }
        public ICollection<PurchaseOrderDetailShipmentFlatData> PurchaseOrderDetailShipmentFlatData { get; set; }
        public ICollection<PurchaseOrderFlatData> PurchaseOrderFlatData { get; set; }
        public ICollection<PurchaseOrderShellFlatData> PurchaseOrderShellFlatData { get; set; }
        public ICollection<PurchaseShipmentFlatData> PurchaseShipmentFlatData { get; set; }
    }
}
