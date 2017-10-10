const request = require('request');
var configuration = require('./configuration.js');
const repository = require('./repository.js');
var Promise = require('bluebird');
var fs = require('fs');

module.exports = {

    getToken: function() {
        return new Promise(
            (resolve, reject) => {

                let loginUrl = configuration.config.apiUrl + configuration.apiConfig.login;
                let body = { Key: configuration.secrets.secretkey, Phrase: configuration.secrets.passphrase }
                request.post(loginUrl, {headers: {'Content-Type':'application/json'}, body: JSON.stringify(body)},

                (err, resp, body) => {
            
                    if (err && resp.statusCode !== 200) {
                        reject(err);
                    }
                    if (body) {
                        let respBody = JSON.parse(body);
                        resolve(respBody);
                    }
                }
            );
                    
            }
        );
    },

    getAllVendors: function(token) {

        return new Promise(
            (resolve, reject) => {
                request.get(configuration.config.apiUrl + configuration.apiConfig.getVendors, { headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token }},
                (err, resp, body) => {

                    if (err || resp.statusCode !== 200) {
                        let errorObj = { statusCode: resp.statusCode, statusMessage: err }
                        
                        reject(err);
                    }
                    if (body) {
                        let mappedBody = [];
                        for (let b of JSON.parse(body)) {
                            let outVal = {
                                VendorID : b.vendorID,
                                VendorName : b.vendorName,
                                Address1 : b.address1,
                                Address2 : b.address2,
                                City : b.city,
                                State : b.state,
                                ZipCode : b.zipCode,
                                Phone : b.phone,
                                Email : b.email
                            }

                            console.log(outVal);

                            mappedBody.push(outVal);
                        }

                        resolve(mappedBody);
                    }
                    else {
                        let errorObj = { statusCode: resp.statusCode, statusMessage: resp.statusMessage }
                        reject(errorObj);
                    }
                }
            );
            }
        );
    },

    getAllProducts: function(token) {
        return new Promise(
            (resolve, reject) => {
                request.get(configuration.config.apiUrl + configuration.apiConfig.getProducts, { headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token }},
                (err, resp, body) => {

                    console.log(resp.statusCode);
                    console.log(resp.statusMessage);

                    if (err || resp.statusCode !== 200) {
                        let errorObj = { statusCode: resp.statusCode, statusMessage: err }
                        reject(err);
                    }
                    if (body) {

                        let mappedBody = []

                        for (let b of body) {
                            let outVal = {
                                ProuctNumber : b.productNumber,
                                ProductName : b.productName,
                                ProductDescription : b.productDescription,
                                ProductType : b.productType,
                                Model : b.model,
                                Manufacturer : b.manufacturer,
                                SuggestedPrice : b.suggestedPrice,
                                SKU : b.sku
                            };

                            mappedBody.push(outVal);
                        }

                        resolve(mappedBody);
                    }
                    else {
                        let errorObj = { statusCode: resp.statusCode, statusMessage: resp.statusMessage }
                        reject(errorObj);
                    }
                }
            );
            }
        );
    },

    getAllFundingSources: function(token) {
        return new Promise(
            (resolve, reject) => {
                request.get(configuration.config.apiUrl + configuration.apiConfig.getFundingSources, { headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token }},
                (err, resp, body) => {
                    if (err || resp.statusCode !== 200) {
                        let errorObj = { statusCode: resp.statusCode, statusMessage: err }
                        reject(err);
                    }
                    if (body) {
                        resolve(JSON.parse(body));
                    }
                    else {
                        let errorObj = { statusCode: resp.statusCode, statusMessage: resp.statusMessage }
                        reject(errorObj);
                    }
                }
            );
            }
        );
    },
    
    upsertHeaders: function(token, body) {
        return new Promise(
            (resolve, reject) => {
                request.post(configuration.config.apiUrl + configuration.apiConfig.addHeader, { body: JSON.stringify(body), headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token }},
                (err, resp, body) => {
                    if (err || resp.statusCode !== 200) {
                        let errorObj = { statusCode: resp.statusCode, statusMessage: err, response: resp }
                        reject(errorObj);
                    }
                    if (body) {
                        resolve(JSON.parse(body));
                    }
                    else {
                        let errorObj = { statusCode: resp.statusCode, statusMessage: resp.statusMessage }
                        reject(errorObj);
                    }
                }
            );
            }
        );
    },

    upsertDetails: function(token, body) {
        return new Promise(
            (resolve, reject) => {
                request.post(configuration.config.apiUrl + configuration.apiConfig.addDetail, 
                    { body: JSON.stringify(body), 
                        headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token }},
                (err, resp, body) => {
                    if (err) {
                        let errorObj = { statusCode: resp.statusCode, statusMessage: err }
                        reject(errorObj);
                    }
                    if (body) {
                        resolve(body);
                    }
                    else {
                        let errorObj = { statusCode: resp.statusCode, statusMessage: resp.statusMessage }
                        reject(errorObj);
                    }
                }
            );
            }
        );
    },

    upsertShipments: function(token, body) {
        return new Promise(
            (resolve, reject) => {
                request.post(configuration.config.apiUrl + configuration.apiConfig.addShipment, { body: JSON.stringify(body), headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token }},
                (err, resp, body) => {
                    if (err || resp.statusCode !== 200) {
                        let errorObj = { statusCode: resp.statusCode, statusMessage: err }
                        reject(err);
                    }
                    if (body) {
                        resolve(body);
                    }
                    else {
                        let errorObj = { statusCode: resp.statusCode, statusMessage: resp.statusMessage }
                        reject(errorObj);
                    }
                }
            );
            }
        );
    },

    upsertProducts: function(token, body) {

        return new Promise(
            (resolve, reject) => {
                request.post(configuration.config.apiUrl + configuration.apiConfig.addProduct, 
                    { body: JSON.stringify(body),
                        headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token }},
                (err, resp, body) => {
                              
                    if (err && resp) {
                        let errorObj = { statusCode: resp.statusCode, statusMessage: err, response: resp }
                        reject(errorObj);
                    }
                    if (body) {
                        resolve(body);
                    }
                    else {
                        let errorObj = { statusCode: resp.statusCode, statusMessage: resp.statusMessage, response: resp }
                        reject(errorObj);
                    }
                }
            );
            }
        );
    },

    upsertVendors: function(token, body) {
        return new Promise(
            (resolve, reject) => {
                request.post(configuration.config.apiUrl + configuration.apiConfig.addVendor, 
                    { body: JSON.stringify(body),
                        headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token }},
                (err, resp, body) => {
                    console.log(body);
                    console.log(resp.statusCode);

                    if (err && resp) {
                        let errorObj = { statusCode: resp.statusCode, statusMessage: err, response: resp }
                        reject(errorObj);
                    }
                    if (body) {
                        resolve(body);
                    }
                    else {
                        let errorObj = { statusCode: resp.statusCode, statusMessage: resp.statusMessage, response: resp }
                        reject(errorObj);
                    }
                }
            );
            }
        );
    } 

}