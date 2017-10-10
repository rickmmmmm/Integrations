#!/usr/bin/env node

"use strict";

var chalk = require('chalk');
var clear = require('clear');
var CLI = require('clui');
var figlet = require('figlet');
var inquirer = require('inquirer');
var Preferences = require('preferences');
var Spinner = CLI.Spinner;
var _ = require('lodash');
var fs = require('fs');
const repository = require('./lib/repository.js');
const filetasks = require('./lib/file-tasks.js');
const logger = require('./lib/log-to-file.js');
const configuration = require('./lib/configuration.js');
const mappings = require('./lib/mappings.js');
const rq = require('./lib/http-requests.js');

var action = process.argv[2];
var subaction = process.argv[3];

var activities = [
  { name: '--help', shortname: '-h', desc: 'Show help menu.', action: showHelp },
  { name: '--config', shortname: '-co', desc: 'Show current configuration settings.', action: getConfiguration },
  { name: '--chunk', shortname: '-chu', desc: 'Toggles a chunk of data imported as completed.', action: toggleChunkedData },
  { name: '--create', shortname: '-cr', desc: 'Creates New Integration ID.', action: createIntegration },
  { name: '--mapflat', shortname: '-m', desc: 'Map purchase order data from JSON to Database table.', action: mapFlat, options: { filename: subaction } },
  { name: '--vendors', shortname: '-v', desc: 'Adds vendors to staging database.', action: stageNewVendors, options: { useIDs: true  } },
  { name: '--products', shortname: '-p', desc: 'Map products from flat data table to Products.', action: productsFunc },
  { name: '--headers', shortname: '-hd', desc: 'Map headers from flat data table to PurchaseOrderHeaders.', action: headersFunc },
  { name: '--details', shortname: '-det', desc: 'Map details from flat data table to PurchaseOrderDetails.', action: detailsFunc },
  { name: '--shipping', shortname: '-sh', desc: 'Map shipments from flat data table to Shipments.', action: shippingFunc },
  { name: '--filter-unncessary', shortname: '-fu', desc: 'Adds vendors to staging database.', action: filterOldRecords },
  { name: '--filter-old-inserts', shortname: '-foi', desc: 'Removes any records that have already been inserted.', action: filterOldInserts },
  { name: '--filter-bad-details', shortname: '-fbd', desc: 'Removes any detail and shipment records with a bad header.', action: filterDetailsWithBadHeaders },
  { name: '--filter-bad-shipments', shortname: '-fbs', desc: 'Removes any shipment records with a bad detail record.', action: filterShipmentsWithBadDetails },
  { name: '--complete', shortname: '-C', desc: 'Toggles an integration ID to completed.', action: toggleSuccessfulIntegration },
  { name: '--send-to-api', shortname: '-S', desc: 'Toggles an integration ID from DataProcessing to DataSentToTipweb.', action: toggleFromProcessingToSending },
  { name: '--toggle-products', shortname: '-tp', desc: 'Toggle products from not sent to sent to TIPWEBAPI.', action: toggleProducts },
  { name: '--toggle-vendors', shortname: '-tv', desc: 'Toggle vendors from not sent to sent to TIPWEBAPI', action: toggleVendors },
  { name: '--funding', shortname: '-f', desc: 'Map funding sources from flat data table to FundingSources.', action: fundingFunc },
  { name: '--push-vendors', shortname: '-pv', desc: 'Push new vendor records via TIPWEBAPI.', action: upsertAllVendors, options:{ iVal: 0, lv: 800 }  },
  { name: '--push-products', shortname: '-pp', desc: 'Push new product records via TIPWEBAPI.', action: upsertAllProducts, options: { iVal: 0, lv: 800 } },
  { name: '--push-headers', shortname: '-ph', desc: 'Push new header records via TIPWEBAPI.', action: upsertAllHeaders, options: { iVal: 0, lv: 800 } },
  { name: '--push-details', shortname: '-pd', desc: 'Push new detail records via TIPWEBAPI.', action: upsertAllDetails, options: { iVal: 0, lv: 800 } },
  { name: '--push-shipments', shortname: '-ps', desc: 'Push new shipment records via TIPWEBAPI.', action: upsertAllShipments, options: { iVal: 0, lv: 800 } },
  { name: '--get-link-data', shortname: '-gld', desc: 'Get link table data for integration.', action: getLinkTableData },
  { name: '--custom-scripts', shortname: '-cust', desc: 'Runs a list of custom scripts on imported data.', action: runCustomScripts },
]

fire(action, subaction);

/**
 * Entry point function.
 * @param {*} action argument 1 from command line
 * @param {*} subaction argument 2 from command line
 */
function fire(action, subaction) {
  let a001 = activities.filter(fil => { return fil.name === action || fil.shortname === action; });
  //console.log(a001);
  if (a001 && a001.length === 1) {
    let cb = a001[0].action;
    let options = a001[0].options;
    let spinner = new Spinner('',['.','..','...','....'] );
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
      figlet.textSync('Hayes', { horizontalLayout : 'full'})
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

/**
 * Container function for all data mapping from flat purchase order data to database.
 * @param {*} options 
 */
function mapFlat(options) {
  return new Promise(
    (res, rej) => {
      if (!options.filename) {
        console.log('A valid file to process must be provided.')
        rej();
      }
  
      let opts = { fileName: options.filename };
      mapFlatDataToDatabase(opts).then(
        resolve => {
          console.log('Task Complete: Map purchase order data from JSON to Database table.');
          res();
        },
        reject => {
          console.error(chalk.red('Task Error: ' + reject));
          rej();
        }
      );
    }
  )
}

function toggleChunkedData() {
  return new Promise(
    (res, rej) => {
      repository.getProcessingIntegrationID().then(
        resolve => {
          let intgid = resolve;
          repository.toggleChunk(intgid).then(
            resolve => {
              res();
            },
            reject => {
              rej();
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

/** */
function productsFunc() {
  return new Promise(
    (res,rej) => {
      repository.runProcIntegrations_StageProductData({ client: configuration.config.client }).then(
        resolve => {
          console.log(resolve);
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
    (res,rej) => {
      repository.getProcessingIntegrationID().then(
        resolve => {
          let intgid = resolve;
          stagePurchaseOrderHeaders({ integrationID: intgid }).then(
            resolve => {
              res();
            },
            reject => {
              rej(reject);
            }
          );
        },
        reject => {
          console.error(chalk.red(reject));
          rej();
        }
      );
    }
  )  
}

/**
 * Container function for all detail related db activity.
 * @param {*} options 
 */
function detailsFunc(options) {
  return new Promise(
    (res,rej) => {
      repository.getProcessingIntegrationID().then(
        resolve => {
          let intgid = resolve;
          stagePurchaseOrderDetails({ integrationID: intgid }).then(
            resolve => {
              res();
            },
            reject => {
              rej(reject);
            }
          );
        },
        reject => {
          console.error(reject);
        }
      );
    }
  )
}

function shippingFunc(options) {
  return new Promise(
    (res,rej) => {
      repository.getProcessingIntegrationID().then(
        resolve => {
  
          if (!resolve) {
            rej();
          }
  
          let intgid = resolve;
          stageShippingRecords({ integrationID: intgid }).then(
            resolve => {
              res();
            },
            reject => {
              rej(reject);
            }
          );
        },
        reject => {
          rej(reject);
        }
      );
    }
  )
}

function fundingFunc(options) {
  return new Promise(
    (res,rej) => {
      repository.getProcessingIntegrationID().then(
        resolve => {
          let intgid = resolve;
          stageFundingSources({ integrationID: intgid }).then(
            resolve => {
              res();
            },
            reject => {
              rej(reject);
            }
          );
        },
        reject => {
          console.error(reject);
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
    (res,rej) => {
      var integid = options.integrationID;
      var mappedData = [];

      repository.getFlatShipments(integid).then(
        resolve => {
          console.log('Retrieved ' + resolve.length + ' shipment records to process.');
          let shipmentsData = resolve.map(m => { return m.dataValues; });
          repository.getMappings({ type: 'shipping', client: configuration.config.client}).then(
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
                  console.log('Successfully inserted ' + mappedData.length + ' into Shipments table.');
                  res();
                },
                reject => {
                  let errorObject = {
                      ErrorNumber: 500,
                      ErrorName: 'Insert Shipments',
                      ErrorDescription: 'Inserting Shipments records failed. More information is available in the ErrorObject.',
                      ErrorObject: reject.toString(),
                      DataIntegrationsID: integid
                    };

                    console.error(errorObject);

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
                      ErrorObject: reject.toString(),
                      DataIntegrationsID: integid
                    };

                    console.error(errorObject);

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
                      ErrorObject: reject.toString(),
                      DataIntegrationsID: integid
                    };

                    console.error(errorObject);

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
    (res,rej) => {
  
      var integid = options.integrationID;

      repository.getDetailRecordsFlatData(integid).then(
        resolve => {
          console.log('Retrieved ' + resolve.length + ' detail records to process.');
          let detailData = resolve.map(m => { return m.dataValues; });
          repository.getMappings({type: 'po details', client: configuration.config.client}).then(
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
                      ErrorObject: reject.toString(),
                      DataIntegrationsID: integid
                    };

                    console.error(errorObject);

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
                      ErrorObject: reject.toString(),
                      DataIntegrationsID: integid
                    };

                    console.error(errorObject);

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
                      ErrorObject: reject.toString(),
                      DataIntegrationsID: integid
                    };

                    console.error(errorObject);

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
          console.log('Retrieved ' + resolve.length + ' header records to process.');
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
                  console.log('Successfully inserted ' + mappedData.length + ' into PurchaseOrderHeaders table.');
                  res();
                },
                reject => {
                  let errorObject = {
                      ErrorNumber: 500,
                      ErrorName: 'Insert Headers',
                      ErrorDescription: 'Inserting Header records failed. More information is available in the ErrorObject.',
                      ErrorObject: reject.toString(),
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
                      ErrorObject: reject.toString(),
                      DataIntegrationsID: integid
                    };

                    console.error(errorObject);

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
                      ErrorObject: reject.toString(),
                      DataIntegrationsID: integid
                    };

                    console.error(errorObject);

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
            console.log('No new products to add!');
            process.exit(0);
          }

          let productsToAdd = [];

          for (let pf of productsFlat) {
            x = { ProductName: pf.PRODUCT_NAME, ProductType: pf.PRODUCT_TYPE, Model: pf.MODEL, Manufacturer: pf.MANUFACTURER, SuggestedPrice: pf.SuggestedPrice };
            productsToAdd.push(x);
          }

          repository.insertNewProducts(productsToAdd).then(
            resolve => {
              console.log('Successfully staged ' + productsToAdd.length + ' records in Products table.');
              process.exit(0);
            },
            reject => {
              var errorObject = {
                  ErrorNumber: 500,
                  ErrorName: 'Insert New Products',
                  ErrorDescription: 'Inserting new products failed. More information is available in the ErrorObject.',
                  ErrorObject: reject.toString(),
                  DataIntegrationsID: integid
                };

                console.error(errorObject);

              repository.logError(errorObject).then(
                resolve => {
                  console.log();
                  process.exit(0);
                },
                reject => {
                  console.log();
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
                  ErrorObject: reject.toString(),
                  DataIntegrationsID: integid
                };

                console.error(errorObject);

              repository.logError(errorObject).then(
                resolve => {
                  console.log();
                  return;
                },
                reject => {
                  console.log();
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
                  ErrorObject: reject.toString(),
                  DataIntegrationsID: integid
                };

                console.error(errorObject);

              repository.logError(errorObject).then(
                resolve => {
                  console.log();
                  return;
                },
                reject => {
                  console.log();
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
    (res,rej) => {
      var integid = options.integrationID;

      repository.getCurrentFundingSources().then(
        resolve => {
          let currentFundingSources = resolve === [] ? resolve : resolve.map(m => { return m.dataValues; } );
          repository.getNewFundingSources(currentFundingSources, integid).then(
            resolve => {
              let sourcesFlat = resolve === [] ? resolve : resolve.map(m => { return m.dataValues; });
              let sourcesToAdd = [];

              if (sourcesFlat && sourcesFlat.length === 0) {
                console.log('No funding sources to add!');
                res();
              }

              for (let sf of sourcesFlat) {
                x = { FundingSourceID: sf.FUNDING_SOURCE };
                sourcesToAdd.push(x);
              }

              repository.insertNewFundingSources(sourcesToAdd).then(
                resolve => {
                  console.log('Successfully added ' + sourcesToAdd.length + ' new funding sources!');
                  res();
                },
                reject => {
                  var errorObject = {
                      ErrorNumber: 500,
                      ErrorName: 'Insert New Funding Sources',
                      ErrorDescription: 'Inserting new funding sources failed. More information is available in the ErrorObject.',
                      ErrorObject: reject.toString(),
                      DataIntegrationsID: integid
                    };

                    console.error(errorObject);

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
                      ErrorObject: reject.toString(),
                      DataIntegrationsID: integid
                    };

                    console.error(errorObject);

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
                      ErrorObject: reject.toString(),
                      DataIntegrationsID: integid
                    };

                    console.error(errorObject);

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
    (res,rej) => {

      var integid;
      var useVendorIDs = options ? options.useIDs : false;

      repository.getProcessingIntegrationID().then(
        resolve => {
          integid = resolve;
          repository.getCurrentVendors(useVendorIDs).then(
            resolve => {
              let currentVendors = resolve === [] ? resolve : resolve.map(m => { return m.dataValues; }); //need to change once we have uploaded data
              repository.getNewVendors(integid,currentVendors,useVendorIDs).then(
                resolve => {
                  let flatVendors = resolve.map(m => { return m.dataValues; });

                  if (flatVendors && flatVendors.length === 0) {
                    console.log('No new vendors to add!');
                    res();
                  }

                  let vendorsToAdd = [];
                  for (let v of flatVendors) {
                    let x = { VendorID: v.VENDOR_ID, VendorName: v.VENDOR_NAME, Client: configuration.config.client };
                    vendorsToAdd.push(x);
                  }
                  console.log('Adding ' + vendorsToAdd.length + ' new vendors to staging table.')

                  repository.insertVendors(vendorsToAdd).then(
                    resolve => {
                      res();
                    },
                    reject => {
                      var errorObject = {
                      ErrorNumber: 500,
                      ErrorName: 'Insert New Vendors',
                      ErrorDescription: 'Insert of new vendors failed. More information is available in the ErrorObject.',
                      ErrorObject: reject.toString(),
                      DataIntegrationsID: integid
                    };

                    console.error(errorObject);

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
                      ErrorObject: reject.toString(),
                      DataIntegrationsID: integid
                    }

                    console.error(errorObject);

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
                      ErrorObject: reject.toString(),
                      DataIntegrationsID: integid
                    }

                    console.error(errorObject);

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
                      ErrorName: 'Get Current Integration ID',
                      ErrorDescription: 'Application was not able to get the current integration ID. More information is available in the ErrorObject.',
                      ErrorObject: reject.toString(),
                      DataIntegrationsID: integid
                    }

                    console.error(errorObject);

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
    (res,rej) =>  {
      repository.insertIntegration({ client: configuration.config.client, description: configuration.config.typeDesc }).then(
        resolve => {
          res(resolve);
        },
        reject => {
          var errorObject = {
            ErrorNumber: 500,
            ErrorName: 'Create Integration Record',
            ErrorDescription: 'Application was not able to create a new integration record in the database. More information is available in the ErrorObject.',
            ErrorObject: JSON.stringify(reject),
            DataIntegrationsID: intgid
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

/**
 * Maps data in file to Integration DB PurchaseOrderIntegrationFlatData
 * @param {*} options 
 */
function mapFlatDataToDatabase(options) {
  return new Promise (
    (res,rej) => {
      var intgid;
      var mappingsData;
      var fileToProcess;
      var mappedData = [];

      if (!options && !options.fileName) {
        console.log('No file provided to process...');
        process.exit(0);
      }

      fileToProcess = options.fileName;

      repository.getProcessingIntegrationID().then(
        resolve => {
          intgid = resolve;
          filetasks.getDataFile(fileToProcess).then(
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
                  mappingsData = resolve.map( m => { return m.dataValues; });
                  console.log('Processing ' + fileData.length + ' records...');
                  for (let line of fileData) {
                    let linVal = JSON.parse(line);
                    let mapData = mappingsData.map(m => { return JSON.parse(m.MappingsObject);});
                    let m = mappings.mapIt(linVal,mapData);
                    m["IntegrationsID"] = intgid;
                    mappedData.push(m);
                  }
                  console.log('Successfully mapped ' + mappedData.length +' records...');
                  console.log('Inserting data...');
                  let trackerList;
                  for (let item of mappedData) {
                    repository.insertFlatData(item).then(
                      resolve => {
                        //console.log('Successfully added ' + JSON.stringify(item));
                        if (!trackerList && mappedData.indexOf(item)===mappedData.length - 1) {
                          res();
                        }
                      },
                      reject => {
                        trackerList = mappedData.indexOf(item);
                        var errorObject = {
                          ErrorNumber: item.PO_NUMBER,
                          ErrorName: 'Bad Record',
                          ErrorDescription: 'Data for this purchase order could not be parsed correctly. This usually indicates a data integrity issue. See error for more information.',
                          ErrorObject: JSON.stringify({ errorData: item, error: reject.toString()}),
                          DataIntegrationsID: intgid
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
                },
                reject => {

                  var errorObject = {
                    ErrorNumber: 500,
                    ErrorName: 'Get Purchases Map',
                    ErrorDescription: 'Application was not able to get the purchases mapping data from the database. More information is available in the ErrorObject.',
                    ErrorObject: reject.toString(),
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
          )
            },
        reject => {
          console.log(reject);
          rej();
        }
      ); 
});
}

/**Gets API token for TIPWEBAPI */
function getApiToken() {
  rq.getToken().then(
    resolve => {
      console.log(resolve);
      process.exit(0);
    },
    reject => {
      console.error(reject);
      process.exit(0);
    }
  );
}

/**Upserts vendors via TIPWEBAPI */
function upsertVendors(options) {
  console.log('in upsert vendors...')
  return new Promise(
    (res,rej) => {
          repository.getVendorsToUpsert({ client: options.client, limitVal: options.limitVal, offsetVal: options.offset }).then(
            resolve => {
              let dataToUpload = resolve.map( m => { return m.dataValues; });
              rq.upsertVendors(options.token,dataToUpload).then(
                resolve => {
                  let rejectedRecords = JSON.parse(resolve);
                  console.log('Successfully processed ' + dataToUpload.length + ' records.');
                  console.log(rejectedRecords.length + ' records were rejected.');
                  if (rejectedRecords.length > 0) {
                    for (let rec of rejectedRecords) {
                      recerr = {
                        ErrorNumber: rec.badVendor.vendorID,
                        ErrorName: 'Vendor Rejected',
                        ErrorDescription: 'All purchase orders must be sourced from an accepted vendor in TipWEB-IT. A vendor record was rejected while attempting to add to application. The error has more information.',
                        ErrorObject: JSON.stringify(rec),
                        DataIntegrationsID: options.intgid
                      }
                      repository.logError(recerr).then(
                        resolve => {
                          console.log('Success');
                          if (rejectedRecords.indexOf(rec) === rejectedRecords.length - 1) {
                            res();
                          }
                        },
                        reject => {
                          console.error(reject);
                          if (rejectedRecords.indexOf(rec) === rejectedRecords.length - 1) {
                            res();
                          }
                        }
                      );
                    }
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
    (res,rej) => {
          repository.getProductsToUpsert({ client: options.client, limitVal: options.limitVal, offset: options.offset }).then(
            resolve => {
              let dataToUpload = resolve.map( m => { return m.dataValues; });
              rq.upsertProducts(options.token,dataToUpload).then(
                resolve => {
                  let rejectedRecords = JSON.parse(resolve);
                  console.log('Successfully processed ' + dataToUpload.length + ' records.');
                  console.log(rejectedRecords.length + ' records were rejected.');
                  if (rejectedRecords.length > 0) {
                    for (let rec of rejectedRecords) {
                      let recerr = {
                        ErrorNumber: rec.badProduct.ProductNumber,
                        ErrorName: 'Product Rejected',
                        ErrorDescription: 'All purchase order items must be added to the TipWEB-IT item catalog. A product record was rejected while attempting to add to the catalog. The error has more information.',
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
                          console.error(reject);
                          if (rejectedRecords.indexOf(rec) === rejectedRecords.length - 1) {
                            res();
                          }
                        }
                      );
                    }
                  }
                  else {
                    res();
                  }
                },
                reject => {
                  let errorObject = {
                    ErrorNumber: 500,
                    ErrorName: 'Upsert Products',
                    ErrorDescription: 'Application was not able to upsert products to TIPWeb-IT API. More information is available in the ErrorObject.',
                    ErrorObject: JSON.stringify(reject),
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
              console.log(resolve);
              console.log('Successfully added records for vendors from TipWEB-IT API');
              repository.toggleVendorSyncSwitch().then(
                resolve => {
                  console.log(resolve);
                },
                reject =>{
                  console.log(reject);
                }
              );
            },
            reject => {
              var errorObject = {
                ErrorNumber: 500,
                ErrorName: 'Insert Vendors from API',
                ErrorDescription: 'Application was not able to insert Vendor information from TipWEB-IT web API. More information is available in the ErrorObject.',
                ErrorObject: JSON.stringify(reject),
              }
        
              repository.logError(errorObject).then(
                resolve => {
                  console.log();
                  return;
                },
                reject => {
                  console.log();
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
          }
    
          repository.logError(errorObject).then(
            resolve => {
              console.log();
              return;
            },
            reject => {
              console.log();
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
      }

      repository.logError(errorObject).then(
        resolve => {
          console.log();
          return;
        },
        reject => {
          console.log();
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
      console.log(resolve);
      let tokenVal = resolve.token;
      rq.getAllProducts(tokenVal).then(
        resolve => {
          let products = resolve;
          repository.insertNewProducts(products).then(
            resolve => {
              console.log('Successfully added records for products from TipWEB-IT API');
              repository.toggleProductsSyncSwitch().then(
                resolve => {
                  console.log(resolve);
                },
                reject =>{
                  console.log(reject);
                }
              );
            },
            reject => {
              var errorObject = {
                ErrorNumber: 500,
                ErrorName: 'Insert Products from API',
                ErrorDescription: 'Application was not able to insert Product information from TipWEB-IT web API. More information is available in the ErrorObject.',
                ErrorObject: JSON.stringify(reject),
              }
        
              repository.logError(errorObject).then(
                resolve => {
                  console.log();
                  return;
                },
                reject => {
                  console.log();
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
          }
    
          repository.logError(errorObject).then(
            resolve => {
              console.log();
              return;
            },
            reject => {
              console.log();
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
      }

      repository.logError(errorObject).then(
        resolve => {
          console.log();
          return;
        },
        reject => {
          console.log();
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
    (res,rej) => {
      let client = configuration.config.client;
      repository.getDataSendingToApiIntegrationID(client).then(
        resolve => {
          let intgid = resolve;
          repository.getTotalProductsToUpsertCount({ client: client }).then(
            resolve => {
              console.log(resolve);
              let total = resolve;
              rq.getToken().then(
                resolve => {
                  console.log(resolve);          
                  let lv = options.lv;
                  let i = options.iVal;
                  let tokenVal = resolve.token;
                  upsertProductsRecursive({ client: client, limitVal: lv, offset: i, token: tokenVal, total: total, intgid: intgid }, () => { res('Done'); });
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
        },
      reject => {
        rej(reject);
      });
    }
  );
}

/**
 * Use to upsert all vendor records available for submission
 */
function upsertAllVendors(options) {
  return new Promise(
    (res,rej) => {
      let client = configuration.config.client;
      repository.getDataSendingToApiIntegrationID(client).then(
        resolve => {
          let intgid = resolve;
          repository.getTotalVendorsToUpsertCount({ client: client }).then(
            resolve => {
              console.log(resolve);
              let total = resolve;
              rq.getToken().then(
                resolve => {
                  console.log(resolve);          
                  let lv = options.lv;
                  let i = options.iVal;
                  let tokenVal = resolve.token;
                  upsertVendorsRecursive({ client: client, limitVal: lv, offset: i, token: tokenVal, total: total, intgid: intgid }, () => { res('Done'); });
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
      },
      reject => {
        rej(reject);
      });
    }
  );
}

/**
 * Use to upsert all header records available for submission
 */
function upsertAllHeaders(options) {
  return new Promise(
    (res,rej) => {
      let client = configuration.config.client;
      repository.getDataSendingToApiIntegrationID(client).then(
        resolve => {
          let intgid = resolve;
          repository.getTotalHeadersToUpsertCount({ client: client }).then(
            resolve => {
              console.log(resolve);
              let total = resolve;
              rq.getToken().then(
                resolve => {
                  console.log(resolve);          
                  let lv = options.lv;
                  let i = options.iVal;
                  let tokenVal = resolve.token;
                  upsertHeadersRecursive({ client: client, limitVal: lv, offset: i, token: tokenVal, total: total, intgid: intgid }, () => { res('Done'); });
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

/**
 * Used to upsert all detail records available for submission
 */
function upsertAllDetails(options) {
  return new Promise(
    (res,rej) => {
      let client = configuration.config.client;
      repository.getDataSendingToApiIntegrationID(client).then(
        resolve => {
          let intgid = resolve;
      repository.getTotalDetailsToUpsertCount({ client: client }).then(
        resolve => {
          console.log(resolve);
          let total = resolve;
          rq.getToken().then(
            resolve => {
              console.log(resolve);          
              let lv = options.lv;
              let i = options.iVal;
              let tokenVal = resolve.token;
              upsertDetailsRecursive({ client: client, limitVal: lv, offset: i, token: tokenVal, total: total, intgid : intgid }, () => { res('Done'); });
            },
            reject => {
              console.error(reject);
              rej();
            }
          )        
        },
        reject => {
          console.error(reject);
          rej();
        }
      );

      
    },
    reject => {
      rej();
    });
});
}

function upsertAllShipments(options) {
  return new Promise(
    (res,rej) => {
      let client = configuration.config.client;
      repository.getDataSendingToApiIntegrationID(client).then(
        resolve => {
          let intgid = resolve;
          repository.getTotalShipmentsToUpsertCount({ client: client }).then(
            resolve => {
              console.log(resolve);
              let total = resolve;
              rq.getToken().then(
                resolve => {
                  console.log(resolve);          
                  let lv = options.lv;
                  let i = options.iVal;
                  let tokenVal = resolve.token;
                  upsertShipmentsRecursive({ client: client, limitVal: lv, offset: i, token: tokenVal, total: total, intgid: intgid }, () => { res('Done'); });
                },
                reject => {
                  console.error(reject);
                  rej();
                }
              )        
            },
            reject => {
              console.error(reject);
              rej();
            }
          );

        },
      reject => {
        rej();
      });
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
  console.log(options);
  upsertProducts(options).then(
    resolve => {
      i += options.limitVal;
      if (i < options.total) {
        console.log(options);
        upsertProductsRecursive({client: options.client, limitVal: options.limitVal, offset: i, token: options.token, total: options.total, intgid: options.intgid}, callback);
      }
      else {
        callback();
      }
    },
    reject => {
      i += options.offset;
      if (i < options.total) {
        console.log(options);
        upsertProductsRecursive({client: options.client, limitVal: options.limitVal, offset: i, token: options.token, total: options.total, intgid: options.intgid}, callback);
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
  console.log(options);
  upsertVendors(options).then(
    resolve => {
      i += options.limitVal;
      if (i < options.total) {
        console.log(options);
        upsertVendorsRecursive({client: options.client, limitVal: options.limitVal, offset: i, token: options.token, total: options.total, intgid: options.intgid}, callback);
      }
      else {
        callback();
      }
    },
    reject => {
      i += options.limitVal;
      if (i < options.total) {
        console.log(options);
        upsertVendorsRecursive({client: options.client, limitVal: options.limitVal, offset: i, token: options.token, total: options.total, intgid: options.intgid}, callback);
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
  console.log(options);
  upsertHeaderRecords(options).then(
    resolve => {
      i += options.limitVal;
      if (i < options.total) {
        upsertHeadersRecursive({client: options.client, limitVal: options.limitVal, offset: i, token: options.token, total: options.total, intgid: options.intgid}, callback);
      }
      else {
        callback();
      }
    },
    reject => {
      i += options.limitVal;
      console.log(reject);
      if (i < options.total) {
        upsertHeadersRecursive({client: options.client, limitVal: options.limitVal, offset: i, token: options.token, total: options.total, intgid: options.intgid}, callback);
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
  console.log(options);
  upsertDetailRecords(options).then(
    resolve => {
      i += options.limitVal;
      if (i < options.total) {
        console.log(options);
        upsertDetailsRecursive({client: options.client, limitVal: options.limitVal, offset: i, token: options.token, total: options.total, intgid: options.intgid }, callback);
      }
      else {
        callback();
      }
    },
    reject => {
      i += options.limitVal;
      console.log(reject);
      if (i < options.total) {
        console.log(options);
        upsertDetailsRecursive({client: options.client, limitVal: options.limitVal, offset: i, token: options.token, total: options.total, intgid: options.intgid }, callback);
      }
      else {
        callback();
      }
    }
  );

}

function upsertShipmentsRecursive(options, callback) {
  let i = options.offset;
  console.log(options);
  upsertShipmentRecords(options).then(
    resolve => {
      i += options.limitVal;
      if (i < options.total) {
        console.log(options);
        upsertShipmentsRecursive({client: options.client, limitVal: options.limitVal, offset: i, token: options.token, total: options.total, intgid: options.intgid}, callback);
      }
      else {
        callback();
      }
    },
    reject => {
      i += options.limitVal;
      console.log(reject);
      if (i < options.total) {
        console.log(options);
        upsertShipmentsRecursive({client: options.client, limitVal: options.limitVal, offset: i, token: options.token, total: options.total, intgid: options.intgid}, callback);
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

  console.log('In upsert headers')

  return new Promise(
    
      (res, rej) => {
        repository.getHeadersToUpsert({ client: options.client, limitVal: options.limitVal, offsetVal: options.offset }).then(
          resolve => {
            let dataToUpload = resolve.map( m => { return m.dataValues; });
            for (let value of dataToUpload) {
              value.DataIntegration = undefined;
            }
            
            rq.upsertHeaders(options.token, dataToUpload).then(
              resolve => {
                let rejectedRecords = resolve;
                console.log('Successfully processed ' + dataToUpload.length + ' records.');
                console.log(rejectedRecords.length + ' records were rejected.');
                if (rejectedRecords.length > 0) {
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
                        console.log('Success');
                        if (rejectedRecords.indexOf(rec) === rejectedRecords.length - 1) {
                          res();
                        }
                      },
                      reject => {
                        console.error(reject);
                        if (rejectedRecords.indexOf(rec) === rejectedRecords.length - 1) {
                          rej(reject);
                        }
                      }
                    );
                  }
                }
              },
              reject => {
                var errorObject = {
                  ErrorNumber: 500,
                  ErrorName: 'Get Headers to Process',
                  ErrorDescription: 'Application was not able to get header values to upsert via TipWEB-IT API. More information is available in the ErrorObject.',
                  ErrorObject: JSON.stringify(reject),
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
        )}
      );
}

function upsertDetailRecords(options) {
  return new Promise(
    (res, rej) => {
      repository.getDetailsToUpsert({ client: options.client, limitVal: options.limitVal, offsetVal: options.offset }).then(
        resolve => {
          let dataToUpload = resolve.map( m => { return m.dataValues; });
          rq.upsertDetails(options.token, dataToUpload).then(
            resolve => {
              console.log(resolve);
              let rejectedRecords = JSON.parse(resolve);
              console.log('Successfully processed ' + dataToUpload.length + ' records.');
              console.log(rejectedRecords.length + ' records were rejected.');
              if (rejectedRecords.length > 0) {
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
                      console.log('Success');
                      if (rejectedRecords.indexOf(rec) === rejectedRecords.length - 1) {
                        res();
                      }
                    },
                    reject => {
                      console.error(reject);
                      if (rejectedRecords.indexOf(rec) === rejectedRecords.length - 1) {
                        res();
                      }
                    }
                  );
                }
              }
              else {
                res();
              }
            },
            reject => {

              console.log(reject);

              var errorObject = {
                ErrorNumber: 500,
                ErrorName: 'Process Details',
                ErrorDescription: 'Application was not able to upsert data via TipWEB-IT API. More information is available in the ErrorObject.',
                ErrorObject: reject.toString(),
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
      repository.getShipmentsToUpsert({ client: options.client, limitVal: options.limitVal, offsetVal: options.offset }).then(
        resolve => {
          let dataToUpload = resolve.map( m => { return m.dataValues; });
          rq.upsertShipments(options.token, dataToUpload).then(
            resolve => {
              console.log(resolve);
              let rejectedRecords = JSON.parse(resolve);
              console.log('Successfully processed ' + dataToUpload.length + ' records.');
              console.log(rejectedRecords.length + ' records were rejected.');
              if (rejectedRecords.length > 0) {
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
                      console.log('Success');
                      if (rejectedRecords.indexOf(rec) === rejectedRecords.length - 1) {
                        res();
                      }
                    },
                    reject => {
                      console.error(reject);
                      if (rejectedRecords.indexOf(rec) === rejectedRecords.length - 1) {
                        res();
                      }
                    }
                  );
                }
              }
              else {
                res();
              }
            },
            reject => {

              console.log(reject);

              var errorObject = {
                ErrorNumber: 500,
                ErrorName: 'Process Details',
                ErrorDescription: 'Application was not able to upsert data via TipWEB-IT API. More information is available in the ErrorObject.',
                ErrorObject: reject.toString(),
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
            ErrorName: 'Get Details to Process',
            ErrorDescription: 'Application was not able to get shipment values to upsert via TipWEB-IT API. More information is available in the ErrorObject.',
            ErrorObject: JSON.stringify(reject),
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


function upsertProductRecords(options) {
  //get token
  rq.getToken().then(
    resolve => {
      let tokenVal = resolve.token;
          repository.getProductsToUpsert({ client: configuration.config.client, limit: options.limit, offset: options.offset }).then(
            resolve => {
              let dataToUpload = resolve.map( m => { return m.dataValues; });
              for (let d of dataToUpload) {
                d.ProductNumber = 'INT' + d.ProductNumber; 
              }
              rq.upsertProducts(tokenVal, dataToUpload).then(
                resolve => {
                  let rejectedRecords = JSON.parse(resolve);
                  console.log('Successfully processed ' + dataToUpload.length + ' records.');
                  console.log(rejectedRecords.length + ' records were rejected.');
                  if (rejectedRecords && rejectedRecords.length > 0) {
                    for (let rec of rejectedRecords) {
                      let recerr = {
                        ErrorNumber: rec.badProduct.productNumber,
                        ErrorName: 'Product Record Rejected',
                        ErrorDescription: 'Product record was rejected. See ErrorObject for Rejection Reason.',
                        ErrorObject: JSON.stringify(rec),
                      }
                      repository.logError(recerr).then(
                        resolve => {
                          console.log('Success!');
                          if (rejectedRecords.indexOf(rec) === rejectedRecords.length - 1) {
                            process.exit(0);
                          }
                        },
                        reject => {
                          console.error(reject);
                          if (rejectedRecords.indexOf(rec) === rejectedRecords.length - 1) {
                            process.exit(0);
                          }
                        }
                      );
                    }
                  }
                  else {
                    process.exit(0);
                  }
                },
                reject => {

                  console.log(reject);

                  var errorObject = {
                    ErrorNumber: 500,
                    ErrorName: 'Product Details',
                    ErrorDescription: 'Application was not able to upsert data via TipWEB-IT API. More information is available in the ErrorObject.',
                    ErrorObject: reject.toString(),
                  }
            
                  repository.logError(errorObject).then(
                    resolve => {
                      console.log();
                      return;
                    },
                    reject => {
                      console.log();
                      return;
                    }
                  );
                }
              )
            },
            reject => {
              var errorObject = {
                ErrorNumber: 500,
                ErrorName: 'Get Shipments to Process',
                ErrorDescription: 'Application was not able to get products to upsert via TipWEB-IT API. More information is available in the ErrorObject.',
                ErrorObject: JSON.stringify(reject),
              }
        
              repository.logError(errorObject).then(
                resolve => {
                  console.log();
                  return;
                },
                reject => {
                  console.log();
                  return;
                }
              );
            }
          );
    },
    reject => {
      var errorObject = {
        ErrorNumber: 500,
        ErrorName: 'Get API Token',
        ErrorDescription: 'Application was not able to get an API token to access TipWEB-IT web API. More information is available in the ErrorObject.',
        ErrorObject: JSON.stringify(reject),
      }

      repository.logError(errorObject).then(
        resolve => {
          console.log();
          return;
        },
        reject => {
          console.log();
          return;
        }
      );
    }
  );
}

function filterOldRecords(options) {
  return new Promise(
    (res, rej) => {
      repository.getProcessingIntegrationID().then(
        resolve => {
          let intgid = resolve;
          repository.runProcIntegrations_RemoveUnnecessaryRecords(intgid, { headers: configuration.dataConfig.procRemove.headers, details:configuration.dataConfig.procRemove.details, shipping: configuration.dataConfig.procRemove.shipping, inventory: configuration.dataConfig.procRemove.inventory, charges: configuration.dataConfig.procRemove.charges, payments: configuration.dataConfig.procRemove.payments }).then(
            resolve => {
              res();
            },
            reject => {
              var errorObject = {
                ErrorNumber: 500,
                ErrorName: 'Filter',
                ErrorDescription: 'Application was not able to filter the unnecessary records from the export to TIPWEBAPI. More information is available in the ErrorObject.',
                ErrorObject: reject.toString(),
                DataIntegrationsID: itgid
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
            ErrorName: 'Get Processing ID',
            ErrorDescription: 'Application was not able to get the currently processing IntegrationsID. More information is available in the ErrorObject.',
            ErrorObject: JSON.stringify(reject)
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

function filterShipmentsWithBadDetails(options) {
  return new Promise(
    (res,rej) => {
      repository.getDataSendingToApiIntegrationID(configuration.config.client).then(
        resolve => {
          let intgid = resolve;
          repository.runProcIntegrations_FlagShipmentsFromBadDetailRecords(intgid).then(
            resolve => {
              res();
            },
            reject => {
              var errorObject = {
                ErrorNumber: 500,
                ErrorName: 'Proc Remove Details',
                ErrorDescription: 'Application was not able to execute stored procedure to remove detail and shipment records from bad purchase order header records. More information is available in the ErrorObject.',
                ErrorObject: reject.toString(),
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
            ErrorObject: reject.toString()
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
    (res,rej) => {
      repository.getDataSendingToApiIntegrationID(configuration.config.client).then(
        resolve => {
          let intgid = resolve;
          repository.runProcIntegrations_FlagDetailsAndShipmentsFromBadHeaderRecords(intgid).then(
            resolve => {
              console.log("Removed detail and shipment records from manifest to send to API.")
              process.exit(0);
            },
            reject => {
              var errorObject = {
                ErrorNumber: 500,
                ErrorName: 'Proc Remove Details',
                ErrorDescription: 'Application was not able to execute stored procedure to remove detail and shipment records from bad purchase order header records. More information is available in the ErrorObject.',
                ErrorObject: reject.toString(),
                DataIntegrationsID: intgid
              }
        
              repository.logError(errorObject).then(
                resolve => {
                  console.log("Success... closing...");
                  process.exit(0);
                  return;
                },
                reject => {
                  console.log('Error logging error.');
                  process.exit(0);
                  return;
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
            ErrorObject: reject.toString()
          }
    
          repository.logError(errorObject).then(
            resolve => {
              console.log("Success... closing...");
              process.exit(0);
              return;
            },
            reject => {
              console.log('Error logging error.');
              process.exit(0);
              return;
            }
          );
        }
      );
  });
}

function filterOldInserts(options) {
  return new Promise(
    (res, rej) => {
      repository.getProcessingIntegrationID().then(
        resolve => {
          let intgid = resolve;
          repository.runProcIntegrations_RemoveExistingInserts(intgid, { headers: configuration.dataConfig.procRemove.headers, details:configuration.dataConfig.procRemove.details, shipping: configuration.dataConfig.procRemove.shipping, inventory: configuration.dataConfig.procRemove.inventory, charges: configuration.dataConfig.procRemove.charges, payments: configuration.dataConfig.procRemove.payments }).then(
            resolve => {
              res();
            },
            reject => {
              var errorObject = {
                ErrorNumber: 500,
                ErrorName: 'Filter',
                ErrorDescription: 'Application was not able to filter the unnecessary records from the export to TIPWEBAPI. More information is available in the ErrorObject.',
                ErrorObject: reject.toString(),
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
            ErrorName: 'Get Processing ID',
            ErrorDescription: 'Application was not able to get the currently processing IntegrationsID. More information is available in the ErrorObject.',
            ErrorObject: JSON.stringify(reject)
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

function toggleFromProcessingToSending(options) {
  return new Promise(
    (res, rej) => {
      repository.beginSendingToTipwebAPI(configuration.config.client).then(
        resolve => {
          console.log('No longer processing. Sending to TipWEB-IT now.');
          res();
        },
        reject => {

          console.log(reject);

          var errorObject = {
            ErrorNumber: 500,
            ErrorName: 'Start Sending',
            ErrorDescription: 'Application was not able to convert process to sending. More information is available in the ErrorObject.',
            ErrorObject: reject.toString()
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

function toggleSuccessfulIntegration() {
  return new Promise( 
    (res,rej) => {
      repository.getDataSendingToApiIntegrationID(configuration.config.client).then(
        resolve => {
          let intgid = resolve;
          repository.completeIntegrationProcessing(intgid).then(
            resolve => {
              res();
            },
            reject => {
              rej();
            }
          );
        },
        reject => {
          var errorObject = {
            ErrorNumber: 500,
            ErrorName: 'Get Integration ID',
            ErrorDescription: 'Application was not able to get the Integration ID that is currently sending data to TIPWEB-IT. More information is available in the ErrorObject.',
            ErrorObject: JSON.stringify(reject),
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

function getLinkTableData(options) {
  return new Promise(
    (res, rej) => {
      //get list of link tables
      //run query for data
      //write to json file
      let linkTypes = configuration.config.links

      for (let l of linkTypes) {
        repository.getLinkTableData({ client: configuration.config.client, type: l.type }).then(
          resolve => {
            let linkVals = resolve.map(m => { return m.dataValues; });
            let fullFileName = configuration.config.linksFolder + l.filename;
            fs.writeFile(fullFileName,JSON.stringify(linkVals),
              (err) => {
                if (err) {
                  rej(err);
                }
                else if (linkTypes.indexOf(l)===linkTypes.length - 1) {
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
      repository.getProcessingIntegrationID().then(
        resolve => {
          let intgid = resolve;
          let custTasks = configuration.config.customTasks;
          let options = { client: configuration.config.client, intgid: intgid };
          for (let task of custTasks) {
            let funk = repository[task.fn];
            console.log(funk);
            funk(options).then(
              resolve => {
                if (custTasks.indexOf(task) === custTasks.length - 1) {
                  res();
                }
              },
              reject => {
                rej(reject);
              }
            );
          } 
        },
        reject => {
            rej(reject);
        });
      }
      );
}