module.exports = {
    config: {
        "apiUrl":"https://www.tipwebintegrationtest.com/TIPWEBAPI/api",
        //"apiUrl":"http://localhost:55556/Tipweb_API/api",
        "dbConnectionString":"Server=chicago-etl.cd3gpflcjjp7.us-east-1.rds.amazonaws.com;Database=TipWebHostedStagingChicaog_ETL;User ID=Tipweb;Password=Bookmaster;",
        "host":"chicago-etl.cd3gpflcjjp7.us-east-1.rds.amazonaws.com",
        "database":"TipWebHostedStagingChicago_ETL",
        "username":"Tipweb",
        "dbType":"mssql",
        "sourceFile":"/home/",
        "sourceType":"csv",
        "docStorageDirectory":"/home/ec2-user/etc/jsonfiles/processing/json/arrays/",
        "mapType":"purchases",
        "client":"Chicago Test",
        "typeDesc":"Purchase Order Integration for Chicago Public Schools sourced from Oracle Purchasing"
    },
    secrets: {
        "secretkey":"7a228e0d-1665-404d-904f-0d5934d804b8",
        "passphrase":"05a23af3-b199-4b72-9ea6-a1b3f4a5726c",
        "password":"Bookmaster"
    },
    apiConfig: {
        "login" : "/Login/AuthorizeAPI",
        "getVendors" : "/Integrations/Vendors/GetAllVendors",
        "getProducts" : "/Integrations/Products/GetAllProducts",
        "getFundingSources": "/Integrations/FundingSources/GetFundingSources",
        "addVendor" : "/Integrations/Vendors/AddVendors",
        "addHeader": "/Integrations/PurchaseOrders/AddPurchaseOrderHeaders",
        "addDetail": "/Integrations/PurchaseOrders/AddPurchaseOrderDetails?dept=0&fund=None",
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
        }
    }
}
// still need to add configurations for client data. I'm thinking each integration will have an object in this configuration file
// which will be referenced externally by all services from S3