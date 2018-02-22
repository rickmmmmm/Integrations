'use strict'
var Promise = require('bluebird');
var request = require('request');

module.exports = {
    getDataIntegrationsByDateAndCustomer(options) {
        let apiUrl = options.apiUrl;
        let date = options.date;
        let client = options.client;

        let requestUrl = apiUrl + '?date=' + encodeURI(date) + '&client=' + encodeURI(client);
        
        let header = {
            "Content-Type":"application/json",
            "hayes-auth-cert": options.cert,
            "hayes-auth-client": client
        }

        return new Promise(
            (resolve, reject) => {
                request.get(requestUrl, { headers: header },
                    (err, resp, body) => {
                        if (err) {
                            reject(err);
                        }
                        else {
                            resolve(JSON.parse(body));
                        }
                    }
                );
            }
        );
    },
}