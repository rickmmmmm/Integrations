'use strict'

module.exports = (app) => {

    var intgError = require('../controllers/intgErrorController.js');
    
    app.route('/api/errorsByDate')
        .get(intgError.getAllErrorsByDate)
        .post(intgError.getAllErrorsByDate);
    
    app.route('/api/errorsByIntgID')
        .get(intgError.getAllErrorsByIntegrationID)
        .post(intgError.getAllErrorsByIntegrationID);
    
    app.route('/api/test')
        .get(intgError.testResponse);

}