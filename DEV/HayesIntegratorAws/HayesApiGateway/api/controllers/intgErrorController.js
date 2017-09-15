'use strict';
var Promise = require('bluebird');
var _repo = require('../../lib/repository.js');

module.exports = {

    getAllErrorsByDate(req, res) {

        let count = req.query.c ? req.query.c : 100;
        let pagenum = req.query.n ? req.query.n : 1;
        let client = req.query.client ? req.query.client : 'None';

        if (!req.query.date) {
            let errmsg = { message: "Must include valid date."};
            res.send(errmsg);
        }

        let dateVal = req.query.date;
                
        _repo.selectErrorsByDate(dateVal, { pagecount: count, pagenum: pagenum, client: client }).then(
            data => {
                res.json(data);
            },
            error => {
                res.send(error);
            }
        );
    },

    getAllErrorsByIntegrationID(req, res) {
        
        let count = req.query.c ? req.query.c : 100;
        let pagenum = req.query.n ? req.query.n : 1;

        if (!req.query.id) {
            let errmsg = { message: 'Must include Integration ID.'};
            res.send(errmsg);
        }
        
        _repo.selectErrorsByIntegrationsID(req.query.id, { pagecount: count, pagenum: pagenum }).then(
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
    }

}