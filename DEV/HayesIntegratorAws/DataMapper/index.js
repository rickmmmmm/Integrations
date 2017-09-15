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

execute(action, subaction);

function execute(action, subaction) {

  if (action === '--create') {
    console.log(
      chalk.yellow(
        figlet.textSync('Hayes', { horizontalLayout : 'full'})
      )
    );
    
    console.log("Welcome to Hayes Software Systems Data Integration Solution");
    createIntegration(configuration);
  }

  if (action === '--filter-unncessary') {
    filterOldRecords();
  }

  if (action === '--filter-old-inserts') {
    filterOldInserts();
  }

  if (action === '--complete') {
    toggleSuccessfulIntegration();
  }

  if (action === '--send-to-api') {
    toggleFromProcessingToSending();
  }

  if (action === '--stage-vendors-api') {
    getVendorsFromTipweb();
  }

  if (action === '--push-vendors') {
    upsertVendors();
  }

  if (action === '--push-products') {
    upsertProductRecords();
  }

  if (action === '--push-headers') {
    upsertHeaderRecords();
  }

  if (action === '--push-details') {
    upsertDetailRecords();
  }

  if (action === '--push-shipments') {
    upsertShipmentRecords();
  }

  if (action === '--shipping') {
    repository.getProcessingIntegrationID().then(
      resolve => {

        if (!resolve) {
          return;
        }

        let intgid = resolve;
        stageShippingRecords(configuration,{ integrationID: intgid });
      },
      reject => {
        console.error(reject);
      }
    );
    
  }
  if (action === '--details') {
    repository.getProcessingIntegrationID().then(
      resolve => {
        let intgid = resolve;
        stagePurchaseOrderDetails(configuration,{ integrationID: intgid });
      },
      reject => {
        console.error(reject);
      }
    );
  }
  if (action === '--headers') {
    repository.getProcessingIntegrationID().then(
      resolve => {
        let intgid = resolve;
        stagePurchaseOrderHeaders(configuration,{ integrationID: intgid });
      },
      reject => {
        console.error(reject);
      }
    );
  }
  if (action === '--products') {
    repository.getProcessingIntegrationID().then(
      resolve => {
        let intgid = resolve;
        stageProducts(configuration,{ integrationID: intgid });
      },
      reject => {
        console.error(reject);
      }
    );
  }
  if (action === '--toggle-products') {
    repository.toggleProductsSyncSwitch().then(
      resolve => {
        console.log(resolve);
        process.exit(0);
      },
      reject => {
        console.log(reject);
        process.exit(0);
      }
    )
  }
  if (action === '--toggle-vendors') {
    repository.toggleVendorSyncSwitch().then(
      resolve => {
        console.log(resolve);
        process.exit(0);
      },
      reject => {
        console.log(reject);
        process.exit(0);
      }
    )
  }
  if (action === '--vendors') {
    stageNewVendors(configuration,{ useIDs: true });
  }
  if (action === '--funding') {
    repository.getProcessingIntegrationID().then(
      resolve => {
        let intgid = resolve;
        stageFundingSources(configuration,{ integrationID: intgid });
      },
      reject => {
        console.error(reject);
      }
    );
  }
  if (action === '--mapflat') {
    if (!subaction) {
      console.log('A valid file to process must be provided.')
      process.exit(0);
    }

    let opts = { fileName: subaction };
    mapFlatDataToDatabase(configuration, opts);
  }
}

//stageNewVendors(configuration, { useIDs: true });
//stageProducts(configuration, { integrationID: 1 });
//stageFundingSources(configuration, { integrationID: 1 });
//stagePurchaseOrderDetails(configuration,  { integrationID: 1 } );
//stageShippingRecords(configuration,{ integrationID: 1 });

function stageShippingRecords(configuration, options) {
  var integid = options.integrationID;
  var mappedData = [];
  //read in details to add
  //read in mapping data
  //map details to details table
  //insert details

  repository.getFlatShipments(integid).then(
    resolve => {
      console.log('Retrieved ' + resolve.length + ' shipment records to process.');
      let shipmentsData = resolve.map(m => { return m.dataValues; });
      repository.getMappings('shipping').then(
        resolve => {
          let stage = resolve.map(m => { return m.dataValues; });
          let mappingValues = stage.map(m => { return JSON.parse(m.MappingsObject); });

          for (let line of shipmentsData) {
            m = mappings.mapIt(line, mappingValues);
            m["DataIntegrationsID"] = integid;
            mappedData.push(m);
          }

          repository.insertShipments(mappedData).then(
            resolve => {
              console.log('Successfully inserted ' + mappedData.length + ' into Shipments table.');
              process.exit(0);
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
                  ErrorName: 'Get Shipments',
                  ErrorDescription: 'Getting Shipments records failed. More information is available in the ErrorObject.',
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

function stagePurchaseOrderDetails(configuration, options) {
  
  var integid = options.integrationID;
  //var mappedData = [];
  //read in details to add
  //read in mapping data
  //map details to details table
  //insert details

  repository.getDetailRecordsFlatData(integid).then(
    resolve => {
      console.log('Retrieved ' + resolve.length + ' detail records to process.');
      let detailData = resolve.map(m => { return m.dataValues; });
      repository.getMappings('po details').then(
        resolve => {
          let stage = resolve.map(m => { return m.dataValues; });
          let mappingValues = stage.map(m => { return JSON.parse(m.MappingsObject); });
          let mappedData = [];

          for (let line of detailData) {
            console.log(line);
            m = mappings.mapIt(line, mappingValues);
            m["DataIntegrationsID"] = integid;
            console.log(m);
            mappedData.push(m);
          }

          repository.insertDetailRecords(mappedData).then(
            resolve => {
              console.log('Successfully inserted ' + mappedData.length + ' into PurchaseOrderDetails table.');
              process.exit(0);
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
                  ErrorName: 'Get Details',
                  ErrorDescription: 'Getting Details records failed. More information is available in the ErrorObject.',
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
  );
}

function stagePurchaseOrderHeaders(configuration, options) {
  var integid = options.integrationID;
  var mappedData = [];
  //read in headers to add
  //read in mapping data
  //map headers to headers table
  //insert headers

  repository.getHeaderRecordsFlatData(integid).then(
    resolve => {
      console.log('Retrieved ' + resolve.length + ' header records to process.');
      let headerData = resolve.map(m => { return m.dataValues; });
      repository.getMappings('po headers').then(
        resolve => {
          let stage = resolve.map(m => { return m.dataValues; });
          let mappingValues = stage.map(m => { return JSON.parse(m.MappingsObject); });
          console.log(mappingValues);
          for (let line of headerData) {
            m = mappings.mapIt(line, mappingValues);
            m["DataIntegrationsID"] = integid;
            mappedData.push(m);
          }

          repository.insertHeaderRecords(mappedData).then(
            resolve => {
              console.log('Successfully inserted ' + mappedData.length + ' into PurchaseOrderHeaders table.');
              process.exit(0);
            },
            reject => {
              let errorObject = {
                  ErrorNumber: 500,
                  ErrorName: 'Insert Headers',
                  ErrorDescription: 'Inserting Header records failed. More information is available in the ErrorObject.',
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
                  ErrorName: 'Get Headers',
                  ErrorDescription: 'Getting Header records failed. More information is available in the ErrorObject.',
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
  );
}

function stageProducts(configuration, options) {
  var integid = options.integrationID;

  //get list of current products
  //get list of new products
  //insert new products
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

function stageFundingSources(configuration, options) {
  var integid = options.integrationID;

  //get list of current funding sources
  //get list of new funding sources
  //insert new funding sources
  repository.getCurrentFundingSources().then(
    resolve => {
      let currentFundingSources = resolve === [] ? resolve : resolve.map(m => { return m.dataValues} );
      repository.getNewFundingSources(currentFundingSources, integid).then(
        resolve => {
          let sourcesFlat = resolve === [] ? resolve : resolve.map(m => { return m.dataValues; });
          let sourcesToAdd = [];

          if (sourcesFlat && sourcesFlat.length === 0) {
            console.log('No funding sources to add!');
            return;
          }

          for (let sf of sourcesFlat) {
            x = { FundingSourceID: sf.FUNDING_SOURCE };
            sourcesToAdd.push(x);
          }

          repository.insertNewFundingSources(sourcesToAdd).then(
            resolve => {
              console.log('Successfully added ' + sourcesToAdd.length + ' new funding sources!');
              process.exit(0);
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
                  ErrorName: 'Get New Funding Sources',
                  ErrorDescription: 'Getting list of new funding sources failed. More information is available in the ErrorObject.',
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

function stageNewVendors(configuration, options) {
  //get processing integration id
  //get list of current vendors
  //get new vendors
  //insert new vendors
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
                process.exit(0);
              }

              let vendorsToAdd = [];
              for (let v of flatVendors) {
                let x = { VendorID: v.VENDOR_ID, VendorName: v.VENDOR_NAME };
                vendorsToAdd.push(x);
              }
              console.log('Adding ' + vendorsToAdd.length + ' new vendors to staging table.')

              repository.insertVendors(vendorsToAdd).then(
                resolve => {
                  console.log(resolve);
                  process.exit(0);
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
                  console.log();
                  process.exit(0);
                  return;
                },
                reject => {
                  console.log();
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
                  ErrorName: 'Get New Vendors',
                  ErrorDescription: 'Select on new vendors failed. More information is available in the ErrorObject.',
                  ErrorObject: reject.toString(),
                  DataIntegrationsID: integid
                }

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
                  ErrorName: 'Get Current Vendors',
                  ErrorDescription: 'Select on current vendors failed. More information is available in the ErrorObject.',
                  ErrorObject: reject.toString(),
                  DataIntegrationsID: integid
                }

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

function createIntegration(configuration, options) {
  repository.insertIntegration({ client: configuration.config.client, description: configuration.config.typeDesc }).then(
    resolve => {
      console.log(resolve);
      process.exit(0);
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

function mapFlatDataToDatabase(configuration, options) {
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
           
          repository.getMappings(configuration.config.mapType).then(
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
                    console.log('Successfully added ' + JSON.stringify(item));
                    if (!trackerList && mappedData.indexOf(item)===mappedData.length - 1) {
                      process.exit(0);
                    }
                  },
                  reject => {
                    console.log('Failed');
                    trackerList = mappedData.indexOf(item);
                    var errorObject = {
                      ErrorNumber: item.PO_NUMBER,
                      ErrorName: 'Record Insert',
                      ErrorDescription: 'Data for record could not be inserted into database. More information is available in the ErrorObject.',
                      ErrorObject: JSON.stringify({ errorData: item, error: reject.toString()}),
                      DataIntegrationsID: intgid
                    }
                    repository.logError(errorObject).then(
                      resolve => {
                        console.log(resolve);
                      },
                      reject => {
                        console.error(reject);
                      }
                    ).then(
                      resolve => {
                        if (mappedData.indexOf(item) === trackerList) {
                          process.exit(0);
                        }
                      },
                      reject => {
                        process.exit(0);
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
                  console.log(resolve);
                },
                reject => {
                  console.log(error);
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
                  console.log(resolve);
                },
                reject => {
                  console.error(reject);
                }
              );
        }
      )
        },
    reject => {
      console.log(reject);
      logger.logErrorToFile({
        error: reject.toString(),
        date: Date(),
        desc: 'Unable to create connection to database to create DataIntegrations.IntegrationsID. Error info in error object.'
      });
    }
  );
}


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

function upsertVendors() {

  rq.getToken().then(
    resolve => {
      let tokenVal = resolve.token
      repository.getVendorsToUpsert().then(
        resolve => {
          let dataToUpload = resolve.map( m => { return m.dataValues; });
          rq.upsertVendors(tokenVal,dataToUpload).then(
            resolve => {
              let rejectedRecords = JSON.parse(resolve);
              console.log('Successfully processed ' + dataToUpload.length + ' records.');
              console.log(rejectedRecords.length + ' records were rejected.');
              if (rejectedRecords.length > 0) {
                for (let rec of rejectedRecords) {
                  recerr = {
                    ErrorNumber: 500,
                    ErrorName: 'Vendor Rejected',
                    ErrorDescription: 'Vendor record was rejected. See ErrorObject for Rejection Reason.',
                    ErrorObject: JSON.stringify(rec)
                  }
                  repository.logError(recerr).then(
                    resolve => {
                      console.log('Success');
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
              var errorObject = {
                ErrorNumber: 500,
                ErrorName: 'Upsert Vendors',
                ErrorDescription: 'Application was not able to upsert vendors to TIPWeb-IT API. More information is available in the ErrorObject.',
                ErrorObject: JSON.stringify(reject),
              }
        
              repository.logError(errorObject).then(
                resolve => {
                  process.exit(0);
                  return;
                },
                reject => {
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
            ErrorName: 'Get Vendors to Upsert',
            ErrorDescription: 'Application was not able to get vendors to upsert. More information is available in the ErrorObject.',
            ErrorObject: JSON.stringify(reject),
          }
    
          repository.logError(errorObject).then(
            resolve => {
              process.exit(0);
              return;
            },
            reject => {
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
        ErrorName: 'Get Token',
        ErrorDescription: 'Application was not able to get an access token from TipWEB-IT web API. More information is available in the ErrorObject.',
        ErrorObject: JSON.stringify(reject),
      }

      repository.logError(errorObject).then(
        resolve => {
          process.exit(0);
          return;
        },
        reject => {
          process.exit(0);
          return;
        }
      );
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

//upsert headers
  //put errors in error table

function upsertHeaderRecords() {
  //get token
  rq.getToken().then(
    resolve => {
      let tokenVal = resolve.token;
      repository.getDataSendingToApiIntegrationID().then(
        resolve => {
          let intgid = resolve;
          repository.getHeadersToUpsert(intgid).then(
            resolve => {
              let dataToUpload = resolve.map( m => { return m.dataValues; });

              rq.upsertHeaders(tokenVal, dataToUpload).then(
                resolve => {
                  let rejectedRecords = resolve;
                  console.log('Successfully processed ' + dataToUpload.length + ' records.');
                  console.log(rejectedRecords.length + ' records were rejected.');
                  if (rejectedRecords.length > 0) {
                    for (let rec of rejectedRecords) {
                      recerr = {
                        ErrorNumber: rec.badPurchaseOrderHeader.orderNumber,
                        ErrorName: 'Header Rejected',
                        ErrorDescription: 'Header record was rejected. See ErrorObject for Rejection Reason.',
                        ErrorObject: JSON.stringify(rec),
                        DataIntegrationsID: intgid
                      }
                      repository.logError(recerr).then(
                        resolve => {
                          console.log('Success');
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
                    ErrorName: 'Process Headers',
                    ErrorDescription: 'Application was not able to upsert data via TipWEB-IT API. More information is available in the ErrorObject.',
                    ErrorObject: JSON.stringify(reject),
                    DataIntegrationsID: intgid
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
                ErrorName: 'Get Headers to Process',
                ErrorDescription: 'Application was not able to get header values to upsert via TipWEB-IT API. More information is available in the ErrorObject.',
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
            ErrorName: 'Get Processing IntegrationID',
            ErrorDescription: 'Application was not able to get the currently processing integration or none exists. More information is available in the ErrorObject.',
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
  //get records to upsert
  //upsert records via http
}

//upsert details
  //put errors in error table

  function upsertDetailRecords() {
    //get token
    rq.getToken().then(
      resolve => {
        let tokenVal = resolve.token;
        repository.getDataSendingToApiIntegrationID().then(
          resolve => {
            let intgid = resolve;
            repository.getDetailsToUpsert(intgid).then(
              resolve => {
                let dataToUpload = resolve.map( m => { return m.dataValues; });
                rq.upsertDetails(tokenVal, dataToUpload).then(
                  resolve => {
                    console.log(resolve);
                    let rejectedRecords = JSON.parse(resolve);
                    console.log('Successfully processed ' + dataToUpload.length + ' records.');
                    console.log(rejectedRecords.length + ' records were rejected.');
                    if (rejectedRecords.length > 0) {
                      for (let rec of rejectedRecords) {
                        recerr = {
                          ErrorNumber: rec.badPurchaseOrderDetail.orderNumber,
                          ErrorName: 'Detail Record Rejected',
                          ErrorDescription: 'Detail record was rejected. See ErrorObject for Rejection Reason.',
                          ErrorObject: JSON.stringify(rec),
                          DataIntegrationsID: intgid
                        }
                        repository.logError(recerr).then(
                          resolve => {
                            console.log('Success');
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
                      ErrorName: 'Process Details',
                      ErrorDescription: 'Application was not able to upsert data via TipWEB-IT API. More information is available in the ErrorObject.',
                      ErrorObject: reject.toString(),
                      DataIntegrationsID: intgid
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
                  ErrorName: 'Get Details to Process',
                  ErrorDescription: 'Application was not able to get detail values to upsert via TipWEB-IT API. More information is available in the ErrorObject.',
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
              ErrorName: 'Get Processing IntegrationID',
              ErrorDescription: 'Application was not able to get the currently processing integration or none exists. More information is available in the ErrorObject.',
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
    //get records to upsert
    //upsert records via http
  }
//upsert shipments
  //put errors in error table
  function upsertShipmentRecords() {
    //get token
    rq.getToken().then(
      resolve => {
        let tokenVal = resolve.token;
        repository.getDataSendingToApiIntegrationID().then(
          resolve => {
            let intgid = resolve;
            repository.getShipmentsToUpsert(intgid).then(
              resolve => {
                let dataToUpload = resolve.map( m => { return m.dataValues; });
                rq.upsertShipments(tokenVal, dataToUpload).then(
                  resolve => {
                    let rejectedRecords = JSON.parse(resolve);
                    console.log('Successfully processed ' + dataToUpload.length + ' records.');
                    console.log(rejectedRecords.length + ' records were rejected.');
                    if (rejectedRecords.length > 0) {
                      for (let rec of rejectedRecords) {
                        recerr = {
                          ErrorNumber: rec.badShipment.orderNumber,
                          ErrorName: 'Shipment Record Rejected',
                          ErrorDescription: 'Shipment record was rejected. See ErrorObject for Rejection Reason.',
                          ErrorObject: JSON.stringify(rec),
                          DataIntegrationsID: intgid
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
                    console.log(reject)

                    var errorObject = {
                      ErrorNumber: 500,
                      ErrorName: 'Shipments Details',
                      ErrorDescription: 'Application was not able to upsert data via TipWEB-IT API. More information is available in the ErrorObject.',
                      ErrorObject: JSON.stringify(reject),
                      DataIntegrationsID: intgid
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
                  ErrorDescription: 'Application was not able to get shipemnts to upsert via TipWEB-IT API. More information is available in the ErrorObject.',
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
              ErrorName: 'Get Processing IntegrationID',
              ErrorDescription: 'Application was not able to get the currently processing integration or none exists. More information is available in the ErrorObject.',
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

//upsert shipments
  //put errors in error table
  function upsertProductRecords() {
    //get token
    rq.getToken().then(
      resolve => {
        let tokenVal = resolve.token;
            repository.getProductsToUpsert().then(
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
                        recerr = {
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

    repository.getProcessingIntegrationID().then(
      resolve => {
        let intgid = resolve;
        repository.runProcIntegrations_RemoveUnnecessaryRecords(intgid, { headers: configuration.dataConfig.procRemove.headers, details:configuration.dataConfig.procRemove.details, shipping: configuration.dataConfig.procRemove.shipping, inventory: configuration.dataConfig.procRemove.inventory, charges: configuration.dataConfig.procRemove.charges, payments: configuration.dataConfig.procRemove.payments }).then(
          resolve => {
            console.log(resolve);
            process.exit(0);
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
          ErrorName: 'Get Processing ID',
          ErrorDescription: 'Application was not able to get the currently processing IntegrationsID. More information is available in the ErrorObject.',
          ErrorObject: JSON.stringify(reject)
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

  function filterOldInserts(options) {
    
        repository.getProcessingIntegrationID().then(
          resolve => {
            let intgid = resolve;
            repository.runProcIntegrations_RemoveExistingInserts(intgid, { headers: configuration.dataConfig.procRemove.headers, details:configuration.dataConfig.procRemove.details, shipping: configuration.dataConfig.procRemove.shipping, inventory: configuration.dataConfig.procRemove.inventory, charges: configuration.dataConfig.procRemove.charges, payments: configuration.dataConfig.procRemove.payments }).then(
              resolve => {
                console.log(resolve);
                process.exit(0);
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
              ErrorName: 'Get Processing ID',
              ErrorDescription: 'Application was not able to get the currently processing IntegrationsID. More information is available in the ErrorObject.',
              ErrorObject: JSON.stringify(reject)
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

  function toggleFromProcessingToSending(configuration, options) {
    repository.beginSendingToTipwebAPI().then(
      resolve => {
        console.log('No longer processing. Sending to TipWEB-IT now.');
        process.exit(0);
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
  }

  function toggleSuccessfulIntegration() {
    repository.getDataSendingToApiIntegrationID().then(
      resolve => {
        let intgid = resolve;
        repository.completeIntegrationProcessing(intgid).then(
          resolve => {
            console.log('Successfully completed integration process. Done!');
            process.exit(0);
          },
          reject => {
            console.error("Uh oh. Something went wrong!");
            process.exit(0);
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
  }