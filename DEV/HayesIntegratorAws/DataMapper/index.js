#!/usr/bin/env node
"use strict";

/**
 * Commands for Data Mapper project
 * @namespace Commands
 */

const chalk = require('chalk');
const CLI = require('clui');
const Promise = require('bluebird');
const figlet = require('figlet');
const Papa = require('papaparse');
const Spinner = CLI.Spinner;
const _ = require('lodash');
const fs = require('fs');
const repository = require('./lib/repository.js');
const fileTasks = require('./lib/file-tasks.js');
const configuration = require('./lib/configuration.js');
const mappings = require('./lib/mappings.js');
const rq = require('./lib/http-requests');

const activities = [
    { name: '--help', shortname: '-h', desc: 'Show help menu.', action: showHelp },
    { name: '--config', shortname: '-co', desc: 'Show current configuration settings.', action: getConfiguration },

    { name: '--create', shortname: '-cr', desc: 'Creates New Integration in database using instance id as unique key.', action: createIntegration, reqOptions: ['id'] },
    { name: '--chunk', shortname: '-chu', desc: 'Toggles a chunk of data imported as completed.', action: toggleChunkedData, reqOptions: ['id'] },
    { name: '--sending-id', shortname: '-sid', desc: 'Gets the first Integration ID that is sending to TIPWEBAPI', action: getProcessingIntegrationID },
    { name: '--get-token', shortname: '-gta', desc: 'Get a token from the API', action: getApiToken },
    { name: '--send-to-api', shortname: '-S', desc: 'Toggles an integration ID from DataProcessing to DataSentToTipweb.', action: toggleToSending, reqOptions: ['id'] },
    { name: '--post-processing', shortname: '-PP', desc: 'Toggles an integration ID to DataPostProcessing.', action: toggleToPostProcessing, reqOptions: ['id'] },
    { name: '--complete', shortname: '-C', desc: 'Toggles an integration ID to completed.', action: toggleSuccessfulIntegration, reqOptions: ['id'] },
    { name: '--mapflat', shortname: '-m', desc: 'Map purchase order data from JSON file to database table.', action: mapFromFile, reqOptions: ['filename', 'id'] },
    { name: '--csv-to-json', shortname: '-ctj', desc: 'Convert an existent csv file to a JSON file', action: csvToJson, reqOptions: ['filename', 'fileOutput'] },

    { name: '--vendors', shortname: '-v', desc: 'Adds vendors to staging database.', action: stageNewVendors, reqOptions: ['useid', 'id'] },
    { name: '--push-vendors', shortname: '-pv', desc: 'Push new vendor records via TIPWEBAPI.', action: upsertAllVendors, reqOptions: ['id', 'iVal', 'lv'] },
    { name: '--toggle-vendors', shortname: '-tv', desc: 'Toggle vendors from not sent to sent to TIPWEBAPI', action: toggleVendors },

    { name: '--products', shortname: '-p', desc: 'Map products from flat data table to Products.', action: productsFunc },
    { name: '--push-products', shortname: '-pp', desc: 'Push new product records via TIPWEBAPI.', action: upsertAllProducts, reqOptions: ['id', 'iVal', 'lv'] },
    { name: '--toggle-products', shortname: '-tp', desc: 'Toggle products from not sent to sent to TIPWEBAPI.', action: toggleProducts },

    { name: '--funding', shortname: '-f', desc: 'Map funding sources from flat data table to FundingSources.', action: fundingFunc },

    { name: '--headers', shortname: '-hd', desc: 'Map headers from flat data table to PurchaseOrderHeaders.', action: headersFunc, reqOptions: ['id'] },
    { name: '--push-headers', shortname: '-ph', desc: 'Push new header records via TIPWEBAPI.', action: upsertAllHeaders, reqOptions: ['id', 'iVal', 'lv'] },

    { name: '--details', shortname: '-det', desc: 'Map details from flat data table to PurchaseOrderDetails.', action: detailsFunc, reqOptions: ['id'] },
    { name: '--push-details', shortname: '-pd', desc: 'Push new detail records via TIPWEBAPI.', action: upsertAllDetails, reqOptions: ['id', 'iVal', 'lv'] },

    { name: '--filter-unncessary', shortname: '-fu', desc: 'Filters out any records already sent.', action: filterOldRecords, reqOptions: ['id'] },

    { name: '--filter-bad-details', shortname: '-fbd', desc: 'Removes any detail and shipment records with a bad header.', action: filterDetailsWithBadHeaders, reqOptions: ['id'] },

    { name: '--shipments', shortname: '-sp', desc: 'Map shipments from flat data to Shipments.', action: shippingFunc, reqOptions: ['id', 'iVal', 'lv'] },
    { name: '--push-shipments', shortname: '-ps', desc: 'Push new shipment records via TIPWEBAPI.', action: upsertAllShipments, reqOptions: ['id', 'iVal', 'lv'] },

    { name: '--filter-bad-shipments', shortname: '-fbs', desc: 'Removes any shipment records with a bad detail record.', action: filterShipmentsWithBadDetails, reqOptions: ['id'] },

    { name: '--invoices', shortname: '-in', desc: 'Map invoice headers from flat data table to Invoices.', action: invoiceHeaderFunc },
    { name: '--invoice-details', shortname: '-ind', desc: 'Map invoice details records from flat data table to InvoiceDetails.', action: invoiceDetailsFunc },

    { name: '--push-invoices', shortname: '-pinv', desc: 'Push new invoice records via TIPWEBAPI.', action: pushInvoiceHeaders, options: { iVal: 0, lv: 800 } },
    { name: '--push-invoice-details', shortname: '-pind', desc: 'Push new invoice detail records via TIPWEBAPI.', action: pushInvoiceDetails, options: { iVal: 0, lv: 800 } },

    { name: '--custom-scripts', shortname: '-cust', desc: 'Runs a list of custom scripts on imported data.', action: runCustomScripts },

    { name: '--get-link-data', shortname: '-gld', desc: 'Get link table data for integration.', action: getLinkTableData },

    { name: '--insert-process-file', shortname: '-ipf', desc: 'Create the Data Integrations files', action: insertDataIntegrationsFile, reqOptions: ['client', 'id', 'filename', 'filelink'] },
    { name: '--get-processed-files', shortname: '-gpf', desc: 'Get the files processed by the specified DataIntegrationsID', action: getProcessedFiles, reqOptions: ['id'] },
    { name: '--get-processed-file-links', shortname: '-gpfl', desc: 'Get the files and links to processed by the specified DataIntegrationsID', action: getProcessedFilesLinks, reqOptions: ['id'] }
];

const args = [
    { name: '--filename', shortname: '-f', desc: 'Generic filename attribute.', objectKey: 'filename' },
    { name: '--filepath', shortname: '-fp', desc: 'Generic filepath attribute.', objectKey: 'filepath' },
    { name: '--filelink', shortname: '-fl', desc: 'Generic filelink attribute.', objectKey: 'filelink' },
    { name: '--file-output', shortname: '-fo', desc: 'Output file as a result', objectKey: 'fileOutput' },
    { name: '--useid', shortname: '-ids', desc: 'Generic element to handle use of ID field', objectKey: 'useIDs' },
    { name: '--iVal', shortname: '-i', desc: 'Value for i in recursive functions.', objectKey: 'iVal' },
    { name: '--lengthValue', shortname: '-lv', desc: 'Value for chunk length in recursive functions.', objectKey: 'lv' },
    { name: '--identifier', shortname: '-id', desc: 'Value for application execution unique identifier.', objectKey: 'id' },
    { name: '--client', shortname: '-cl', desc: 'Value for application Client.', objectKey: 'client' },
];

let theArgs = processArgs(process.argv.slice(3, process.argv.length));

fire(process.argv[2], theArgs);

function processArgs(argsv) {

    let subaction = {};
    let argVal;

    for (let arg of argsv) {
        let argid = argsv.indexOf(arg);
        if (argid % 2 === 0) {
            argVal = args.filter(fil => { return fil.name === arg || fil.shortname === arg });
            if (argVal) {
                subaction[argVal[0].objectKey] = argsv[argid + 1];
            }
            else {
                console.error(chalk.red('Error in' + arg + 'No such option'));
            }
        }

    }

    return subaction;
}
/**
 * Entry point function.
 * @param {*} action argument 1 from command line
 * @param {*} subaction argument 2 from command line
 * @memberOf Commands
 * @public
 */
function fire(action, subaction) {
    let a001 = activities.filter(fil => fil.name === action || fil.shortname === action);
    if (a001 && a001.length === 1) {
        let cb = a001[0].action;
        let options = subaction;
        let spinner = new Spinner('', ['.', '..', '...', '....']);
        spinner.start();

        cb(options).then(result => {
            console.log(chalk.green('Done!'));
            console.log(chalk.green(result));
            process.exit(0);
        }).catch(err => {
            console.error(chalk.red(err && err.message || err));
            console.error(chalk.red('Done but with errors...'));
            process.exit(100);
        });
    }
    else {
        console.error('Invalid command.');
        process.exit(100);
    }
}

/**
 * Converts csv files into json.
 * @param {*} options Options.
 * @param {string} options.filename CSV file name.
 * @param {string} options.fileOutput JSON file path.
 * @returns {PromiseLike<string>} A promise with the a success message or a fail one.
 * @memberOf Commands
 */
function csvToJson(options) {
    const opt = options || {};
    if (!opt.filename) {
        return Promise.reject(`file name wasn't provided.`);
    }

    if (!opt.fileOutput) {
        return Promise.reject(`file output wasn't provided.`)
    }
    return fileTasks.getDataFile(options.filename).then(csv => {
        return new Promise((resolve, reject) => {
            const data = [];
            Papa.parse(mappings.normalizeCSV(csv), {
                worker: true,
                header: true,
                skipEmptyLines: true,
                delimiter: `,`,
                quoteChar: `'`,
                step(result) {
                    data.push(_.head(result.data));
                    if (result.errors.length) {
                        console.log(`Row data: ${result.data}`);
                        result.errors.map(error => {
                            console.log(chalk.red(`Row error: ${error.message}`));
                        });
                    }
                },
                error(err) {
                    console.log(err.message);
                    reject(err);
                },
                complete() {
                    resolve(JSON.stringify(data));
                    console.log(chalk.green('File converted to JSON.'));
                }
            });
        });
    }).then(result => fileTasks.writeDataFile(opt.fileOutput, result));
}

/**
 * Help command
 * @memberOf Commands
 */
function showHelp() {
    console.log(chalk.bgBlue(figlet.textSync('Hayes', { horizontalLayout: 'full' })));
    console.log('Help menu:');
    console.log('Command' + ' '.repeat(22) + 'Short' + ' '.repeat(17) + 'Description');
    for (let act of activities) {
        console.log(act.name + ' '.repeat(29 - act.name.length) + act.shortname + ' '.repeat(22 - act.shortname.length) + act.desc);
    }
    console.log('Hayes DataMapper version 1.0.0');
    process.exit(0);
    return Promise.resolve('Help was finished')
}

/**
 * Get configuration.
 * @memberOf Commands
 */
function getConfiguration() {
    let configString = JSON.stringify(configuration.config);

    console.log(chalk.bgBlue(configString));
    process.exit(0);
}

/**
 * Toggle chuncked data
 * @param {*} options Options.
 * @param {string} options.id Id
 * @memberOf Commands
 * @returns {*}
 */
function toggleChunkedData(options) {
    if (!options.id) {
        return Promise.reject('No GUID provided. Task cannot continue.');
    }

    let tableInfo = configuration.dataConfig.flatDataTable;

    return repository.toggleChunk({ intgid: options.id, target: tableInfo });
}

/**
 * Process products data.
 * @memberOf Commands
 */
function productsFunc() {
    const { client } = configuration.config;
    return repository.runProcIntegrations_StageProductData({
        client
    });
}

/**
 * Container function for all header related db activity.
 * @param {*} options Options.
 * @param {string} options.id Integration identifier.
 * @memberOf Commands
 */
function headersFunc(options) {
    if (!options && !options.id) {
        return Promise.reject('No GUID provided. Task cannot proceed.');
    }
    return stagePurchaseOrderHeaders({ integrationID: options.id });
}

/**
 * Container function for all detail related db activity.
 * @param {*} options Options.
 * @param {string} options.id Integration identifier.
 * @memberOf Commands
 */
function detailsFunc(options) {
    if (!options && !options.id) {
        return Promise.reject('No GUID provided. Task cannot proceed.');
    }

    return stagePurchaseOrderDetails({ integrationID: options.id });
}

function shippingFunc(options) {
    if (!options && !options.id) {
        return Promise.reject('No GUID provided. Task cannot proceed.');
    }

    return stageShippingRecords({ integrationID: options.id });
}

/**
 * Container function for all funding related db activity.
 * @param {*} options Options.
 * @param {string} options.id Integration identifier
 * @memberOf Commands
 */
function fundingFunc(options) {
    if (!options && !options.id) {
        return Promise.reject('No GUID provided. Task cannot proceed.');
    }

    return stageFundingSources({ integrationID: options.id });
}

function toggleVendors() {
    return repository.toggleVendorSyncSwitch();
}

function toggleProducts() {
    return repository.toggleProductsSyncSwitch();
}

/**
 * Inserts shipping records in Integrations DB staging table from Integration that is currenlty in DataProcess = true.
 * @param {*} options
 * @memberOf Commands
 */
function stageShippingRecords(options) {
    const { integrationID } = options;

    return repository.getShipmentsRecordsFlatData(integrationID).then(result => {
        console.log();
        console.log(chalk.green(`${mappings.getCurrentDate()} Retrieved ${result.length} shipment records to process.`));
        const detailData = result.map(m => m.dataValues);
        const { client } = configuration.config;
        return repository.getMappings({ type: 'shipping', client })
            .then(mapping => ({ mapping, detailData }));
    }).then(result => {
        const { mapping, detailData } = result;
        const stage = mapping.map(m => m.dataValues);
        const mappingValues = stage.map(m => JSON.parse(m.MappingsObject));
        const mappedData = [];

        for (let line of detailData) {
            let m = mappings.mapIt(line, mappingValues);
            m.IntegrationsID = integrationID;
            m.ShouldSubmit = true;
            mappedData.push(m);
        }

        return repository.insertShipmentRecords(mappedData).then(() => mappedData);
    }).then(mappedData => {
        return `${mappings.getCurrentDate()} Successfully inserted ${mappedData.length} into Shipments table.`;
    }).catch(error => {
        let errorObject = {
            ErrorNumber: 500,
            ErrorName: 'Insert Shipments',
            ErrorDescription: 'Inserting Shipment records failed. More information is available in the ErrorObject.',
            ErrorObject: JSON.stringify(error),
            DataIntegrationsID: integrationID
        };

        return repository.logError(errorObject);
    });
}

/**
 * Purchase order details get staged in Integrations DB where DataIngrations.DataProcessing = true
 * @param {*} options Options.
 * @param {string} options.integrationID Integration identifier.
 * @memberOf Commands
 */
function stagePurchaseOrderDetails(options) {
    const { integrationID } = options;

    return repository.getDetailRecordsFlatData(integrationID).then(result => {
        console.log(chalk.green(`${mappings.getCurrentDate()} Retrieved ${result.length} detail records to process.`));
        const detailData = result.map(m => m.dataValues);
        const { client } = configuration.config;
        return repository.getMappings({ type: 'po details', client })
            .then(mapping => ({ mapping, detailData }));
    }).then(result => {
        const { mapping, detailData } = result;
        const stage = mapping.map(m => m.dataValues);
        const mappingValues = stage.map(m => JSON.parse(m.MappingsObject));
        const mappedData = [];

        for (let line of detailData) {
            let m = mappings.mapIt(line, mappingValues);
            m.DataIntegrationsID = integrationID;
            m.ShouldSubmit = true;
            mappedData.push(m);
        }

        return repository.insertDetailRecords(mappedData).then(() => mappedData);
    }).then(mappedData => {
        return `${mappings.getCurrentDate()} Successfully inserted ${mappedData.length} into PurchaseOrderDetail table.`;
    }).catch(error => {
        let errorObject = {
            ErrorNumber: 500,
            ErrorName: 'Insert Details',
            ErrorDescription: 'Inserting Details records failed. More information is available in the ErrorObject.',
            ErrorObject: JSON.stringify(error),
            DataIntegrationsID: integrationID
        };

        return repository.logError(errorObject);
    });
}

/**
 * Purchase order headers get staged in Integrations DB where DataIngrations.DataProcessing = true
 * @param {*} options Options.
 * @param {string} options.integrationID Integration identifier.
 * @memberOf Commands
 */
function stagePurchaseOrderHeaders(options) {
    const { integrationID } = options;
    const { client } = configuration.config;

    return repository.getHeaderRecordsFlatData(integrationID).then(result => {
        const headerData = result.map(m => m.dataValues);
        console.log(chalk.green(`${mappings.getCurrentDate()} Retrieved ${result.length} header records to process.`));
        return repository.getMappings({ type: 'po headers', client })
            .then(mapping => ({ headerData, mapping }));
    }).then(result => {
        const { headerData, mapping } = result;
        let stage = mapping.map(m => m.dataValues);
        let mappingValues = stage.map(m => JSON.parse(m.MappingsObject));
        const mappedData = [];
        for (let line of headerData) {
            let m = mappings.mapIt(line, mappingValues);
            m.PurchaseDate = mappings.stringToISODate(m.PurchaseDate);
            m.DataIntegrationsID = integrationID;
            m.ShouldSubmit = true;
            mappedData.push(m);
        }

        console.log('Printing Purchase Order Header Mapping!');

        return repository.insertHeaderRecords(mappedData).then(() => mappedData);
    }).then(mappedData => {
        return `${mappings.getCurrentDate()} Successfully inserted ${mappedData.length} into PurchaseOrderHeader table.`;
    }).catch(error => {
        let errorObject = {
            ErrorNumber: 500,
            ErrorName: 'Insert Headers',
            ErrorDescription: 'Inserting Header records failed. More information is available in the ErrorObject.',
            ErrorObject: JSON.stringify(error),
            DataIntegrationsID: integrationID
        };
        return repository.logError(errorObject);
    });
}

/**
 * All products get staged in Integrations DB.
 * @param {*} configuration Configuration.
 * @param {*} options Options.
 * @memberOf Commands
 */
function stageProducts(configuration, options) {
    var integid = options.integrationID;

    repository.getCurrentProducts().then(
        resolve => {
            let currentProducts = resolve === [] ? resolve : resolve.map(m => { return m.dataValues; });
            repository.getNewProducts(currentProducts, integid).then(
                resolve => {
                    let productsFlat = resolve === [] ? resolve : resolve.map(m => { return m.dataValues; });

                    if (productsFlat && productsFlat.length === 0) {
                        console.log(mappings.getCurrentDate() + ' No new products to add!');
                        process.exit(0);
                    }

                    let productsToAdd = [];

                    for (let pf of productsFlat) {
                        productsToAdd.push({
                            ProductName: pf.PRODUCT_NAME,
                            ProductType: pf.PRODUCT_TYPE,
                            Model: pf.MODEL,
                            Manufacturer: pf.MANUFACTURER,
                            SuggestedPrice: pf.SuggestedPrice
                        });
                    }

                    repository.insertNewProducts(productsToAdd)
                        .then(() => {
                            console.log(mappings.getCurrentDate() + ' Successfully staged ' + productsToAdd.length + ' records in Products table.');
                            process.exit(0);
                        }, reject => {
                            const errorObject = {
                                ErrorNumber: 500,
                                ErrorName: 'Insert New Products',
                                ErrorDescription: 'Inserting new products failed. More information is available in the ErrorObject.',
                                ErrorObject: JSON.stringify(reject),
                                DataIntegrationsID: integid
                            };

                            // console.error(errorObject);

                            repository.logError(errorObject).then(() => {
                                // console.log();
                                process.exit(0);
                            }, (error) => {
                                console.error(error);
                                // console.log();
                                process.exit(0);
                            }
                            );
                        }
                        )
                },
                reject => {
                    var errorObject = {
                        ErrorNumber: 500,
                        ErrorName: 'Get new Products',
                        ErrorDescription: 'Getting list of new products failed. More information is available in the ErrorObject.',
                        ErrorObject: JSON.stringify(reject),
                        DataIntegrationsID: integid
                    };

                    // console.error(errorObject);

                    repository.logError(errorObject).then(
                        resolve => {
                            // console.log();
                            return;
                        },
                        reject => {
                            // console.log();
                            return;
                        }
                    );
                }
            )
        },
        reject => {
            var errorObject = {
                ErrorNumber: 500,
                ErrorName: 'Get Current Products',
                ErrorDescription: 'Getting list of current products failed. More information is available in the ErrorObject.',
                ErrorObject: JSON.stringify(reject),
                DataIntegrationsID: integid
            };

            // console.error(errorObject);

            repository.logError(errorObject).then(
                resolve => {
                    // console.log();
                    return;
                },
                reject => {
                    // console.log();
                    return;
                }
            );
        }
    )
}

/**
 * All funding sources get staged in Integrations DB.
 * @param {*} options Options.
 * @param {string} options.integrationID Integration identifier.
 * @memberOf Commands
 */
function stageFundingSources(options) {
    const integid = options.integrationID;

    return repository.getCurrentFundingSources().then(result => {
        let currentFundingSources = result === [] ? result : result.map(m => m.dataValues);
        return repository.getNewFundingSources(currentFundingSources, integid)
    }).then(result => {
        let sourcesFlat = result === [] ? result : result.map(m => m.dataValues);
        let sourcesToAdd = [];

        if (sourcesFlat && sourcesFlat.length === 0) {
            console.log(mappings.getCurrentDate() + ' No funding sources to add!');
            resolve(mappings.getCurrentDate() + ' No funding sources to add!');
        }

        for (let sf of sourcesFlat) {
            sourcesToAdd.push({ FundingSourceID: sf.FUNDING_SOURCE });
        }

        return repository.insertNewFundingSources(sourcesToAdd);

    }).then(() => {
        console.log(mappings.getCurrentDate() + ' Successfully added ' + sourcesToAdd.length + ' new funding sources!');
        res();
    }).catch(reject => {
        const errorObject = {
            ErrorNumber: 500,
            ErrorName: 'Insert New Funding Sources',
            ErrorDescription: 'Inserting new funding sources failed. More information is available in the ErrorObject.',
            ErrorObject: JSON.stringify(reject),
            DataIntegrationsID: integid
        };

        // console.error(errorObject);

        repository.logError(errorObject).then(resolve => {
            rej();
        }, reject => {
            rej();
        });
    })
}

/**
 * All new vendors get staged in Integrations DB
 * @param {*} options Options.
 * @param {string} options.id Integration identifier.
 * @param {boolean} options.useIDs Determine whether or not the vendors id should be used.
 * @memberOf Commands
 */
function stageNewVendors(options) {
    const { client } = configuration.config;
    return repository.runProcIntegrations_StageVendorData({ client });
}

/**
 * Inserts new record into Integrations DB DataIntegrations table.
 * @param {*} options Options.
 * @param {string} options.id Integration identifier.
 * @memberOf Commands
 */
function createIntegration(options) {

    return repository.insertIntegration({
        client: configuration.config.client,
        description: configuration.config.typeDesc,
        id: options.id,
        integrationType: configuration.config.mapType
    }).catch(err => {
        const errorObject = {
            ErrorNumber: 500,
            ErrorName: 'Create Integration Record',
            ErrorDescription: 'Application was not able to create a new integration record in the database. More information is available in the ErrorObject.',
            ErrorObject: JSON.stringify(err),
            DataIntegrationsID: options.id
        };

        return repository.logError(errorObject);

    });
}

/**
 * Gets API token for TIPWEBAPI
 * @memberOf Commands
 */
function getApiToken() {
    return rq.getToken().then(result => {
        console.log(`${mappings.getCurrentDate()} getApiToken resolved`);
        return fileTasks.writeDataFile(configuration.config.idFileLoc + 'token.json', JSON.stringify(result));
    });
}

/**
 * Upserts vendors via TIPWEBAPI
 * @memberOf Commands
 */
function upsertVendors(options) {
    return repository.getVendorsToUpsert({
        client: options.client,
        limitVal: options.limitVal,
        offsetVal: options.offset
    }).then(result => {
        let noteAppend = `Hayes Integration ${mappings.getCurrentShortDate()}`;
        let dataToUpload = result.map(m => ({
            VendorID: 0,
            VendorName: m.VendorName,
            Contact: '',
            Address1: m.Address1,
            Address2: m.Address2,
            City: m.City,
            State: m.State,
            ZipCode: m.ZipCode,
            Phone: m.Phone,
            Fax: '',
            Email: m.Email,
            AccountNumber: m.VendorID,
            Notes: noteAppend
        }));

        console.log();
        console.log(`${mappings.getCurrentDate()} upsertVendors starting at ${options.offset} run ${options.limitVal}`);
        return rq.upsertVendors(options.token, dataToUpload).then(rejectedRecords => ({ rejectedRecords: JSON.parse(rejectedRecords), dataToUpload }));
    }).then(resolve => {
        let { rejectedRecords, dataToUpload } = resolve;

        if (rejectedRecords == null) {
            rejectedRecords = [];
        }

        console.log();
        console.log(`${mappings.getCurrentDate()} Successfully processed ${dataToUpload.length} records, ${rejectedRecords.length} records were rejected.`);

        if (rejectedRecords.length > 0) {
            console.log(`${mappings.getCurrentDate()} Saving ${rejectedRecords.length} Errors`);
            const errors = [];
            for (let rec of rejectedRecords) {
                let recErr = {
                    ErrorNumber: rec.badVendor.vendorID,
                    ErrorName: 'Vendor Rejected',
                    ErrorDescription: 'All purchase orders must be sourced from an accepted vendor in TipWEB-IT. ' +
                        'A vendor record was rejected while attempting to add to application. The error has more information.',
                    ErrorObject: JSON.stringify(rec),
                    DataIntegrationsID: options.intgid
                };
                errors.push(repository.logError(recErr));
            }
            console.log(`${mappings.getCurrentDate()} Save Errors Complete`);
            return Promise.all(errors);
        } else {
            return Promise.resolve('Vendors were sent.');
        }
    }).catch(err => {
        console.log();
        console.log(mappings.getCurrentDate() + ' upsertVendors failed');

        const errorObject = {
            ErrorNumber: 500,
            ErrorName: 'Upsert Vendors',
            ErrorDescription: 'Application was not able to upsert vendors to TIPWeb-IT API. More information is available in the ErrorObject.',
            ErrorObject: err,
            DataIntegrationsID: options.intgid
        };

        return repository.logError(errorObject);
    });
}

/**
 * Upserts products via TIPWEBAPI
 * @memberOf Commands
 */
function upsertProducts(options) {
    return repository.getProductsToUpsert({
        client: options.client,
        limitVal: options.limitVal,
        offset: options.offset
    }).then(result => {
        let noteAppend = `Hayes Integration ${mappings.getCurrentShortDate()}`;
        let dataToUpload = result.map(m => ({
            ProductNumber: 0,
            ProductName: m.ProductName,
            ProductDescription: m.ProductDescription,
            ProductType: m.ProductType,
            Model: m.Model,
            Manufacturer: m.Manufacturer,
            SuggestedPrice: m.SuggestedPrice,
            Notes: noteAppend,
            SKU: m.SKU,
            ProjectedLife: 0,
            CustomField1: null,
            CustomField2: null,
            CustomField3: null
        }));
        // for (let p of dataToUpload) {
        //     p.ProductNumber = 'INTG' + p.ProductNumber; //Empty the Product Number to alow the API to auto assign the Product Number
        // }
        console.log();
        console.log(`${mappings.getCurrentDate()} upsertProducts starting at ${options.offset} run ${options.limitVal}`);
        return rq.upsertProducts(options.token, dataToUpload).then(data => {
            let rejectedRecords = JSON.parse(data);
            if (rejectedRecords == null) {
                // console.log('No Detail records were rejected');
                rejectedRecords = [];
            }
            console.log(); // to create a new line
            console.log(`${mappings.getCurrentDate()} Successfully processed ${dataToUpload.length} records, ${rejectedRecords.length} records were rejected.`);

            if (rejectedRecords.length > 0) {
                const errors = [];
                console.log(`${mappings.getCurrentDate()} Saving ${rejectedRecords.length} Errors`);
                for (let rec of rejectedRecords) {
                    let recErr = {
                        ErrorNumber: rec.badProduct.productNumber,
                        ErrorName: 'Product Rejected',
                        ErrorDescription: 'All purchase order items must be added to the TipWEB-IT item catalog. ' +
                            'A product record was rejected while attempting to add to the catalog. The error has more ' +
                            'information.',
                        ErrorObject: JSON.stringify(rec),
                        DataIntegrationsID: options.intgid
                    };
                    errors.push(repository.logError(recErr));
                }
                console.log(mappings.getCurrentDate() + ' Save Errors Complete');
                return Promise.all(errors);
            }
            else {
                return Promise.resolve('Products were sent!');
            }
        }).catch(err => {
            console.log();
            console.log(mappings.getCurrentDate() + ' upsertProducts failed');

            let errorObject = {
                ErrorNumber: 500,
                ErrorName: 'Upsert Products',
                ErrorDescription: 'Application was not able to upsert products to TIPWeb-IT API. ' +
                    'More information is available in the ErrorObject.',
                ErrorObject: JSON.stringify(err),
                DataIntegrationsID: options.intgid
            };

            return repository.logError(errorObject)
        })
    });
}

//Add vendors to db
function getVendorsFromTipweb() {
    rq.getToken().then(
        resolve => {
            let tokenVal = resolve.token;
            rq.getAllVendors(tokenVal).then(
                resolve => {
                    let vendors = resolve;
                    repository.insertVendors(vendors).then(
                        resolve => {
                            // console.log(resolve);
                            // console.log('Successfully added records for vendors from TipWEB-IT API');
                            repository.toggleVendorSyncSwitch().then(
                                resolve => {
                                    console.log('toggleVendorySyncSwitch resolve');
                                    // console.log(resolve);
                                },
                                reject => {
                                    console.log('toggleVendorSyncSwitch reject');
                                    console.error(reject);
                                }
                            );
                        },
                        reject => {
                            var errorObject = {
                                ErrorNumber: 500,
                                ErrorName: 'Insert Vendors from API',
                                ErrorDescription: 'Application was not able to insert Vendor information from TipWEB-IT web API. More information is available in the ErrorObject.',
                                ErrorObject: JSON.stringify(reject),
                                //DataIntegrationsID: options.intgid
                            }

                            repository.logError(errorObject).then(
                                resolve => {
                                    // console.log();
                                    return;
                                },
                                reject => {
                                    // console.log();
                                    // console.log("logError failed");
                                    // console.error(reject);
                                    return;
                                }
                            );
                        }
                    )
                },
                reject => {
                    var errorObject = {
                        ErrorNumber: 500,
                        ErrorName: 'Get Vendors from API',
                        ErrorDescription: 'Application was not able to get Vendor information from TipWEB-IT web API. More information is available in the ErrorObject.',
                        ErrorObject: JSON.stringify(reject),
                        //DataIntegrationsID: options.intgid
                    }

                    repository.logError(errorObject).then(
                        resolve => {
                            // console.log();
                            return;
                        },
                        reject => {
                            // console.log("logError failed");
                            // console.error(reject);
                            return;
                        }
                    );
                }
            )
        },
        reject => {
            var errorObject = {
                ErrorNumber: 500,
                ErrorName: 'Get API Token',
                ErrorDescription: 'Application was not able to get an API token to access TipWEB-IT web API. More information is available in the ErrorObject.',
                ErrorObject: JSON.stringify(reject),
                //DataIntegrationsID: options.intgid
            }

            repository.logError(errorObject).then(
                resolve => {
                    // console.log();
                    return;
                },
                reject => {
                    // console.log("logError failed");
                    // console.error(reject);
                    return;
                }
            );
        }
    );
}

//add products to db
function getProductsFromTipweb() {
    rq.getToken().then(
        resolve => {
            // console.log('getToken resolved');
            // console.log(resolve);
            let tokenVal = resolve.token;
            rq.getAllProducts(tokenVal).then(
                resolve => {
                    let products = resolve;
                    repository.insertNewProducts(products).then(
                        resolve => {
                            // console.log('Successfully added records for products from TipWEB-IT API');
                            repository.toggleProductsSyncSwitch().then(
                                resolve => {
                                    console.log(resolve);
                                },
                                reject => {
                                    console.error(reject);
                                }
                            );
                        },
                        reject => {
                            var errorObject = {
                                ErrorNumber: 500,
                                ErrorName: 'Insert Products from API',
                                ErrorDescription: 'Application was not able to insert Product information from TipWEB-IT web API. More information is available in the ErrorObject.',
                                ErrorObject: JSON.stringify(reject),
                                //DataIntegrationsID: options.intgid
                            }

                            repository.logError(errorObject).then(
                                resolve => {
                                    // console.log();
                                    return;
                                },
                                reject => {
                                    // console.log("logError failed");
                                    // console.error(reject);
                                    return;
                                }
                            );
                        }
                    )
                },
                reject => {
                    var errorObject = {
                        ErrorNumber: 500,
                        ErrorName: 'Get Products from API',
                        ErrorDescription: 'Application was not able to get Product information from TipWEB-IT web API. More information is available in the ErrorObject.',
                        ErrorObject: JSON.stringify(reject),
                        //DataIntegrationsID: options.intgid
                    }

                    repository.logError(errorObject).then(
                        resolve => {
                            // console.log();
                            return;
                        },
                        reject => {
                            // console.log("logError failed");
                            // console.error(reject);
                            return;
                        }
                    );
                }
            )
        },
        reject => {
            var errorObject = {
                ErrorNumber: 500,
                ErrorName: 'Get API Token',
                ErrorDescription: 'Application was not able to get an API token to access TipWEB-IT web API. More information is available in the ErrorObject.',
                ErrorObject: JSON.stringify(reject),
                //DataIntegrationsID: options.intgid
            }

            repository.logError(errorObject).then(
                resolve => {
                    // console.log();
                    return;
                },
                reject => {
                    // console.log("logError failed");
                    // console.error(reject);
                    return;
                }
            );
        }
    )
}

/**
 * Use to upsert all product records available for submission
 * @memberOf Commands
 */
function upsertAllProducts(options) {
    if (!options && !options.id) {
        return Promise.reject('No GUID provided. Process cannot proceed.');
    }

    if (!options && !options.iVal && !options.lv) {
        return Promise.reject('Pushing to API requires offset and limit values.');
    }


    const { client } = configuration.config;
    const intgid = options.id;
    const token = JSON.parse(require(configuration.config.idFileLoc + 'token.json')).token;
    if (token) {
        return repository.getTotalProductsToUpsertCount({ client }).then(total => {
            console.log(); // to create a new line
            console.log(`${mappings.getCurrentDate()} upserAllProducts count: ${total}`);
            const offset = parseInt(options.iVal);
            const limitVal = parseInt(options.lv);

            return upsertProductsRecursive({
                client,
                limitVal,
                offset,
                token,
                total,
                intgid
            }, () => 'Done');
        });

    } else {
        return Promise.resolve(`${mappings.getCurrentDate()} token value is empty`);
    }
}

/**
 * Use to upsert all vendor records available for submission
 * @memberOf Commands
 */
function upsertAllVendors(options) {
    if (!options && !options.id) {
        rej('No GUID provided. Process cannot proceed.');
    }

    if (!options && !options.iVal && !options.lv) {
        rej('Pushing to API requires offset and limit values.');
    }

    const { client } = configuration.config;
    const intgid = options.id;
    const token = JSON.parse(require(`${configuration.config.idFileLoc}token.json`)).token;
    if (token) {
        return repository.getTotalVendorsToUpsertCount({ client }).then(total => {
            console.log(); // to create a new line
            console.log(`${mappings.getCurrentDate()} upserAllVendors count: ${total}`);
            const limitVal = parseInt(options.lv);
            const offset = parseInt(options.iVal);
            return upsertVendorsRecursive({
                client,
                limitVal,
                offset,
                token,
                total,
                intgid
            }, () => 'Done');
        });
    } else {
        return Promise.resolve(mappings.getCurrentDate() + ' token value is empty');
    }
}

/**
 * Use to upsert all header records available for submission
 * @memberOf Commands
 */
function upsertAllHeaders(options) {

    if (!options && !options.id) {
        rej('No GUID provided. Process cannot proceed.');
    }

    if (!options && !options.iVal && !options.lv) {
        rej('Pushing to API requires offset and limit values.');
    }

    const client = configuration.config.client;
    const { id } = options;
    const intgid = id;
    const token = JSON.parse(require(configuration.config.idFileLoc + 'token.json')).token;
    if (token) {
        return repository.getTotalHeadersToUpsertCount({ id, client }).then(total => {
            console.log();
            console.log(`${mappings.getCurrentDate()} upsertAllHeaders count: ${total}`);
            // console.log(resolve);
            const limitVal = parseInt(options.lv);
            const offset = parseInt(options.iVal);
            return upsertHeadersRecursive({
                client,
                limitVal,
                offset,
                token,
                total,
                intgid
            }, () => 'Done');
        });
    } else {
        return Promise.resolve(`${mappings.getCurrentDate()} token value is empty`);
    }
}

/**
 * Used to upsert all detail records available for submission
 * @memberOf Commands
 */
function upsertAllDetails(options) {
    if (!options && !options.id) {
        rej('No GUID provided. Process cannot proceed.');
    }

    if (!options && !options.iVal && !options.lv) {
        rej('Pushing to API requires offset and limit values.');
    }

    let client = configuration.config.client;
    let intgid = options.id;
    let token = JSON.parse(require(configuration.config.idFileLoc + 'token.json')).token;
    if (token) {
        return repository.getTotalDetailsToUpsertCount({
            id: intgid,
            client: client
        }).then(total => {
            console.log();
            console.log(mappings.getCurrentDate() + ' upsertAllDetails count: ' + total);
            // console.log(resolve);
            const limitVal = parseInt(options.lv);
            const offset = parseInt(options.iVal);
            return upsertDetailsRecursive({
                client,
                limitVal,
                offset,
                token,
                total,
                intgid
            }, () => 'Done');
        });
    } else {
        return Promise.resolve(mappings.getCurrentDate() + ' token value is empty');
    }
}

function upsertAllShipments(options) {
    return new Promise(
        (res, rej) => {
            if (!options && !options.id) {
                rej('No GUID provided. Process cannot proceed.');
            }

            if (!options && !options.iVal && !options.lv) {
                rej('Pushing to API requires offset and limit values.');
            }

            let client = configuration.config.client;
            let intgid = options.id;
            let tokenJson = require(configuration.config.idFileLoc + 'token.json');
            tokenJson = JSON.parse(tokenJson);
            let tokenVal = tokenJson['token'];
            if (tokenVal) {
                repository.getTotalShipmentsToUpsertCount({ id: intgid, client: client }).then(
                    resolve => {
                        console.log();
                        console.log(mappings.getCurrentDate() + ' upsertAllShipments count: ' + resolve);
                        // console.log(resolve);
                        let total = resolve;
                        let lv = parseInt(options.lv);
                        let i = parseInt(options.iVal);
                        upsertShipmentsRecursive({ client: client, limitVal: lv, offset: i, token: tokenVal, total: total, intgid: intgid }, () => { res('Done'); });
                    },
                    reject => {
                        console.error(reject);
                        rej();
                    }
                );
            } else {
                console.log(mappings.getCurrentDate() + ' tokenVal is empty');
                process.exit(0);
            }
        }
    );
}

/**
 * Adding data to TipWEB-IT requires chunking data.
 * @param {*} options
 * @param {*} callback
 * @memberOf Commands
 */
function upsertProductsRecursive(options, callback) {
    let { offset } = options;
    // console.log(options);
    return upsertProducts(options).finally(() => {
        offset += options.limitVal;
        if (offset < options.total) {
            // console.log(options);
            return upsertProductsRecursive({
                client: options.client,
                limitVal: options.limitVal,
                offset: offset,
                token: options.token,
                total: options.total,
                intgid: options.intgid
            }, callback);
        } else {
            return callback();
        }
    });
}

/**
 * Adding data to TipWEB-IT requires chunking data.
 * @param {*} options
 * @param {*} callback
 * @memberOf Commands
 */
function upsertVendorsRecursive(options, callback) {
    let { offset } = options;
    // console.log(options);
    return upsertVendors(options).finally(() => {
        offset += options.limitVal;
        if (offset < options.total) {
            return upsertVendorsRecursive({
                client: options.client,
                limitVal: options.limitVal,
                offset,
                token: options.token,
                total: options.total,
                intgid: options.intgid
            }, callback);
        } else {
            return callback();
        }
    });
}

/**
 * Adding data to TipWEB-IT requires chunking data.
 * @param {*} options
 * @param {*} callback
 * @memberOf Commands
 */
function upsertHeadersRecursive(options, callback) {
    let { offset } = options;
    // console.log(options);
    return upsertHeaderRecords(options).finally(() => {
        offset += options.limitVal;
        if (offset < options.total) {
            return upsertHeadersRecursive({
                client: options.client,
                limitVal: options.limitVal,
                offset,
                token: options.token,
                total: options.total,
                intgid: options.intgid
            }, callback);
        } else {
            return callback();
        }
    });
}

/**
 * Adding data to TipWEB-IT requires chunking data.
 * @param {*} options
 * @param {*} callback
 * @memberOf Commands
 */
function upsertDetailsRecursive(options, callback) {
    let { offset } = options;
    // console.log(options);
    return upsertDetailRecords(options).then(() => {
        offset += options.limitVal;
        if (offset < options.total) {
            // console.log(options);
            return upsertDetailsRecursive({
                client: options.client,
                limitVal: options.limitVal,
                offset,
                token: options.token,
                total: options.total,
                intgid: options.intgid
            }, callback);
        } else {
            return callback();
        }
    });
}

function upsertShipmentsRecursive(options, callback) {
    let i = options.offset;
    // console.log(options);
    upsertShipmentRecords(options).finally(() => {
        i += options.limitVal;
        if (i < options.total) {
            // console.log(options);
            upsertShipmentsRecursive({
                client: options.client,
                limitVal: options.limitVal,
                offset: i,
                token: options.token,
                total: options.total,
                intgid: options.intgid
            }, callback);
        }
        else {
            callback();
        }
    });

}

/**
 * Upsert Header records via TIPWEBAPI
 * @memberOf Commands
 * RJ Gailey 8/28/2018 - Added State and FederalFunding
 */
function upsertHeaderRecords(options) {
    return repository.getHeadersToUpsert({
        intgid: options.intgid,
        client: options.client,
        limitVal: options.limitVal,
        offsetVal: options.offset
    }).then(result => {
        let noteAppend = 'Hayes Integration ' + mappings.getCurrentShortDate();
        //console.log(result);
        let dataToUpload = result.map(m => ({
            OrderNumber: m.OrderNumber,
            Status: m.Status,
            VendorID: m.VendorID,
            VendorName: m.VendorName,
            SiteID: m.SiteID,
            PurchaseDate: m.PurchaseDate,
            EstimatedDeliveryDate: m.EstimatedDeliveryDate,
            Notes: `${noteAppend} ${m.Notes || ''}`,
            Other1: m.Other1,
            StateFunding: m.STATEFUNDING,
            FederalFunding: m.FEDERALFUNDING
        }));
        console.log();
        //console.log('Debugging');
        //console.log(dataToUpload);
        console.log(mappings.getCurrentDate() + ' upsertHeaderRecords starting at ' + options.offset + ' run ' + options.limitVal);

        return rq.upsertHeaders(options.token, dataToUpload).then(data => ({ rejectedRecords: JSON.parse(data), dataToUpload }));
    }).then(({ rejectedRecords, dataToUpload }) => {
        rejectedRecords = rejectedRecords || [];
        console.log();
        console.log(mappings.getCurrentDate() + ' Successfully processed ' + dataToUpload.length + ' records, ' + rejectedRecords.length + ' records were rejected.');
        let rejectedRecordNumbers = rejectedRecords.map(m => m.badPurchaseOrderHeader.orderNumber);
        let dataToUploadNumbers = dataToUpload.map(m => m.OrderNumber);
        let submittedVals = dataToUploadNumbers.filter(fil => rejectedRecordNumbers.indexOf(fil) < 0);

        let submittedHeaders = dataToUpload.filter(data => rejectedRecordNumbers.indexOf(data.OrderNumber) < 0);
        submittedHeaders = submittedHeaders.map(submitted => ({
            orderNumber: submitted.OrderNumber,
            vendorID: submitted.VendorID,
            siteID: submitted.SiteID
        }));

        return repository.updateSubmittedHeaders({
            id: options.intgid,
            headers: submittedHeaders
        }).then(result => ({
            result,
            submittedVals,
            rejectedRecords
        }));
    }).then(({ result, submittedVals, rejectedRecords }) => {
        console.log();
        console.log(mappings.getCurrentDate() + ' updateSubmittedHeaders count: ' + submittedVals.length);
        if (rejectedRecords.length > 0) {
            console.log(mappings.getCurrentDate() + ' Saving ' + rejectedRecords.length + ' Errors');
            // const logErrors = [];
            for (let rec of rejectedRecords) {
                let recerr = {
                    ErrorNumber: rec.badPurchaseOrderHeader.orderNumber,
                    ErrorName: 'Purchase Rejected',
                    ErrorDescription: 'A purchase order consists of 3 parts; "header" which contains the shell information; "detail" which contains individual items on a purchase order; and "shipment" to an initial site. An invalid header record was submitted. See errors for reason it was rejected.',
                    ErrorObject: JSON.stringify(rec),
                    DataIntegrationsID: options.intgid
                };
                repository.logError(recerr);
            }
            console.log(mappings.getCurrentDate() + ' Save Errors Complete');
            // return Promise.all(logErrors);
            return 'Save Errors Complete';
        } else {
            return 'Done';
        }
    }).catch(err => {
        console.log();
        console.log(mappings.getCurrentDate() + ' upsertHeaderRecords failed');

        const errorObject = {
            ErrorNumber: 500,
            ErrorName: 'upsertHeaderRecords',
            ErrorDescription: 'Application was not able to upsertHeaderRecords to TipWEB-IT API. More information is available in the ErrorObject.',
            ErrorObject: JSON.stringify(err),
            DataIntegrationsID: options.intgid
        };

        return repository.logError(errorObject);
    });
}

function upsertDetailRecords(options) {

    console.log();
    console.log(mappings.getCurrentDate() + ' upsertDetailRecords starting at ' + options.offset + ' run ' + options.limitVal);

    return repository.getDetailsToUpsert({
        intgid: options.intgid,
        client: options.client,
        limitVal: options.limitVal,
        offsetVal: options.offset
    }).then(result => {
        let dataToUpload = result.map(m => m.dataValues);

        console.log();
        console.log(mappings.getCurrentDate() + ' upsertDetailRecords got batch to upsert');

        return rq.upsertDetails(options.token, dataToUpload).then(data => ({ rejectedRecords: JSON.parse(data), dataToUpload }));
    }).then(({ rejectedRecords, dataToUpload }) => {
        if (rejectedRecords == null) {
            rejectedRecords = [];
        }
        console.log();
        console.log(mappings.getCurrentDate() + ' Successfully processed ' + dataToUpload.length + ' records, ' + rejectedRecords.length + ' records were rejected.');
        let rejectedRecordNumbers = rejectedRecords.map(m => m.badPurchaseOrderDetail.orderNumber);
        let dataToUploadNumbers = dataToUpload.map(m => m.OrderNumber);
        let submittedVals = dataToUploadNumbers.filter(fil => rejectedRecordNumbers.indexOf(fil) < 0);

        let submittedDetails = dataToUpload.filter(data => rejectedRecordNumbers.indexOf(data.OrderNumber) < 0);
        submittedDetails = submittedDetails.map(({ OrderNumber, LineNumber, SiteID }) => ({
            orderNumber: OrderNumber,
            lineNumber: LineNumber,
            siteID: SiteID
        }));
        return repository.updateSubmittedDetails({
            id: options.intgid,
            details: submittedDetails
        }).then(result => ({
            result,
            submittedVals,
            rejectedRecords
        }));
    }).then(({ result, submittedVals, rejectedRecords }) => {
        console.log();
        console.log(mappings.getCurrentDate() + ' updateSubmittedDetails count: ' + submittedVals.length);
        if (rejectedRecords.length > 0) {
            console.log(mappings.getCurrentDate() + ' Saving ' + rejectedRecords.length + ' Errors');
            // const logErrors = [];
            for (let rec of rejectedRecords) {
                let recerr = {
                    ErrorNumber: rec.badPurchaseOrderDetail.orderNumber,
                    ErrorName: 'Detail Record Rejected',
                    ErrorDescription: 'A purchase order consists of 3 parts; "header" which contains the shell information; "detail" which contains individual items on a purchase order; and "shipment" to an initial site. An invalid detail record was submitted. See errors for reason it was rejected.',
                    ErrorObject: JSON.stringify(rec),
                    DataIntegrationsID: options.intgid
                };
                repository.logError(recerr);
            }
            console.log(mappings.getCurrentDate() + ' Save Errors Complete');
            // return Promise.all(logErrors);
            return 'Save Errors Complete';
        } else {
            return 'Done';
        }
    }).catch((err) => {
        console.log();
        console.log(mappings.getCurrentDate() + ' upsertDetailRecords failed');
        console.log(err);

        const errorObject = {
            ErrorNumber: 500,
            ErrorName: 'upsertDetailRecords',
            ErrorDescription: 'Application was not able to upsertDetailRecords to TipWEB-IT API. More information is available in the ErrorObject.',
            ErrorObject: JSON.stringify(err),
            DataIntegrationsID: options.intgid
        };

        return repository.logError(errorObject);
    });
}

function upsertShipmentRecords(options) {
    return repository.getShipmentsToUpsert({
        client: options.client,
        limitVal: options.limitVal,
        offsetVal: options.offset,
        id: options.intgid
    }).then(result => {
        let dataToUpload = result.map(m => m.dataValues);
        console.log();
        console.log(mappings.getCurrentDate() + ' upsertShipmentRecords starting at ' + options.offset + ' run ' + options.limitVal);

        return rq.upsertShipments(options.token, dataToUpload).then(data => ({ rejectedRecords: JSON.parse(data), dataToUpload }));
    }).then(({ rejectedRecords, dataToUpload }) => {
        if (rejectedRecords == null) {
            rejectedRecords = [];
        }
        console.log();
        console.log(mappings.getCurrentDate() + ' Successfully processed ' + dataToUpload.length + ' records, ' + rejectedRecords.length + ' records were rejected.');
        let rejectedRecordNumbers = rejectedRecords.map(m => m.badShipment.orderNumber);
        let dataToUploadNumbers = dataToUpload.map(m => m.OrderNumber);
        let submittedVals = dataToUploadNumbers.filter(fil => rejectedRecordNumbers.indexOf(fil) < 0);

        let submittedShipments = dataToUpload.filter(data => rejectedRecordNumbers.indexOf(data.OrderNumber) < 0);
        submittedShipments = submittedShipments.map(submitted => ({
            orderNumber: submitted.OrderNumber,
            lineNumber: submitted.LineNumber,
            siteID: submitted.SiteID
        }));

        return repository.updateSubmittedShipments({
            id: options.intgid,
            shipments: submittedShipments
        }).then(result => ({
            submittedVals,
            rejectedRecords,
            result
        }));
    }).then(({ submittedVals, rejectedRecords }) => {
        console.log();
        console.log(mappings.getCurrentDate() + ' updateSubmittedShipments count: ' + submittedVals.length);
        if (rejectedRecords.length > 0) {
            console.log(mappings.getCurrentDate() + ' Saving ' + rejectedRecords.length + ' Errors');
            // const logErrors = [];
            for (let rec of rejectedRecords) {
                let recerr = {
                    ErrorNumber: rec.badShipment.orderNumber,
                    ErrorName: 'Shipment Rejected',
                    ErrorDescription: 'A purchase order consists of 3 parts; "header" which contains the shell information; "detail" which contains individual items on a purchase order; and "shipment" to an initial site. An invalid shipment record was submitted. See errors for reason it was rejected.',
                    ErrorObject: JSON.stringify(rec),
                    DataIntegrationsID: options.intgid
                };
                repository.logError(recerr);
            }
            console.log(mappings.getCurrentDate() + ' Save Errors Complete');
            // return Promise.all(logErrors);
            return 'Save Errors Complete';
        } else {
            return 'Done';
        }
    }).catch(error => {
        console.log();
        console.log(mappings.getCurrentDate() + ' updateSubmittedShipments failed');

        const errorObject = {
            ErrorNumber: 500,
            ErrorName: 'upsertShipmentRecords',
            ErrorDescription: 'Application was not able to upsertShipmentRecords to TipWEB-IT API. More information is available in the ErrorObject.',
            ErrorObject: error,
            DataIntegrationsID: options.intgid
        };

        return repository.logError(errorObject);
    });
}

// function upsertProductRecords(options) {
//     //get token
//     rq.getToken().then(
//         resolve => {
//             let tokenVal = resolve.token;
//             repository.getProductsToUpsert({ client: configuration.config.client, limit: options.limit, offset: options.offset }).then(
//                 resolve => {
//                     let dataToUpload = resolve.map(m => { return m.dataValues; });
//                     for (let d of dataToUpload) {
//                         d.ProductNumber = '0'; // 'INT' + d.ProductNumber;
//                     }
//                     console.log();
//                     console.log(mappings.getCurrentDate() + ' upsertProductRecords starting at ' + options.offset + ' run ' + options.limitVal);
//                     rq.upsertProducts(tokenVal, dataToUpload).then(
//                         resolve => {
//                             let rejectedRecords = JSON.parse(resolve);
//                             if (rejectedRecords == undefined || rejectedRecords === null) {
//                                 // console.log('No Detail records were rejected');
//                                 rejectedRecords = [];
//                             }
//                             console.log();
//                             console.log(mappings.getCurrentDate() + ' Successfully processed ' + dataToUpload.length + ' records, ' + rejectedRecords.length + ' records were rejected.');
//                             if (rejectedRecords && rejectedRecords.length > 0) {
//                                 for (let rec of rejectedRecords) {
//                                     let recerr = {
//                                         ErrorNumber: rec.badProduct.productNumber,
//                                         ErrorName: 'Product Record Rejected',
//                                         ErrorDescription: 'Product record was rejected. See ErrorObject for Rejection Reason.',
//                                         ErrorObject: JSON.stringify(rec),
//                                     }
//                                     repository.logError(recerr).then(
//                                         resolve => {
//                                             // console.log('Success!');
//                                             if (rejectedRecords.indexOf(rec) === rejectedRecords.length - 1) {
//                                                 process.exit(0);
//                                             }
//                                         },
//                                         reject => {
//                                             // console.error(reject);
//                                             // console.log("LogError failed");
//                                             if (rejectedRecords.indexOf(rec) === rejectedRecords.length - 1) {
//                                                 process.exit(0);
//                                             }
//                                         }
//                                     );
//                                 }
//                             }
//                             else {
//                                 process.exit(0);
//                             }
//                         },
//                         reject => {

//                             // console.error(reject);

//                             var errorObject = {
//                                 ErrorNumber: 500,
//                                 ErrorName: 'Product Details',
//                                 ErrorDescription: 'Application was not able to upsert data via TipWEB-IT API. More information is available in the ErrorObject.',
//                                 ErrorObject: JSON.stringify(reject),
//                                 // DataIntegrationsID: options.intgid
//                             }

//                             repository.logError(errorObject).then(
//                                 resolve => {
//                                     // console.log();
//                                     return;
//                                 },
//                                 reject => {
//                                     // console.log("LogError failed");
//                                     // console.error(reject);
//                                     return;
//                                 }
//                             );
//                         }
//                     )
//                 },
//                 reject => {
//                     var errorObject = {
//                         ErrorNumber: 500,
//                         ErrorName: 'Get Shipments to Process',
//                         ErrorDescription: 'Application was not able to get products to upsert via TipWEB-IT API. More information is available in the ErrorObject.',
//                         ErrorObject: JSON.stringify(reject),
//                         //DataIntegrationsID: options.intgid
//                     }

//                     repository.logError(errorObject).then(
//                         resolve => {
//                             // console.log();
//                             return;
//                         },
//                         reject => {
//                             // console.log("LogError failed");
//                             // console.error(reject);
//                             return;
//                         }
//                     );
//                 }
//             );
//         },
//         reject => {
//             var errorObject = {
//                 ErrorNumber: 500,
//                 ErrorName: 'Get API Token',
//                 ErrorDescription: 'Application was not able to get an API token to access TipWEB-IT web API. More information is available in the ErrorObject.',
//                 ErrorObject: JSON.stringify(reject),
//                 //DataIntegrationsID: options.intgid
//             }

//             repository.logError(errorObject).then(
//                 resolve => {
//                     // console.log();
//                     return;
//                 },
//                 reject => {
//                     // console.error(reject);
//                     // console.log("LogError failed");
//                     return;
//                 }
//             );
//         }
//     );
// }

function filterOldRecords(options) {
    if (!options && !options.id) {
        return Promise.reject(`No GUID provided. Task cannot proceed.`);
    }

    const {
        headers,
        details,
        shipping,
        inventory,
        charges,
        payments
    } = configuration.dataConfig.procRemove;
    return repository.runProcIntegrations_RemoveUnnecessaryRecords(options.id, {
        headers,
        details,
        shipping,
        inventory,
        charges,
        payments
    }).catch(err => {
        let errorObject = {
            ErrorNumber: 500,
            ErrorName: 'Filter',
            ErrorDescription: 'Application was not able to filter the unnecessary records from the export to TIPWEBAPI. More information is available in the ErrorObject.',
            ErrorObject: JSON.stringify(err),
            DataIntegrationsID: options.id
        };

        return repository.logError(errorObject);
    })
}

function filterShipmentsWithBadDetails() {
    return repository.getDataSendingToApiIntegrationID(configuration.config.client).then(intgid => {
        return repository.runProcIntegrations_FlagShipmentsFromBadDetailRecords(intgid).then(() => {
            console.log();
            return mappings.getCurrentDate() + ' Removed shipment records from manifest to send to API.';
        }).catch(reject => {
            const errorObject = {
                ErrorNumber: 500,
                ErrorName: 'Proc Remove Details',
                ErrorDescription: 'Application was not able to execute stored procedure to remove detail and shipment records from bad purchase order header records. More information is available in the ErrorObject.',
                ErrorObject: JSON.stringify(reject),
                DataIntegrationsID: intgid
            };

            return repository.logError(errorObject);
        });
    });
}

function filterDetailsWithBadHeaders(options) {
    if (!options && !options.id) {
        return Promise.reject('No GUID provided. Task cannot proceed.');
    }

    return repository.runProcIntegrations_FlagDetailsAndShipmentsFromBadHeaderRecords(options.id).then(() => {
        console.log();
        return mappings.getCurrentDate() + ' Removed detail and shipment records from manifest to send to API.';
    }).catch(error => {
        const errorObject = {
            ErrorNumber: 500,
            ErrorName: 'Proc Remove Details',
            ErrorDescription: 'Application was not able to execute stored procedure to remove detail and shipment records from bad purchase order header records. More information is available in the ErrorObject.',
            ErrorObject: JSON.stringify(error),
            DataIntegrationsID: options.id
        };

        return repository.logError(errorObject);
    });
}

/**
 * Toggle DataSentToTipweb
 * @param {*} options Must contain an id parameter.
 * @memberOf Commands
 */
function toggleToSending(options) {
    if (!options && !options.id) {
        return Promise.reject('No GUID provided. Task cannot proceed.');
    }
    return repository.beginSendingToTipwebAPI(options.id).then(() => {
        return `${mappings.getCurrentDate()} No longer processing. Sending to TipWEB-IT now.`;
    }).catch(err => {
        const errorObject = {
            ErrorNumber: 500,
            ErrorName: 'Toggle',
            ErrorDescription: 'Application was not able to toggle DataSentToTipweb process. More information is available in the ErrorObject.',
            ErrorObject: JSON.stringify(err),
            DataIntegrationsID: options.id
        };

        return repository.logError(errorObject);

    });
}

function toggleToPostProcessing(options) {
    if (!options && !options.id) {
        return Promise.reject('No GUID provided. Task cannot proceed.');
    }

    return repository.beginDataPostProcessing(options.id).then(() => {
        return `${mappings.getCurrentDate()} beginDataPostprocessing Complete!`;
    }).catch(err => {
        let errorObject = {
            ErrorNumber: 500,
            ErrorName: 'Toggle',
            ErrorDescription: 'Application was not able to toggle DataPostProcessing process. More information is available in the ErrorObject.',
            ErrorObject: JSON.stringify(err),
            DataIntegrationsID: options.id
        };

        return repository.logError(errorObject);
    })
}

/**
 * Toggle Integration as successful.
 * @param {*} options Must contain an id parameter.
 * @memberOf Commands
 */
function toggleSuccessfulIntegration(options) {
    if (!options && !options.id) {
        return Promise.reject('No GUID provided to complete!');
    }

    return repository.completeIntegrationProcessing(options.id).then(() => {
        return `${mappings.getCurrentDate()} completeIntegrationProcessing resolved`;
    }).catch(err => {
        console.log(chalk.red(err.message));
        return Promise.reject(`${mappings.getCurrentDate()} completeIntegrationProcessing rejected`);
    });
}

function getLinkTableData() {
    // get list of link tables
    // run query for data
    // write to json file
    const linkTypes = configuration.config.links;
    const folderName = configuration.config.linksFolder;
    const generate = [];
    for (let l of linkTypes) {
        generate.push(repository.getLinkTableData({
            client: configuration.config.client,
            l
        }).then(({ result, options }) => {
            const linkVals = result.map(m => m.dataValues);
            return fileTasks.writeDataFile(folderName + options.l.filename, JSON.stringify(linkVals));
        }));
    }

    return Promise.all(generate);
}

/**
 * Run custom tasks.
 * @returns {PromiseLike<T>} A promise that inform whether all tasks were executed successfully.
 * @memberOf Commands
 */
function runCustomScripts() {
    console.log();
    console.log(mappings.getCurrentDate() + ' runCustomScripts Starting');

    return repository.getProcessingIntegrationID().then(intgid => {
        let custTasks = configuration.config.customTasks;
        let options = {
            client: configuration.config.client,
            intgid: '\'' + intgid + '\''
        };

        let process = [];
        for (let task of custTasks) {
            let funk = repository[task.fn];
            process.push(funk(options));
        }
        return Promise.all(process);
    }).then(() => {
        return `${mappings.getCurrentDate()} All custom functions were completed`;
    });
}
/*Invoice Integrations */

function mapFlatInvoicesToDatabase(options) {
    return new Promise(
        (res, rej) => {
            repository.getProcessingIntegrationID().then(
                resolve => {
                    let intgid = resolve;
                    console.log();
                    console.log(mappings.getCurrentDate() + ' mapFlatInvoicesToDatabase for file: ' + options.fileName);
                    fileTasks.getDataFile(options.fileName).then(
                        resolve => {
                            let fileData;
                            if (!resolve.length) {
                                fileData = [];
                                fileData.push(JSON.parse(resolve));
                            }
                            else {
                                fileData = JSON.parse(resolve);
                            }
                            repository.getMappings({ type: configuration.config.mapType, client: configuration.config.client }).then(
                                resolve => {
                                    let mappingsData = resolve.map(m => { return m.dataValues; });
                                    let mappedData = [];
                                    console.log();
                                    console.log(mappings.getCurrentDate() + ' Processing ' + fileData.length + ' records...');
                                    for (let line of fileData) {
                                        let linVal = JSON.parse(line);
                                        let mapData = mappingsData.map(m => { return JSON.parse(m.MappingsObject); });
                                        let m = mappings.mapIt(linVal, mapData);
                                        m["DataIntegrationsID"] = intgid;
                                        mappedData.push(m);
                                    }
                                    console.log();
                                    console.log(mappings.getCurrentDate() + ' Successfully mapped ' + mappedData.length + ' records...');
                                    console.log('Inserting data...');
                                    let trackerList;
                                    for (let item of mappedData) {
                                        repository.insertFlatDataInvoices(item).then(
                                            resolve => {
                                                if (!trackerList && mappedData.indexOf(item) === mappedData.length - 1) {
                                                    res();
                                                }
                                            },
                                            reject => {
                                                trackerList = mappedData.indexOf(item);
                                                let errorObject = {
                                                    ErrorNumber: item.InvoiceNumber,
                                                    ErrorName: 'Bad Record',
                                                    ErrorDescription: 'Data for this invoicecould not be parsed correctly. This usually indicates a data integrity issue. See error for more information.',
                                                    ErrorObject: JSON.stringify({ errorData: item, error: reject.toString() }),
                                                    DataIntegrationsID: intgid
                                                }
                                                repository.logError(errorObject).then(
                                                    resolve => {
                                                        // rej();
                                                    },
                                                    reject => {
                                                        // console.log("logError failed");
                                                        // res();
                                                    }
                                                ).then(
                                                    resolve => {
                                                        if (mappedData.indexOf(item) === trackerList) {
                                                            res();
                                                        }
                                                    },
                                                    reject => {
                                                        res();
                                                    }
                                                );
                                            }
                                        );
                                    }
                                    console.log();
                                    console.log(mappings.getCurrentDate() + ' insert all invoice flat data complete');
                                },
                                reject => {

                                    var errorObject = {
                                        ErrorNumber: 500,
                                        ErrorName: 'Get Purchases Map',
                                        ErrorDescription: 'Application was not able to get the purchases mapping data from the database. More information is available in the ErrorObject.',
                                        ErrorObject: JSON.stringify(reject),
                                        DataIntegrationsID: intgid
                                    }

                                    repository.logError(errorObject).then(
                                        resolve => {
                                            rej();
                                        },
                                        reject => {
                                            rej();
                                        }
                                    );
                                }
                            );
                        },
                        reject => {
                            let errorObject = {
                                ErrorNumber: 500,
                                ErrorName: 'Get File Data',
                                ErrorDescription: 'Application was not able to get the data from file. More information is available in the ErrorObject.',
                                ErrorObject: JSON.stringify(reject),
                                DataIntegrationsID: intgid
                            }

                            repository.logError(errorObject).then(
                                resolve => {
                                    rej();
                                },
                                reject => {
                                    rej();
                                }
                            );
                        });
                },
                reject => {
                    let errorObject = {
                        ErrorNumber: 500,
                        ErrorName: 'Get Integration ID',
                        ErrorDescription: 'Application was not able to get the Integration ID that is currently sending data to TIPWEB-IT. More information is available in the ErrorObject.',
                        ErrorObject: JSON.stringify(reject),
                        DataIntegrationsID: intgid
                    }

                    repository.logError(errorObject).then(
                        resolve => {
                            rej();
                        },
                        reject => {
                            rej();
                        }
                    );
                }
            );
        }
    );
}

function stageInvoices(options) {
    return new Promise(
        (res, rej) => {
            let integid = options.intgid;
            let mappedData = [];

            repository.getInvoiceHeaders(options).then(
                resolve => {
                    console.log();
                    console.log(mappings.getCurrentDate() + ' Retrieved ' + resolve.length + ' invoice records to process.');
                    let headerData = resolve.map(m => { return m.dataValues; });
                    repository.getMappings({ type: 'invoice header', client: configuration.config.client }).then(
                        resolve => {
                            let stage = resolve.map(m => { return m.dataValues; });
                            let mappingValues = stage.map(m => { return JSON.parse(m.MappingsObject); });
                            for (let line of headerData) {
                                let m = mappings.mapIt(line, mappingValues);
                                m["DataIntegrationsID"] = integid;
                                mappedData.push(m);
                            }

                            repository.insertInvoiceHeaders(mappedData).then(
                                resolve => {
                                    console.log(mappings.getCurrentDate() + ' Successfully inserted ' + mappedData.length + ' into Invoices table.');
                                    res();
                                },
                                reject => {
                                    let errorObject = {
                                        ErrorNumber: 500,
                                        ErrorName: 'Invoice Headers',
                                        ErrorDescription: 'Inserting Invoice records failed. More information is available in the ErrorObject.',
                                        ErrorObject: JSON.stringify(reject),
                                        DataIntegrationsID: integid
                                    };

                                    repository.logError(errorObject).then(
                                        resolve => {
                                            rej();
                                        },
                                        reject => {
                                            rej();
                                        }
                                    );
                                }
                            )
                        },
                        reject => {
                            let errorObject = {
                                ErrorNumber: 500,
                                ErrorName: 'Get Invoice Mappings',
                                ErrorDescription: 'Getting Invoice Mapping records failed. More information is available in the ErrorObject.',
                                ErrorObject: JSON.stringify(reject),
                                DataIntegrationsID: integid
                            };

                            // console.error(errorObject);

                            repository.logError(errorObject).then(
                                resolve => {
                                    rej();
                                },
                                reject => {
                                    rej();
                                }
                            );
                        }
                    );
                },
                reject => {
                    var errorObject = {
                        ErrorNumber: 500,
                        ErrorName: 'Get Invoice',
                        ErrorDescription: 'Getting Invoice records failed. More information is available in the ErrorObject.',
                        ErrorObject: JSON.stringify(reject),
                        DataIntegrationsID: integid
                    };

                    // console.error(errorObject);

                    repository.logError(errorObject).then(
                        resolve => {
                            rej();
                        },
                        reject => {
                            rej();
                        }
                    );
                }
            );
        });
}

function stageInvoiceDetails(options) {
    return new Promise(
        (res, rej) => {
            var integid = options.integrationID;
            var mappedData = [];

            repository.getInvoiceDetails({ intgid: integid }).then(
                resolve => {
                    console.log(mappings.getCurrentDate() + ' Retrieved ' + resolve.length + ' invoice records to process.');
                    let headerData = resolve.map(m => { return m.dataValues; });
                    repository.getMappings({ type: 'invoice detail', client: configuration.config.client }).then(
                        resolve => {
                            let stage = resolve.map(m => { return m.dataValues; });
                            let mappingValues = stage.map(m => { return JSON.parse(m.MappingsObject); });
                            // console.log(mappingValues);
                            for (let line of headerData) {
                                let m = mappings.mapIt(line, mappingValues);
                                m["DataIntegrationsID"] = integid;
                                mappedData.push(m);
                            }

                            repository.insertInvoiceDetails(mappedData).then(
                                resolve => {
                                    console.log(mappings.getCurrentDate() + ' Successfully inserted ' + mappedData.length + ' into InvoiceDetails table.');
                                    res();
                                },
                                reject => {
                                    let errorObject = {
                                        ErrorNumber: 500,
                                        ErrorName: 'Invoice Details',
                                        ErrorDescription: 'Inserting Invoice Details records failed. More information is available in the ErrorObject.',
                                        ErrorObject: JSON.stringify(reject),
                                        DataIntegrationsID: integid
                                    };

                                    repository.logError(errorObject).then(
                                        resolve => {
                                            rej();
                                        },
                                        reject => {
                                            rej();
                                        }
                                    );
                                }
                            )
                        },
                        reject => {
                            let errorObject = {
                                ErrorNumber: 500,
                                ErrorName: 'Get Invoice Details Mappings',
                                ErrorDescription: 'Getting Invoice Details Mapping records failed. More information is available in the ErrorObject.',
                                ErrorObject: JSON.stringify(reject),
                                DataIntegrationsID: integid
                            };

                            // console.error(errorObject);

                            repository.logError(errorObject).then(
                                resolve => {
                                    rej();
                                },
                                reject => {
                                    rej();
                                }
                            );
                        }
                    );
                },
                reject => {
                    var errorObject = {
                        ErrorNumber: 500,
                        ErrorName: 'Get Invoice Details',
                        ErrorDescription: 'Getting Invoice Details records failed. More information is available in the ErrorObject.',
                        ErrorObject: JSON.stringify(reject),
                        DataIntegrationsID: integid
                    };

                    // console.error(errorObject);

                    repository.logError(errorObject).then(
                        resolve => {
                            rej();
                        },
                        reject => {
                            rej();
                        }
                    );
                }
            );
        });
}

function invoiceHeaderFunc(options) {
    return new Promise(
        (res, rej) => {
            repository.getProcessingIntegrationID().then(
                resolve => {
                    let intgid = resolve;
                    // console.log(resolve);
                    stageInvoices({ intgid: intgid }).then(
                        resolve => {
                            res();
                        },
                        reject => {
                            rej(reject);
                        }
                    );
                },
                reject => {
                    // console.error(chalk.red(reject));
                    rej();
                }
            );
        }
    );
}

function invoiceDetailsFunc(options) {
    return new Promise(
        (res, rej) => {
            repository.getProcessingIntegrationID().then(
                resolve => {
                    let intgid = resolve;
                    stageInvoiceDetails({ integrationID: intgid }).then(
                        resolve => {
                            res();
                        },
                        reject => {
                            rej(reject);
                        }
                    );
                },
                reject => {
                    // console.error(chalk.red(reject));
                    rej();
                }
            );
        }
    );
}

function mapFlatInvoices(options) {
    return new Promise(
        (res, rej) => {
            if (!options.filename) {
                console.log('A valid file to process must be provided.')
                rej();
            }

            let opts = { fileName: options.filename };
            mapFlatInvoicesToDatabase(opts).then(
                resolve => {
                    console.log('Task Complete: Map purchase order data from JSON to Database table.');
                    res();
                },
                reject => {
                    // console.error(chalk.red('Task Error: ' + reject));
                    rej();
                }
            );
        }
    );
}

function pushInvoiceHeaders(options) {
    return new Promise(
        (res, rej) => {
            let client = configuration.config.client;
            repository.getDataSendingToApiIntegrationID(client).then(
                resolve => {
                    let intgid = resolve;
                    repository.getInvoiceHeadersTotalCount({ client: client }).then(
                        resolve => {
                            // console.log(resolve);
                            let total = resolve;
                            rq.getToken().then(
                                resolve => {
                                    // console.log(resolve);
                                    let lv = options.lv;
                                    let i = options.iVal;
                                    let tokenVal = resolve.token;
                                    pushInvoiceHeadersRecursive({ client: client, limitVal: lv, offset: i, token: tokenVal, total: total, intgid: intgid }, () => { res('Done'); });
                                },
                                reject => {
                                    rej(reject);
                                }
                            )
                        },
                        reject => {
                            rej(reject);
                        }
                    );
                }
            );
        },
        reject => {
            rej(reject);
        });
}

function pushInvoiceHeadersRecursive(options, callback) {
    let i = options.offset;
    // console.log(options);
    pushInvoiceHeadersToApi(options).then(
        resolve => {
            i += options.limitVal;
            if (i < options.total) {
                // console.log(options);
                pushInvoiceHeadersRecursive({ client: options.client, limitVal: options.limitVal, offset: i, token: options.token, total: options.total, intgid: options.intgid }, callback);
            }
            else {
                callback();
            }
        },
        reject => {
            i += options.limitVal;
            // console.error(reject);
            if (i < options.total) {
                // console.log(options);
                pushInvoiceHeadersRecursive({ client: options.client, limitVal: options.limitVal, offset: i, token: options.token, total: options.total, intgid: options.intgid }, callback);
            }
            else {
                callback();
            }
        }
    );
}

function pushInvoiceHeadersToApi(options) {
    return new Promise(

        (res, rej) => {
            repository.getInvoiceHeadersToAdd({ id: options.intgid, client: options.client, limitVal: options.limitVal, offsetVal: options.offset }).then(
                resolve => {
                    let dataToUpload = resolve.map(m => { return m.dataValues; });
                    for (let value of dataToUpload) {
                        value.DataIntegration = undefined;
                    }
                    console.log();
                    console.log(mappings.getCurrentDate() + ' addInvoices starting at ' + options.offset + ' run ' + options.limitVal);
                    rq.addInvoices(options.token, dataToUpload).then(
                        resolve => {
                            let rejectedRecords = JSON.parse(resolve);
                            if (rejectedRecords == undefined || rejectedRecords === null) {
                                // console.log('No Detail records were rejected');
                                rejectedRecords = [];
                            }
                            console.log();
                            console.log(mappings.getCurrentDate() + ' Successfully processed ' + dataToUpload.length + ' records, ' + rejectedRecords.length + ' records were rejected.');
                            if (rejectedRecords.length > 0) {
                                console.log(mappings.getCurrentDate() + ' Saving ' + rejectedRecords.length + ' Errors');
                                for (let rec of rejectedRecords) {
                                    let recerr = {
                                        ErrorNumber: rec.badInvoice.orderNumber,
                                        ErrorName: 'Invoice Rejected',
                                        ErrorDescription: 'An invoice record was rejected.',
                                        ErrorObject: JSON.stringify(rec),
                                        DataIntegrationsID: options.intgid
                                    }
                                    repository.logError(recerr).then(
                                        resolve => {
                                            if (rejectedRecords.indexOf(rec) === rejectedRecords.length - 1) {
                                                res();
                                            }
                                        },
                                        reject => {
                                            // console.log("logError failed");
                                            // console.error(reject);
                                            if (rejectedRecords.indexOf(rec) === rejectedRecords.length - 1) {
                                                rej(reject);
                                            }
                                        }
                                    );
                                }
                                console.log(mappings.getCurrentDate() + ' Save Errors Complete');
                            } else {
                                res();
                            }
                        },
                        reject => {
                            var errorObject = {
                                ErrorNumber: 500,
                                ErrorName: 'Get Invoices to Process',
                                ErrorDescription: 'Application was not able to get invoice values to add via TipWEB-IT API. More information is available in the ErrorObject.',
                                ErrorObject: JSON.stringify(reject),
                                DataIntegrationsID: options.intgid
                            }

                            repository.logError(errorObject).then(
                                resolve => {
                                    rej();
                                },
                                reject => {
                                    rej();
                                }
                            );
                        }
                    )
                },
                reject => {
                    rej();
                }
            )
        }
    );
}

function pushInvoiceDetails(options) {
    return new Promise(
        (res, rej) => {
            let client = configuration.config.client;
            repository.getDataSendingToApiIntegrationID(client).then(
                resolve => {
                    let intgid = resolve;
                    repository.getInvoiceDetailsTotalCount({ client: client }).then(
                        resolve => {
                            // console.log(resolve);
                            let total = resolve;
                            rq.getToken().then(
                                resolve => {
                                    // console.log(resolve);
                                    let lv = options.lv;
                                    let i = options.iVal;
                                    let tokenVal = resolve.token;
                                    pushInvoiceDetailsRecursive({ client: client, limitVal: lv, offset: i, token: tokenVal, total: total, intgid: intgid }, () => { res('Done'); });
                                },
                                reject => {
                                    rej(reject);
                                }
                            )
                        },
                        reject => {
                            rej(reject);
                        }
                    );
                }
            );
        },
        reject => {
            rej(reject);
        });
}

function pushInvoiceDetailsRecursive(options, callback) {
    let i = options.offset;
    // console.log(options);
    pushInvoiceDetailsToApi(options).then(
        resolve => {
            i += options.limitVal;
            if (i < options.total) {
                // console.log(options);
                pushInvoiceDetailsRecursive({ client: options.client, limitVal: options.limitVal, offset: i, token: options.token, total: options.total, intgid: options.intgid }, callback);
            }
            else {
                callback();
            }
        },
        reject => {
            i += options.limitVal;
            // console.error(reject);
            if (i < options.total) {
                // console.log(options);
                pushInvoiceDetailsRecursive({ client: options.client, limitVal: options.limitVal, offset: i, token: options.token, total: options.total, intgid: options.intgid }, callback);
            }
            else {
                callback();
            }
        }
    );
}

function pushInvoiceDetailsToApi(options) {
    return new Promise(

        (res, rej) => {
            repository.getInvoiceDetailsToAdd({ id: options.intgid, client: options.client, limitVal: options.limitVal, offsetVal: options.offset }).then(
                resolve => {
                    let dataToUpload = resolve.map(m => { return m.dataValues; });
                    for (let value of dataToUpload) {
                        value.DataIntegration = undefined;
                    }
                    console.log();
                    console.log(mappings.getCurrentDate() + ' pushInvoiceDetailsToApi starting at ' + options.offset + ' run ' + options.limitVal);
                    rq.addInvoiceDetails(options.token, dataToUpload).then(
                        resolve => {
                            let rejectedRecords = JSON.parse(resolve);
                            if (rejectedRecords == undefined || rejectedRecords === null) {
                                // console.log('No Detail records were rejected');
                                rejectedRecords = [];
                            }
                            console.log();
                            console.log(mappings.getCurrentDate() + ' Successfully processed ' + dataToUpload.length + ' records, ' + rejectedRecords.length + ' records were rejected.');
                            if (rejectedRecords.length > 0) {
                                console.log(mappings.getCurrentDate() + ' Saving ' + rejectedRecords.length + ' Errors');
                                for (let rec of rejectedRecords) {
                                    let recerr = {
                                        ErrorNumber: rec.badInvoiceDetail.orderNumber,
                                        ErrorName: 'Invoice Detail Rejected',
                                        ErrorDescription: 'An invoice detail record was rejected.',
                                        ErrorObject: JSON.stringify(rec),
                                        DataIntegrationsID: options.intgid
                                    }
                                    repository.logError(recerr).then(
                                        resolve => {
                                            if (rejectedRecords.indexOf(rec) === rejectedRecords.length - 1) {
                                                res();
                                            }
                                        },
                                        reject => {
                                            // console.log("logError failed");
                                            // console.error(reject);
                                            if (rejectedRecords.indexOf(rec) === rejectedRecords.length - 1) {
                                                rej(reject);
                                            }
                                        }
                                    );
                                }
                                console.log(mappings.getCurrentDate() + ' Save Errors Complete');
                            } else {
                                res();
                            }
                        },
                        reject => {
                            var errorObject = {
                                ErrorNumber: 500,
                                ErrorName: 'Get Invoice Details to Process',
                                ErrorDescription: 'Application was not able to get invoice detail values to upsert via TipWEB-IT API. More information is available in the ErrorObject.',
                                ErrorObject: JSON.stringify(reject),
                                DataIntegrationsID: options.intgid
                            }

                            repository.logError(errorObject).then(
                                resolve => {
                                    rej();
                                },
                                reject => {
                                    rej();
                                }
                            );
                        }
                    )
                },
                reject => {
                    rej();
                }
            )
        }
    );
}

function getProcessingIntegrationID() {
    return new Promise(
        (res, rej) => {
            let client = configuration.config.client;
            let type = configuration.config.mapType;
            repository.getDataSendingToApiIntegrationID(client, type).then(
                resolve => {
                    // console.log(resolve);
                    let intgid = resolve;
                    fs.writeFile(configuration.config.idFileLoc + 'intgid.txt', intgid.toString(), (err) => { res(err); });
                },
                reject => {
                    console.error(chalk.red('Error! Did not get integration ID value.'));
                    rej()
                }
            );
        }
    );
}

/**
 * Follows the below steps to map JSON data to a specified DB table.
 * Step 1: Get file
 * Step 2: Get mapping data
 * Step 3: Map data
 * Step 4: Call proper method to insert data into database
 * @param {*} options Options.
 * @param {string} options.filename Filename.
 * @param {string} options.id Integration identifier.
 * @memberOf Commands
 */
function mapFromFile(options) {

    if (!options || !options.filename) {
        console.error(chalk.red('No file provided.'));
        return Promise.reject('Task Error');
    } else if (!options || !options.id) {
        console.error(chalk.red('No GUID provided. Process cannot continue.'));
        return Promise.reject('Task Error');
    } else {
        let dataId = options.id;
        let filename = options.filename;
        let mapType = configuration.config.mapType;
        let mapClient = configuration.config.client;
        let mappedData = [];
        let tableInfo = configuration.dataConfig.flatDataTable;

        console.log();
        console.log(mappings.getCurrentDate() + ' mapFromFile for file: ' + filename);
        return fileTasks.getDataFile(filename).then(result => {
            let fileData = JSON.parse(result);

            return repository.getMappings({
                client: mapClient,
                type: mapType
            }).then(resultMaps => ({
                resultMaps,
                fileData
            }));
        }).then(results => {
            let { fileData, resultMaps } = results;
            let maps = resultMaps.map(m => m.dataValues);
            console.log();
            console.log(`${mappings.getCurrentDate()} Processing ${fileData.length} records...`);

            let mapData = maps.map(m => JSON.parse(m.MappingsObject));

            for (let line of fileData) {
                let m = mappings.mapIt(JSON.parse(line), mapData);
                m.IntegrationsID = dataId;
                mappedData.push(m);
            }
            //RJ Gailey commented out console is used for debuging
            //console.log(mappedData);
            console.log(`${mappings.getCurrentDate()} Successfully mapped ${mappedData.length} records...`);
            return repository[tableInfo].describe().then(schema => ({ schema, mappedData }));
            }).then(({ schema, mappedData }) => {
                //console.log(schema);
                for (let item of mappedData) {
                    _.each(item, (value, name) => {
                        //console.log(name)
                        //console.log(value)
                    if (~schema[name].type.indexOf('DATE')) {
                        item[name] = mappings.stringToISODate(item[name]);
                    }
                });
            }
            return repository.insertFlatData(mappedData, {
                target: tableInfo
            });
        }).then(() => {
            return `${mappings.getCurrentDate()} map flat data from file complete`;
        }).catch(err => {
            const errorObject = {
                ErrorNumber: 422,
                ErrorName: 'Bad Record',
                ErrorDescription: 'Error was found.',
                ErrorObject: JSON.stringify(err),
                DataIntegrationsID: dataId
            };

            console.log(chalk.red(`${err}`));
            console.log(chalk.red(`${mappings.getCurrentDate()} map flat data was not completed imported`));
            return repository.logError(errorObject);
        });
    }
}

/**
 * Inserts new record into Integrations DB DataIntegrations table.
 * @param {*} options Options.
 * @param {string} options.filename File name.
 * @param {string} options.filelink File link.
 * @param {string} options.client Client.
 * @param {string} options.id Integration identifier.
 * @memberOf Commands
 */
function insertDataIntegrationsFile(options) {

    return repository.insertDataIntegrationsFile({
        FileNameAws: options.filename,
        AwsFileLink: options.filelink,
        Client: options.client,
        DataIntegrationsID: options.id
    }).then(result => {
        console.log(chalk.green(`File: ${options.filename} inserted.`));
        return result;
    }).catch(err => {
        const errorObject = {
            ErrorNumber: 500,
            ErrorName: 'Create Integration Record',
            ErrorDescription: 'Application was not able to create a new integration file record in the database. More information is available in the ErrorObject.',
            ErrorObject: JSON.stringify(err),
            DataIntegrationsID: options.id
        };

        return repository.logError(errorObject);
    });
}

function getProcessedFiles(options) {
    return new Promise(
        (res, rej) => {
            // let client = configuration.config.client;
            // let type = configuration.config.mapType;
            repository.getProcessedFiles({ id: options.id }).then(
                resolve => {
                    // console.log(resolve);
                    res(resolve);
                },
                reject => {
                    console.error(chalk.red('Failed to Get Integration Files'));
                    rej();
                }
            );
        }
    );
}

function getProcessedFilesLinks(options) {
    return new Promise(
        (res, rej) => {
            let client = configuration.config.client;
            let type = configuration.config.mapType;
            repository.getProcessedFilesLinks({ id: options.id }).then(
                resolve => {
                    // console.log(resolve);
                    res(resolve);
                },
                reject => {
                    console.error(chalk.red('Failed to Get Integration Files'));
                    rej();
                }
            );
        }
    );
}
