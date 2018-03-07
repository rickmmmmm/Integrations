module.exports = {
    config: {
        "apiUrl":"http://www.tipwebittraining.com/TIPWEBAPI5/api",
        "host":"chicago-etl.cd3gpflcjjp7.us-east-1.rds.amazonaws.com",
        "database":"IntgAppData",
        "username":"sa",
        "dbType":"mssql",
        "sourceFile":"/home/",
        "sourceType":"csv",
        "mapType":"purchases",
		"idFileLoc":"/home/ec2-user/",
        "client":"CPS",
        "typeDesc":"Purchase Order Integration for Chicago Public Schools sourced from Oracle Purchasing",
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
        "password":"gemcap3663"
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
        "addProduct":"/Integrations/Products/UpsertProducts"
    },
    dataConfig: {
        procRemove: {
            "headers": "1",
            "details": "1",
            "shipping": "1",
            "charges": "0",
            "payments":"0",
            "inventory": "0"
        },
		flatDataTable: "PurchaseOrderIntegrationFlatData"
    }
}
// still need to add configurations for client data. I'm thinking each integration will have an object in this configuration file
// which will be referenced externally by all services from S3