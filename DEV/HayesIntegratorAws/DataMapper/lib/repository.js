var sequelize = require('sequelize');
var Promise = require('bluebird');
var fs = require('fs')
const configuration = require('./configuration.js');

var database = configuration.config.database;
var username = configuration.config.username;
var password = configuration.secrets.password;

seq =  new sequelize(database,username,password, {
        host: configuration.config.host,
          dialect: 'mssql',
          logging: false,
          define: {
            timestamps: false
          },
          pool: {
            maxIdleTime: 1000
          }
    });

DataIntegrationsModel = { 
    IntegrationsID: { type: sequelize.INTEGER, primaryKey: true, autoIncrement: true}, 
    IntegrationsObject: sequelize.STRING,
    DateAdded: sequelize.DATE,
    DataProcessedSuccessfully: sequelize.BOOLEAN,
    DataProcessing: sequelize.BOOLEAN,
    DataSentToTipweb: sequelize.BOOLEAN,
    DataCleared: sequelize.BOOLEAN,
    Client: sequelize.STRING
};

PurchaseOrderIntegrationFlatDataModel = {
    PO_NUMBER: { type: sequelize.STRING, unique: 'cindex' },
    PO_DATE: sequelize.DATEONLY,
    VENDOR_NAME: sequelize.STRING,
    VENDOR_ID: sequelize.INTEGER,
    LINE_NUMBER: { type: sequelize.INTEGER, unique: 'cindex' },
    PRODUCT_NAME: sequelize.STRING(100),
    PRODUCT_TYPE: sequelize.STRING,
    MODEL: sequelize.STRING,
    MANUFACTURER: sequelize.STRING,
    FUNDING_SOURCE: sequelize.STRING,
    DEPARTMENT: sequelize.STRING,
    ACCOUNT_CODE: sequelize.STRING(100),
    PURCHASE_PRICE: sequelize.STRING,
    QUANTITYORDERED: sequelize.INTEGER,
    NOTES: sequelize.STRING(5000),
    SHIPPEDTOSITE: sequelize.STRING,
    QUANTITYSHIPPED: sequelize.INTEGER,
    IntegrationsID: {type: sequelize.INTEGER, unique: 'cindex' }
};

DataIntegrationsErrorsModel = {
    DataIntegrationsErrorsID: { type: sequelize.INTEGER, primaryKey: true, autoIncrement: true },
    ErrorNumber: sequelize.STRING,
    ErrorName: sequelize.STRING,
    ErrorDescription: sequelize.STRING,
    ErrorObject: sequelize.STRING(10000),
    DataIntegrationsID: sequelize.INTEGER
};

DataIntegrationsActivityMonitorModel = {
    DataIntegrationsActivityMonitorID:  { type: sequelize.INTEGER, primaryKey: true, autoIncrement: true },
    DataIntegrationsActivityMonitorObject: sequelize.STRING(10000),
    AddedDate: sequelize.DATE,
    DataIntegrationsID: sequelize.INTEGER
};

DataIntegrationsMappingsModel = {
    MappingsID: { type: sequelize.STRING, unique: 'compositeIdx' },
    MappingsStep: { type: sequelize.INTEGER, primaryKey: true, autoIncrement: true, unique: 'compositeIdx' },
    MappingsObject: sequelize.STRING(10000),
};

PurchaseOrderHeaderModel = {
    OrderNumber: {type: sequelize.STRING, unique:'cidx' },
    Status: sequelize.STRING,
    VendorID: sequelize.INTEGER,
    VendorName: sequelize.STRING,
    SiteID: sequelize.STRING,
    PurchaseDate: sequelize.DATEONLY,
    EstimatedDeliveryDate: sequelize.STRING,
    Notes: sequelize.STRING(500),
    Other1: sequelize.STRING(100),
    DataIntegrationsID: { type: sequelize.INTEGER , unique:'cidx' },
    ShouldSubmit: sequelize.BOOLEAN
};

PurchaseOrderDetailModel = {
    OrderNumber: {type: sequelize.STRING, unique:'ccidx' },
    LineNumber: { type: sequelize.INTEGER , unique:'ccidx' },
    Status: sequelize.STRING,
    SiteID: sequelize.STRING,
    QuantityOrdered: sequelize.INTEGER,
    QuantityReceived: sequelize.INTEGER,
    FundingSource: sequelize.STRING,
    ProductName: sequelize.STRING,
    PurchasePrice: sequelize.DECIMAL,
    AccountCode: sequelize.STRING(100),
    DepartmentID: sequelize.INTEGER,
    DataIntegrationsID: { type: sequelize.INTEGER , unique:'ccidx' },
    ShouldSubmit: sequelize.BOOLEAN
};

ShipmentsModel = {
    OrderNumber: {type: sequelize.STRING, unique:'cccidx' },
    LineNumber: { type: sequelize.INTEGER , unique:'cccidx' },
    SiteID: {type: sequelize.STRING, unique:'cccidx' },
    TicketNumber: sequelize.INTEGER,
    QuantityShipped: sequelize.INTEGER,
    TicketedBy: sequelize.STRING,
    TicketedDate: sequelize.DATEONLY,
    Status: sequelize.STRING,
    InvoiceNumber: sequelize.STRING,
    InvoiceDate: sequelize.STRING,
    DataIntegrationsID: { type: sequelize.INTEGER , unique:'cccidx' },
    ShouldSubmit: sequelize.BOOLEAN
}

ProductsModel = {
    ProductNumber:  { type: sequelize.INTEGER , unique:'ccccidx' },
    ProductName:  { type: sequelize.STRING , unique:'ccccidx' },
    ProductDescription: sequelize.STRING(1000),
    ProductType: sequelize.STRING,
    Model: sequelize.STRING,
    Manufacturer: sequelize.STRING,
    SuggestedPrice: sequelize.DECIMAL,
    SKU: sequelize.STRING,
    Serial: { type: sequelize.STRING },
    Added: sequelize.BOOLEAN,
    Updated: sequelize.BOOLEAN,
    AddedDate: sequelize.STRING,
    LastUpdatedDate: sequelize.STRING
}

VendorsModel = {
    VendorID: sequelize.INTEGER,
    VendorName: sequelize.STRING,
    Address1: sequelize.STRING,
    Address2: sequelize.STRING,
    City: sequelize.STRING,
    State: sequelize.STRING,
    ZipCode: sequelize.STRING,
    Phone: sequelize.STRING,
    Email: sequelize.STRING,
    Added: sequelize.BOOLEAN,
    Updated: sequelize.BOOLEAN,
    AddedDate: sequelize.STRING,
    LastUpdatedDate: sequelize.STRING
},

FundingSourcesModel = {
    FundingSourceID: sequelize.STRING(500),
    Added: sequelize.BOOLEAN,
    Updated: sequelize.BOOLEAN
},

module.exports = {

    DataIntegrations: seq.define('DataIntegrations', DataIntegrationsModel),
    PurchaseOrderIntegrationFlatData: seq.define('PurchaseOrderIntegrationFlatData', PurchaseOrderIntegrationFlatDataModel),
    DataIntegrationsErrors: seq.define('DataIntegrationsErrors', DataIntegrationsErrorsModel),
    DataIntegrationsActivityMonitor: seq.define('DataIntegrationsActivityMonitor', DataIntegrationsActivityMonitorModel),
    DataIntegrationsMappings: seq.define('DataIntegrationsMappings', DataIntegrationsMappingsModel),
    Vendors: seq.define('Vendors', VendorsModel),
    Products: seq.define('Products',ProductsModel),
    FundingSources: seq.define('FundingSources', FundingSourcesModel),
    PurchaseOrderHeader: seq.define('PurchaseOrderHeader', PurchaseOrderHeaderModel, {tableName:'PurchaseOrderHeader'}),
    PurchaseOrderDetail: seq.define('PurchaseOrderDetail', PurchaseOrderDetailModel, {tableName: 'PurchaseOrderDetail'}),
    Shipments: seq.define('Shipments', ShipmentsModel),

    /**
     * Gets map objects from database of the type specified.
     * @param {string} type Filters map type. Accepted options are 'purchases', 'charges', 'inventory' OR internal db maps for 'po headers', 'po details', 'shipping'.
     */
    getMappings(type) {
        return new Promise(
            (resolve, reject) => {
                this.DataIntegrationsMappings.findAll(
                    { where: {
                        MappingsID: type
                    }}
                ).then(
                    data => {
                        resolve(data);
                    },
                    err => {
                        reject(err);
                    }
                )
            }
        );
    },

    /**
     * Creates new integration record.
     * @param {*} payload Object that maps to DataIntegrations table columns.
     */
    insertIntegration(payload) {

        return new Promise(
            (resolve,reject) => {
                
                if (!payload) {
                    reject();
                }

                this.DataIntegrations.create({
                    IntegrationsObject: JSON.stringify(payload),
                    Client: payload.client,
                    DataProcessing: true
                }).then(
                    () => {
                        this.DataIntegrations.max('IntegrationsID').then(
                            data => {
                                resolve(data);
                            },
                            error => {
                                reject(error);
                            }
                        );
                    }
                );

            }
        );
    },

    beginSendingToTipwebAPI() {
        return new Promise(
            (resolve,reject) => {

                this.DataIntegrations.update({
                    DataProcessing: false,
                    DataSentToTipweb: true
                }, { 
                    where: { DataProcessing: true 
                    } 
                }).then(
                    () => {
                        this.DataIntegrations.max('IntegrationsID').then(
                            data => {
                                resolve(data);
                            },
                            error => {
                                reject(error);
                            }
                        );
                    }
                );

            }
        );
    },

    completeIntegrationProcessing(intgid) {
        return new Promise(
            (resolve, reject) => {
                this.DataIntegrations.update({DataProcessing: false, DataSentToTipweb: false, DataProcessedSuccessfully: true },
                { where: {IntegrationsID: intgid } }).then(
                    data => { resolve('Success')},
                    error => { reject(error); }
                );
            }
        );
    },

    /**
     * Gets the currently processing integration ID value.
     */
    getProcessingIntegrationID() {
        return new Promise(
            (resolve, reject) => {
                this.DataIntegrations.findOne(
                    {
                        attributes: ['IntegrationsID'],
                        where: {
                            DataProcessing: true
                        }
                    }
                ).then(
                    data => {
                        if(!data) {
                            reject('No integrations currently processing!');
                        }
                        resolve(data.dataValues.IntegrationsID);
                    },
                    err => {
                        reject(err);
                    }
                );
            }
        );
    },

    getDataSendingToApiIntegrationID() {
        return new Promise(
            (resolve, reject) => {
                this.DataIntegrations.findOne(
                    {
                        attributes: ['IntegrationsID'],
                        where: {
                            DataProcessing: false,
                            DataSentToTipweb: true
                        }
                    }
                ).then(
                    data => {
                        if(!data) {
                            reject('No integration data currently being sent to TipWEB-IT!');
                        }
                        resolve(data.dataValues.IntegrationsID);
                    },
                    err => {
                        reject(err);
                    }
                );
            }
        );
    },

    /**
     * Inserts flat purchase order data into database.
     * @param {*} payload A valid map to the table columns the flat data table. Required columns are PO_NUMBER, LINE_NUMBER, IntegrationsID
     */
    insertFlatData(payload) {

        return new Promise(
            (resolve, reject)=>{

                if (!payload) {
                    reject();
                }

                this.PurchaseOrderIntegrationFlatData.create(payload).then(
                    () => {
                        resolve('Success');
                    },
                    err => {
                        reject(err);
                    }
                )
        });

    },

    /**
     * Logs an error in the database DataIntegrationsErrors table.
     * @param {*} payload Valid object to map to error table. Standard error object includes ErrorNumber, ErrorName, ErrorDescription, ErrorObject, IntegrationsID
     */
    logError(payload) {
        return new Promise(
            (resolve, reject)=>{

                if (!payload) {
                    reject();
                }

                this.DataIntegrationsErrors.create(payload).then(
                    () => {
                        resolve('Success');
                    },
                    err => {
                        //error logging the error?
                        reject(err);
                    }
                )
        });
    },

    runProcIntegrations_RemoveUnnecessaryRecords(intgid, options) {
        return new Promise(
            (resolve, reject) => {

                if (options) {
                    let params = intgid + ','+ options.headers + ',' + options.details + ',' + options.shipping + ',' + options.inventory + ',' + options.charges + ',' + options.payments
                    seq.query("EXEC Integrations_RemoveUnnecessaryUpdates " + params).then(
                        data => {
                            resolve(data);
                        },
                        error => {
                            reject(error);
                        }
                    );
                }
                else {
                    reject('No parameters provided.');
                }
            }
        )
    },

    runProcIntegrations_RemoveExistingInserts(intgid, options) {
        return new Promise(
            (resolve, reject) => {

                if (options) {
                    let params = intgid + ','+ options.headers + ',' + options.details + ',' + options.shipping + ',' + options.inventory + ',' + options.charges + ',' + options.payments
                    seq.query("EXEC Integrations_RemoveExistingInserts " + params).then(
                        data => {
                            resolve(data);
                        },
                        error => {
                            reject(error);
                        }
                    );
                }
                else {
                    reject('No parameters provided.');
                }
            }
        )
    },
    /**
     * DEPRECATED. Suggested for use to log actions as they occur in the application. May not need this and may instead copy the console to a text file upon completion.
     * @param {*} payload 
     */
    logActivity(payload) {
        return new Promise(
            (resolve, reject)=>{

                if (!payload) {
                    reject();
                }

                this.DataIntegrationsActivityMonitor.create(payload).then(
                    () => {
                        resolve('Success');
                    },
                    err => {
                        //error logging the error?
                        reject(err);
                    }
                )
        });
    },

    /* Vendors */

    getCurrentVendors(useIDs = false) {
        return new Promise(
            (resolve, reject) => {
                if (useIDs) {
                    this.Vendors.findAll(
                        {
                            attributes: ['VendorName','VendorID'],
                            group: ['VendorName','VendorID']
                        }
                    ).then(
                        data => {
                            resolve(data);
                        },
                        err => {
                            reject(err);
                        }
                    );
                }
                else {
                    this.Vendors.findAll(
                        {
                            attributes: ['VendorName'],
                            group: ['VendorName']
                        }
                    ).then(
                        data => {
                            resolve(data);
                        },
                        err => {
                            reject(err);
                        }
                    );

                }
            }
        )
    },

    getNewVendors(intgid, currentVendors, useGroups) {
        return new Promise(
            (resolve, reject) => {
                if (!intgid) {
                    reject();
                }

                var sqlConditions = [
                                { IntegrationsID: intgid },
                                { VENDOR_NAME: { $notIn: currentVendors } }
                            ];

                if (useGroups) {

                    var currentVendorNames = currentVendors === [] ? currentVendors : currentVendors.map(m => { return m.VendordName; });
                    var currentVendorIDs = currentVendors === [] ? currentVendors : currentVendors.map(m => { return m.VendorID; });

                    sqlConditions = [
                                { IntegrationsID: intgid },
                                { VENDOR_NAME: { $notIn: currentVendorNames } },
                                { VENDOR_ID: { $notIn: currentVendorIDs } }
                            ];
                }

                this.PurchaseOrderIntegrationFlatData.findAll(
                    {
                        attributes: useGroups ? ['VENDOR_NAME','VENDOR_ID'] : 'VENDOR_NAME',
                        where: {
                            $and: sqlConditions
                        },
                        group: useGroups ? ['VENDOR_NAME','VENDOR_ID'] : 'VENDOR_NAME' 
                    }
                ).then(
                    data => {
                        resolve(data);
                    },
                    err => {
                        reject(err);
                    }
                )
            }
        );
    },

    insertVendors(payload) {
        return new Promise(
            (resolve, reject) => {
                if (!payload) {
                    reject();
                }

                this.Vendors.bulkCreate(payload).then(
                    data => {
                        resolve('Success!');
                    },
                    err => {
                        reject(err);
                    }
                )
            }
        )
    },

    getVendorsToUpsert() {
        return new Promise(
            (resolve, reject) => {
                this.Vendors.findAll(
                    {   attributes: {exclude : 'id'},
                        where: { $or:[{ Added: true }, { Updated: true }] } }
                ).then(
                    data => {
                        resolve(data);
                    },
                    error => {
                        reject(error);
                    }
                );
            }
        );
    },

    toggleVendorSyncSwitch() {
        return new Promise(
            (resolve, reject) => {
                this.Vendors.update(
                    {   
                        Added: false,
                        Updated: false
                    },
                    { where: { $or:[{ Added: true }, { Updated: false }] }}
                ).then(
                    data => {
                        resolve('Success!');
                    },
                    err => {
                        reject(err);
                    }
                )
            }
        );
    },

    /* Products */

    getNewProducts(oldProducts, intgid) {
        return new Promise(
            (resolve, reject) => {
                var sqlIn = oldProducts === [] ? oldProducts : oldProducts.map(m => { return m.ProductName; });

                this.PurchaseOrderIntegrationFlatData.findAll({
                        attributes: ['PRODUCT_NAME','PRODUCT_TYPE','MODEL','MANUFACTURER', [sequelize.fn('MAX', sequelize.col('PURCHASE_PRICE')), 'SuggestedPrice']],
                        group: ['PRODUCT_NAME','PRODUCT_TYPE','MODEL','MANUFACTURER'],
                        where: {
                            IntegrationsID: intgid,
                            PRODUCT_NAME: {
                                $notIn: sqlIn
                            }
                        }
                    }
                ).then(
                    data => {
                        resolve(data);
                    },
                    error => {
                        reject(error);
                    }
                )
            }
        )
    },

    getCurrentProducts() {
        return new Promise(
            (resolve, reject) => {
                this.Products.findAll({
                        attributes: ['ProductName'],
                        group: 'ProductName'
                    }
                ).then(
                    data => {
                        resolve(data);        
                    },
                    error => {
                        reject(error);
                    }
                );
            }
        );
    },

    insertNewProducts(payload) {
        return new Promise(
            (resolve, reject) => {
                this.Products.bulkCreate(payload).then(
                    data => {
                        resolve(data);
                    },
                    error => {
                        reject(error);
                    }
                );
            }
        );
    },

    updateProductNames(newProductNamesList) {
        return new Promise(
            (resolve, reject) => {
                for (let upd of newProductNamesList) {
                    this.Products.update(upd,
                        { where: { ProductName: upd.ProductName }}
                    ).then(
                        data => {
                            resolve(data); //rows affected
                        },
                        error => {
                            reject(error);
                        }
                    )
                }
            }
        )
    },

    toggleProductsSyncSwitch() {
        return new Promise(
            (resolve, reject) => {
                this.Products.update(
                    {   
                        Added: false,
                        Updated: false
                    },
                    {
                        where: { $or: [{ Added: true}, { Updated: true }] }
                    }
                ).then(
                    data => {
                        resolve('Success!');
                    },
                    err => {
                        reject(err);
                    }
                )
            }
        );
    },

    /**
     * Retrieve products that have either been added or updated.
     */
    getProductsToUpsert() {
        return new Promise(
        (resolve, reject) => {
            this.Products.findAll({
                attributes: { exclude: ['Added', 'Updated', 'AddedDate', 'LastUpdatedDate', 'id']},
                where: { 
                    $or: [{Added: true}, {Updated: true }] 
            }
            }).then(
                data => {
                    resolve(data);
                },
                error => {
                    reject(error);
                }
            );
        }
        );
    },

    /* Funding Sources */

    toggleFundingSourcesSyncSwitch() {
        return new Promise(
            (resolve, reject) => {
                this.FundingSources.update(
                    {   
                        Added: false
                    }
                ).then(
                    data => {
                        return('Success!');
                    },
                    err => {
                        reject(err);
                    }
                )
            }
        );
    },

    getCurrentFundingSources() {
        return new Promise(
            (resolve, reject) => {
                this.FundingSources.findAll(
                    { attributes: ['FundingSourceID'] }
                ).then(
                    data => {
                        resolve(data);
                    },
                    error => {
                        reject(error);
                    }
                );
            }
        );
        
    },

    getNewFundingSources(oldSources, intgid) {
        return new Promise(
            (resolve, reject) => {
                var sqlIn = oldSources === [] ? oldSources : oldSources.map(m => { return m.FundingSourceID; });

                this.PurchaseOrderIntegrationFlatData.findAll(
                    { attributes: ['FUNDING_SOURCE'],
                        group: ['FUNDING_SOURCE'],
                        where: { IntegrationsID: intgid, FUNDING_SOURCE: { $notIn: sqlIn } }
                    }
                ).then(
                    data => {
                        resolve(data);
                    },
                    error => {
                        reject(error);
                    }
                );
            }
        );
    },

    insertNewFundingSources(payload) {
        return new Promise(
            (resolve, reject) => {
                this.FundingSources.bulkCreate(payload).then(
                    data => {
                        resolve(data);
                    },
                    error => {
                        reject(error);
                    }
                );
            }
        );
    },

    /* Purchase Order Headers and Detail Data */

    getHeaderRecordsFlatData(intgid) {
        return new Promise(
            (resolve, reject) => {
                this.PurchaseOrderIntegrationFlatData.findAll({
                        attributes: ['PO_NUMBER','PO_DATE','VENDOR_ID','VENDOR_NAME','SHIPPEDTOSITE'],
                        group: ['PO_NUMBER','PO_DATE','VENDOR_ID','VENDOR_NAME','SHIPPEDTOSITE'],
                        where: { IntegrationsID: intgid }
                    }
                ).then(
                    data => {
                        resolve(data);
                    },
                    error => {
                        reject(error);
                    }
                );
            }
        );
    },

    getDetailRecordsFlatData(intgid) {
        return new Promise(
            (resolve, reject) => {
                this.PurchaseOrderIntegrationFlatData.findAll({
                    attributes: { exclude: 'id'},
                    where: { IntegrationsID: intgid }
                }).then(
                    data => {
                        resolve(data);
                    },
                    error => {
                        reject(error);
                    }
                )
            }
        );
    },

    insertHeaderRecords(payload) {
        return new Promise(
            (resolve, reject) => {
                this.PurchaseOrderHeader.bulkCreate(payload).then(
                    data => {
                        resolve('Success!');
                    },
                    error => {
                        reject(error);
                    }
                );
            }
        );
    },

    insertDetailRecords(payload) {

        return new Promise(
            (resolve, reject) => {
                this.PurchaseOrderDetail.bulkCreate(payload).then(
                    data => {
                        resolve('Success!');
                    },
                    error => {
                        reject(error);
                    }
                );
            }
        );
    },

    getHeadersToUpsert(intgid) {
        return new Promise(
            (resolve, reject) => {
                this.PurchaseOrderHeader.findAll({
                        attributes: { exclude: ['ShouldSubmit','DataIntegrationsID', 'id']},
                        where: { DataIntegrationsID: intgid }
                    }
                ).then(
                    data => {resolve(data);},
                    error => { reject(error); }
                );
            }
        );
    },

    getDetailsToUpsert(intgid) {
        return new Promise(
            (resolve, reject) => {
                this.PurchaseOrderDetail.findAll({
                        attributes: { exclude: ['ShouldSubmit','DataIntegrationsID', 'id']},
                        where: { DataIntegrationsID: intgid }
                    }
                ).then(
                    data => {resolve(data);},
                    error => { reject(error); }
                );
            }
        );
    },

    /* Shipments */

    getFlatShipments(intgid) {
        return new Promise(
            (resolve, reject) => {
                this.PurchaseOrderIntegrationFlatData.findAll({
                    attributes: { exclude: 'id'},
                    where: { IntegrationsID: intgid }
                }).then(
                    data => {
                        resolve(data);
                    },
                    error => {
                        reject(error);
                    }
                )
            }
        );
    },

    insertShipments(payload) {
        return new Promise(
            (resolve, reject) => {
                this.Shipments.bulkCreate(payload).then(
                    data => {
                        resolve('Success!');
                    },
                    error => {
                        reject(error);
                    }
                );
            }
        );
    },

    getShipmentsToUpsert(intgid) {
        return new Promise(
            (resolve, reject) => {
                this.Shipments.findAll({
                        attributes: { exclude: ['ShouldSubmit','DataIntegrationsID', 'id']},
                        where: { DataIntegrationsID: intgid }
                    }
                ).then(
                    data => {resolve(data);},
                    error => { reject(error);}
                );
            }
        );
    }
}