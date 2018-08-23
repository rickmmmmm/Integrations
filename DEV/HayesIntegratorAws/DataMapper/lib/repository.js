const sequelize = require('sequelize');
const Promise = require('bluebird');
const chalk = require('chalk');
const _ = require('lodash');

const { Op } = sequelize;
const configuration = require('./configuration.js');

const database = configuration.config.database;
const username = configuration.config.username;
const password = configuration.secrets.password;

const success = obj => {
    console.log(chalk.green(`result: ${JSON.stringify(obj)}`));
};

const seq = new sequelize(database, username, password, {
    host: configuration.config.host,
    dialect: 'mssql',
    logging: false,
    define: {
        timestamps: false
    },
    pool: {
        max: 10,
        min: 1,
        requestTimeout: 10 * 60 * 1000,
        idle: 60 * 1000,
        acquire: 60 * 1000,
        evict: 10 * 60 * 1000,
        handleDisconnects: true
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
    PurchaseOrderIntegrationFlatDataID: { type: sequelize.INTEGER, unique: 'cindex', primaryKey: true, autoIncrement: true },
    PO_NUMBER: sequelize.STRING(255),
    LINE_NUMBER: sequelize.INTEGER,
    PO_DATE: sequelize.DATEONLY,
    VENDOR_NAME: sequelize.STRING(100),
    VENDOR_ID: sequelize.INTEGER,
    PRODUCT_NAME: sequelize.TEXT,
    PRODUCT_TYPE: sequelize.STRING(100),
    MODEL: sequelize.STRING(100),
    MANUFACTURER: sequelize.STRING(100),
    FUNDING_SOURCE: sequelize.STRING(100),
    DEPARTMENT: sequelize.STRING(100),
    ACCOUNT_CODE: sequelize.STRING(100),
    PURCHASE_PRICE: sequelize.DECIMAL,
    QUANTITYORDERED: sequelize.INTEGER,
    NOTES: sequelize.TEXT,
    SHIPPEDTOSITE: sequelize.STRING(100),
    QUANTITYSHIPPED: sequelize.INTEGER,
    CFDA: sequelize.STRING(50),
    IntegrationsID: sequelize.STRING(100),
    Chunk: sequelize.BOOLEAN
};

DataIntegrationsErrorsModel = {
    DataIntegrationsErrorsID: { type: sequelize.INTEGER, primaryKey: true, autoIncrement: true },
    ErrorNumber: sequelize.STRING(50),
    ErrorName: sequelize.TEXT,
    ErrorDescription: sequelize.TEXT,
    ErrorObject: sequelize.TEXT,
    DataIntegrationsID: sequelize.STRING(100)
};

DataIntegrationsActivityMonitorModel = {
    DataIntegrationsActivityMonitorID: { type: sequelize.INTEGER, primaryKey: true, autoIncrement: true },
    DataIntegrationsActivityMonitorObject: sequelize.STRING(10000),
    AddedDate: sequelize.DATE,
    DataIntegrationsID: sequelize.INTEGER
};

DataIntegrationsMappingsModel = {
    MappingsID: { type: sequelize.STRING(50), unique: 'compositeIdx' },
    MappingsStep: { type: sequelize.INTEGER, primaryKey: true, autoIncrement: true, unique: 'compositeIdx' },
    MappingsObject: sequelize.TEXT,
    Client: sequelize.STRING(50)
};

PurchaseOrderHeaderModel = {
    OrderNumber: { type: sequelize.STRING(50), unique: 'cidx', primaryKey: true },
    DataIntegrationsID: { type: sequelize.STRING(100), unique: 'cidx', primaryKey: true },
    Status: sequelize.STRING(50),
    VendorID: sequelize.INTEGER,
    VendorName: sequelize.STRING(100),
    SiteID: sequelize.STRING(500),
    PurchaseDate: sequelize.DATEONLY,
    EstimatedDeliveryDate: sequelize.DATE,
    Notes: sequelize.STRING(500),
    Other1: sequelize.STRING(100),
    ShouldSubmit: sequelize.BOOLEAN,
    Submitted: sequelize.BOOLEAN
};

PurchaseOrderDetailModel = {
    OrderNumber: { type: sequelize.STRING(50), unique: 'ccidx', primaryKey: true },
    LineNumber: { type: sequelize.INTEGER, unique: 'ccidx', primaryKey: true },
    DataIntegrationsID: { type: sequelize.STRING(100), unique: 'ccidx', primaryKey: true },
    Status: sequelize.STRING(50),
    SiteID: sequelize.STRING(100),
    QuantityOrdered: sequelize.INTEGER,
    QuantityReceived: sequelize.INTEGER,
    FundingSource: sequelize.STRING(500),
    ProductName: sequelize.STRING(100),
    PurchasePrice: sequelize.DECIMAL,
    AccountCode: sequelize.STRING(100),
    DepartmentID: sequelize.STRING(50),
    CFDA: sequelize.STRING(50),
    ShouldSubmit: sequelize.BOOLEAN,
    Submitted: sequelize.BOOLEAN
};

ShipmentIntegrationFlatDataModel = {
    ShipmentIntegrationFlatDataID: { type: sequelize.INTEGER, unique: 'cindex', primaryKey: true, autoIncrement: true },
    PO_NUMBER: sequelize.STRING(50),
    PO_CREATION_DATE: sequelize.STRING(100),
    LINE_NUM: sequelize.INTEGER,
    QUANTITY_RECEIVED: sequelize.INTEGER,
    SHIP_TO_UNIT: sequelize.STRING(100),
    SHIP_TO_LOCATION_CODE: sequelize.STRING(100),
    SHIP_TO_ADDRESS_LINE1: sequelize.STRING(100),
    SHIP_TO_CITY: sequelize.STRING(100),
    SHIP_TO_STATE: sequelize.STRING(100),
    SHIP_TO_ZIP: sequelize.STRING(100),
    IntegrationsID: sequelize.STRING(100),
    Chunk: sequelize.BOOLEAN
};

ShipmentsModel = {
    OrderNumber: { type: sequelize.STRING(50), unique: 'cccidx', primaryKey: true },
    LineNumber: { type: sequelize.INTEGER, unique: 'cccidx', primaryKey: true },
    SiteID: { type: sequelize.STRING(50), unique: 'cccidx', primaryKey: true },
    TicketNumber: sequelize.INTEGER,
    QuantityShipped: sequelize.INTEGER,
    TicketedBy: sequelize.STRING(50),
    TicketedDate: sequelize.DATEONLY,
    Status: sequelize.STRING(50),
    InvoiceNumber: sequelize.STRING(25),
    InvoiceDate: sequelize.DATEONLY,
    IntegrationsID: { type: sequelize.STRING(100), unique: 'cccidx' },
    ShouldSubmit: sequelize.BOOLEAN,
    Submitted: sequelize.BOOLEAN
};

ProductsModel = {
    ProductNumber: { type: sequelize.INTEGER, unique: 'ccccidx', primaryKey: true },
    ProductName: { type: sequelize.STRING(100), unique: 'ccccidx', primaryKey: true },
    ProductDescription: sequelize.STRING(500),
    ProductType: sequelize.STRING(50),
    Model: sequelize.STRING(50),
    Manufacturer: sequelize.STRING(100),
    SuggestedPrice: sequelize.DECIMAL,
    SKU: sequelize.STRING(50),
    Serial: sequelize.STRING(50),
    Added: sequelize.BOOLEAN,
    Updated: sequelize.BOOLEAN,
    AddedDate: sequelize.DATE,
    LastUpdatedDate: sequelize.DATE,
    Client: { type: sequelize.STRING(50), primaryKey: true }
};

VendorsModel = {
    VendorID: { type: sequelize.INTEGER, primaryKey: true },
    VendorName: { type: sequelize.STRING(100), primaryKey: true },
    Address1: sequelize.STRING(50),
    Address2: sequelize.STRING(50),
    City: sequelize.STRING(50),
    State: sequelize.STRING(2),
    ZipCode: sequelize.STRING(50),
    Phone: sequelize.STRING(50),
    Email: sequelize.STRING(100),
    Added: sequelize.BOOLEAN,
    Updated: sequelize.BOOLEAN,
    AddedDate: sequelize.DATE,
    LastUpdatedDate: sequelize.DATE,
    Client: { type: sequelize.STRING(50), primaryKey: true }
};

FundingSourcesModel = {
    FundingSourceID: sequelize.STRING(500),
    Added: sequelize.BOOLEAN,
    Updated: sequelize.BOOLEAN
};

LinkTableModel = {
    LinkID: { type: sequelize.INTEGER, primaryKey: true, autoIncrement: true },
    Client: sequelize.STRING,
    SourceVal: sequelize.STRING,
    DestVal: sequelize.STRING,
    LinkType: sequelize.STRING
};

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
};

InvoicesModel = {
    OrderNumber: { type: sequelize.STRING, primaryKey: true },
    InvoiceNumber: { type: sequelize.STRING, primaryKey: true },
    InvoiceDate: sequelize.STRING,
    InvoiceStatus: sequelize.STRING,
    AuthorizationStatus: sequelize.STRING,
    ShouldSubmit: sequelize.BOOLEAN,
    DataIntegrationsID: { type: sequelize.INTEGER, primaryKey: true },
    LastModifiedDate: sequelize.STRING
};

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
};

DataIntegrationsFilesModel = {
    DataIntegrationsFilesID: { type: sequelize.INTEGER, primaryKey: true, autoIncrement: true },
    FileNameAws: sequelize.STRING,
    AwsFileLink: sequelize.STRING,
    Client: sequelize.STRING(50),
    DataIntegrationsID: sequelize.STRING(100),
    DateAdded: sequelize.DATE
};

const resolveWhere = (batch, primaryKey) => {
    const where = {};
    for (let field of primaryKey) {
        const values = [];
        where[field] = {
            [Op.in]: values
        };
        for (let record of batch) {
            values.push(record[field]);
        }
    }
    return where;
};

const generatePrimaryKey = (row, primaryKey) => {
    if (_.isArray(primaryKey)) {
        return _.reduce(primaryKey, (result, key) => {
            return `${result}${row[key]}`;
        }, '')
    } else if (_.isString(primaryKey)) {
        return row[primaryKey];
    } else {
        throw Error(`The value ${primaryKey} is not a valid primary key field.`);
    }
};

const removeUnneededRecords = (primaryKey, batch, rows) => {
    if (rows.length) {
        for (const row of rows) {
            const rowPrimaryKey = generatePrimaryKey(row, primaryKey);
            _.remove(batch, record => {
                return rowPrimaryKey === generatePrimaryKey(record, primaryKey);
            });
        }
    }
    return batch;
};

/**
 *
 * @param {*} model Sequelize model.
 * @param {Array<*>} items Rows to be inserted.
 * @param {*} options Options for sequelize bulkCreate method. For more information consult <br/>
 *   http://docs.sequelizejs.com/class/lib/model.js~Model.html#static-method-bulkCreate
 * @returns {Promise<T>} Inserted rows.
 * @memberOf Repository
 * @private
 */
const bulkCreate = (model, items, options) => {
    return model.describe().then((schema) => {
        return Object.keys(schema).filter(field => schema[field].primaryKey);
    }).then(primaryKey => {
        let currentBatchIndex = 0;
        const batchSize = 25;
        const batches = [[]];
        for (const item of items) {
            const batch = batches[currentBatchIndex];
            if (batch.length >= batchSize) {
                batches.push([]);
                currentBatchIndex = batches.length - 1;
            }

            batch.push(item);
        }

        return seq.transaction(transaction => {
            const toSelect = batches.map(batch => {
                const where = resolveWhere(batch, primaryKey);
                return model.all({ where, raw: true, transaction });
            });
            return Promise.all(toSelect).then(data => ({ rows: _.concat.apply(null, data), primaryKey }));
        });

    }).then(({ rows, primaryKey }) => {
        return removeUnneededRecords(primaryKey, items, rows);
    }).then(items => {
        let currentBatchIndex = 0;
        const batchSize = 25,
            batches = [[]];
        for (const item of items) {
            const batch = batches[currentBatchIndex];

            if (batch.length >= batchSize) {
                batches.push([]);
                currentBatchIndex = batches.length - 1;
            }

            batch.push(item);
        }

        return seq.transaction((transaction) => {
            const opt = options || {};
            opt.transaction = transaction;
            const toInsert = batches.map(batch => model.bulkCreate(batch, opt));

            return Promise.all(toInsert);
        });
    }).catch(err => {
        console.log(JSON.stringify(err));
        throw err;
    });
};


/**
 * Insert the items provided to the table indicated by model without checking
 * for duplicate records. Only to be used on tables with Identity columns
 * @param {*} model Sequelize model.
 * @param {Array<*>} items Rows to be inserted.
 * @param {*} options Options for sequelize bulkCreate method. For more information consult <br/>
 *   http://docs.sequelizejs.com/class/lib/model.js~Model.html#static-method-bulkCreate
 * @returns {Promise<T>} Inserted rows.
 * @memberOf Repository
 * @private
 */
const bulkCreateFlat = (model, items, options) => {
    let currentBatchIndex = 0;
    const batchSize = 25,
        batches = [[]];
    for (const item of items) {
        const batch = batches[currentBatchIndex];

        if (batch.length >= batchSize) {
            batches.push([]);
            currentBatchIndex = batches.length - 1;
        }

        batch.push(item);
    }

    return seq.transaction((transaction) => {
        const opt = options || {};
        opt.transaction = transaction;
        const toInsert = batches.map(batch => model.bulkCreate(batch, opt));

        return Promise.all(toInsert);
    });
};

/**
 * Represents database manipulation.
 * @namespace Repository
 */
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
    ShipmentIntegrationFlatData: seq.define('ShipmentIntegrationFlatData', ShipmentIntegrationFlatDataModel),
    Shipments: seq.define('Shipments', ShipmentsModel),
    DataIntegrationsLinkTable: seq.define('DataIntegrationsLinkTable', LinkTableModel, { tableName: 'DataIntegrationsLinkTable' }),
    Invoices: seq.define('Invoices', InvoicesModel),
    InvoiceDetails: seq.define('InvoiceDetails', InvoiceDetailsModel),
    InvoiceDetailsIntegrationFlatData: seq.define('InvoiceDetailsIntegrationFlatData', InvoiceDetailsIntegrationFlatDataModel),
    DataIntegrationsFiles: seq.define('DataIntegrationsFiles', DataIntegrationsFilesModel),

    /**
     * Gets map objects from database of the type specified.
     * @param {Object} options Filters map type. Accepted options are 'purchases', 'charges',
     * 'inventory' OR internal db maps for 'po headers', 'po details', 'shipping'.
     * @memberOf Repository
     */
    getMappings(options) {
        return this.DataIntegrationsMappings.findAll({
            where: {
                MappingsID: options.type,
                Client: options.client
            }
        });
    },

    /**
     * Creates new integration record.
     * @param {*} payload Object that maps to DataIntegrations table columns.
     * @memberOf Repository
     */
    insertIntegration(payload) {
        if (!payload) {
            return Promise.reject(new Error('There\'s no payload sent to insertIntegration method'));
        }

        return this.DataIntegrations.create({
            IntegrationsObject: JSON.stringify(payload),
            Client: payload.client,
            DataProcessing: true,
            IntegrationType: payload.integrationType,
            IntegrationsID: payload.id
        }).then(result => {
            success(result.dataValues);
            return this.DataIntegrations.max('IntegrationsID');
        });
    },

    /**
     * Update data integration table to set that data was sent to TIPWeb server.
     * @param {string} intgid Integration identifier.
     * @returns {Promise<*>} Updated rows
     * @memberOf Repository
     */
    beginSendingToTipwebAPI(intgid) {
        return this.DataIntegrations.update({ DataSentToTipweb: true }, {
            where: {
                IntegrationsID: intgid
            }
        });
    },

    /**
     * Update data integration table to set that data was post processed.
     * @param {string} intgid Integration identifier.
     * @returns {Promise<*>} Updated rows.
     * @memberOf Repository
     */
    beginDataPostProcessing(intgid) {
        return this.DataIntegrations.update({ DataPostProcessing: true }, {
            where: {
                IntegrationsID: intgid
            }
        });
    },

    /**
     * Update data integration table to set that data processed successfully.
     * @param {string} intgid Integration identifier.
     * @returns {Promise<*>} Updated rows.
     * @memberOf Repository
     */
    completeIntegrationProcessing(intgid) {
        return this.DataIntegrations.update({ DataProcessedSuccessfully: true }, {
            where: { IntegrationsID: intgid }
        });
    },

    /**
     * Gets the currently processing integration ID value.
     * @returns {Promise<string>} Integration identifier.
     * @memberOf Repository
     */
    getProcessingIntegrationID() {
        return this.DataIntegrations.findOne({
            attributes: ['IntegrationsID'],
            where: {
                DataProcessing: true
            }
        }).then(data => {
            if (!data) {
                throw new Error('No integrations currently processing!');
            }
            return data.dataValues.IntegrationsID;
        });
    },

    /**
     * Gets the processing integration ID value from data that was sent to TIPWeb server.
     * @returns {Promise<string>} Integration identifier.
     * @memberOf Repository
     */
    getDataSendingToApiIntegrationID(client, mapType) {
        return this.DataIntegrations.findOne({
            attributes: ['IntegrationsID'],
            where: {
                DataSentToTipweb: true,
                DataProcessedSuccessfully: false,
                Client: client,
                IntegrationType: mapType
            }
        }).then(data => {
            if (!data) {
                throw new Error('No integration data currently being sent to TipWEB-IT!');
            }
            return data.dataValues.IntegrationsID;
        });
    },

    /**
     * Inserts flat purchase order data into database.
     * @param {*} payload A valid map to the table columns the flat data table. Required columns are PO_NUMBER, LINE_NUMBER, IntegrationsID
     * @param {*} options A valid options with a target.
     * @member Repository
     */
    insertFlatData(payload, options) {
        if (!payload) {
            const msg = 'No data provided to method insertFlatData.';
            console.error(chalk.red(msg));
            return Promise.reject(msg);
        }

        if (!options && !options.target) {
            const msg = 'No data target provided for flat client data.';
            console.error(chalk.red(msg));
            return Promise.reject(msg);
        }

        return bulkCreateFlat(this[options.target], payload);
    },

    /**
     * Logs an error in the database DataIntegrationsErrors table.
     * @param {*} payload Valid object to map to error table. Standard error object includes ErrorNumber, ErrorName, ErrorDescription, ErrorObject, IntegrationsID
     * @param {*} err Original error
     * @member Repository
     */
    logError(payload) {
        if (!payload) {
            return Promise.reject('There\'s no payload to log an error');
        }

        return this.DataIntegrationsErrors.create(payload).then(() => {
            // throw err; // shows original error on command line
            return Promise.resolve('Success');
        }).catch(err => {
            // throw err; // Treat any possible error with DataIntegrationsErrors model
            console.log();
            console.log('Save Error Failed');
            console.log(err);
            return Promise.reject(err);
        })
    },


    /**
     * Update submitted headers.
     * @param {*} options Options.
     * @param {Array<string>} options.headers Headers.
     * @returns {Promise<*>} Updated headers.
     * @memberOf Repository
     */
    updateSubmittedHeaders(options) {
        if (options.headers.length > 0) {
            return seq.transaction(transaction => {
                const toUpdate = [];
                for (let header of options.headers) {
                    toUpdate.push(this.PurchaseOrderHeader.update({
                        ShouldSubmit: false,
                        Submitted: true
                    }, {
                            where: {
                                OrderNumber: header.orderNumber,
                                VendorID: header.vendorID,
                                SiteID: header.siteID
                            },
                            transaction
                        }));
                }
                return Promise.all(toUpdate);
            }).catch(error => {
                let errObj = {
                    ErrorNumber: 500,
                    ErrorName: 'Update Submitted Headers',
                    ErrorDescription: 'Could not updated Submitted Headers values.',
                    ErrorObject: JSON.stringify(error),
                    DataIntegrationsID: options.id
                };

                return this.logError(errObj);
            });
        } else {
            // return Promise.reject(`There's no headers to update.`);
            return Promise.all([]);
        }
    },

    /**
     * Update submitted details.
     * @param {*} options Options.
     * @param {Array<string>} options.details Details.
     * @returns {Promise<*>} Updated details.
     * @memberOf Repository
     */
    updateSubmittedDetails(options) {
        if (options.details.length > 0) {
            return seq.transaction(transaction => {
                const toUpdate = [];
                for (let detail of options.details) {
                    toUpdate.push(this.PurchaseOrderDetail.update({
                        ShouldSubmit: false,
                        Submitted: true
                    }, {
                            where: {
                                OrderNumber: detail.orderNumber,
                                LineNumber: detail.lineNumber,
                                SiteID: detail.siteID
                            },
                            transaction
                        }));
                }

                return Promise.all(toUpdate);
            }).catch(error => {
                console.log();
                console.log('Update submitted details exception');
                console.log(error);
                let errObj = {
                    ErrorNumber: 500,
                    ErrorName: 'Update Submitted Details',
                    ErrorDescription: 'Could not updated Submitted Details values.',
                    ErrorObject: JSON.stringify(error),
                    DataIntegrationsID: options.id
                };

                return this.logError(errObj);
            })
        } else {
            console.log();
            console.log('No submitted details to update');
            return Promise.resolve([]);
        }
    },

    /**
     * Update submitted shipments.
     * @param {*} options Options.
     * @param {Array<string>} options.shipments Shipments.
     * @returns {Promise<*>} Updated shipments.
     * @memberOf Repository
     */
    updateSubmittedShipments(options) {
        if (options.shipments.length > 0) {
            return seq.transaction(transaction => {
                const toUpdate = [];
                for (let shipment of options.shipments) {
                    toUpdate.push(this.Shipments.update({
                        ShouldSubmit: false,
                        Submitted: true
                    }, {
                            where: {
                                OrderNumber: shipment.orderNumber,
                                LineNumber: shipment.lineNumber,
                                SiteID: shipment.siteID
                            },
                            transaction
                        }));
                }
                return Promise.all(toUpdate);
            }).catch(error => {
                let errObj = {
                    ErrorNumber: shipment.orderNumber + '-' + shipment.lineNumber + '-' + shipment.siteID,
                    ErrorName: 'Update Submitted Shipments',
                    ErrorDescription: 'Could not updated Submitted Shipments values.',
                    ErrorObject: JSON.stringify(error),
                    DataIntegrationsID: options.id
                };
                return this.logError(errObj);
            });

        } else {
            return Promise.all([]);
        }
    },

    /**
     * Execute a stored procedure to stage product data.
     * @param {*} options Options.
     * @param {string} options.client Client.
     * @returns {Promise<*>} Processed result.
     * @memberOf Repository
     */
    runProcIntegrations_StageProductData(options) {
        if (options && options.client) {
            return seq.query("EXEC Integrations_StageProductData '" + options.client + "'");
        }
        else {
            return Promise.reject('No parameters provided.');
        }
    },

    /**
     * Execute a stored procedure to remove unnecessary records.
     * @param {string} intgid Integration identifier.
     * @param {*} options Options.
     * @param {string} options.details Details.
     * @param {string} options.headers Headers.
     * @param {string} options.shipping Shipping.
     * @param {string} options.inventory Inventory.
     * @param {string} options.charges Charges.
     * @param {string} options.payments Payments.
     * @returns {Promise<*>} Processed result.
     * @memberOf Repository
     */
    runProcIntegrations_RemoveUnnecessaryRecords(intgid, options) {
        if (options) {
            let params = `'${intgid}',${options.headers}, \
                ${options.details}, ${options.shipping}, ${options.inventory}, \
                ${options.charges}, ${options.payments}`;
            return seq.query("EXEC Integrations_RemoveUnnecessaryUpdates " + params);
        } else {
            Promise.reject('No parameters provided.');
        }
    },

    runProcIntegrations_FlagDetailsAndShipmentsFromBadHeaderRecords(intgid) {
        return seq.query(`EXEC Integrations_FlagDetailsFromBadHeaderRecords ${intgid}`);
    },

    runProcIntegrations_FlagShipmentsFromBadDetailRecords(intgid) {
        return seq.query(`EXEC Integrations_FlagShipmentsFromBadDetailRecords ${intgid}`);
    },

    runProcIntegrations_AggregateDatafromPurchaseIntegration(date, client) {
        return seq.query(`EXEC Integrations_AggregateDatafromPurchaseIntegration '${client}', ${date}`);
    },

    /**
     * Execute a stored procedure to remove bad sites.
     * @param {*} options Options.
     * @param {string} options.client Client.
     * @param {string} options.intgid Integration identifier.
     * @returns {Promise<*>} Processed result.
     * @memberOf Repository
     */
    runProcz_custom_stpro_CPS_RemoveBadSites(options) {
        return seq.query(`EXEC z_custom_stpro_CPS_RemoveBadSites '${options.client}', ${options.intgid}`)
    },

    /**
     * Suggested for use to log actions as they occur in the application. May not need this and may instead copy the console to a text file upon completion.
     * @param {*} payload
     * @memberOf Repository
     * @deprecated
     */
    logActivity(payload) {
        if (!payload) {
            return Promise.reject(`Payload is required`);
        }

        return this.DataIntegrationsActivityMonitor.create(payload);
    },

    /* Vendors */

    // getCurrentVendors(useIDs = false) {
    // 	if (useIDs) {
    // 		return this.Vendors.findAll({
    // 			attributes: ['VendorName', 'VendorID'],
    // 			where: { Client: configuration.config.client },
    // 			group: ['VendorName', 'VendorID']
    // 		});
    // 	} else {
    // 		return this.Vendors.findAll({
    // 			attributes: ['VendorName'],
    // 			group: ['VendorName']
    // 		});
    // 	}
    // },

    // getNewVendors(intgid, currentVendors, useGroups) {
    // 	if (!intgid) {
    // 		return Promise.reject(`IntegrationID is required`);
    // 	}

    // 	let sqlConditions = [
    // 		{ IntegrationsID: intgid },
    // 		{ VENDOR_NAME: { $notIn: currentVendors } }
    // 	];

    // 	if (useGroups) {

    // 		const currentVendorNames = currentVendors.map(m => m.VendordName);
    // 		const currentVendorIDs = currentVendors.map(m => m.VendorID);

    // 		sqlConditions = [
    // 			{ IntegrationsID: intgid },
    // 			{ VENDOR_NAME: { $notIn: currentVendorNames } },
    // 			{ VENDOR_ID: { $notIn: currentVendorIDs } }
    // 		];
    // 	}
    // 	const fields = useGroups ? ['VENDOR_NAME', 'VENDOR_ID'] : 'VENDOR_NAME';
    // 	return this.PurchaseOrderIntegrationFlatData.findAll({
    // 		attributes: fields,
    // 		where: {
    // 			[Op.and]: sqlConditions
    // 		},
    // 		group: fields
    // 	})
    // },

    // insertVendors(payload) {
    // 	if (!payload) {
    // 		return Promise.reject(`Payload is required`);
    // 	}

    // 	return this.Vendors.bulkCreate(payload);
    // },

    /**
     * Execute a stored procedure to stage product data.
     * @param {*} options Options.
     * @param {string} options.client Client.
     * @returns {Promise<*>} Processed result.
     * @memberOf Repository
     */
    runProcIntegrations_StageVendorData(options) {
        if (options && options.client) {
            return seq.query("EXEC Integrations_StageVendorData '" + options.client + "'");
        }
        else {
            return Promise.reject('No parameters provided.');
        }
    },

    getVendorsToUpsert(options) {
        return this.Vendors.findAll({
            attributes: { exclude: 'id' },
            where: {
                [Op.or]: [
                    { Added: true },
                    { Updated: true }
                ],
                Client: options.client
            },
            limit: options.limitVal,
            offset: options.offsetVal
        });
    },

    toggleVendorSyncSwitch() {
        return this.Vendors.update({
            Added: false,
            Updated: false
        }, {
                where: {
                    [Op.or]: [
                        { Added: true },
                        { Updated: true }
                    ]
                },
                Client: configuration.config.client
            });
    },

    /* Products */

    getNewProducts(oldProducts, intgid) {
        const sqlIn = oldProducts.map(m => m.ProductName);

        return this.PurchaseOrderIntegrationFlatData.findAll({
            attributes: [
                'PRODUCT_NAME',
                'PRODUCT_TYPE',
                'MODEL',
                'MANUFACTURER',
                [sequelize.fn('MAX', sequelize.col('PURCHASE_PRICE')), 'SuggestedPrice']
            ],
            group: ['PRODUCT_NAME', 'PRODUCT_TYPE', 'MODEL', 'MANUFACTURER'],
            where: {
                IntegrationsID: intgid,
                PRODUCT_NAME: {
                    [Op.notIn]: sqlIn
                }
            }
        })
    },

    getCurrentProducts() {
        return this.Products.findAll({
            attributes: ['ProductName'],
            group: 'ProductName'
        });
    },

    insertNewProducts(payload) {
        return bulkCreate(this.Products, payload);
    },

    updateProductNames(newProductNamesList) {
        const toUpdate = [];
        for (let product of newProductNamesList) {
            const { ProductName } = product;
            toUpdate.push(this.Products.update(product, {
                where: { ProductName }
            }));
        }
        return Promise.all(toUpdate);
    },

    toggleProductsSyncSwitch() {
        return this.Products.update({
            Added: false,
            Updated: false
        }, {
                where: {
                    [Op.or]: [
                        { Added: true },
                        { Updated: true }
                    ],
                    Client: configuration.config.client
                }
            });
    },

    /**
     * Retrieve products that have either been added or updated.
     */
    getProductsToUpsert(options) {
        return this.Products.findAll({
            attributes: {
                exclude: ['Added', 'Updated', 'AddedDate', 'LastUpdatedDate', 'id']
            },
            where: {
                [Op.or]: [{ Added: true }, { Updated: true }],
                Client: options.client
            },
            limit: options.limitVal,
            offset: options.offset
        });
    },

    /* Funding Sources */

    toggleFundingSourcesSyncSwitch() {
        return this.FundingSources.update({
            Added: false
        });
    },

    getCurrentFundingSources() {
        return this.FundingSources.findAll({ attributes: ['FundingSourceID'] });
    },

    getNewFundingSources(oldSources, intgid) {
        const sqlIn = oldSources.map(m => m.FundingSourceID);
        return this.PurchaseOrderIntegrationFlatData.findAll({
            attributes: ['FUNDING_SOURCE'],
            group: ['FUNDING_SOURCE'],
            where: {
                IntegrationsID: intgid,
                FUNDING_SOURCE: { $notIn: sqlIn }
            }
        });
    },

    insertNewFundingSources(payload) {
        return this.FundingSources.bulkCreate(payload);
    },

    /**
     * Get purchase order headers.
     * @param {string} IntegrationsID Integration identifier.
     * @returns {Promise<*>} Selected rows.
     * @memberOf Repository
     */
    getHeaderRecordsFlatData(IntegrationsID) {
        return this.PurchaseOrderIntegrationFlatData.all({
            attributes: ['PO_NUMBER', 'PO_DATE', 'VENDOR_ID', 'VENDOR_NAME', 'SHIPPEDTOSITE'],
            group: ['PO_NUMBER', 'PO_DATE', 'VENDOR_ID', 'VENDOR_NAME', 'SHIPPEDTOSITE'],
            where: {
                IntegrationsID,
                Chunk: true
            }
        });
    },

    /**
     * Get purchase detail data.
     * @param {string} IntegrationsID Integration identifier.
     * @returns {Promise<*>} Selected rows.
     * @memberOf Repository
     */
    getDetailRecordsFlatData(IntegrationsID) {
        return this.PurchaseOrderIntegrationFlatData.all({
            attributes: { exclude: 'id' },
            where: {
                IntegrationsID,
                Chunk: true
            }
        });
    },

    /**
     * Get shipments data.
     * @param {string} IntegrationsID Integration identifier.
     * @returns {Promise<*>} Selected rows.
     * @memberOf Repository
     */
    getShipmentsRecordsFlatData(IntegrationsID) {
        return this.ShipmentIntegrationFlatData.all({
            attributes: { exclude: 'id' },
            where: {
                IntegrationsID,
                Chunk: true
            }
        });
    },

    /**
     * Insert purchase order headers.
     * @param {*} payload Payload.
     * @returns {Promise<*>} Inserted rows.
     * @memberOf Repository
     */
    insertHeaderRecords(payload) {
        return bulkCreate(this.PurchaseOrderHeader, payload);
    },

    /**
     * Insert purchase detail data.
     * @param {*} payload Payload.
     * @returns {Promise<*>} Inserted rows.
     * @memberOf Repository
     */
    insertDetailRecords(payload) {
        return bulkCreate(this.PurchaseOrderDetail, payload);
    },

    /**
     * Insert Shipment data.
     * @param {*} payload Payload.
     * @returns {Promise<*>} Inserted rows.
     * @memberOf Repository
     */
    insertShipmentRecords(payload) {
        return bulkCreate(this.Shipments, payload);
    },

    getTotalHeadersToUpsertCount(options) {
        this.DataIntegrations.hasMany(this.PurchaseOrderHeader, {
            foreignKey: {
                name: 'IntegrationsID'
            }
        });

        this.PurchaseOrderHeader.belongsTo(this.DataIntegrations, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        var targetPurchaseDate = new Date();
        targetPurchaseDate.setDate(targetPurchaseDate.getDate() - 365); //Set date to one year prior

        return this.PurchaseOrderHeader.count({
            // attributes: ['OrderNumber'],
            // group: ['OrderNumber'],
            include: [{
                attributes: [],
                model: this.DataIntegrations,
                where: {
                    Client: options.client,
                    IntegrationsID: options.id,
                    DataSentToTipweb: true
                }
            }],
            where: {
                DataIntegrationsID: options.id,
                ShouldSubmit: true,
                PurchaseDate: {
                    [Op.gte]: targetPurchaseDate
                }
            },
        });
    },

    getTotalDetailsToUpsertCount(options) {

        this.DataIntegrations.hasMany(this.PurchaseOrderDetail, {
            foreignKey: {
                name: 'IntegrationsID'
            }
        });

        this.PurchaseOrderHeader.belongsTo(this.DataIntegrations, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        this.PurchaseOrderDetail.belongsTo(this.DataIntegrations, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        this.PurchaseOrderDetail.hasOne(this.PurchaseOrderHeader, {
            foreignKey: {
                name: 'OrderNumber'
            }
        });

        this.PurchaseOrderHeader.hasMany(this.PurchaseOrderDetail, {
            foreignKey: {
                name: 'OrderNumber'
            }
        });

        var targetPurchaseDate = new Date();
        targetPurchaseDate.setDate(targetPurchaseDate.getDate() - 365); //Set date to one year prior

        return this.PurchaseOrderDetail.count({
            // attributes: ['OrderNumber', 'LineNumber'],
            // group: ['OrderNumber', 'LineNumber'],
            include: [{
                attributes: [],
                model: this.DataIntegrations,
                where: {
                    Client: options.client,
                    DataSentToTipweb: true,
                    IntegrationsID: options.id
                }
            },
            {
                attributes: [],
                model: this.PurchaseOrderHeader,
                where: {
                    DataIntegrationsID: options.id,
                    PurchaseDate: {
                        [Op.gte]: targetPurchaseDate
                    }
                }
            }],
            where: { ShouldSubmit: true, DataIntegrationsID: options.id },
        });
    },

    getTotalVendorsToUpsertCount(options) {

        return this.Vendors.count({
            where: {
                [Op.or]: [
                    { Added: true },
                    { Updated: true }
                ],
                Client: options.client
            }
        });
    },

    getTotalProductsToUpsertCount(options) {
        return this.Products.count({
            where: {
                [Op.or]: [
                    { Added: true },
                    { Updated: true }
                ],
                Client: options.client
            }
        });
    },

    getHeadersToUpsert(options) {

        this.DataIntegrations.hasMany(this.PurchaseOrderHeader, {
            foreignKey: {
                name: 'IntegrationsID'
            }
        });

        this.PurchaseOrderHeader.belongsTo(this.DataIntegrations, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        var targetPurchaseDate = new Date();
        targetPurchaseDate.setDate(targetPurchaseDate.getDate() - 365); //Set date to one year prior

        const fields = [
            'OrderNumber', 'Status', 'VendorID', 'VendorName', 'SiteID',
            'PurchaseDate', 'EstimatedDeliveryDate', 'Notes', 'Other1'
        ];
        return this.PurchaseOrderHeader.findAll({
            // attributes: { exclude: ['ShouldSubmit', 'DataIntegrationsID', 'id'] },
            attributes: fields,
            group: fields,
            include: [{
                attributes: [],
                model: this.DataIntegrations,
                where: {
                    Client: options.client,
                    IntegrationsID: options.intgid
                }
            }],
            where: {
                ShouldSubmit: true,
                DataIntegrationsID: options.intgid,
                PurchaseDate: {
                    [Op.gte]: targetPurchaseDate
                }
            },
            offset: options.offsetVal,
            limit: options.limitVal,
            order: ['OrderNumber']
        });
    },

    getDetailsToUpsert(options) {

        this.DataIntegrations.hasMany(this.PurchaseOrderDetail, {
            foreignKey: {
                name: 'IntegrationsID'
            }
        });

        this.PurchaseOrderDetail.belongsTo(this.DataIntegrations, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        this.PurchaseOrderHeader.belongsTo(this.DataIntegrations, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        this.PurchaseOrderDetail.hasOne(this.PurchaseOrderHeader, {
            foreignKey: {
                name: 'OrderNumber'
            }
        });

        this.PurchaseOrderHeader.hasMany(this.PurchaseOrderDetail, {
            foreignKey: {
                name: 'OrderNumber'
            }
        });

        var targetPurchaseDate = new Date();
        targetPurchaseDate.setDate(targetPurchaseDate.getDate() - 365); //Set date to one year prior

        const fields = [
            'OrderNumber', 'LineNumber', 'Status', 'SiteID', 'FundingSource',
            'ProductName', 'QuantityOrdered', 'QuantityReceived', 'PurchasePrice',
            'AccountCode', 'DepartmentID', 'CFDA'
        ];

        const groupByFields = [
            'PurchaseOrderDetail.OrderNumber', 'LineNumber', 'PurchaseOrderDetail.Status', 'PurchaseOrderDetail.SiteID', 'FundingSource',
            'ProductName', 'QuantityOrdered', 'QuantityReceived', 'PurchasePrice',
            'AccountCode', 'DepartmentID', 'CFDA'
        ];

        return this.PurchaseOrderDetail.findAll({
            // attributes: { exclude: ['ShouldSubmit', 'DataIntegrationsID', 'id'] },
            attributes: fields,
            group: groupByFields,
            include: [{
                attributes: [],
                model: this.DataIntegrations,
                where: {
                    Client: options.client,
                    IntegrationsID: options.intgid
                }
            },
            {
                attributes: [],
                model: this.PurchaseOrderHeader,
                where: {
                    DataIntegrationsID: options.intgid,
                    PurchaseDate: {
                        [Op.gte]: targetPurchaseDate
                    }
                }
            }],
            where: { ShouldSubmit: true, DataIntegrationsID: options.intgid },
            offset: options.offsetVal,
            limit: options.limitVal
        }).then(data => this.escapePurchaseOrderDetails(data));
    },

    escapePurchaseOrderDetails(data) {
        // console.log(data);
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
        return this.PurchaseOrderIntegrationFlatData.findAll({
            attributes: { exclude: 'id' },
            where: {
                IntegrationsID: intgid,
                Chunk: true
            }
        }).then(data => this.escapeShipments(data));
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
        return bulkCreate(this.Shipments, payload);
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

        // this.PurchaseOrderHeader.belongsTo(this.DataIntegrations, {
        //     foreignKey: {
        //         name: 'DataIntegrationsID'
        //     }
        // });

        this.Shipments.hasOne(this.PurchaseOrderHeader, {
            foreignKey: {
                name: 'OrderNumber'
            }
        });

        this.PurchaseOrderHeader.hasMany(this.Shipments, {
            foreignKey: {
                name: 'OrderNumber'
            }
        });

        var targetPurchaseDate = new Date();
        targetPurchaseDate.setDate(targetPurchaseDate.getDate() - 365); //Set date to one year prior

        return this.Shipments.count({
            // attributes: ['OrderNumber', 'LineNumber'],
            // group: ['OrderNumber', 'LineNumber'],
            include: [{
                attributes: [],
                model: this.DataIntegrations,
                where: {
                    Client: options.client,
                    DataSentToTipweb: true,
                    IntegrationsID: options.id
                }
            },
            {
                attributes: [],
                model: this.PurchaseOrderHeader,
                where: {
                    // DataIntegrationsID: options.id,
                    PurchaseDate: {
                        [Op.gte]: targetPurchaseDate
                    }
                }
            }],
            where: { ShouldSubmit: true, IntegrationsID: options.id },
        });
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

        // this.PurchaseOrderHeader.belongsTo(this.DataIntegrations, {
        //     foreignKey: {
        //         name: 'DataIntegrationsID'
        //     }
        // });

        this.Shipments.hasOne(this.PurchaseOrderHeader, {
            foreignKey: {
                name: 'OrderNumber'
            }
        });

        this.PurchaseOrderHeader.hasMany(this.Shipments, {
            foreignKey: {
                name: 'OrderNumber'
            }
        });

        const fields = [
            'OrderNumber', 'LineNumber', 'SiteID', 'TicketNumber',
            'QuantityShipped', 'TicketedBy', 'TicketedDate',
            'Status', 'InvoiceNumber', 'InvoiceDate'
        ];

        const groupByFields = [
            'Shipments.OrderNumber', 'LineNumber', 'Shipments.SiteID', 'TicketNumber',
            'QuantityShipped', 'TicketedBy', 'TicketedDate',
            'Shipments.Status', 'InvoiceNumber', 'InvoiceDate'
        ];

        var targetPurchaseDate = new Date();
        targetPurchaseDate.setDate(targetPurchaseDate.getDate() - 365); //Set date to one year prior

        return this.Shipments.findAll({
            // attributes: { exclude: ['ShouldSubmit', 'DataIntegrationsID', 'id'] },
            attributes: fields,
            group: groupByFields,
            include: [{
                attributes: [],
                model: this.DataIntegrations,
                where: {
                    Client: options.client,
                    IntegrationsID: options.id
                }
            },
            {
                attributes: [],
                model: this.PurchaseOrderHeader,
                where: {
                    // DataIntegrationsID: options.id,
                    PurchaseDate: {
                        [Op.gte]: targetPurchaseDate
                    }
                }
            }],
            where: { ShouldSubmit: true, IntegrationsID: options.id },
            offset: options.offsetVal,
            limit: options.limitVal
        });
    },

    /**
    * Determine chunk field on PurchaseOrderIntegrationFlatData table as false.
    * @param {string} intgid Integration identifier.
    * @returns {Promise<*>} Updated rows.
    * @memberOf Repository
    */
    toggleChunk(options) {
        if (!options || !options.target || !options.intgid) {
            const msg = 'No data target provided for flat client data.';
            return Promise.reject(msg);
        }

        return this[options.target].update({
            Chunk: false
        }, {
                where: {
                    Chunk: true,
                    IntegrationsID: options.intgid
                }
            })
    },

    getLinkTableData(options) {
        return this.DataIntegrationsLinkTable.findAll({
            attributes: ['SourceVal', 'DestVal'],
            where: {
                Client: options.client,
                LinkType: options.l.type
            }
        }).then(result => ({ options, result }));
    },

    /*Invoices Data for CPS*/
    insertFlatDataInvoices(payload) {
        if (!payload) {
            return Promise.reject(`Payload is needed`);
        }

        return this.InvoiceDetailsIntegrationFlatData.create(payload);

    },

    getInvoiceHeaders(options) {
        const fields = [
            'OrderNumber', 'InvoiceNumber', 'InvoiceDate', 'InvoiceStatus', 'AuthorizationStatus'
        ];

        return this.InvoiceDetailsIntegrationFlatData.findAll({
            attributes: fields,
            group: fields,
            where: {
                DataIntegrationsID: options.intgid,
                Chunk: true
            }
        });
    },

    insertInvoiceHeaders(payload) {
        return bulkCreate(this.Invoices, payload);
    },

    getInvoiceDetails(options) {
        return this.InvoiceDetailsIntegrationFlatData.findAll({
            where: {
                DataIntegrationsID: options.intgid,
                Chunk: true
            }
        }).then(data => this.escapeInvoiceDetails(data));
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
        return bulkCreate(this.InvoiceDetails, payload);
    },

    getInvoiceHeadersToAdd(options) {
        this.DataIntegrations.hasMany(this.Invoices, {
            foreignKey: {
                name: 'IntegrationsID'
            }
        });

        this.Invoices.belongsTo(this.DataIntegrations, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        return this.Invoices.findAll({
            attributes: { exclude: ['ShouldSubmit', 'DataIntegrationsID', 'id'] },
            include: [{
                model: this.DataIntegrations,
                where: {
                    Client: options.client,
                    DataSentToTipweb: true,
                    IntegrationsID: options.id
                }
            }],
            where: { ShouldSubmit: true, DataIntegrationsID: options.id },
            offset: options.offsetVal,
            limit: options.limitVal,
            order: ['OrderNumber']

        });
    },

    getInvoiceDetailsToAdd(options) {
        this.DataIntegrations.hasMany(this.InvoiceDetails, {
            foreignKey: {
                name: 'IntegrationsID'
            }
        });

        this.InvoiceDetails.belongsTo(this.DataIntegrations, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        return this.InvoiceDetails.findAll({
            attributes: { exclude: ['ShouldSubmit', 'DataIntegrationsID', 'id'] },
            include: [{
                model: this.DataIntegrations,
                where: {
                    Client: options.client,
                    DataSentToTipweb: true,
                    IntegrationsID: options.id
                }
            }],
            where: { ShouldSubmit: true, DataIntegrationsID: options.id },
            offset: options.offsetVal,
            limit: options.limitVal,
            order: ['OrderNumber']
        }).then(data => this.escapeInvoiceDetails(data));

    },

    getInvoiceHeadersTotalCount(options) {
        this.DataIntegrations.hasMany(this.Invoices, {
            foreignKey: {
                name: 'IntegrationsID'
            }
        });

        this.Invoices.belongsTo(this.DataIntegrations, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        return this.Invoices.count({
            include: [{
                model: this.DataIntegrations,
                where: {
                    Client: options.client,
                    DataSentToTipweb: true,
                    IntegrationsID: options.id
                }
            }],
            where: { ShouldSubmit: true, DataIntegrationsID: options.id },
        });
    },

    getInvoiceDetailsTotalCount(options) {
        this.DataIntegrations.hasMany(this.InvoiceDetails, {
            foreignKey: {
                name: 'IntegrationsID'
            }
        });

        this.InvoiceDetails.belongsTo(this.DataIntegrations, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        return this.InvoiceDetails.count({
            include: [{
                model: this.DataIntegrations,
                where: {
                    Client: options.client,
                    DataSentToTipweb: true,
                    IntegrationsID: options.id
                }
            }],
            where: { ShouldSubmit: true, DataIntegrationsID: options.id },
        });
    },

    /**
    * Creates new data integration files record.
    * @param {*} payload Object that maps to DataIntegrationsFiles table columns.
    * @returns {Promise<string>} Last created integration file id.
    * @memberOf Repository
    */
    insertDataIntegrationsFile(payload) {

        if (!payload) {
            return Promise.reject('Payload is not informed to insert data integration file');
        }

        return this.DataIntegrationsFiles.create({
            FileNameAws: payload.FileNameAws,
            AwsFileLink: payload.AwsFileLink,
            Client: payload.Client,
            DataIntegrationsID: payload.DataIntegrationsID
        }, { raw: true }).then(result => {
            success(result.dataValues);
            return this.DataIntegrationsFiles.max('DataIntegrationsFilesID')
        });
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

        return this.DataIntegrationsFiles.findAll({
            attributes: ['FileNameAws'],
            where: { DataIntegrationsID: options.id }
        }).then(data => {
            // console.log('Get Processed files completed');
            let fileNames = [];
            for (let dataFile of data) {
                fileNames.push(dataFile.dataValues.FileNameAws);
            }
            // console.log(fileNames);
            return fileNames;
        });
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

        return this.DataIntegrationsFiles.findAll({
            attributes: ['FileNameAws', 'AwsFileLink'],
            where: { DataIntegrationsID: options.id }
        });
    },

    escapeString(line) {
        return line.replace('\\', '\\\\').replace('"', '\"');
    }

};