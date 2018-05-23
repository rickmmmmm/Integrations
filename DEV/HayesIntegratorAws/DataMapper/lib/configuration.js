module.exports = {
    config: {
        "apiUrl":"http://localhost/TIPWebAPI/api",
        "host":".",
        "database":"IntgAppData",
        "username":"",
        "dbType":"mssql",
        "sourceType":"csv",
        "mapType":"shipments", // "mapType":"purchases",
        "client":"CPS",
        "idFileLoc":"/home/ec2-user/",
        "typeDesc":"Invoice Detail Integration for Chicago Public Schools sourced from Oracle Purchasing",
        "linksFolder":"/home/ec2-user/etc/CPS/linktables/",
        "links": [
            { "type":"Sites", "filename":"sites.json" }
        ],
        "customTasks":[
            { "type":"repository", "fn":"runProcz_custom_stpro_CPS_RemoveBadSites" }
        ]
    },
    secrets: {
        "secretkey":"7cd7f145-7989-4866-bd54-f646fa3ef739",
        "passphrase":"632b5da8-2608-4aee-93aa-d2c41be137a9",
        "password":"integration"
    },
    apiConfig: {
        "login" : "/Login/AuthorizeAPI",
        "getVendors" : "/Integrations/Vendors/GetAllVendors",
        "getProducts" : "/Integrations/Products/GetAllProducts",
        "getFundingSources": "/Integrations/FundingSources/GetFundingSources",
        "addVendor" : "/Integrations/Vendors/AddVendors",
        "addHeader": "/Integrations/PurchaseOrders/AddPurchaseOrderHeaders",
        "addDetail": "/Integrations/PurchaseOrders/AddPurchaseOrderDetails?dept=999&fund=None",
        "addShipment":"/Integrations/Shipments/AddShipmentsExcludeExisting",
        "addProduct":"/Integrations/Products/UpsertProducts",
        "addInvoices":"/Integrations/Invoices/AddInvoices",
        "addInvoiceDetails":"/Integrations/Invoices/AddInvoiceDetails"
    },
    dataConfig: {
        procRemove: {
            "headers": "1",
            "details": "1",
            "shipping": "0",
            "charges": "0",
            "payments":"0",
            "inventory": "0"
        },
        flatDataTable: "ShipmentIntegrationFlatData", // flatDataTable: "PurchaseOrderIntegrationFlatData"
    }
}
// still need to add configurations for client data. I'm thinking each integration will have an object in this configuration file
// which will be referenced externally by all services from S3