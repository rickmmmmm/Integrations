var sequelize = require('sequelize');
var Promise = require('bluebird');
var fs = require('fs')
const configuration = require('./configuration.js');

var database = configuration.config.database;
var username = configuration.config.username;
var password = configuration.secrets.password;

seq = new sequelize(database, username, password, {
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
    IntegrationsID: { type: sequelize.INTEGER, primaryKey: true },
    IntegrationsObject: sequelize.STRING,
    DateAdded: sequelize.DATE,
    DataProcessedSuccessfully: sequelize.BOOLEAN,
    DataProcessing: sequelize.BOOLEAN,
    DataSentToTipweb: sequelize.BOOLEAN,
    DataCleared: sequelize.BOOLEAN,
    Client: sequelize.STRING,
    IntegrationType: sequelize.STRING,
    DataPostProcessing: sequelize.BOOLEAN
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
    IntegrationsID: { type: sequelize.INTEGER, unique: 'cindex' },
    Chunk: sequelize.BOOLEAN,
    CFDA: sequelize.STRING
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
    DataIntegrationsActivityMonitorID: { type: sequelize.INTEGER, primaryKey: true, autoIncrement: true },
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
    OrderNumber: { type: sequelize.STRING, unique: 'cidx', primaryKey: true },
    Status: sequelize.STRING,
    VendorID: sequelize.INTEGER,
    VendorName: sequelize.STRING,
    SiteID: sequelize.STRING,
    PurchaseDate: sequelize.DATEONLY,
    EstimatedDeliveryDate: sequelize.STRING,
    Notes: sequelize.STRING(500),
    Other1: sequelize.STRING(100),
    DataIntegrationsID: { type: sequelize.INTEGER, unique: 'cidx' },
    ShouldSubmit: sequelize.BOOLEAN
};

PurchaseOrderDetailModel = {
    OrderNumber: { type: sequelize.STRING, unique: 'ccidx', primaryKey: true },
    LineNumber: { type: sequelize.INTEGER, unique: 'ccidx', primaryKey: true },
    Status: sequelize.STRING,
    SiteID: sequelize.STRING,
    QuantityOrdered: sequelize.INTEGER,
    QuantityReceived: sequelize.INTEGER,
    FundingSource: sequelize.STRING,
    ProductName: sequelize.STRING,
    PurchasePrice: sequelize.DECIMAL,
    AccountCode: sequelize.STRING(100),
    DepartmentID: sequelize.INTEGER,
    DataIntegrationsID: { type: sequelize.INTEGER, unique: 'ccidx' },
    ShouldSubmit: sequelize.BOOLEAN,
    CFDA: sequelize.STRING
};

ShipmentsModel = {
    OrderNumber: { type: sequelize.STRING, unique: 'cccidx', primaryKey: true },
    LineNumber: { type: sequelize.INTEGER, unique: 'cccidx', primaryKey: true },
    SiteID: { type: sequelize.STRING, unique: 'cccidx', primaryKey: true },
    TicketNumber: sequelize.INTEGER,
    QuantityShipped: sequelize.INTEGER,
    TicketedBy: sequelize.STRING,
    TicketedDate: sequelize.DATEONLY,
    Status: sequelize.STRING,
    InvoiceNumber: sequelize.STRING,
    InvoiceDate: sequelize.STRING,
    IntegrationsID: { type: sequelize.INTEGER, unique: 'cccidx' },
    ShouldSubmit: sequelize.BOOLEAN
}

ProductsModel = {
    ProductNumber: { type: sequelize.INTEGER, unique: 'ccccidx', primaryKey: true },
    ProductName: { type: sequelize.STRING, unique: 'ccccidx', primaryKey: true },
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

InvoiceDetailsIntegrationFlatDataModel = {
    OrderNumber: { type: sequelize.STRING, primaryKey: true },
    InvoiceNumber: { type: sequelize.STRING, primaryKey: true },
    InvoiceDate: sequelize.STRING,
    InvoiceStatus: sequelize.STRING,
    AuthorizationStatus: sequelize.STRING,
    LineNumber: sequelize.STRING,
    LineDescription: sequelize.STRING,
    AssetPrice: sequelize.STRING,
    InvoicePrice: sequelize.STRING,
    Quantity: sequelize.STRING,
    LineAmount: sequelize.STRING,
    DataIntegrationsID: { type: sequelize.INTEGER, primaryKey: true },
    Chunk: sequelize.BOOLEAN
}

InvoicesModel = {
    OrderNumber: { type: sequelize.STRING, primaryKey: true },
    InvoiceNumber: { type: sequelize.STRING, primaryKey: true },
    InvoiceDate: sequelize.STRING,
    InvoiceStatus: sequelize.STRING,
    AuthorizationStatus: sequelize.STRING,
    ShouldSubmit: sequelize.BOOLEAN,
    DataIntegrationsID: { type: sequelize.INTEGER, primaryKey: true },
    LastModifiedDate: sequelize.STRING
}

InvoiceDetailsModel = {
    OrderNumber: { type: sequelize.STRING, primaryKey: true },
    InvoiceNumber: { type: sequelize.STRING, primaryKey: true },
    LineNumber: { type: sequelize.STRING, primaryKey: true },
    LineDescription: sequelize.STRING,
    AssetPrice: sequelize.STRING,
    InvoicePrice: sequelize.STRING,
    Quantity: sequelize.STRING,
    LineAmount: sequelize.STRING,
    ShouldSubmit: sequelize.BOOLEAN,
    DataIntegrationsID: { type: sequelize.INTEGER, primaryKey: true }
}

DataIntegrationsFilesModel = {
    DataIntegrationsFilesID: { type: sequelize.INTEGER, primaryKey: true, autoIncrement: true },
    FileNameAws: sequelize.STRING,
    AwsFileLink: sequelize.STRING,
    Client: sequelize.STRING,
    DataIntegrationsID: sequelize.INTEGER,
    DateAdded: sequelize.DATE
};

module.exports = {

    DataIntegrations: seq.define('DataIntegrations', DataIntegrationsModel),
    PurchaseOrderIntegrationFlatData: seq.define('PurchaseOrderIntegrationFlatData', PurchaseOrderIntegrationFlatDataModel),
    DataIntegrationsErrors: seq.define('DataIntegrationsErrors', DataIntegrationsErrorsModel),
    DataIntegrationsActivityMonitor: seq.define('DataIntegrationsActivityMonitor', DataIntegrationsActivityMonitorModel),
    DataIntegrationsMappings: seq.define('DataIntegrationsMappings', DataIntegrationsMappingsModel),
    Vendors: seq.define('Vendors', VendorsModel),
    Products: seq.define('Products', ProductsModel),
    FundingSources: seq.define('FundingSources', FundingSourcesModel),
    PurchaseOrderHeader: seq.define('PurchaseOrderHeader', PurchaseOrderHeaderModel, { tableName: 'PurchaseOrderHeader' }),
    PurchaseOrderDetail: seq.define('PurchaseOrderDetail', PurchaseOrderDetailModel, { tableName: 'PurchaseOrderDetail' }),
    Shipments: seq.define('Shipments', ShipmentsModel),
    DataIntegrationsLinkTable: seq.define('DataIntegrationsLinkTable', LinkTableModel, { tableName: 'DataIntegrationsLinkTable' }),
    Invoices: seq.define('Invoices', InvoicesModel),
    InvoiceDetails: seq.define('InvoiceDetails', InvoiceDetailsModel),
    InvoiceDetailsIntegrationFlatData: seq.define('InvoiceDetailsIntegrationFlatData', InvoiceDetailsIntegrationFlatDataModel),
    DataIntegrationsFiles: seq.define('DataIntegrationsFiles', DataIntegrationsFilesModel),

    /**
     * Gets map objects from database of the type specified.
     * @param {string} type Filters map type. Accepted options are 'purchases', 'charges', 'inventory' OR internal db maps for 'po headers', 'po details', 'shipping'.
     */
    getMappings(options) {
        return new Promise(
            (resolve, reject) => {
                this.DataIntegrationsMappings.findAll(
                    {
                        where: {
                            MappingsID: options.type,
                            Client: options.client
                        }
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

    /**
     * Creates new integration record.
     * @param {*} payload Object that maps to DataIntegrations table columns.
     */
    insertIntegration(payload) {

        return new Promise(
            (resolve, reject) => {

                if (!payload) {
                    reject();
                }

                this.DataIntegrations.create({
                    IntegrationsObject: JSON.stringify(payload),
                    Client: payload.client,
                    DataProcessing: true,
                    IntegrationType: payload.integrationType,
                    IntegrationsID: payload.id
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

    beginSendingToTipwebAPI(intgid) {
        return new Promise(
            (resolve, reject) => {

                this.DataIntegrations.update({
                    DataSentToTipweb: true
                }, {
                        where: {
                            IntegrationsID: intgid
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

    beginDataPostProcessing(intgid) {
        return new Promise(
            (resolve, reject) => {

                this.DataIntegrations.update({
                    DataPostProcessing: true
                }, {
                        where: {
                            IntegrationsID: intgid
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

    completeIntegrationProcessing(intgid) {
        return new Promise(
            (resolve, reject) => {
                this.DataIntegrations.update({ DataProcessedSuccessfully: true },
                    { where: { IntegrationsID: intgid } }).then(
                        data => { resolve('Success') },
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
                        if (!data) {
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

    getDataSendingToApiIntegrationID(client, mapType) {
        return new Promise(
            (resolve, reject) => {
                this.DataIntegrations.findOne(
                    {
                        attributes: ['IntegrationsID'],
                        where: {
                            DataSentToTipweb: true,
                            DataProcessedSuccessfully: false,
                            Client: client,
                            IntegrationType: mapType
                        }
                    }
                ).then(
                    data => {
                        if (!data) {
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
    insertFlatData(payload, options) {

        return new Promise(
            (resolve, reject) => {

                if (!payload) {
                    console.error(chalk.red('No data provided.'))
                    reject();
                }

                if (!options && !options.target) {
                    console.error(chalk.red('No data target provided for flat client data.'));
                    reject();
                }

                this[options.target].create(payload).then(
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
            (resolve, reject) => {

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

    updateSubmittedValues(options) {
        return new Promise(
            (resolve, reject) => {
                // console.log('Calling updateSubmittedValues on ' + options.target);
                this[options.target].update(
                    {
                        Submitted: true
                    },
                    { where: { DataIntegrationsID: options.id, $in: options.ins } }
                ).then(
                    data => {
                        // console.log('Update Submitted resolved');
                        resolve();
                    },
                    error => {
                        // console.log('Update Submitted failed');
                        // console.log(error);
                        reject(error);
                    }
                )
            }
        );
    },

    updateSubmittedValues2(options) {
        return new Promise(
            (resolve, reject) => {
                // console.log('Calling updateSubmittedValues2 on ' + options.target);
                this[options.target].update(
                    {
                        Submitted: true
                    },
                    { where: { IntegrationsID: options.id, $in: options.ins } }
                ).then(
                    data => {
                        // console.log('Update Submitted resolved');
                        resolve();
                    },
                    error => {
                        // console.log('Update Submitted failed');
                        // console.log(error);
                        reject(error);
                    }
                );
            }
        );
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
                    let params = '\'' + intgid + '\',' + options.headers + ',' + options.details + ',' + options.shipping + ',' + options.inventory + ',' + options.charges + ',' + options.payments
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

    runProcIntegrations_FlagDetailsAndShipmentsFromBadHeaderRecords(intgid, options) {
        return new Promise(
            (resolve, reject) => {
                seq.query("EXEC Integrations_FlagDetailsFromBadHeaderRecords " + intgid).then(
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
            (resolve, reject) => {

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
                            attributes: ['VendorName', 'VendorID'],
                            where: { Client: configuration.config.client },
                            group: ['VendorName', 'VendorID']
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
                        attributes: useGroups ? ['VENDOR_NAME', 'VENDOR_ID'] : 'VENDOR_NAME',
                        where: {
                            $and: sqlConditions
                        },
                        group: useGroups ? ['VENDOR_NAME', 'VENDOR_ID'] : 'VENDOR_NAME'
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
                    {
                        attributes: { exclude: 'id' },
                        where: { $or: [{ Added: true }, { Updated: true }], Client: options.client },
                        limit: options.limitVal,
                        offset: options.offsetVal
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

    toggleVendorSyncSwitch() {
        return new Promise(
            (resolve, reject) => {
                this.Vendors.update(
                    {
                        Added: false,
                        Updated: false
                    },
                    { where: { $or: [{ Added: true }, { Updated: true }] }, Client: configuration.config.client }
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
                    attributes: ['PRODUCT_NAME', 'PRODUCT_TYPE', 'MODEL', 'MANUFACTURER', [sequelize.fn('MAX', sequelize.col('PURCHASE_PRICE')), 'SuggestedPrice']],
                    group: ['PRODUCT_NAME', 'PRODUCT_TYPE', 'MODEL', 'MANUFACTURER'],
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
                        { where: { ProductName: upd.ProductName } }
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
                        where: { $or: [{ Added: true }, { Updated: true }], Client: configuration.config.client }
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
                    attributes: { exclude: ['Added', 'Updated', 'AddedDate', 'LastUpdatedDate', 'id'] },
                    where: {
                        $or: [{ Added: true }, { Updated: true }], Client: options.client
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
                        return ('Success!');
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
                    {
                        attributes: ['FUNDING_SOURCE'],
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
                    attributes: ['PO_NUMBER', 'PO_DATE', 'VENDOR_ID', 'VENDOR_NAME', 'SHIPPEDTOSITE'],
                    group: ['PO_NUMBER', 'PO_DATE', 'VENDOR_ID', 'VENDOR_NAME', 'SHIPPEDTOSITE'],
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
                    attributes: { exclude: 'id' },
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
                            IntegrationsID: options.id,
                            DataSentToTipweb: true
                        }
                    }],
                    where: { ShouldSubmit: true, DataIntegrationsID: options.id, },
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
                            DataSentToTipweb: true,
                            IntegrationsID: options.id
                        }
                    }],
                    where: { ShouldSubmit: true, DataIntegrationsID: options.id, },
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
                    where: { $or: [{ Added: true }, { Updated: true }], Client: options.client },
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
                    where: { $or: [{ Added: true }, { Updated: true }], Client: options.client },
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
                    attributes: { exclude: ['ShouldSubmit', 'DataIntegrationsID', 'id'] },
                    include: [{
                        model: this.DataIntegrations,
                        where: {
                            IntegrationsID: options.intgid
                        }
                    }],
                    where: { ShouldSubmit: true },
                    offset: options.offsetVal,
                    limit: options.limitVal,
                    order: ['OrderNumber']

                }
                ).then(
                    data => { resolve(data); },
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
                    attributes: { exclude: ['ShouldSubmit', 'DataIntegrationsID', 'id'] },
                    include: [{
                        model: this.DataIntegrations,
                        where: {
                            Client: options.client,
                            IntegrationsID: options.intgid
                        }
                    }],
                    where: { ShouldSubmit: true },
                    offset: options.offsetVal,
                    limit: options.limitVal
                }
                ).then(
                    data => { resolve(this.escapePurchaseOrderDetails(data)); },
                    error => { reject(error); }
                );
            }
        );
    },

    escapePurchaseOrderDetails(data) {
        let escapedData = [];
        for (let detail of data) {
            detail.FundingSource = this.escapeString(detail.FundingSource);
            detail.ProductName = this.escapeString(detail.ProductName);
            detail.CFDA = this.escapeString(detail.CFDA);
            escapedData.push(detail);
        }
        return escapedData;
    },

    /* Shipments */

    getFlatShipments(intgid) {
        return new Promise(
            (resolve, reject) => {
                this.PurchaseOrderIntegrationFlatData.findAll({
                    attributes: { exclude: 'id' },
                    where: { IntegrationsID: intgid, Chunk: true }
                }).then(
                    data => {
                        resolve(this.escapeShipments(data));
                    },
                    error => {
                        reject(error);
                    }
                )
            }
        );
    },

    escapeShipments(data) {
        let escapedData = [];
        for (let shipment of data) {
            shipment.VENDOR_NAME = this.escapeString(shipment.VENDOR_NAME);
            shipment.PRODUCT_NAME = this.escapeString(shipment.PRODUCT_NAME);
            shipment.PRODUCT_TYPE = this.escapeString(shipment.PRODUCT_TYPE);
            shipment.MODEL = this.escapeString(shipment.MODEL);
            shipment.MANUFACTURER = this.escapeString(shipment.MANUFACTURER);
            shipment.FUNDING_SOURCE = this.escapeString(shipment.FUNDING_SOURCE);
            shipment.NOTES = this.escapeString(shipment.NOTES);
            shipment.CFDA = this.escapeString(shipment.CFDA);
            escapedData.push(shipment);
        }
        return escapedData;
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
                name: 'IntegrationsID'
            }
        });

        this.Shipments.belongsTo(this.DataIntegrations, {
            foreignKey: {
                name: 'IntegrationsID'
            }
        });

        return new Promise(
            (resolve, reject) => {
                this.Shipments.count({
                    include: [{
                        model: this.DataIntegrations,
                        where: {
                            Client: options.client,
                            DataSentToTipweb: true,
                            IntegrationsID: options.id
                        }
                    }],
                    where: { ShouldSubmit: true, IntegrationsID: options.id },
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
                name: 'IntegrationsID'
            }
        });

        this.Shipments.belongsTo(this.DataIntegrations, {
            foreignKey: {
                name: 'IntegrationsID'
            }
        });

        return new Promise(
            (resolve, reject) => {
                this.Shipments.findAll({
                    attributes: { exclude: ['ShouldSubmit', 'DataIntegrationsID', 'id'] },
                    include: [{
                        model: this.DataIntegrations,
                        where: {
                            IntegrationsID: options.id
                        }
                    }],
                    where: { ShouldSubmit: true, IntegrationsID: options.id },
                    offset: options.offsetVal,
                    limit: options.limitVal
                }
                ).then(
                    data => { resolve(data); },
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
    },

    /*Invoices Data for CPS*/
    insertFlatDataInvoices(payload) {

        return new Promise(
            (resolve, reject) => {

                if (!payload) {
                    reject();
                }

                this.InvoiceDetailsIntegrationFlatData.create(payload).then(
                    () => {
                        resolve('Success');
                    },
                    err => {
                        reject(err);
                    }
                )
            });

    },

    getInvoiceHeaders(options) {
        return new Promise(
            (resolve, reject) => {
                this.InvoiceDetailsIntegrationFlatData.findAll(
                    {
                        attributes: ['OrderNumber', 'InvoiceNumber', 'InvoiceDate', 'InvoiceStatus', 'AuthorizationStatus'],
                        where: { DataIntegrationsID: options.intgid, Chunk: true },
                        group: ['OrderNumber', 'InvoiceNumber', 'InvoiceDate', 'InvoiceStatus', 'AuthorizationStatus']
                    }).then(
                        data => {
                            resolve(data);
                        },
                        err => {
                            reject(err);
                        }
                    );
            }
        );
    },

    insertInvoiceHeaders(payload) {
        return new Promise(
            (resolve, reject) => {
                this.Invoices.bulkCreate(payload).then(
                    data => {
                        resolve();
                    },
                    err => {
                        reject(err);
                    }
                );
            }
        );
    },

    getInvoiceDetails(options) {
        return new Promise(
            (resolve, reject) => {
                this.InvoiceDetailsIntegrationFlatData.findAll(
                    { where: { DataIntegrationsID: options.intgid, Chunk: true } }).then(
                        data => {
                            resolve(this.escapeInvoiceDetails(data));
                        },
                        err => {
                            reject(err);
                        }
                    );
            }
        );
    },

    escapeInvoiceDetails(data) {
        let escapedData = [];
        for (let invoiceDetail of data) {
            invoiceDetail.LineDescription = this.escapeString(invoiceDetail.LineDescription);
            escapedData.push(invoiceDetail);
        }
        return escapedData;
    },

    insertInvoiceDetails(payload) {
        return new Promise(
            (resolve, reject) => {
                this.InvoiceDetails.bulkCreate(payload).then(
                    data => {
                        resolve();
                    },
                    err => {
                        reject(err);
                    }
                );
            }
        );
    },

    getInvoiceHeadersToAdd(options) {
        this.DataIntegrations.hasMany(this.Invoices, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        this.Invoices.belongsTo(this.DataIntegrations, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        return new Promise(
            (resolve, reject) => {
                this.Invoices.findAll({
                    attributes: { exclude: ['ShouldSubmit', 'DataIntegrationsID', 'id'] },
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
                    data => { resolve(data); },
                    error => { reject(error); }
                );
            }
        );
    },

    getInvoiceDetailsToAdd(options) {
        this.DataIntegrations.hasMany(this.InvoiceDetails, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        this.InvoiceDetails.belongsTo(this.DataIntegrations, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        return new Promise(
            (resolve, reject) => {
                this.InvoiceDetails.findAll({
                    attributes: { exclude: ['ShouldSubmit', 'DataIntegrationsID', 'id'] },
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
                    data => { resolve(this.escapeInvoiceDetails(data)); },
                    error => { reject(error); }
                );
            }
        );

    },

    getInvoiceHeadersTotalCount(options) {
        this.DataIntegrations.hasMany(this.Invoices, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        this.Invoices.belongsTo(this.DataIntegrations, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        return new Promise(
            (resolve, reject) => {
                this.Invoices.count({
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

    getInvoiceDetailsTotalCount(options) {
        this.DataIntegrations.hasMany(this.InvoiceDetails, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        this.InvoiceDetails.belongsTo(this.DataIntegrations, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        return new Promise(
            (resolve, reject) => {
                this.InvoiceDetails.count({
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

    /**
     * Creates new data integration files record.
     * @param {*} payload Object that maps to DataIntegrationsFiles table columns.
     */
    insertDataIntegrationsFile(payload) {

        return new Promise(
            (resolve, reject) => {

                if (!payload) {
                    reject();
                }
                // console.log(payload);
                this.DataIntegrationsFiles.create({
                    FileNameAws: payload.FileNameAws,
                    AwsFileLink: payload.AwsFileLink,
                    Client: payload.Client,
                    DataIntegrationsID: payload.DataIntegrationsID
                }).then(
                    () => {
                        this.DataIntegrationsFiles.max('DataIntegrationsFilesID').then(
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

    getProcessedFiles(options) {
        this.DataIntegrationsFiles.hasMany(this.DataIntegrationsFiles, {
            foreignKey: {
                name: 'DataIntegrationsFilesID'
            }
        });

        this.DataIntegrationsFiles.belongsTo(this.DataIntegrationsFiles, {
            foreignKey: {
                name: 'DataIntegrationsFilesID'
            }
        });

        return new Promise(
            (resolve, reject) => {
                this.DataIntegrationsFiles
                    .findAll({
                        attributes: ['FileNameAws'],
                        where: { DataIntegrationsID: options.id }
                    })
                    .then(
                        data => {
                            // console.log('Get Processed files completed');
                            let fileNames = [];
                            for (let dataFile of data) {
                                fileNames.push(dataFile.dataValues.FileNameAws);
                            }
                            // console.log(fileNames);
                            resolve(fileNames);
                        },
                        err => {
                            console.log('Get Processed Files failed');
                            console.log(err);
                            reject(err);
                        }
                    );
            }
        );
    },

    getProcessedFilesLinks(options) {
        this.DataIntegrationsFiles.hasMany(this.DataIntegrationsFiles, {
            foreignKey: {
                name: 'DataIntegrationsFilesID'
            }
        });

        this.DataIntegrationsFiles.belongsTo(this.DataIntegrationsFiles, {
            foreignKey: {
                name: 'DataIntegrationsFilesID'
            }
        });

        return new Promise(
            (resolve, reject) => {
                this.DataIntegrationsFiles
                    .findAll({
                        attributes: ['FileNameAws', 'AwsFileLink'],
                        where: { DataIntegrationsID: options.id }
                    })
                    .then(
                        data => {
                            resolve(data);
                        },
                        err => {
                            reject(err);
                        }
                    );
            }
        );
    },

    escapeString(line) {
        return line.replace('\\', '\\\\').replace('"', '\"');
    }

}