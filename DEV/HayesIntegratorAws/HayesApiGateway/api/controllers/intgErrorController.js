'use strict';
var Promise = require('bluebird');
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
                            { issueType: 'Already Exists', desc: 'Order or Detail already exists but should not', count: 0 },
                            { issueType: 'Invalid Order', desc: 'Order or Detail does not exist but should.', count: 0 },
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

        console.log(req.query);
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

    }

}