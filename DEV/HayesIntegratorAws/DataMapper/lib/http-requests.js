const request = require('request');
var configuration = require('./configuration.js');
const repository = require('./repository.js');
var Promise = require('bluebird');
var fs = require('fs');

module.exports = {

    getToken() {
        return new Promise(
            (resolve, reject) => {
                let loginUrl = configuration.config.apiUrl + configuration.apiConfig.login;
                let body = { Key: configuration.secrets.secretkey, Phrase: configuration.secrets.passphrase }
                // console.log('LoginUrl: ' + loginUrl);
                // console.log('body: ' + JSON.stringify(body));
                request.post(loginUrl,
                    {
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify(body)
                    },
                    (error, response, data) => {
                        if (error) {
                            let errorObj = { err: error, response: response }
                            reject(errorObj);
                        } else if (response.statusCode !== 200) {
                            let errorObj = { err: response.statusCode, response: response.statusMessage }
                            reject(errorObj);
                        } else if (data) {
                            resolve(data);
                        } else {
                            let errorObj = { err: response.statusCode, response: response.statusMessage }
                            reject(errorObj);
                        }
                    }
                );
            }
        );
    },

    getAllVendors(token) {

        return new Promise(
            (resolve, reject) => {
                request.get(configuration.config.apiUrl + configuration.apiConfig.getVendors, { headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token } },
                    (err, resp, body) => {
                        if (err || resp.statusCode !== 200) {
                            let errorObj = { statusCode: resp.statusCode, statusMessage: err }
                            reject(err);
                        }
                        if (body) {
                            let mappedBody = [];
                            for (let b of JSON.parse(body)) {
                                let outVal = {
                                    VendorID: b.vendorID,
                                    VendorName: b.vendorName,
                                    Address1: b.address1,
                                    Address2: b.address2,
                                    City: b.city,
                                    State: b.state,
                                    ZipCode: b.zipCode,
                                    Phone: b.phone,
                                    Email: b.email
                                }

                                // console.log(outVal);

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

    getAllProducts(token) {
        return new Promise(
            (resolve, reject) => {
                request.get(configuration.config.apiUrl + configuration.apiConfig.getProducts,
                    {
                        headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token }
                    },
                    (err, resp, body) => {

                        // console.log(resp.statusCode);
                        // console.log(resp.statusMessage);

                        if (err) {
                            let errorObj = { err: err, response: response }
                            reject(err);
                        }
                        if (body) {

                            let mappedBody = []

                            for (let b of body) {
                                let outVal = {
                                    ProuctNumber: b.productNumber,
                                    ProductName: b.productName,
                                    ProductDescription: b.productDescription,
                                    ProductType: b.productType,
                                    Model: b.model,
                                    Manufacturer: b.manufacturer,
                                    SuggestedPrice: b.suggestedPrice,
                                    SKU: b.sku
                                };

                                mappedBody.push(outVal);
                            }

                            resolve(mappedBody);
                        }
                        else {
                            let errorObj = { err: err, response: response }
                            reject(errorObj);
                        }
                    }
                );
            }
        );
    },

    getAllFundingSources(token) {
        return new Promise(
            (resolve, reject) => {
                request.get(configuration.config.apiUrl + configuration.apiConfig.getFundingSources,
                    {
                        headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token }
                    },
                    (error, response, data) => {
                        if (error) {
                            let errorObj = { err: error, response: response }
                            reject(errorObj);
                        } else if (response.statusCode !== 200) {
                            let errorObj = { err: response.statusCode, response: response.statusMessage }
                            reject(errorObj);
                        } else if (data) {
                            resolve(data);
                        } else {
                            let errorObj = { err: response.statusCode, response: response.statusMessage }
                            reject(errorObj);
                        }
                    }
                );
            }
        );
    },

    upsertHeaders(token, body) {
        return new Promise(
            (resolve, reject) => {
                // console.log('upsertHeaders body:');
                // console.log(JSON.stringify(body));
                request.post(configuration.config.apiUrl + configuration.apiConfig.addHeader,
                    {
                        body: JSON.stringify(body),
                        headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token }
                    },
                    (error, response, data) => {
                        if (error) {
                            let errorObj = { err: error, response: response }
                            reject(errorObj);
                        } else if (response.statusCode !== 200) {
                            let errorObj = { err: response.statusCode, response: response.statusMessage }
                            reject(errorObj);
                        } else if (data) {
                            resolve(data);
                        } else {
                            let errorObj = { err: response.statusCode, response: response.statusMessage }
                            reject(errorObj);
                        }
                    }
                );
            }
        );
    },

    upsertDetails(token, body) {
        return new Promise(
            (resolve, reject) => {
                request.post(configuration.config.apiUrl + configuration.apiConfig.addDetail,
                    {
                        body: JSON.stringify(body),
                        headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token }
                    },
                    (error, response, data) => {
                        if (error) {
                            let errorObj = { err: error, response: response }
                            reject(errorObj);
                        } else if (response.statusCode !== 200) {
                            let errorObj = { err: response.statusCode, response: response.statusMessage }
                            reject(errorObj);
                        } else if (data) {
                            resolve(data);
                        } else {
                            let errorObj = { err: response.statusCode, response: response.statusMessage }
                            reject(errorObj);
                        }
                    }
                );
            }
        );
    },

    upsertShipments(token, body) {
        return new Promise(
            (resolve, reject) => {
                request.post(configuration.config.apiUrl + configuration.apiConfig.addShipment,
                    {
                        body: JSON.stringify(body),
                        headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token }
                    },
                    (error, response, data) => {
                        if (error) {
                            let errorObj = { err: error, response: response }
                            reject(errorObj);
                        } else if (response.statusCode !== 200) {
                            let errorObj = { err: response.statusCode, response: response.statusMessage }
                            reject(errorObj);
                        } else if (data) {
                            resolve(data);
                        } else {
                            let errorObj = { err: response.statusCode, response: response.statusMessage }
                            reject(errorObj);
                        }
                    }
                );
            }
        );
    },

    upsertProducts(token, body) {

        return new Promise(
            (resolve, reject) => {
                request.post(configuration.config.apiUrl + configuration.apiConfig.addProduct,
                    {
                        body: JSON.stringify(body),
                        headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token }
                    },
                    (error, response, data) => {
                        if (error) {
                            let errorObj = { err: error, response: response }
                            reject(errorObj);
                        } else if (response.statusCode !== 200) {
                            let errorObj = { err: response.statusCode, response: response.statusMessage }
                            reject(errorObj);
                        } else if (data) {
                            resolve(data);
                        } else {
                            let errorObj = { err: response.statusCode, response: response.statusMessage }
                            reject(errorObj);
                        }
                    }
                );
            }
        );
    },

    upsertVendors(token, body) {
        return new Promise(
            (resolve, reject) => {
                request.post(configuration.config.apiUrl + configuration.apiConfig.addVendor,
                    {
                        body: JSON.stringify(body),
                        headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token }
                    },
                    (error, response, data) => {
                        if (error) {
                            let errorObj = { err: error, response: response }
                            reject(errorObj);
                        } else if (response.statusCode !== 200) {
                            let errorObj = { err: response.statusCode, response: response.statusMessage }
                            reject(errorObj);
                        } else if (data) {
                            resolve(data);
                        } else {
                            let errorObj = { err: response.statusCode, response: response.statusMessage }
                            reject(errorObj);
                        }
                    }
                );
            }
        );
    },

    addInvoices(token, body) {
        return new Promise(
            (resolve, reject) => {
                request.post(configuration.config.apiUrl + configuration.apiConfig.addInvoices,
                    {
                        body: JSON.stringify(body),
                        headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token }
                    },
                    (error, response, data) => {
                        if (error) {
                            let errorObj = { err: error, response: response }
                            reject(errorObj);
                        } else if (response.statusCode !== 200) {
                            let errorObj = { err: response.statusCode, response: response.statusMessage }
                            reject(errorObj);
                        } else if (data) {
                            resolve(data);
                        } else {
                            let errorObj = { err: response.statusCode, response: response.statusMessage }
                            reject(errorObj);
                        }
                    }
                );
            }
        );
    },

    addInvoiceDetails(token, body) {
        return new Promise(
            (resolve, reject) => {
                request.post(configuration.config.apiUrl + configuration.apiConfig.addInvoiceDetails,
                    {
                        body: JSON.stringify(body),
                        headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token }
                    },
                    (error, response, data) => {
                        if (error) {
                            let errorObj = { err: error, response: response }
                            reject(errorObj);
                        } else if (response.statusCode !== 200) {
                            let errorObj = { err: response.statusCode, response: response.statusMessage }
                            reject(errorObj);
                        } else if (data) {
                            resolve(data);
                        } else {
                            let errorObj = { err: response.statusCode, response: response.statusMessage }
                            reject(errorObj);
                        }
                    }
                );
            }
        );
    }
}