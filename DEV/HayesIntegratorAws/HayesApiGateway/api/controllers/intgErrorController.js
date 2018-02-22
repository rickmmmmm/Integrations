'use strict';
var Promise = require('bluebird');
var bcrypt = require('bcrypt-nodejs-as-promised');
var bcrypt2 = require('bcrypt-nodejs');

var _repo = require('../../lib/repository.js');

module.exports = {

    getAllErrorsByDate(req, res) {

        let count = req.query.c ? req.query.c : 100;
        let pagenum = req.query.n ? req.query.n : 1;
        let client = req.query.client ? req.query.client : 'None';
        let errtype = req.query.e;

        if (!req.query.date) {
            let errmsg = { message: "Must include valid date."};
            res.send(errmsg);
        }

        let dateVal = req.query.date;
                
        _repo.selectErrorsByDate(dateVal, { pagecount: count, pagenum: pagenum, client: client,  errtype: errtype }).then(
            data => {
                res.json(data);
            },
            error => {
                res.send(error);
            }
        );
    },

    analyzeErrors(req, res) {
        
        let count = 10000;
        let pagenum = 1;
        let client = req.query.client ? req.query.client : 'None';
        let errtype = req.query.e;

        if (!req.query.date) {
            let errmsg = { message: "Must include valid date."};
            res.send(errmsg);
        }

        if (!errtype) {
            let errmsg = { message: "Must include valid error type." };
            res.send(errmsg);
        }

        let dateVal = req.query.date;
                
        _repo.selectErrorsByDate(dateVal, { pagecount: count, pagenum: pagenum, client: client,  errtype: errtype, random: true }).then(
            data => {

                let dataVals = data.map(m => { return m.dataValues; });

                let aggData = [
                    { issueType: 'Site', desc: 'Site Not Found', count: 0 },
                    { issueType: 'Product', desc: 'Product Not Found', count: 0 },
                    { issueType: 'Vendor', desc: 'Vendor not found', count: 0 },
                    { issueType: 'Already Exists', desc: 'Order or Detail already exists.', count: 0 },
                    { issueType: 'Invalid Order', desc: 'Order or Detail must exist to remove error.', count: 0 },
                    { issueType: 'Other', desc: 'Other', count: 0 },
                ]

                for (let v of dataVals) {

                    let eobj = JSON.parse(v.ErrorObject);
                    for (let errobj of eobj.errorDescription) {
                        if (errobj.indexOf('Site') !== -1) {
                            aggData.filter(fil => { return fil.issueType === 'Site'})[0].count += 1;
                        }
                        else if (errobj.indexOf('Product') !== -1) {
                            aggData.filter(fil => { return fil.issueType === 'Product'})[0].count += 1;
                        }
                        else if (errobj.indexOf('Vendor') !== -1) {
                            aggData.filter(fil => { return fil.issueType === 'Vendor'})[0].count += 1;
                        }
                        else if (errobj.indexOf('already exists') !== -1) {
                            aggData.filter(fil => { return fil.issueType === 'Already Exists'})[0].count += 1;
                        }
                        else if (errobj.indexOf('does not exist') !== -1) {
                            aggData.filter(fil => { return fil.issueType === 'Already Exists'})[0].count += 1;
                        }
                        else {
                            aggData.filter(fil => { return fil.issueType === 'Other'})[0].count += 1;
                        }
                    }
                }

                for (let a of aggData) {
                    a['percent'] = a.count / 10000;
                }
                res.json(aggData);
            },
            error => {
                res.send(error);
            }
        );
    },

    getAllErrorsByIntegrationID(req, res) {
        
        let count = req.query.c ? req.query.c : 100;
        let pagenum = req.query.n ? req.query.n : 1;
        let errtype = req.query.e;

        if (!req.query.id) {
            let errmsg = { message: 'Must include Integration ID.'};
            res.send(errmsg);
        }
        
        _repo.selectErrorsByIntegrationsID(req.query.id, { pagecount: count, pagenum: pagenum, errtype: errtype }).then(
            data => {
                res.json(data);
            },
            error => {
                res.send(error);
            }
        );
    },

    testResponse(req, res) {
        let testMessage = { message: "Success!" };
        res.json(testMessage);
    },

    getAggregateErrorsByDate(req, res) {

        let dateVal = req.query.date;
        let client = req.query.client;

        if (!dateVal || !client) {
            let errmsg = { message: 'Must include Date and Client with request.'};
            res.send(errmsg);
        }

        _repo.selectAggregatesByDate(dateVal, { client: client }).then(
            data => {
                res.json(data);
            },
            error => {
                res.send(error);
            }
        );

    },

    authenticateUser(req, res) {

        let requestBody = req.body;

        if (!requestBody.userName || ! requestBody.password) {
            let errorMessage = { message: 'Incorrect username or password.' };
            res.status(401).send(errorMessage);
        }

        let username = requestBody.userName;
        let password = requestBody.password;

        _repo.selectAuthData({ username: username }).then(
            resolve => {
                let authData = resolve.dataValues;
                bcrypt.compare(password, authData.Passphrase).then(
                    resolve => {
                        if (resolve) {
                            let successMessage = { message: 'Successfully logged in.', cert: authData.CertificateVal, client: authData.Client, clientName: authData.ClientFullName }
                            res.json(successMessage);
                        }
                        else {
                            let errorMessage = { message: 'Incorrect username or password.' };
                            res.status(401).send(errorMessage);
                        }
                    },
                    reject => {
                        let errorMessage = { message: 'Incorrect username or password.' };
                        res.status(401).send(errorMessage);
                    }
                )
            },
            reject => {
                let errorMessage = { message: 'Incorrect username or password.' };
                res.status(401).send(errorMessage);
            }
        )
    },

    generateHash(req, res) {
        let pw = req.body.password;

        console.log(pw);

        bcrypt2.hash(pw, null, null,(error, result) => {
            console.log(result);
            console.log(typeof result);
            console.log(error);
            // if (error !== '') {
            //     console.log(error);
            //     let errorMessage = { message: error };
            //     res.status(404).send(errorMessage);
            //     return;
            // }

            let successMessage = { message: 'Successfully hashed', hash: result };
            res.json(successMessage);
        });

        // bcrypt.hash(pw, 10).then(
        //     resolve => {
        //         let successMessage = { message: 'Successfully hashed', hash: resolve }
        //         res.json(successMessage);
        //     },
        //     reject => {
        //         let errorMessage = { message: reject };
        //         res.send(errorMessage);
        //     }
        // );
    },

    getCustomerIntegrationData(req, res) {
        
        let options = { 
            date: req.query.date
        };

        let returnArr = [];

        _repo.selectDataIntegrations(options).then(
            resolve => {
                returnArr = resolve.map(m => { return m.dataValues; });
                let opts = { pagecount: 10000 , pagenum: 1 };
                _repo.selectIntegrationsByDate(options.date, opts).then(
                    resolve => {
                        for (let val of returnArr) {
                            val['dataIntegrationsRun'] = resolve.filter(fil => { return fil.Client === val.Client && fil.IntegrationType === val.IntegrationType});
                        }
                        _repo.selectAllAggregatesByDate(options.date, opts).then(
                            resolve => {
                                for (let val of returnArr) {
                                    val['dataIntegrationAggregates'] = resolve.filter(fil => { return fil.Client === val.Client && fil.IntegrationType === val.IntegrationType});
                                }
                                res.json(returnArr);
                            },
                            reject => {
                                res.send(reject);
                            }
                        )
                    },
                    reject => {
                        res.send(reject);
                    }
                )

            },
            reject => {
                res.send(reject);
            }
        )
    },

    getIntegrationDataByClient(req, res) {
        
        let options = { 
            date: req.query.date,
            client: req.query.client
        };

        let returnArr = [];

        _repo.selectDataIntegrationsByClient(options).then(
            resolve => {
                returnArr = resolve.map(m => { return m.dataValues; });
                let opts = { pagecount: 10000 , pagenum: 1, client: options.client };
                _repo.selectIntegrationsByDate(options.date, opts).then(
                    resolve => {
                        for (let val of returnArr) {
                            val['dataIntegrationsRun'] = resolve.filter(fil => { return fil.Client === val.Client && fil.IntegrationType === val.IntegrationType});
                        }
                        _repo.selectAggregatesByDate(options.date, opts).then(
                            resolve => {
                                for (let val of returnArr) {
                                    val['dataIntegrationAggregates'] = resolve.filter(fil => { return fil.Client === val.Client && fil.IntegrationType === val.IntegrationType});
                                }
                                res.json(returnArr);
                            },
                            reject => {
                                res.send(reject);
                            }
                        )
                    },
                    reject => {
                        res.send(reject);
                    }
                );

            },
            reject => {
                res.send(reject);
            }
        )
    },

    getErrorCountByDate(req, res) {
        let options = {
            date: req.query.date,
            client: req.query.client
        }
        
        _repo.selectErrorCountByDate(options.date, options).then(
            resolve => {
                let returnObj = { count: resolve }
                res.json(returnObj);
            },
            reject => {
                res.send(rej);
            }
        );
    },

    getErrorCountByIntgID(req, res) {
        let options = {
            intgid: req.query.id,
            client: req.query.client
        }
        
        _repo.selectCountErrors(options).then(
            resolve => {
                let returnObj = { count: resolve }
                res.json(returnObj);
            },
            reject => {
                res.send(rej);
            }
        );
    },

    addUser(req, res) {
        let userData = req.body;

        bcrypt.hash(userData.Passphrase, 10).then(
            resolve => {
                userData.Passphrase = resolve;
                console.log(userData);
                _repo.insertUser(userData).then(
                    resolve => {
                        res.json(resolve);
                    },
                    reject => {
                        res.send(reject);
                    }
                );
            },
            reject => {
                let errorMessage = { message: reject };
                res.send(errorMessage);
            }
        );
    },

    removeUser(req, res) {
        //something here eventually...
    },

    changePassword(req, res) {
        //something here eventually...
    },

    addFileInfo(req, res) {
        let fileInfo = {
            Client: req.body.client,
            FileNameAws: req.body.filename,
            AwsFileLink: req.body.bucket
        }

        console.log(fileInfo);

        _repo.insertFileInfo(fileInfo).then(
            resolve => {
                let successMessage = { message: 'Success', other: resolve }
                res.send(successMessage);
            },
            reject => {
                let errorMessage = { message: 'Error', other: reject }
                res.status(400).send(errorMessage);
            }
        );
    }
}