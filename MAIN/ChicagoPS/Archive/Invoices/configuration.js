module.exports = {
    config: {
        "apiUrl":"https://www.staginghss.com/IntgCPSTIPWebAPI/api",
        "host":"integrations.cu5oaecilfzk.us-east-1.rds.amazonaws.com",
        "database":"IntgAppData",
        "username":"intg-cps",
        "dbType":"mssql",
        "mapType":"invoices",
        "client":"CPS",
        "typeDesc":"Invoice Detail Integration for Chicago Public Schools sourced from Oracle Purchasing"
    },
    secrets: {
        "secretkey":"",
        "passphrase":"",
        "password":"c4afrus7aSU&"
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