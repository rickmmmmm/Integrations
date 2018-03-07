module.exports = {
    config: {
        //"apiUrl":"https://assets.cps.edu/TIPWEBAPI/api",
        "apiUrl":"https://www.tipwebintegrationtest.com/TIPWEBAPI/api",
        "host":"chicago-etl.cd3gpflcjjp7.us-east-1.rds.amazonaws.com",
        "database":"TipWebHostedStagingChicago_ETL",
        "username":"Tipweb",
        "dbType":"mssql",
        "mapType":"invoices",
        "client":"CPS",
        "typeDesc":"Invoice Detail Integration for Chicago Public Schools sourced from Oracle Purchasing"
    },
    secrets: {
        "secretkey":"Mofo1234",
        "passphrase":"gemcap3663",
        "password":"Bookmaster"
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
            "shipping": "1",
            "charges": "0",
            "payments":"0",
            "inventory": "0"
        }
    }
}
// still need to add configurations for client data. I'm thinking each integration will have an object in this configuration file
// which will be referenced externally by all services from S3