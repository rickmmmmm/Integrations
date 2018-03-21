module.exports = {
    config: {
        "apiUrl" : "api_endpoint",
        "host" : "database_endpoint",
        "database" : "database_name",
        "username" : "database_user",
        "dbType" : "mssql",
        "mapType" : "invoices",
        "client" : "client_name",
        "typeDesc" : "description",
        "sourceFile":"/folder_name/",
        "sourceType":"csv",
        "idFileLoc":"/folder_name/",
        "linksFolder":"/folder_name/",
        "links": [
            { "type":"Type", "filename":"File" }
        ],
        "customTasks":[
            { "type":"repository", "fn":"dataMapper_function_name" }
        ]
    },
    secrets: {
        "secretkey" : "aws_access_key",
        "passphrase" : "aws_access_passphrase",
        "password" : "database_user_password"
    },
    apiConfig: {
        "login" : "login_endpoint",
        "getVendors" : "api_endpoint",
        "getProducts" : "api_endpoint",
        "getFundingSources" : "api_endpoint",
        "addVendor" : "api_endpoint",
        "addHeader" : "api_endpoint",
        "addDetail" : "api_endpoint",
        "addShipment" : "api_endpoint",
        "addProduct" : "api_endpoint",
        "addInvoices" : "api_endpoint",
        "addInvoiceDetails" : "api_endpoint"
    },
    dataConfig: {
        procRemove: {
            "headers" : "1",
            "details" : "1",
            "shipping" : "1",
            "charges" : "0",
            "payments" :"0",
            "inventory" : "0"
        },
        flatDataTable: "FlatData"
    }
}
// still need to add configurations for client data. I'm thinking each integration will have an object in this configuration file
// which will be referenced externally by all services from S3