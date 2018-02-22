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
    
    app.route('/api/aggregates/errors')
        .get(intgError.getAggregateErrorsByDate);
    
    app.route('/api/aggregates/errors/analysis')
        .get(intgError.analyzeErrors);
    
    app.route('/api/passphrase')
        .post(intgError.generateHash);
    
    app.route('/api/authenticate')
        .post(intgError.authenticateUser);
    
    app.route('/api/support/integrations')
        .get(intgError.getCustomerIntegrationData);
    
    app.route('/api/integrations')
        .get(intgError.getIntegrationDataByClient);

    app.route('/api/integrations/errors/countByDate')
        .get(intgError.getErrorCountByDate);
    
    app.route('/api/integrations/errors/countByID')
        .get(intgError.getAllErrorsByIntegrationID);
    
    app.route('/api/users/add')
        .post(intgError.addUser);
    
    app.route('/api/files/add')
        .post(intgError.addFileInfo);

}