const request = require('request');
const configuration = require('./configuration.js');
const Promise = require('bluebird');
const mappings = require('./mappings.js');

/**
 * Requests specific information for DataMapper project.
 * @namespace HttpRequests
 */
module.exports = {

    /**
     * Get auth token for TIPWeb server.
     * @returns {Promise<{err: string, response: string}>|Promise<*>} Response from TIPWeb server.
     * @memberOf HttpRequests
     */
    getToken() {
        return new Promise((resolve, reject) => {
            let loginUrl = configuration.config.apiUrl + configuration.apiConfig.login;
            let body = {
                Key: configuration.secrets.secretkey,
                Phrase: configuration.secrets.passphrase
            };

            request.post(loginUrl, {
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(body)
            }, (err, response, data) => {
                if (err) {
                    reject(err);
                } else if (response.statusCode !== 200) {
                    reject({
                        err: response.statusCode,
                        response: response.statusMessage
                    });
                } else if (data) {
                    resolve(data);
                } else {
                    reject({
                        err: response.statusCode,
                        response: response.statusMessage
                    });
                }
            });
        });
    },

    getAllVendors(token) {
        return new Promise((resolve, reject) => {
            request.get(configuration.config.apiUrl + configuration.apiConfig.getVendors, {
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + token
                }
            }, (err, resp, body) => {
                if (err || resp.statusCode !== 200) {
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
                        };

                        mappedBody.push(outVal);
                    }

                    resolve(mappedBody);
                } else {
                    reject({
                        statusCode: resp.statusCode,
                        statusMessage: resp.statusMessage
                    });
                }
            });
        });
    },

    getAllProducts(token) {
        return new Promise((resolve, reject) => {
            request.get(configuration.config.apiUrl + configuration.apiConfig.getProducts, {
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + token
                }
            }, (err, response, body) => {

                if (err) {
                    reject(err);
                }

                if (body) {
                    let mappedBody = [];

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
                    reject({ err, response });
                }
            });
        });
    },

    getAllFundingSources(token) {
        return new Promise((resolve, reject) => {
            request.get(configuration.config.apiUrl + configuration.apiConfig.getFundingSources, {
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + token
                }
            }, (err, response, data) => {
                if (err) {
                    reject(err);
                } else if (response.statusCode !== 200) {
                    reject({
                        err: response.statusCode,
                        response: response.statusMessage
                    });
                } else if (data) {
                    resolve(data);
                } else {
                    reject({
                        err: response.statusCode,
                        response: response.statusMessage
                    });
                }
            });
        });
    },

    upsertHeaders(token, body) {

        console.log();
        console.log(mappings.getCurrentDate() + ' upsertHeaders Post starting');

        return new Promise((resolve, reject) => {
            return request.post(configuration.config.apiUrl + configuration.apiConfig.addHeader, {
                body: JSON.stringify(body),
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + token
                }
            }, (err, response, data) => {
                console.log();
                console.log(mappings.getCurrentDate() + ' upsertHeaders Post complete');
                if (err) {
                    console.log('post result return error');
                    reject(err);
                } else if (response.statusCode !== 200) {
                    console.log('response: ' + response.statusCode + ', message: ' + response.statusMessage);
                    reject({
                        err: response.statusCode,
                        response: response.statusMessage
                    });
                } else if (data) {
                    resolve(data);
                } else {
                    console.log('No error or data');
                    reject({
                        err: response.statusCode,
                        response: response.statusMessage
                    });
                }
            });
        });
    },

    upsertDetails(token, body) {

        console.log();
        console.log(mappings.getCurrentDate() + ' upsertDetails Post starting');

        return new Promise((resolve, reject) => {
            request.post(configuration.config.apiUrl + configuration.apiConfig.addDetail, {
                body: JSON.stringify(body),
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + token
                }
            }, (err, response, data) => {
                console.log();
                console.log(mappings.getCurrentDate() + ' upsertDetails Post Complete');
                if (err) {
                    console.log('post result return error');
                    reject(err);
                } else if (response.statusCode !== 200) {
                    console.log('response: ' + response.statusCode + ', message: ' + response.statusMessage);
                    reject({
                        err: response.statusCode,
                        response: response.statusMessage
                    });
                } else if (data) {
                    resolve(data);
                } else {
                    console.log('No error or data');
                    reject({
                        err: response.statusCode,
                        response: response.statusMessage
                    });
                }
            });
        });
    },

    upsertShipments(token, body) {

        console.log();
        console.log(mappings.getCurrentDate() + ' upsertHeaders Post starting');

        return new Promise((resolve, reject) => {
            request.post(configuration.config.apiUrl + configuration.apiConfig.addShipment, {
                body: JSON.stringify(body),
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + token
                }
            }, (err, response, data) => {
                console.log();
                console.log(mappings.getCurrentDate() + ' upsertShipments Post Complete');
                if (err) {
                    console.log('post result return error');
                    reject(err);
                } else if (response.statusCode !== 200) {
                    console.log('response: ' + response.statusCode + ', message: ' + response.statusMessage);
                    reject({
                        err: response.statusCode,
                        response: response.statusMessage
                    });
                } else if (data) {
                    resolve(data);
                } else {
                    console.log('No error or data');
                    reject({
                        err: response.statusCode,
                        response: response.statusMessage
                    });
                }
            });
        });
    },

    upsertProducts(token, body) {

        console.log();
        console.log(mappings.getCurrentDate() + ' upsertProducts Post starting');

        return new Promise((resolve, reject) => {
            request.post(configuration.config.apiUrl + configuration.apiConfig.addProduct, {
                body: JSON.stringify(body),
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + token
                }
            }, (error, response, data) => {
                console.log();
                console.log(mappings.getCurrentDate() + ' upsertProducts Post Complete');
                if (error) {
                    console.log('post result return error');
                    reject(error);
                } else if (response.statusCode !== 200) {
                    console.log('response: ' + response.statusCode + ', message: ' + response.statusMessage);
                    reject({
                        err: response.statusCode,
                        response: response.statusMessage
                    });
                } else if (data) {
                    resolve(data);
                } else {
                    console.log('No error or data');
                    reject({
                        err: response.statusCode,
                        response: response.statusMessage
                    });
                }
            });
        });
    },

    upsertVendors(token, body) {

        console.log();
        console.log(mappings.getCurrentDate() + ' upsertVendors Post starting');

        return new Promise((resolve, reject) => {
            request.post(configuration.config.apiUrl + configuration.apiConfig.addVendor, {
                body: JSON.stringify(body),
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + token
                }
            }, (error, response, data) => {
                console.log();
                console.log(mappings.getCurrentDate() + ' upsertVendors Post Complete');
                if (error) {
                    console.log('post result return error');
                    reject(error);
                } else if (response.statusCode !== 200) {
                    console.log('response: ' + response.statusCode + ', message: ' + response.statusMessage);
                    reject({
                        err: response.statusCode,
                        response: response.statusMessage
                    });
                } else if (data) {
                    resolve(data);
                } else {
                    console.log('No error or data');
                    reject({
                        err: response.statusCode,
                        response: response.statusMessage
                    });
                }
            });
        });
    },

    addInvoices(token, body) {
        return new Promise((resolve, reject) => {
            request.post(configuration.config.apiUrl + configuration.apiConfig.addInvoices, {
                body: JSON.stringify(body),
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + token
                }
            }, (err, response, data) => {
                if (err) {
                    reject(err);
                } else if (response.statusCode !== 200) {
                    reject({
                        err: response.statusCode,
                        response: response.statusMessage
                    });
                } else if (data) {
                    resolve(data);
                } else {
                    reject({
                        err: response.statusCode,
                        response: response.statusMessage
                    });
                }
            });
        });
    },

    addInvoiceDetails(token, body) {
        return new Promise((resolve, reject) => {
            request.post(configuration.config.apiUrl + configuration.apiConfig.addInvoiceDetails, {
                body: JSON.stringify(body),
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + token
                }
            }, (err, response, data) => {
                if (err) {
                    reject(err);
                } else if (response.statusCode !== 200) {
                    reject({
                        err: response.statusCode,
                        response: response.statusMessage
                    });
                } else if (data) {
                    resolve(data);
                } else {
                    reject({
                        err: response.statusCode,
                        response: response.statusMessage
                    });
                }
            });
        });
    }
};