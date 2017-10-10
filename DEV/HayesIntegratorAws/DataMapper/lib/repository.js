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
            requestTimeout: 150000,
            idle: 20000,
            acquire: 20000
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
    IntegrationsID: {type: sequelize.INTEGER, unique: 'cindex' },
    Chunk: sequelize.BOOLEAN
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
    Client: sequelize.STRING
};

PurchaseOrderHeaderModel = {
    OrderNumber: {type: sequelize.STRING, unique:'cidx', primaryKey: true },
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
    OrderNumber: {type: sequelize.STRING, unique:'ccidx', primaryKey: true },
    LineNumber: { type: sequelize.INTEGER , unique:'ccidx', primaryKey: true },
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
    OrderNumber: {type: sequelize.STRING, unique:'cccidx', primaryKey: true },
    LineNumber: { type: sequelize.INTEGER , unique:'cccidx', primaryKey: true },
    SiteID: {type: sequelize.STRING, unique:'cccidx', primaryKey: true },
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
    ProductNumber:  { type: sequelize.INTEGER , unique:'ccccidx', primaryKey: true },
    ProductName:  { type: sequelize.STRING , unique:'ccccidx', primaryKey: true },
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
    LastUpdatedDate: sequelize.STRING,
    Client: { type: sequelize.STRING, primaryKey: true }
}

VendorsModel = {
    VendorID: { type: sequelize.INTEGER, primaryKey: true },
    VendorName: { type: sequelize.STRING, primaryKey: true },
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
    LastUpdatedDate: sequelize.STRING,
    Client: { type: sequelize.STRING, primaryKey: true }
}

FundingSourcesModel = {
    FundingSourceID: sequelize.STRING(500),
    Added: sequelize.BOOLEAN,
    Updated: sequelize.BOOLEAN
}

LinkTableModel = {
    LinkID: { type: sequelize.INTEGER, primaryKey: true, autoIncrement: true },
    Client: sequelize.STRING,
    SourceVal: sequelize.STRING,
    DestVal: sequelize.STRING,
    LinkType: sequelize.STRING
}

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
    DataIntegrationsLinkTable: seq.define('DataIntegrationsLinkTable', LinkTableModel, {tableName: 'DataIntegrationsLinkTable'} ),

    /**
     * Gets map objects from database of the type specified.
     * @param {string} type Filters map type. Accepted options are 'purchases', 'charges', 'inventory' OR internal db maps for 'po headers', 'po details', 'shipping'.
     */
    getMappings(options) {
        return new Promise(
            (resolve, reject) => {
                this.DataIntegrationsMappings.findAll(
                    { where: {
                        MappingsID: options.type,
                        Client: options.client
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

    beginSendingToTipwebAPI(client) {
        return new Promise(
            (resolve,reject) => {

                this.DataIntegrations.update({
                    DataProcessing: false,
                    DataSentToTipweb: true
                }, { 
                    where: { DataProcessing: true, Client: client 
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

    getDataSendingToApiIntegrationID(client) {
        return new Promise(
            (resolve, reject) => {
                this.DataIntegrations.findOne(
                    {
                        attributes: ['IntegrationsID'],
                        where: {
                            DataProcessing: false,
                            DataSentToTipweb: true,
                            Client: client
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

    runProcIntegrations_StageProductData(options) {
        return new Promise(
            (resolve, reject) => {
                if (options) {
                    seq.query("EXEC Integrations_StageProductData '" + options.client + "'").then(
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
        );
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
        );
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

    runProcIntegrations_FlagDetailsAndShipmentsFromBadHeaderRecords(intgid, options) {
        return new Promise(
            (resolve, reject) => {
                seq.query("EXEC Integrations_FlagDetailsAndShipmentsFromBadHeaderRecords " + intgid).then(
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
    runProcIntegrations_FlagShipmentsFromBadDetailRecords(intgid, options) {
        return new Promise(
            (resolve, reject) => {
                seq.query("EXEC Integrations_FlagShipmentsFromBadDetailRecords " + intgid).then(
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
    runProcIntegrations_AggregateDatafromPurchaseIntegration(date, client) {
        return new Promise(
            (resolve, reject) => {
                seq.query("EXEC Integrations_AggregateDatafromPurchaseIntegration '" + client + "'," + date).then(
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

    runProcz_custom_stpro_CPS_RemoveBadSites(options) {
        return new Promise(
            (resolve, reject) => {
                seq.query("EXEC z_custom_stpro_CPS_RemoveBadSites '" + options.client + "'," + options.intgid).then(
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
                            where: { Client: configuration.config.client },
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

    getVendorsToUpsert(options) {
        return new Promise(
            (resolve, reject) => {
                this.Vendors.findAll(
                    {   attributes: {exclude : 'id'},
                        where: { $or:[{ Added: true }, { Updated: true }], Client: options.client },
                        limit: options.limitVal,
                        offset: options.offsetVal }
                    
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
                    { where: { $or:[{ Added: true }, { Updated: true }] }, Client: configuration.config.client }
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
                        where: { $or: [{ Added: true}, { Updated: true }], Client: configuration.config.client }
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
    getProductsToUpsert(options) {
        return new Promise(
        (resolve, reject) => {
            this.Products.findAll({
                attributes: { exclude: ['Added', 'Updated', 'AddedDate', 'LastUpdatedDate', 'id']},
                where: { 
                    $or: [{Added: true }, {Updated: true }], Client: options.client
                },
                limit: options.limitVal,
                offset: options.offset
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
                        where: { IntegrationsID: intgid, Chunk: true }
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
                    where: { IntegrationsID: intgid, Chunk: true }
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

    getTotalHeadersToUpsertCount(options) {

        this.DataIntegrations.hasMany(this.PurchaseOrderHeader, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        this.PurchaseOrderHeader.belongsTo(this.DataIntegrations, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        return new Promise(
            (resolve, reject) => {
                this.PurchaseOrderHeader.count({
                        include: [{
                            model: this.DataIntegrations,
                            where: {
                                Client: options.client,
                                DataSentToTipweb: true
                            }
                        }],
                        where: { ShouldSubmit: true },
                    }
                ).then(
                    data => { resolve(data); },
                    error => { reject(error); }
                );
            }
        );
    },

    getTotalDetailsToUpsertCount(options) {
        
                this.DataIntegrations.hasMany(this.PurchaseOrderDetail, {
                    foreignKey: {
                        name: 'DataIntegrationsID'
                    }
                });
        
                this.PurchaseOrderDetail.belongsTo(this.DataIntegrations, {
                    foreignKey: {
                        name: 'DataIntegrationsID'
                    }
                });
        
                return new Promise(
                    (resolve, reject) => {
                        this.PurchaseOrderDetail.count({
                                include: [{
                                    model: this.DataIntegrations,
                                    where: {
                                        Client: options.client,
                                        DataSentToTipweb: true
                                    }
                                }],
                                where: { ShouldSubmit: true },
                            }
                        ).then(
                            data => { resolve(data); },
                            error => { reject(error); }
                        );
                    }
                );
            },
    getTotalVendorsToUpsertCount(options) {
        
        return new Promise(
            (resolve, reject) => {
                this.Vendors.count({
                        where: { $or: [ { Added: true }, { Updated: true }], Client: options.client },
                    }
                ).then(
                    data => { resolve(data); },
                    error => { reject(error); }
                );
            }
        );
    },
    getTotalProductsToUpsertCount(options) {
        
        return new Promise(
            (resolve, reject) => {
                this.Products.count({
                        where: { $or: [ { Added: true }, { Updated: true }], Client: options.client },
                    }
                ).then(
                    data => { resolve(data); },
                    error => { reject(error); }
                );
            }
        );
    },

    getHeadersToUpsert(options) {

        this.DataIntegrations.hasMany(this.PurchaseOrderHeader, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        this.PurchaseOrderHeader.belongsTo(this.DataIntegrations, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        return new Promise(
            (resolve, reject) => {
                this.PurchaseOrderHeader.findAll({
                        attributes: { exclude: ['ShouldSubmit','DataIntegrationsID', 'id']},
                        include: [{
                            model: this.DataIntegrations,
                            where: {
                                Client: options.client,
                                DataSentToTipweb: true
                            }
                        }],
                        where: { ShouldSubmit: true },
                        offset: options.offsetVal,
                        limit: options.limitVal,
                        order: ['OrderNumber']
                        
                    }
                ).then(
                    data => {resolve(data);},
                    error => { reject(error); }
                );
            }
        );
    },

    getDetailsToUpsert(options) {

        this.DataIntegrations.hasMany(this.PurchaseOrderDetail, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        this.PurchaseOrderDetail.belongsTo(this.DataIntegrations, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        return new Promise(
            (resolve, reject) => {
                this.PurchaseOrderDetail.findAll({
                        attributes: { exclude: ['ShouldSubmit','DataIntegrationsID', 'id']},
                        include: [{
                            model: this.DataIntegrations,
                            where: {
                                Client: options.client,
                                DataSentToTipweb: true
                            }
                        }],
                        where: { ShouldSubmit: true },
                        offset: options.offsetVal,
                        limit: options.limitVal
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
                    where: { IntegrationsID: intgid, Chunk: true }
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

    getTotalShipmentsToUpsertCount(options) {
        
        this.DataIntegrations.hasMany(this.Shipments, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        this.Shipments.belongsTo(this.DataIntegrations, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        return new Promise(
            (resolve, reject) => {
                this.Shipments.count({
                        include: [{
                            model: this.DataIntegrations,
                            where: {
                                Client: options.client,
                                DataSentToTipweb: true
                            }
                        }],
                        where: { ShouldSubmit: true },
                    }
                ).then(
                    data => { resolve(data); },
                    error => { reject(error); }
                );
            }
        );
    },

    getShipmentsToUpsert(options) {
        this.DataIntegrations.hasMany(this.Shipments, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        this.Shipments.belongsTo(this.DataIntegrations, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        return new Promise(
            (resolve, reject) => {
                this.Shipments.findAll({
                        attributes: { exclude: ['ShouldSubmit','DataIntegrationsID', 'id']},
                        include: [{
                            model: this.DataIntegrations,
                            where: {
                                Client: options.client,
                                DataSentToTipweb: true
                            }
                        }],
                        where: { ShouldSubmit: true },
                        offset: options.offsetVal,
                        limit: options.limitVal
                    }
                ).then(
                    data => {resolve(data);},
                    error => { reject(error); }
                );
            }
        );
    },

    toggleChunk(intgid) {
        return new Promise(
            (resolve, reject) => {
                this.PurchaseOrderIntegrationFlatData.update(
                    {   
                        Chunk: false
                    },
                    { where: { Chunk: true, IntegrationsID: intgid } }
                ).then(
                    data => { resolve(data); },
                    error => { reject(error); }
                );
            }
        )
    },

    getLinkTableData(options) {
        return new Promise(
            (resolve, reject) => {
                this.DataIntegrationsLinkTable.findAll({
                        attributes: ['SourceVal', 'DestVal'],
                        where: { Client: options.client, LinkType: options.type }
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
        );
    }
}