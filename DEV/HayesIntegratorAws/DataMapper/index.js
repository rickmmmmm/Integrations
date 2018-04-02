#!/usr/bin/env node
"use strict";

const chalk = require('chalk');
const clear = require('clear');
const CLI = require('clui');
const figlet = require('figlet');
const inquirer = require('inquirer');
const Preferences = require('preferences');
const Spinner = CLI.Spinner;
const _ = require('lodash');
const fs = require('fs');
const repository = require('./lib/repository.js');
const filetasks = require('./lib/file-tasks.js');
const logger = require('./lib/log-to-file.js');
const configuration = require('./lib/configuration.js');
const mappings = require('./lib/mappings.js');
const rq = require('./lib/http-requests.js');

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
]

const args = [
    { name: '--filename', shortname: '-f', desc: 'Generic filename attribute.', objectKey: 'filename' },
    { name: '--filepath', shortname: '-fp', desc: 'Generic filepath attribute.', objectKey: 'filepath' },
    { name: '--filelink', shortname: '-fl', desc: 'Generic filelink attribute.', objectKey: 'filelink' },
    { name: '--useid', shortname: '-ids', desc: 'Generic element to handle use of ID field', objectKey: 'useIDs' },
    { name: '--iVal', shortname: '-i', desc: 'Value for i in recursive functions.', objectKey: 'iVal' },
    { name: '--lengthValue', shortname: '-lv', desc: 'Value for chunk length in recursive functions.', objectKey: 'lv' },
    { name: '--identifier', shortname: '-id', desc: 'Value for application execution unique identifier.', objectKey: 'id' },
    { name: '--client', shortname: '-cl', desc: 'Value for application Client.', objectKey: 'client' },
]

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
 */
function fire(action, subaction) {
    let a001 = activities.filter(fil => { return fil.name === action || fil.shortname === action; });
    if (a001 && a001.length === 1) {
        let cb = a001[0].action;
        let options = subaction;
        let spinner = new Spinner('', ['.', '..', '...', '....']);
        spinner.start();
        cb(options).then(
            resolve => {
                console.log(chalk.green('Done!'));
                process.exit(0);
            },
            reject => {
                console.error(chalk.red(reject));
                console.error(chalk.red('Done but with errors...'));
                process.exit(100);
            }
        );
    }
    else {
        console.error('Invalid command.');
        process.exit(100);
    }
}

function showHelp() {
    console.log(
        chalk.bgBlue(
            figlet.textSync('Hayes', { horizontalLayout: 'full' })
        )
    );
    console.log('Help menu:');
    console.log('Command' + ' '.repeat(22) + 'Short' + ' '.repeat(17) + 'Description');
    for (let act of activities) {

        console.log(act.name + ' '.repeat(29 - act.name.length) + act.shortname + ' '.repeat(22 - act.shortname.length) + act.desc);
    }
    console.log('Hayes DataMapper version 1.0.0')
    process.exit(0);
}

function getConfiguration() {
    let configString = JSON.stringify(configuration.config);

    console.log(chalk.bgBlue(configString));
    process.exit(0);
}

function toggleChunkedData(options) {
    return new Promise(
        (res, rej) => {

            if (!options.id) {
                console.error(chalk.red('No GUID provided. Task cannot continue.'))
                rej();
            }

            repository.toggleChunk(options.id).then(
                resolve => {
                    res();
                },
                reject => {
                    rej();
                }
            );
        },
        reject => {
            rej();
        }
    );
}

/** */
function productsFunc() {
    return new Promise(
        (res, rej) => {
            repository.runProcIntegrations_StageProductData({ client: configuration.config.client }).then(
                resolve => {
                    res(resolve);
                },
                reject => {
                    rej(reject);
                }
            )
        }
    );
}

/**
 * Container function for all header related db activity.
 * @param {*} options
 */
function headersFunc(options) {
    return new Promise(
        (res, rej) => {
            if (!options && !options.id) {
                console.error(chalk.red('No GUID provided. Task cannot proceed.'));
                rej();
            }
            stagePurchaseOrderHeaders({ integrationID: options.id }).then(
                resolve => {
                    res();
                },
                reject => {
                    rej(reject);
                }
            );
        }
    );
}

/**
 * Container function for all detail related db activity.
 * @param {*} options
 */
function detailsFunc(options) {
    return new Promise(
        (res, rej) => {

            if (!options && !options.id) {
                console.error(chalk.red('No GUID provided. Task cannot proceed.'));
                rej();
            }

            stagePurchaseOrderDetails({ integrationID: options.id }).then(
                resolve => {
                    res();
                },
                reject => {
                    rej(reject);
                }
            );
        });
}

// function shippingFunc(options) {
//   return new Promise(
//     (res,rej) => {
//       repository.getProcessingIntegrationID().then(
//         resolve => {

//           if (!resolve) {
//             rej();
//           }

//           let intgid = resolve;
//           stageShippingRecords({ integrationID: intgid }).then(
//             resolve => {
//               res();
//             },
//             reject => {
//               rej(reject);
//             }
//           );
//         },
//         reject => {
//           rej(reject);
//         }
//       );
//     }
//   )
// }

function fundingFunc(options) {
    return new Promise(
        (res, rej) => {

            if (!options && !options.id) {
                console.error(chalk.red('No GUID provided. Task cannot proceed.'));
                rej();
            }
            stageFundingSources({ integrationID: options.id }).then(
                resolve => {
                    res();
                },
                reject => {
                    rej(reject);
                }
            );
        }
    );
}

function toggleVendors() {
    return new Promise(
        (res, rej) => {
            repository.toggleVendorSyncSwitch().then(
                resolve => {
                    res();
                },
                reject => {
                    rej();
                }
            )
        }
    )
}

function toggleProducts() {
    return new Promise(
        (res, rej) => {
            repository.toggleProductsSyncSwitch().then(
                resolve => {
                    res();
                },
                reject => {
                    rej();
                }
            )
        }
    )
}

/**
 * Inserts shipping records in Integrations DB staging table from Integration that is currenlty in DataProcess = true.
 * @param {*} configuration
 * @param {*} options
 */
function stageShippingRecords(options) {
    return new Promise(
        (res, rej) => {
            var integid = options.integrationID;
            var mappedData = [];

            repository.getFlatShipments(integid).then(
                resolve => {
                    console.log(mappings.getCurrentDate() + ' Retrieved ' + resolve.length + ' shipment records to process.');
                    let shipmentsData = resolve.map(m => { return m.dataValues; });
                    repository.getMappings({ type: 'shipping', client: configuration.config.client }).then(
                        resolve => {
                            let stage = resolve.map(m => { return m.dataValues; });
                            let mappingValues = stage.map(m => { return JSON.parse(m.MappingsObject); });

                            for (let line of shipmentsData) {
                                let m = mappings.mapIt(line, mappingValues);
                                m["DataIntegrationsID"] = integid;
                                mappedData.push(m);
                            }

                            repository.insertShipments(mappedData).then(
                                resolve => {
                                    console.log(mappings.getCurrentDate() + ' Successfully inserted ' + mappedData.length + ' into Shipments table.');
                                    res();
                                },
                                reject => {
                                    let errorObject = {
                                        ErrorNumber: 500,
                                        ErrorName: 'Insert Shipments',
                                        ErrorDescription: 'Inserting Shipments records failed. More information is available in the ErrorObject.',
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
                            )
                        },
                        reject => {
                            let errorObject = {
                                ErrorNumber: 500,
                                ErrorName: 'Get Shipments Mappings',
                                ErrorDescription: 'Getting Shipments Mapping records failed. More information is available in the ErrorObject.',
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
                        ErrorName: 'Get Shipments',
                        ErrorDescription: 'Getting Shipments records failed. More information is available in the ErrorObject.',
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

/**
 * Purchase order details get staged in Integrations DB where DataIngrations.DataProcessing = true
 * @param {*} configuration
 * @param {*} options
 */
function stagePurchaseOrderDetails(options) {
    return new Promise(
        (res, rej) => {

            var integid = options.integrationID;

            repository.getDetailRecordsFlatData(integid).then(
                resolve => {
                    console.log(mappings.getCurrentDate() + ' Retrieved ' + resolve.length + ' detail records to process.');
                    let detailData = resolve.map(m => { return m.dataValues; });
                    repository.getMappings({ type: 'po details', client: configuration.config.client }).then(
                        resolve => {
                            let stage = resolve.map(m => { return m.dataValues; });
                            let mappingValues = stage.map(m => { return JSON.parse(m.MappingsObject); });
                            let mappedData = [];

                            for (let line of detailData) {
                                let m = mappings.mapIt(line, mappingValues);
                                m["DataIntegrationsID"] = integid;
                                mappedData.push(m);
                            }

                            repository.insertDetailRecords(mappedData).then(
                                resolve => {
                                    res();
                                },
                                reject => {
                                    let errorObject = {
                                        ErrorNumber: 500,
                                        ErrorName: 'Insert Details',
                                        ErrorDescription: 'Inserting Details records failed. More information is available in the ErrorObject.',
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
                            )
                        },
                        reject => {
                            let errorObject = {
                                ErrorNumber: 500,
                                ErrorName: 'Get Details Mappings',
                                ErrorDescription: 'Getting Details Mapping records failed. More information is available in the ErrorObject.',
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
                        ErrorName: 'Get Details',
                        ErrorDescription: 'Getting Details records failed. More information is available in the ErrorObject.',
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

/**
 * Purchase order headers get staged in Integrations DB where DataIngrations.DataProcessing = true
 * @param {*} configuration
 * @param {*} options
 */
function stagePurchaseOrderHeaders(options) {
    return new Promise(
        (res, rej) => {
            var integid = options.integrationID;
            var mappedData = [];

            repository.getHeaderRecordsFlatData(integid).then(
                resolve => {
                    console.log(mappings.getCurrentDate() + ' Retrieved ' + resolve.length + ' header records to process.');
                    let headerData = resolve.map(m => { return m.dataValues; });
                    repository.getMappings({ type: 'po headers', client: configuration.config.client }).then(
                        resolve => {
                            let stage = resolve.map(m => { return m.dataValues; });
                            let mappingValues = stage.map(m => { return JSON.parse(m.MappingsObject); });
                            for (let line of headerData) {
                                let m = mappings.mapIt(line, mappingValues);
                                m["DataIntegrationsID"] = integid;
                                mappedData.push(m);
                            }

                            repository.insertHeaderRecords(mappedData).then(
                                resolve => {
                                    console.log(mappings.getCurrentDate() + ' Successfully inserted ' + mappedData.length + ' into PurchaseOrderHeaders table.');
                                    res();
                                },
                                reject => {
                                    let errorObject = {
                                        ErrorNumber: 500,
                                        ErrorName: 'Insert Headers',
                                        ErrorDescription: 'Inserting Header records failed. More information is available in the ErrorObject.',
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
                                ErrorName: 'Get Header Mappings',
                                ErrorDescription: 'Getting Header Mapping records failed. More information is available in the ErrorObject.',
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
                        ErrorName: 'Get Headers',
                        ErrorDescription: 'Getting Header records failed. More information is available in the ErrorObject.',
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

/**
 * All products get staged in Integrations DB
 * @param {*} configuration
 * @param {*} options
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
                        x = { ProductName: pf.PRODUCT_NAME, ProductType: pf.PRODUCT_TYPE, Model: pf.MODEL, Manufacturer: pf.MANUFACTURER, SuggestedPrice: pf.SuggestedPrice };
                        productsToAdd.push(x);
                    }

                    repository.insertNewProducts(productsToAdd).then(
                        resolve => {
                            console.log(mappings.getCurrentDate() + ' Successfully staged ' + productsToAdd.length + ' records in Products table.');
                            process.exit(0);
                        },
                        reject => {
                            var errorObject = {
                                ErrorNumber: 500,
                                ErrorName: 'Insert New Products',
                                ErrorDescription: 'Inserting new products failed. More information is available in the ErrorObject.',
                                ErrorObject: JSON.stringify(reject),
                                DataIntegrationsID: integid
                            };

                            // console.error(errorObject);

                            repository.logError(errorObject).then(
                                resolve => {
                                    // console.log();
                                    process.exit(0);
                                },
                                reject => {
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
 * @param {*} configuration
 * @param {*} options
 */
function stageFundingSources(options) {
    return new Promise(
        (res, rej) => {
            var integid = options.integrationID;

            repository.getCurrentFundingSources().then(
                resolve => {
                    let currentFundingSources = resolve === [] ? resolve : resolve.map(m => { return m.dataValues; });
                    repository.getNewFundingSources(currentFundingSources, integid).then(
                        resolve => {
                            let sourcesFlat = resolve === [] ? resolve : resolve.map(m => { return m.dataValues; });
                            let sourcesToAdd = [];

                            if (sourcesFlat && sourcesFlat.length === 0) {
                                console.log(mappings.getCurrentDate() + ' No funding sources to add!');
                                res();
                            }

                            for (let sf of sourcesFlat) {
                                x = { FundingSourceID: sf.FUNDING_SOURCE };
                                sourcesToAdd.push(x);
                            }

                            repository.insertNewFundingSources(sourcesToAdd).then(
                                resolve => {
                                    console.log(mappings.getCurrentDate() + ' Successfully added ' + sourcesToAdd.length + ' new funding sources!');
                                    res();
                                },
                                reject => {
                                    var errorObject = {
                                        ErrorNumber: 500,
                                        ErrorName: 'Insert New Funding Sources',
                                        ErrorDescription: 'Inserting new funding sources failed. More information is available in the ErrorObject.',
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
                            )

                        },
                        reject => {
                            var errorObject = {
                                ErrorNumber: 500,
                                ErrorName: 'Get New Funding Sources',
                                ErrorDescription: 'Getting list of new funding sources failed. More information is available in the ErrorObject.',
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
                        ErrorName: 'Get Current Funding Sources',
                        ErrorDescription: 'Getting list of current funding sources failed. More information is available in the ErrorObject.',
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

/**
 * All new vendors get staged in Integrations DB
 * @param {*} options
 */
function stageNewVendors(options) {

    return new Promise(
        (res, rej) => {

            if (!options && !options.id) {
                console.error(chalk.red('No GUID provided. Task cannot proceed.'));
                rej();
            }

            let dataid = options.id;
            let useVendorIDs = options ? options.useIDs : false;

            repository.getCurrentVendors(useVendorIDs).then(
                resolve => {
                    let currentVendors = resolve === [] ? resolve : resolve.map(m => { return m.dataValues; }); //need to change once we have uploaded data
                    repository.getNewVendors(dataid, currentVendors, useVendorIDs).then(
                        resolve => {
                            let flatVendors = resolve.map(m => { return m.dataValues; });

                            if (flatVendors && flatVendors.length === 0) {
                                console.log(mappings.getCurrentDate() + ' No new vendors to add!');
                                res();
                            }

                            let vendorsToAdd = [];
                            for (let v of flatVendors) {
                                let x = { VendorID: v.VENDOR_ID, VendorName: v.VENDOR_NAME, Client: configuration.config.client };
                                vendorsToAdd.push(x);
                            }
                            console.log(mappings.getCurrentDate() + ' Adding ' + vendorsToAdd.length + ' new vendors to staging table.')

                            repository.insertVendors(vendorsToAdd).then(
                                resolve => {
                                    res();
                                },
                                reject => {
                                    var errorObject = {
                                        ErrorNumber: 500,
                                        ErrorName: 'Insert New Vendors',
                                        ErrorDescription: 'Insert of new vendors failed. More information is available in the ErrorObject.',
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
                            )
                        },
                        reject => {
                            var errorObject = {
                                ErrorNumber: 500,
                                ErrorName: 'Get New Vendors',
                                ErrorDescription: 'Select on new vendors failed. More information is available in the ErrorObject.',
                                ErrorObject: JSON.stringify(reject),
                                DataIntegrationsID: integid
                            }

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
                    )
                },
                reject => {
                    var errorObject = {
                        ErrorNumber: 500,
                        ErrorName: 'Get Current Vendors',
                        ErrorDescription: 'Select on current vendors failed. More information is available in the ErrorObject.',
                        ErrorObject: JSON.stringify(reject),
                        DataIntegrationsID: integid
                    }

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

/**
 * Inserts new record into Integrations DB DataIntegrations table.
 * @param {*} configuration
 * @param {*} options
 */
function createIntegration(options) {

    return new Promise(
        (res, rej) => {
            repository.insertIntegration({ client: configuration.config.client, description: configuration.config.typeDesc, id: options.id, integrationType: configuration.config.mapType }).then(
                resolve => {
                    res(resolve);
                },
                reject => {
                    var errorObject = {
                        ErrorNumber: 500,
                        ErrorName: 'Create Integration Record',
                        ErrorDescription: 'Application was not able to create a new integration record in the database. More information is available in the ErrorObject.',
                        ErrorObject: JSON.stringify(reject),
                        DataIntegrationsID: options.id
                    }

                    repository.logError(errorObject).then(
                        resolve => {
                            rej(resolve);
                        },
                        reject => {
                            rej(reject);
                        }
                    );

                }
            );
        });
}

/**Gets API token for TIPWEBAPI */
function getApiToken() {
    return new Promise(
        (res, rej) => {
            rq.getToken().then(
                resolve => {
                    console.log(mappings.getCurrentDate() + ' getApiToken resolved');
                    // console.log(resolve);
                    fs.writeFile(configuration.config.idFileLoc + 'token.json', JSON.stringify(resolve),
                        (err) => {
                            if (err) {
                                rej(err);
                                process.exit(0);
                            }

                            else {
                                res();
                                process.exit(0);
                            }
                        }
                    )

                },
                reject => {
                    console.error(reject);
                    process.exit(0);
                }
            );
        }
    );
}

/**Upserts vendors via TIPWEBAPI */
function upsertVendors(options) {
    return new Promise(
        (res, rej) => {
            repository.getVendorsToUpsert({ client: options.client, limitVal: options.limitVal, offsetVal: options.offset }).then(
                resolve => {
                    let noteAppend = 'Hayes Integration ' + mappings.getCurrentShortDate();
                    let dataToUpload = resolve.map(m => { return { VendorID: 0, VendorName: m.VendorName, Contact: '', Address1: m.Address1, Address2: m.Address2, City: m.City,
                                                               State: m.State, ZipCode: m.ZipCode, Phone: m.Phone, Fax: '', Email: m.Email, AccountNumber: m.VendorID, Notes: noteAppend }; });
                    console.log();
                    console.log(mappings.getCurrentDate() + ' upsertVendors starting at ' + options.offset + ' run ' + options.limitVal);
                    rq.upsertVendors(options.token, dataToUpload).then(
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
                                        ErrorNumber: rec.badVendor.vendorID,
                                        ErrorName: 'Vendor Rejected',
                                        ErrorDescription: 'All purchase orders must be sourced from an accepted vendor in TipWEB-IT. A vendor record was rejected while attempting to add to application. The error has more information.',
                                        ErrorObject: JSON.stringify(rec),
                                        DataIntegrationsID: options.intgid
                                    }
                                    repository.logError(recerr).then(
                                        resolve => {
                                            // console.log('Success');
                                            if (rejectedRecords.indexOf(rec) === rejectedRecords.length - 1) {
                                                res();
                                            }
                                        },
                                        reject => {
                                            // console.log("logError failed");
                                            // console.error(reject);
                                            if (rejectedRecords.indexOf(rec) === rejectedRecords.length - 1) {
                                                res();
                                            }
                                        }
                                    );
                                }
                                console.log(mappings.getCurrentDate() + ' Save Errors Complete');
                            }
                            else {
                                res();
                            }
                        },
                        reject => {
                            var errorObject = {
                                ErrorNumber: 500,
                                ErrorName: 'Upsert Vendors',
                                ErrorDescription: 'Application was not able to upsert vendors to TIPWeb-IT API. More information is available in the ErrorObject.',
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
                    var errorObject = {
                        ErrorNumber: 500,
                        ErrorName: 'Get Vendors to Upsert',
                        ErrorDescription: 'Application was not able to get vendors to upsert. More information is available in the ErrorObject.',
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
        }
    );
}

/**Upserts products via TIPWEBAPI */
function upsertProducts(options) {
    return new Promise(
        (res, rej) => {
            repository.getProductsToUpsert({ client: options.client, limitVal: options.limitVal, offset: options.offset }).then(
                resolve => {
                    let noteAppend = 'Hayes Integration ' + mappings.getCurrentShortDate();
                    let dataToUpload = resolve.map(m => { return { ProductNumber: 0, ProductName: m.ProductName, ProductDescription: m.ProductDescription, ProductType: m.ProductType, Model: m.Model, Manufacturer: m.Manufacturer,
                                                                   SuggestedPrice: m.SuggestedPrice, Notes: noteAppend, SKU: m.SKU, ProjectedLife: null, CustomField1: null, CustomField2: null, CustomField3: null }; });
                    // for (let p of dataToUpload) {
                    //     p.ProductNumber = 'INTG' + p.ProductNumber; //Empty the Product Number to alow the API to auto assign the Product Number
                    // }
                    console.log();
                    console.log(mappings.getCurrentDate() + ' upsertProducts starting at ' + options.offset + ' run ' + options.limitVal);
                    rq.upsertProducts(options.token, dataToUpload).then(
                        resolve => {
                            let rejectedRecords = JSON.parse(resolve);
                            if (rejectedRecords == undefined || rejectedRecords === null) {
                                // console.log('No Detail records were rejected');
                                rejectedRecords = [];
                            }
                            console.log(); // to create a new line
                            console.log(mappings.getCurrentDate() + ' Successfully processed ' + dataToUpload.length + ' records, ' + rejectedRecords.length + ' records were rejected.');
                            if (rejectedRecords.length > 0) {
                                console.log(mappings.getCurrentDate() + ' Saving ' + rejectedRecords.length + ' Errors');
                                for (let rec of rejectedRecords) {
                                    let recerr = {
                                        ErrorNumber: rec.badProduct.ProductNumber,
                                        ErrorName: 'Product Rejected',
                                        ErrorDescription: 'All purchase order items must be added to the TipWEB-IT item catalog. A product record was rejected while attempting to add to the catalog. The error has more information.',
                                        ErrorObject: JSON.stringify(rec),
                                        DataIntegrationsID: options.intgid
                                    };
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
                                                res();
                                            }
                                        }
                                    );
                                }
                                console.log(mappings.getCurrentDate() + ' Save Errors Complete');
                            }
                            else {
                                res();
                            }
                        },
                        reject => {
                            // console.error(reject);

                            let errorObject = {
                                ErrorNumber: 500,
                                ErrorName: 'Upsert Products',
                                ErrorDescription: 'Application was not able to upsert products to TIPWeb-IT API. More information is available in the ErrorObject.',
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
                    let errorObject = {
                        ErrorNumber: 500,
                        ErrorName: 'Get Products to Upsert',
                        ErrorDescription: 'Application was not able to get products to upsert. More information is available in the ErrorObject.',
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
        }
    );
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
 */
function upsertAllProducts(options) {
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
                repository.getTotalProductsToUpsertCount({ client: client }).then(
                    resolve => {
                        console.log(); // to create a new line
                        console.log(mappings.getCurrentDate() + ' upserAllProducts count: ' + resolve);
                        let total = resolve;
                        let lv = parseInt(options.lv);
                        let i = parseInt(options.iVal);
                        upsertProductsRecursive({ client: client, limitVal: lv, offset: i, token: tokenVal, total: total, intgid: intgid }, () => { res('Done'); });
                    },
                    reject => {
                        rej(reject);
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
 * Use to upsert all vendor records available for submission
 */
function upsertAllVendors(options) {
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
                repository.getTotalVendorsToUpsertCount({ client: client }).then(
                    resolve => {
                        console.log(); // to create a new line
                        console.log(mappings.getCurrentDate() + ' upserAllVendors count: ' + resolve);
                        let total = resolve;
                        let lv = parseInt(options.lv);
                        let i = parseInt(options.iVal);
                        upsertVendorsRecursive({ client: client, limitVal: lv, offset: i, token: tokenVal, total: total, intgid: intgid }, () => { res('Done'); });
                    },
                    reject => {
                        rej(reject);
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
 * Use to upsert all header records available for submission
 */
function upsertAllHeaders(options) {
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
                repository.getTotalHeadersToUpsertCount({ id: intgid, client: client }).then(
                    resolve => {
                        console.log();
                        console.log(mappings.getCurrentDate() + ' upsertAllHeaders count: ' + resolve);
                        // console.log(resolve);
                        let total = resolve;
                        let lv = parseInt(options.lv);
                        let i = parseInt(options.iVal);
                        upsertHeadersRecursive({ client: client, limitVal: lv, offset: i, token: tokenVal, total: total, intgid: intgid }, () => { res('Done'); });
                    },
                    reject => {
                        rej(reject);
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
 * Used to upsert all detail records available for submission
 */
function upsertAllDetails(options) {
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
                repository.getTotalDetailsToUpsertCount({ id: intgid, client: client }).then(
                    resolve => {
                        console.log();
                        console.log(mappings.getCurrentDate() + ' upsertAllDetails count: ' + resolve);
                        // console.log(resolve);
                        let total = resolve;
                        let lv = parseInt(options.lv);
                        let i = parseInt(options.iVal);
                        upsertDetailsRecursive({ client: client, limitVal: lv, offset: i, token: tokenVal, total: total, intgid: intgid }, () => { res('Done'); });
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
 */
function upsertProductsRecursive(options, callback) {
    let i = options.offset;
    // console.log(options);
    upsertProducts(options).then(
        resolve => {
            i += options.limitVal;
            if (i < options.total) {
                // console.log(options);
                upsertProductsRecursive({ client: options.client, limitVal: options.limitVal, offset: i, token: options.token, total: options.total, intgid: options.intgid }, callback);
            }
            else {
                callback();
            }
        },
        reject => {
            i += options.limitVal;
            if (i < options.total) {
                // console.log(options);
                upsertProductsRecursive({ client: options.client, limitVal: options.limitVal, offset: i, token: options.token, total: options.total, intgid: options.intgid }, callback);
            }
            else {
                callback();
            }
        }
    );

}

/**
 * Adding data to TipWEB-IT requires chunking data.
 * @param {*} options
 * @param {*} callback
 */
function upsertVendorsRecursive(options, callback) {
    let i = options.offset;
    // console.log(options);
    upsertVendors(options).then(
        resolve => {
            i += options.limitVal;
            if (i < options.total) {
                // console.log(options);
                upsertVendorsRecursive({ client: options.client, limitVal: options.limitVal, offset: i, token: options.token, total: options.total, intgid: options.intgid }, callback);
            }
            else {
                callback();
            }
        },
        reject => {
            i += options.limitVal;
            if (i < options.total) {
                // console.error(options);
                upsertVendorsRecursive({ client: options.client, limitVal: options.limitVal, offset: i, token: options.token, total: options.total, intgid: options.intgid }, callback);
            }
            else {
                callback();
            }
        }
    );

}

/**
 * Adding data to TipWEB-IT requires chunking data.
 * @param {*} options
 * @param {*} callback
 */
function upsertHeadersRecursive(options, callback) {
    let i = options.offset;
    // console.log(options);
    upsertHeaderRecords(options).then(
        resolve => {
            i += options.limitVal;
            if (i < options.total) {
                upsertHeadersRecursive({ client: options.client, limitVal: options.limitVal, offset: i, token: options.token, total: options.total, intgid: options.intgid }, callback);
            }
            else {
                callback();
            }
        },
        reject => {
            i += options.limitVal;
            // console.error(reject);
            if (i < options.total) {
                upsertHeadersRecursive({ client: options.client, limitVal: options.limitVal, offset: i, token: options.token, total: options.total, intgid: options.intgid }, callback);
            }
            else {
                callback();
            }
        }
    );

}

/**
 * Adding data to TipWEB-IT requires chunking data.
 * @param {*} options
 * @param {*} callback
 */
function upsertDetailsRecursive(options, callback) {
    let i = options.offset;
    // console.log(options);
    upsertDetailRecords(options).then(
        resolve => {
            i += options.limitVal;
            if (i < options.total) {
                // console.log(options);
                upsertDetailsRecursive({ client: options.client, limitVal: options.limitVal, offset: i, token: options.token, total: options.total, intgid: options.intgid }, callback);
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
                upsertDetailsRecursive({ client: options.client, limitVal: options.limitVal, offset: i, token: options.token, total: options.total, intgid: options.intgid }, callback);
            }
            else {
                callback();
            }
        }
    );

}

function upsertShipmentsRecursive(options, callback) {
    let i = options.offset;
    // console.log(options);
    upsertShipmentRecords(options).then(
        resolve => {
            i += options.limitVal;
            if (i < options.total) {
                // console.log(options);
                upsertShipmentsRecursive({ client: options.client, limitVal: options.limitVal, offset: i, token: options.token, total: options.total, intgid: options.intgid }, callback);
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
                upsertShipmentsRecursive({ client: options.client, limitVal: options.limitVal, offset: i, token: options.token, total: options.total, intgid: options.intgid }, callback);
            }
            else {
                callback();
            }
        }
    );

}

/**
 * Upsert Header records via TIPWEBAPI
 */
function upsertHeaderRecords(options) {
    return new Promise(
        (res, rej) => {
            repository.getHeadersToUpsert({ intgid: options.intgid, client: options.client, limitVal: options.limitVal, offsetVal: options.offset }).then(
                resolve => {
                    let noteAppend = 'Hayes Integration ' + mappings.getCurrentShortDate();
                    let dataToUpload = resolve.map(m => { return { OrderNumber: m.OrderNumber, Status: m.Status, VendorID: m.VendorID, VendorName: m.VendorName, SiteID: m.SiteID, PurchaseDate: m.PurchaseDate,
                                                                   EstimatedDeliveryDate: m.EstimatedDeliveryDate, Notes: noteAppend + ' ' + m.Notes, Other1: m.Other1}; });
                    // for (let value of dataToUpload) {
                    //     value.DataIntegration = undefined;
                    // }
                    console.log();
                    console.log(mappings.getCurrentDate() + ' upsertHeaderRecords starting at ' + options.offset + ' run ' + options.limitVal);
                    rq.upsertHeaders(options.token, dataToUpload).then(
                        resolve => {
                            console.log();
                            // console.log('upsertHeaders resolved');
                            let rejectedRecords = JSON.parse(resolve);
                            if (rejectedRecords === undefined || rejectedRecords === null) {
                                // console.log('No Detail records were rejected');
                                rejectedRecords = [];
                            }
                            // console.log(rejectedRecords);
                            console.log(mappings.getCurrentDate() + ' Successfully processed ' + dataToUpload.length + ' records, ' + rejectedRecords.length + ' records were rejected.');
                            let rejectedRecordNumbers = rejectedRecords.map(m => { return m.badPurchaseOrderHeader.orderNumber; });
                            let dataToUploadNumbers = dataToUpload.map(m => { return m.OrderNumber });
                            let submittedVals = dataToUploadNumbers.filter(fil => { return rejectedRecordNumbers.indexOf(fil) < 0; });

                            let submittedHeaders = dataToUpload.filter(data => { return rejectedRecordNumbers.indexOf(data.OrderNumber) < 0; });
                            submittedHeaders = submittedHeaders.map(submitted => { return { orderNumber: submitted.OrderNumber, vendorID: submitted.VendorID, siteID: submitted.SiteID }; });
                            //repository.updateSubmittedValues({ target: 'PurchaseOrderHeader', ins: submittedVals, id: options.intgid }).then(
                            repository.updateSubmittedHeaders({ id: options.intgid, headers: submittedHeaders }).then(
                                resolve => {
                                    console.log();
                                    console.log(mappings.getCurrentDate() + ' updateSubmittedHeaders count: ' + submittedVals.length);
                                    if (rejectedRecords.length > 0) {
                                        console.log(mappings.getCurrentDate() + ' Saving ' + rejectedRecords.length + ' Errors');
                                        for (let rec of rejectedRecords) {
                                            let recerr = {
                                                ErrorNumber: rec.badPurchaseOrderHeader.orderNumber,
                                                ErrorName: 'Purchase Rejected',
                                                ErrorDescription: 'A purchase order consists of 3 parts; "header" which contains the shell information; "detail" which contains individual items on a purchase order; and "shipment" to an initial site. An invalid header record was submitted. See errors for reason it was rejected.',
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
                                    console.log();
                                    console.log(mappings.getCurrentDate() + ' updateSubmittedHeaders failed');

                                    var errorObject = {
                                        ErrorNumber: 500,
                                        ErrorName: 'Toggle',
                                        ErrorDescription: 'Application was not able to toggle headers submitted to TipWEB-IT API. More information is available in the ErrorObject.',
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
                            // console.log();
                            // console.log("upsertHeaders http-request failed");

                            var errorObject = {
                                ErrorNumber: 500,
                                ErrorName: 'Get Headers to Process',
                                ErrorDescription: 'Application was not able to get header values to upsert via TipWEB-IT API. More information is available in the ErrorObject.',
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
                    console.log('getHeadersToUpsert failed: ' + reject);
                    rej();
                }
            )
        }
    );
}

function upsertDetailRecords(options) {
    return new Promise(
        (res, rej) => {
            repository.getDetailsToUpsert({ intgid: options.intgid, client: options.client, limitVal: options.limitVal, offsetVal: options.offset }).then(
                resolve => {
                    let dataToUpload = resolve.map(m => { return m.dataValues; });
                    console.log();
                    console.log(mappings.getCurrentDate() + ' upsertDetailRecords starting at ' + options.offset + ' run ' + options.limitVal);
                    // console.log('token: ' + options.token);
                    rq.upsertDetails(options.token, dataToUpload).then(
                        resolve => {
                            console.log();
                            // console.log('upsertDetails resolved');
                            let rejectedRecords = JSON.parse(resolve);
                            if (rejectedRecords == undefined || rejectedRecords === null) {
                                // console.log('No Detail records were rejected');
                                rejectedRecords = [];
                            }
                            // console.log(rejectedRecords);
                            console.log(mappings.getCurrentDate() + ' Successfully processed ' + dataToUpload.length + ' records, ' + rejectedRecords.length + ' records were rejected.');
                            let rejectedRecordNumbers = rejectedRecords.map(m => { return m.badPurchaseOrderDetail.orderNumber; });
                            let dataToUploadNumbers = dataToUpload.map(m => { return m.OrderNumber });
                            let submittedVals = dataToUploadNumbers.filter(fil => { return rejectedRecordNumbers.indexOf(fil) < 0; });

                            let submittedDetails = dataToUpload.filter(data => { return rejectedRecordNumbers.indexOf(data.OrderNumber) < 0; });
                            submittedDetails = submittedDetails.map(submitted => { return { orderNumber: submitted.OrderNumber, lineNumber: submitted.LineNumber, siteID: submitted.SiteID }; });
                            //repository.updateSubmittedValues({ target: 'PurchaseOrderDetail', ins: submittedVals, id: options.intgid }).then(
                            repository.updateSubmittedDetails({ id: options.intgid, details: submittedDetails }).then(
                                resolve => {
                                    console.log();
                                    console.log(mappings.getCurrentDate() + ' updateSubmittedDetails count: ' + submittedVals.length);
                                    if (rejectedRecords.length > 0) {
                                        console.log(mappings.getCurrentDate() + ' Saving ' + rejectedRecords.length + ' Errors');
                                        for (let rec of rejectedRecords) {
                                            let recerr = {
                                                ErrorNumber: rec.badPurchaseOrderDetail.orderNumber,
                                                ErrorName: 'Detail Record Rejected',
                                                ErrorDescription: 'A purchase order consists of 3 parts; "header" which contains the shell information; "detail" which contains individual items on a purchase order; and "shipment" to an initial site. An invalid detail record was submitted. See errors for reason it was rejected.',
                                                ErrorObject: JSON.stringify(rec),
                                                DataIntegrationsID: options.intgid
                                            }
                                            repository.logError(recerr).then(
                                                resolve => {
                                                    // console.log('Success');
                                                    if (rejectedRecords.indexOf(rec) === rejectedRecords.length - 1) {
                                                        res();
                                                    }
                                                },
                                                reject => {
                                                    // console.log("LogError failed");
                                                    // console.error(reject);
                                                    if (rejectedRecords.indexOf(rec) === rejectedRecords.length - 1) {
                                                        res();
                                                    }
                                                }
                                            );
                                        }
                                        console.log(mappings.getCurrentDate() + ' Save Errors Complete');
                                    }
                                    else {
                                        res();
                                    }
                                },
                                reject => {
                                    console.log();
                                    console.log(mappings.getCurrentDate() + ' updateSubmittedDetails failed');

                                    var errorObject = {
                                        ErrorNumber: 500,
                                        ErrorName: 'Process Details',
                                        ErrorDescription: 'Application was not able to toggle details submitted to TipWEB-IT API. More information is available in the ErrorObject.',
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
                                });
                        },
                        reject => {
                            // console.log();
                            // console.log("upsertDetails http-request failed");

                            var errorObject = {
                                ErrorNumber: 500,
                                ErrorName: 'Process Details',
                                ErrorDescription: 'Application was not able to upsert data via TipWEB-IT API. More information is available in the ErrorObject.',
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
                    var errorObject = {
                        ErrorNumber: 500,
                        ErrorName: 'Get Details to Process',
                        ErrorDescription: 'Application was not able to get detail values to upsert via TipWEB-IT API. More information is available in the ErrorObject.',
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
            );
        });
}

function upsertShipmentRecords(options) {
    return new Promise(
        (res, rej) => {
            repository.getShipmentsToUpsert({ client: options.client, limitVal: options.limitVal, offsetVal: options.offset, id: options.intgid }).then(
                resolve => {
                    let dataToUpload = resolve.map(m => { return m.dataValues; });
                    console.log();
                    console.log(mappings.getCurrentDate() + ' upsertShipmentRecords starting at ' + options.offset + ' run ' + options.limitVal);
                    // console.log('token: ' + options.token);
                    rq.upsertShipments(options.token, dataToUpload).then(
                        resolve => {
                            console.log();
                            // console.log('upsertShipments resolved');
                            let rejectedRecords = JSON.parse(resolve);
                            if (rejectedRecords == undefined || rejectedRecords === null) {
                                // console.log('No Detail records were rejected');
                                rejectedRecords = [];
                            }
                            // console.log(rejectedRecords);
                            console.log(mappings.getCurrentDate() + ' Successfully processed ' + dataToUpload.length + ' records, ' + rejectedRecords.length + ' records were rejected.');
                            let rejectedRecordNumbers = rejectedRecords.map(m => { return m.badShipment.orderNumber; });
                            let dataToUploadNumbers = dataToUpload.map(m => { return m.OrderNumber });
                            let submittedVals = dataToUploadNumbers.filter(fil => { return rejectedRecordNumbers.indexOf(fil) < 0; });

                            let submittedShipments = dataToUpload.filter(data => { return rejectedRecordNumbers.indexOf(data.OrderNumber) < 0; });
                            submittedShipments = submittedShipments.map(submitted => { return { orderNumber: submitted.OrderNumber, lineNumber: submitted.LineNumber, siteID: submitted.SiteID }; });
                            //repository.updateSubmittedValues2({ target: 'Shipments', ins: submittedVals, id: options.intgid }).then(
                            repository.updateSubmittedShipments({ id: options.intgid, shipments: submittedShipments }).then(
                                resolve => {
                                    console.log();
                                    console.log(mappings.getCurrentDate() + ' updateSubmittedShipments count: ' + submittedVals.length);
                                    if (rejectedRecords.length > 0) {
                                        console.log(mappings.getCurrentDate() + ' Saving ' + rejectedRecords.length + ' Errors');
                                        for (let rec of rejectedRecords) {
                                            let recerr = {
                                                ErrorNumber: rec.badShipment.orderNumber,
                                                ErrorName: 'Shipment Rejected',
                                                ErrorDescription: 'A purchase order consists of 3 parts; "header" which contains the shell information; "detail" which contains individual items on a purchase order; and "shipment" to an initial site. An invalid shipment record was submitted. See errors for reason it was rejected.',
                                                ErrorObject: JSON.stringify(rec),
                                                DataIntegrationsID: options.intgid
                                            };
                                            repository.logError(recerr).then(
                                                resolve => {
                                                    // console.log('Success');
                                                    if (rejectedRecords.indexOf(rec) === rejectedRecords.length - 1) {
                                                        res();
                                                    }
                                                },
                                                reject => {
                                                    // console.log("LogError failed");
                                                    // console.error(reject);
                                                    if (rejectedRecords.indexOf(rec) === rejectedRecords.length - 1) {
                                                        res();
                                                    }
                                                }
                                            );
                                        }
                                        console.log(mappings.getCurrentDate() + ' Save Errors Complete');
                                    }
                                    else {
                                        res();
                                    }
                                },
                                reject => {
                                    console.log();
                                    console.log(mappings.getCurrentDate() + ' updateSubmittedShipments failed');

                                    var errorObject = {
                                        ErrorNumber: 500,
                                        ErrorName: 'Toggle',
                                        ErrorDescription: 'Application was not able to toggle shipments submitted to TipWEB-IT API. More information is available in the ErrorObject.',
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
                                });
                            //to here
                        },
                        reject => {
                            // console.log();
                            // console.log("upsertShipments http-request failed");

                            var errorObject = {
                                ErrorNumber: 500,
                                ErrorName: 'Process Shipments',
                                ErrorDescription: 'Application was not able to upsert data via TipWEB-IT API. More information is available in the ErrorObject.',
                                ErrorObject: JSON.stringify(reject), // + ', ' + JSON.stringify(dataToUpload),
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
                    var errorObject = {
                        ErrorNumber: 500,
                        ErrorName: 'Get Details to Process',
                        ErrorDescription: 'Application was not able to get shipment values to upsert via TipWEB-IT API. More information is available in the ErrorObject.',
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
            );
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
    return new Promise(
        (res, rej) => {
            if (!options && !options.id) {
                console.error(chalk.red('No GUID provided. Task cannot proceed.'));
                rej();
            }
            repository.runProcIntegrations_RemoveUnnecessaryRecords(options.id, { headers: configuration.dataConfig.procRemove.headers, details: configuration.dataConfig.procRemove.details, shipping: configuration.dataConfig.procRemove.shipping, inventory: configuration.dataConfig.procRemove.inventory, charges: configuration.dataConfig.procRemove.charges, payments: configuration.dataConfig.procRemove.payments }).then(
                resolve => {
                    res();
                },
                reject => {
                    var errorObject = {
                        ErrorNumber: 500,
                        ErrorName: 'Filter',
                        ErrorDescription: 'Application was not able to filter the unnecessary records from the export to TIPWEBAPI. More information is available in the ErrorObject.',
                        ErrorObject: JSON.stringify(reject),
                        DataIntegrationsID: options.id
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
        });
}

function filterShipmentsWithBadDetails(options) {
    return new Promise(
        (res, rej) => {
            repository.getDataSendingToApiIntegrationID(configuration.config.client).then(
                resolve => {
                    let intgid = resolve;
                    repository.runProcIntegrations_FlagShipmentsFromBadDetailRecords(intgid).then(
                        resolve => {
                            console.log();
                            console.log(mappings.getCurrentDate() + ' Removed shipment records from manifest to send to API.')
                            res();
                        },
                        reject => {
                            var errorObject = {
                                ErrorNumber: 500,
                                ErrorName: 'Proc Remove Details',
                                ErrorDescription: 'Application was not able to execute stored procedure to remove detail and shipment records from bad purchase order header records. More information is available in the ErrorObject.',
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
                    )
                },
                reject => {
                    var errorObject = {
                        ErrorNumber: 500,
                        ErrorName: 'Get Integration ID',
                        ErrorDescription: 'Application was not able to get Integration ID currently sending to TIPWEBAPI. More information is available in the ErrorObject.',
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
        });
}

function filterDetailsWithBadHeaders(options) {
    return new Promise(
        (res, rej) => {

            if (!options && !options.id) {
                console.error('No GUID provided. Task cannot proceed.');
                rej();
            }

            repository.runProcIntegrations_FlagDetailsAndShipmentsFromBadHeaderRecords(options.id).then(
                resolve => {
                    console.log();
                    console.log(mappings.getCurrentDate() + ' Removed detail and shipment records from manifest to send to API.')
                    process.exit(0);
                },
                reject => {
                    var errorObject = {
                        ErrorNumber: 500,
                        ErrorName: 'Proc Remove Details',
                        ErrorDescription: 'Application was not able to execute stored procedure to remove detail and shipment records from bad purchase order header records. More information is available in the ErrorObject.',
                        ErrorObject: JSON.stringify(reject),
                        DataIntegrationsID: intgid
                    }

                    repository.logError(errorObject).then(
                        resolve => {
                            // console.log("Success... closing...");
                            process.exit(0);
                            return;
                        },
                        reject => {
                            // console.log('Error logging error.');
                            process.exit(0);
                            return;
                        }
                    );
                }
            )
        });
}

/**
 * Toggle DataSentToTipweb
 * @param {*} options Must contain an id parameter.
 *//** */
function toggleToSending(options) {
    return new Promise(
        (res, rej) => {

            if (!options && !options.id) {
                console.error('No GUID provided. Task cannot proceed.');
                rej();
            }

            repository.beginSendingToTipwebAPI(options.id).then(
                resolve => {
                    console.log();
                    console.log(mappings.getCurrentDate() + ' No longer processing. Sending to TipWEB-IT now.');
                    res();
                },
                reject => {

                    // console.reject(reject);

                    var errorObject = {
                        ErrorNumber: 500,
                        ErrorName: 'Toggle',
                        ErrorDescription: 'Application was not able to toggle DataSentToTipweb process. More information is available in the ErrorObject.',
                        ErrorObject: JSON.stringify(reject),
                        DataIntegrationsID: options.id
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
        });
}

function toggleToPostProcessing(options) {
    return new Promise(
        (res, rej) => {

            if (!options && !options.id) {
                console.error('No GUID provided. Task cannot proceed.');
                rej();
            }

            repository.beginDataPostProcessing(options.id).then(
                resolve => {
                    console.log();
                    console.log(mappings.getCurrentDate() + ' beginDataPostprocessing Complete!');
                    res();
                },
                reject => {

                    // console.error(reject);

                    var errorObject = {
                        ErrorNumber: 500,
                        ErrorName: 'Toggle',
                        ErrorDescription: 'Application was not able to toggle DataPostProcessing process. More information is available in the ErrorObject.',
                        ErrorObject: JSON.stringify(reject),
                        DataIntegrationsID: options.id
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
        });
}

/**
 * Toggle Integration as successful.
 * @param {*} options Must contain an id parameter.
 */
function toggleSuccessfulIntegration(options) {
    return new Promise(
        (res, rej) => {

            if (!options && !options.id) {
                console.error(chalk.red('No GUID provided to complete!'));
                rej();
            }

            repository.completeIntegrationProcessing(options.id).then(
                resolve => {
                    console.log();
                    console.log(mappings.getCurrentDate() + ' completeIntegrationProcessing resolved')
                    res();
                },
                reject => {
                    console.log();
                    console.log(mappings.getCurrentDate() + ' completeIntegrationProcessing rejected');
                    rej();
                }
            );
        });
}

function getLinkTableData(options) {
    return new Promise(
        (res, rej) => {
            //get list of link tables
            //run query for data
            //write to json file
            let linkTypes = configuration.config.links;

            for (let l of linkTypes) {
                repository.getLinkTableData({ client: configuration.config.client, type: l.type }).then(
                    resolve => {
                        let linkVals = resolve.map(m => { return m.dataValues; });
                        let fullFileName = configuration.config.linksFolder + l.filename;
                        fs.writeFile(fullFileName, JSON.stringify(linkVals),
                            (err) => {
                                if (err) {
                                    rej(err);
                                }
                                else if (linkTypes.indexOf(l) === linkTypes.length - 1) {
                                    res();
                                }
                            }
                        );
                    },
                    reject => {
                        rej(reject);
                    }
                );
            }
        }
    )
}

function runCustomScripts(options) {
    return new Promise(
        (res, rej) => {
            console.log();
            console.log(mappings.getCurrentDate() + ' runCustomScripts Starting');
            repository.getProcessingIntegrationID().then(
                resolve => {
                    let intgid = resolve;
                    let custTasks = configuration.config.customTasks;
                    let options = { client: configuration.config.client, intgid: '\'' + intgid + '\'' };
                    for (let task of custTasks) {
                        let funk = repository[task.fn];
                        // console.log('getProcessingIntegrationID repository task: ' + funk);
                        funk(options).then(
                            resolve => {
                                if (custTasks.indexOf(task) === custTasks.length - 1) {
                                    console.log();
                                    console.log(mappings.getCurrentDate() + ' Custom Function ' + task.fn + ' completed');
                                    res();
                                }
                            },
                            reject => {
                                console.log();
                                console.log(mappings.getCurrentDate() + ' Custom function ' + task.fn + ' failed');
                                rej(reject);
                            }
                        );
                    }
                },
                reject => {
                    rej(reject);
                }
            );
        }
    );
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
                    filetasks.getDataFile(options.fileName).then(
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
            repository.getInvoiceHeadersToAdd({ client: options.client, limitVal: options.limitVal, offsetVal: options.offset }).then(
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
            repository.getInvoiceDetailsToAdd({ client: options.client, limitVal: options.limitVal, offsetVal: options.offset }).then(
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

/**Final Application Methods */

/**General */

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
 */
function mapFromFile(options) {

    return new Promise(
        (res, rej) => {
            if (!options || !options.filename) {
                console.error(chalk.red('No file provided.'));
                rej('Task Error');
            }
            else if (!options || !options.id) {
                console.error(chalk.red('No GUID provided. Process cannot continue.'));
                rej('Task Error');
            }
            else {
                let dataid = options.id;
                let filename = options.filename;
                let mapType = configuration.config.mapType;
                let mapClient = configuration.config.client;
                let mappedData = [];
                console.log();
                console.log(mappings.getCurrentDate() + ' mapFromFile for file: ' + filename);
                filetasks.getDataFile(filename).then(
                    resolve => {
                        let filedata = JSON.parse(resolve);
                        // console.log('Processing ' + filedata.length + ' records...');
                        repository.getMappings({ client: mapClient, type: mapType }).then(
                            resolve => {
                                let theMaps = resolve.map(m => { return m.dataValues; });
                                console.log();
                                console.log(mappings.getCurrentDate() + ' Processing ' + filedata.length + ' records...');
                                for (let line of filedata) {
                                    let mapData = theMaps.map(m => { return JSON.parse(m.MappingsObject); });
                                    let m = mappings.mapIt(JSON.parse(line), mapData);
                                    m["IntegrationsID"] = dataid;
                                    mappedData.push(m);
                                }
                                console.log();
                                console.log(mappings.getCurrentDate() + ' Successfully mapped ' + mappedData.length + ' records...');
                                let trackerList;
                                let tableInfo = configuration.dataConfig.flatDataTable;
                                for (let item of mappedData) {
                                    repository.insertFlatData(item, { target: tableInfo }).then(
                                        resolve => {
                                            // console.log('Successfully added ' + JSON.stringify(item));
                                            if (!trackerList && mappedData.indexOf(item) === mappedData.length - 1) {
                                                res();
                                            }
                                        },
                                        reject => {
                                            trackerList = mappedData.indexOf(item);
                                            var errorObject = {
                                                ErrorNumber: item.PO_NUMBER,
                                                ErrorName: 'Bad Record',
                                                ErrorDescription: 'Data for this record violated one or multiple data constraints. See error for more information.',
                                                ErrorObject: JSON.stringify({ errorData: item, error: reject.toString() }),
                                                DataIntegrationsID: dataid
                                            }
                                            repository.logError(errorObject).then(
                                                resolve => {
                                                },
                                                reject => {
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
                                console.log(mappings.getCurrentDate() + ' map flat data from file complete');
                            },
                            reject => {
                                let errorObject = {
                                    ErrorNumber: 500,
                                    ErrorName: 'Get Map Error',
                                    ErrorDescription: 'Application was not able to retrieve data field mappings from database. More information is available in the ErrorObject.',
                                    ErrorObject: JSON.stringify(reject),
                                    DataIntegrationsID: dataid
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
                        let errorObject = {
                            ErrorNumber: 500,
                            ErrorName: 'File Error',
                            ErrorDescription: 'Application was not able to parse data from a file: ' + filename + '. More information is available in the ErrorObject.',
                            ErrorObject: JSON.stringify(reject),
                            DataIntegrationsID: dataid
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
            }

        }
    );
}

/**Purchase Orders */

/**Invoices */

/** Data Integration Files */


/**
 * Inserts new record into Integrations DB DataIntegrations table.
 * @param {*} configuration
 * @param {*} options
 */
function insertDataIntegrationsFile(options) {

    return new Promise(
        (res, rej) => {
            // console.log(options);
            repository.insertDataIntegrationsFile({ FileNameAws: options.filename, AwsFileLink: options.filelink, Client: options.client, DataIntegrationsID: options.id }).then(
                resolve => {
                    // console.log("File: " + options.filename + " inserted.");
                    res(resolve);
                },
                reject => {
                    var errorObject = {
                        ErrorNumber: 500,
                        ErrorName: 'Create Integration Record',
                        ErrorDescription: 'Application was not able to create a new integration file record in the database. More information is available in the ErrorObject.',
                        ErrorObject: JSON.stringify(reject),
                        DataIntegrationsID: options.id
                    }

                    repository.logError(errorObject).then(
                        resolve => {
                            rej(resolve);
                        },
                        reject => {
                            rej(reject);
                        }
                    );
                }
            );
        }
    );
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

/** Data Integration Files */