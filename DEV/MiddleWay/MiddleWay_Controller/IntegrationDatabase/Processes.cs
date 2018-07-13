using System;
using System.Collections.Generic;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public partial class Processes
    {
        public Processes()
        {
            Configurations = new HashSet<Configurations>();
            EtlDetails = new HashSet<EtlDetails>();
            EtlHeaders = new HashSet<EtlHeaders>();
            EtlInventory = new HashSet<EtlInventory>();
            EtlProducts = new HashSet<EtlProducts>();
            EtlShipments = new HashSet<EtlShipments>();
            InventoryFlatData = new HashSet<InventoryFlatData>();
            Mappings = new HashSet<Mappings>();
            ProcessTasks = new HashSet<ProcessTasks>();
            ProductsFlatData = new HashSet<ProductsFlatData>();
            PurchaseInvoiceFlatData = new HashSet<PurchaseInvoiceFlatData>();
            PurchaseOrderDetailShipmentFlatData = new HashSet<PurchaseOrderDetailShipmentFlatData>();
            PurchaseOrderFlatData = new HashSet<PurchaseOrderFlatData>();
            PurchaseOrderShellFlatData = new HashSet<PurchaseOrderShellFlatData>();
            PurchaseShipmentFlatData = new HashSet<PurchaseShipmentFlatData>();
            TransformationLookup = new HashSet<TransformationLookup>();
            Transformations = new HashSet<Transformations>();
        }

        public int ProcessUid { get; set; }
        public string Client { get; set; }
        public string ProcessName { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<Configurations> Configurations { get; set; }
        public ICollection<EtlDetails> EtlDetails { get; set; }
        public ICollection<EtlHeaders> EtlHeaders { get; set; }
        public ICollection<EtlInventory> EtlInventory { get; set; }
        public ICollection<EtlProducts> EtlProducts { get; set; }
        public ICollection<EtlShipments> EtlShipments { get; set; }
        public ICollection<InventoryFlatData> InventoryFlatData { get; set; }
        public ICollection<Mappings> Mappings { get; set; }
        public ICollection<ProcessTasks> ProcessTasks { get; set; }
        public ICollection<ProductsFlatData> ProductsFlatData { get; set; }
        public ICollection<PurchaseInvoiceFlatData> PurchaseInvoiceFlatData { get; set; }
        public ICollection<PurchaseOrderDetailShipmentFlatData> PurchaseOrderDetailShipmentFlatData { get; set; }
        public ICollection<PurchaseOrderFlatData> PurchaseOrderFlatData { get; set; }
        public ICollection<PurchaseOrderShellFlatData> PurchaseOrderShellFlatData { get; set; }
        public ICollection<PurchaseShipmentFlatData> PurchaseShipmentFlatData { get; set; }
        public ICollection<TransformationLookup> TransformationLookup { get; set; }
        public ICollection<Transformations> Transformations { get; set; }
    }
}
