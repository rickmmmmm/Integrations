var sequelize = require('sequelize');
var Promise = require('bluebird');
var fs = require('fs')
const configuration = require('../configuration.json');

var database = configuration.config.database;
var username = configuration.config.username;
var password = configuration.secrets.password;

seq =  new sequelize(database,username,password, {
        host: configuration.config.host,
          dialect: 'mssql',
          logging: false,
          define: {
            timestamps: false
          },
          pool: {
            maxIdleTime: 1000
          }
    });

DataIntegrationsModel = { 
    IntegrationsID: { type: sequelize.INTEGER, primaryKey: true, autoIncrement: true}, 
    IntegrationsObject: sequelize.STRING,
    DateAdded: sequelize.DATE,
    DataProcessedSuccessfully: sequelize.BOOLEAN,
    DataProcessing: sequelize.BOOLEAN,
    DataSentToTipweb: sequelize.BOOLEAN,
    DataCleared: sequelize.BOOLEAN,
    Client: sequelize.STRING,
    IntegrationDate: sequelize.STRING
};

DataIntegrationsErrorsModel = {
    DataIntegrationsErrorsID: { type: sequelize.INTEGER, primaryKey: true, autoIncrement: true },
    ErrorNumber: sequelize.STRING,
    ErrorName: sequelize.STRING,
    ErrorDescription: sequelize.STRING,
    ErrorObject: sequelize.STRING(10000),
    DataIntegrationsID: sequelize.INTEGER,
    AddedDate: sequelize.STRING
};

DataIntegrationsMappingsModel = {
    MappingsID: { type: sequelize.STRING, unique: 'compositeIdx' },
    MappingsStep: { type: sequelize.INTEGER, primaryKey: true, autoIncrement: true, unique: 'compositeIdx' },
    MappingsObject: sequelize.STRING(10000),
};

DataIntegrationsAggregatesModel = {
    AggregatesID: { type: sequelize.INTEGER, primaryKey: true, autoIncrement: true },
    Client: { type: sequelize.STRING },
    IntegrationType: { type: sequelize.STRING },
    DateRun: { type: sequelize.STRING },
    DataType: { type: sequelize.STRING },
    ReferenceVal: { type: sequelize.STRING },
    TotalCount: { type: sequelize.INTEGER },
    ReferenceDescription: sequelize.STRING
}

module.exports = {

    DataIntegrations: seq.define('DataIntegrations', DataIntegrationsModel),
    DataIntegrationsErrors: seq.define('DataIntegrationsErrors', DataIntegrationsErrorsModel),
    DataIntegrationsMappings: seq.define('DataIntegrationsMappings', DataIntegrationsMappingsModel),
    DataIntegrationsAggregates: seq.define('DataIntegrationsAggregates', DataIntegrationsAggregatesModel),

    selectIntegrationsById(id, options) {

        return new Promise(
            (resolve, reject) => {
                this.DataIntegrations.findAll({
                    attributes: { exclude: 'id' },
                    where: { IntegrationsID: id },
                    limit: parseInt(options.pagecount),
                    offset: parseInt(options.pagenum - 1) * parseInt(options.pagecount)
                }).then(
                    data => {
                        resolve(data);
                    },
                    err => {
                        reject(err);
                    }
                );
            }
        );
    },

    selectIntegrationsByDate(date, options) {
        return new Promise(
            (resolve, reject) => {
                this.DataIntegrations.findAll({
                    attributes: { exclude: 'id' },
                    where: { IntegrationDate: date },
                    limit: parseInt(options.pagecount),
                    offset: parseInt(options.pagenum - 1) * parseInt(options.pagecount)
                }).then(
                    data => {
                        resolve(data);
                    },
                    err => {
                        reject(err);
                    }
                );
            }
        );
    },

    selectErrorsByDate(date, options) {

        this.DataIntegrations.hasMany(this.DataIntegrationsErrors, {
            foreignKey: { 
                name: 'DataIntegrationsID'}
            });
        
        this.DataIntegrationsErrors.belongsTo(this.DataIntegrations, {
            foreignKey: {
                name: 'DataIntegrationsID'
            }
        });

        return new Promise(
            (resolve, reject) => {
                this.DataIntegrationsErrors.findAll({
                    include: [{
                        model: this.DataIntegrations,
                        where: { IntegrationDate: date, Client: options.client }
                    }],
                    where : options.errtype ? { ErrorName: options.errtype } : {},
                    limit: parseInt(options.pagecount),
                    offset: parseInt(options.pagenum - 1) * parseInt(options.pagecount),
                    order: [options.random ? [sequelize.fn('RAND', '')] : 'DataIntegrationsErrorsID']
                }).then(
                    data => {
                        resolve(data);
                    },
                    err => {
                        reject(err);
                    }
                );
            }
        );
    },

    selectErrorsByIntegrationsID(id, options) {
        return new Promise(
            (resolve, reject) => {
                this.DataIntegrationsErrors.findAll({
                    where: { DataIntegrationsID: id },
                    limit: parseInt(options.pagecount),
                    offset: parseInt(options.pagenum - 1) * parseInt(options.pagecount)
                }).then(
                    data => {
                        resolve(data);
                    },
                    err => {
                        reject(err);
                    }
                );
            }
        );
    },

    selectAggregatesByDate(date, options) {
        
                return new Promise(
                    (resolve, reject) => {
                        this.DataIntegrationsAggregates.findAll({
                            where: { DateRun: date, Client: options.client },
                            limit: parseInt(options.pagecount),
                            offset: parseInt(options.pagenum - 1) * parseInt(options.pagecount)
                        }).then(
                            data => {
                                resolve(data);
                            },
                            err => {
                                reject(err);
                            }
                        );
                    }
                );
            }

}