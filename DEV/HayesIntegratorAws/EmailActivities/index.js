'use strict'

var Promise = require('bluebird');
var fs = require('fs');
var config = require('./config.json');

var httprequest = require('./lib/http-requests/http-request');
var filetasks = require('./lib/file-tasks/file-tasks');

sendIntegrationEmail({ messageFile: './tests/support-message.json', messageHTML: './tests/message.html', date: '1-11-2018', client: 'CPS', outFile: './tests/fixed-message.json' }).then(
    res => {
        console.log(res);
    },
    rej => {
        console.error(rej);
    }
);
/**
 * Step 1: Get and parse data from web api.
 * Step 2: Get HTML file
 * Step 3: Format HTML file
 * Step 4: Import 
 */
 function sendIntegrationEmail(options) {
    let messageJSON = require(options.messageFile);
    let messageHtmlFile = options.messageHTML;
    let messageHTML;

    return new Promise(
        (resolve,reject) => {
            httprequest.getDataIntegrationsByDateAndCustomer({
                apiUrl: config.config.apiUrl + config.config.apiRoutes.getIntegrationsByDateAndCustomer,
                cert: config.config.secrets.cert,
                date: options.date,
                client: options.client    
            }).then(
                res => {
                    let intgdata = res;
                    let datastring = '';

                    for (let item of intgdata) {
                        if (item.dataIntegrationsRun && item.dataIntegrationsRun.length > 0) {
                            for (let val of item.dataIntegrationsRun) {
                                let bool = item.dataIntegrationsRun.DataProcessedSuccessfully ? 'true' : 'false';
                                let dateVal = new Date(val.DateAdded);
                                datastring += '<tr><td>'+ item.Client +'</td><td>' + dateVal.getFullYear() + '-' + (dateVal.getMonth() + 1) + '-' + dateVal.getDate() + '</td><td>' + bool + '</td></tr>';
                            }
                        }
                    }

                    filetasks.readHTMLFile({ filepath: messageHtmlFile }).then(
                        res => {
                            let htmlString = res;
                            htmlString = htmlString.replace('{0}',options.client).replace('{1}',datastring);
                            messageJSON.Body.Html.Data = htmlString;
                            //resolve(messageJSON);
                            fs.writeFile(options.outFile, JSON.stringify(messageJSON), (err) => { resolve(err); });
                        },
                        rej => {
                            console.error(rej);
                            reject();
                        }
                    )
                },
                rej => {
                    console.error(rej);
                    reject();
                }
            )
        }
    );
 }