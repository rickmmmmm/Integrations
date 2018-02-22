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
            maxIdleTime: 100000
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
    IntegrationDate: sequelize.STRING,
    IntegrationType: sequelize.STRING
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

UserAPISettingsModel = {
    Client: { type: sequelize.STRING, primaryKey: true },
    UserName: { type: sequelize.STRING, primaryKey: true },
	Passphrase: sequelize.STRING,
	Email: sequelize.STRING,
	Valid: sequelize.BOOLEAN,
    CertificateVal: sequelize.STRING,
    ClientFullName: sequelize.STRING,
    Support: sequelize.BOOLEAN,
    Admin: sequelize.BOOLEAN
}

DataIntegrationsMasterListModel = {
    Client: { type: sequelize.STRING, primaryKey: true },
    IntegrationType: { type: sequelize.STRING, primaryKey: true },
    DateAdded: sequelize.STRING,
    AddObj: sequelize.STRING,
    Active: sequelize.BOOLEAN
}

DataIntegrationsFilesModel = {
    DataIntegrationsFilesID: { type: sequelize.INTEGER, primaryKey: true, autoIncrement: true },
    Client: sequelize.STRING,
    FileNameAws: sequelize.STRING,
    AwsFileLink: sequelize.STRING,
    AddedDate: sequelize.STRING
}

module.exports = {

    DataIntegrations: seq.define('DataIntegrations', DataIntegrationsModel),
    DataIntegrationsErrors: seq.define('DataIntegrationsErrors', DataIntegrationsErrorsModel),
    DataIntegrationsMappings: seq.define('DataIntegrationsMappings', DataIntegrationsMappingsModel),
    DataIntegrationsAggregates: seq.define('DataIntegrationsAggregates', DataIntegrationsAggregatesModel),
    UserAPISettings: seq.define('UserAPISettings',UserAPISettingsModel),
    DataIntegrationsMasterList: seq.define('DataIntegrationsMasterList', DataIntegrationsMasterListModel, { tableName: 'DataIntegrationsMasterList'}),
    DataIntegrationsFiles: seq.define('DataIntegrationsFiles', DataIntegrationsFilesModel, { tableName: 'DataIntegrationsFiles'}),

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

    selectErrorCountByDate(date, options) {
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
                this.DataIntegrationsErrors.count({
                    where : options.errtype ? { ErrorName: options.errtype } : {},
                    include: [{
                        model: this.DataIntegrations,
                        where: { IntegrationDate: date, Client: options.client }
                    }]
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
            
    },


    selectAllAggregatesByDate(date, options) {
        
                return new Promise(
                    (resolve, reject) => {
                        this.DataIntegrationsAggregates.findAll({
                            where: { DateRun: date },
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

    selectAuthData(options) {
        return new Promise(
            (resolve, reject) => {
                this.UserAPISettings.findOne({ where: { UserName: options.username }}).then(
                    data =>{
                        resolve(data);
                    },
                    error => {
                        reject(error);
                    }
                )
            }
        );
    },

    checkCertByClientAndCertificateVal(options) {
        return new Promise(
            (resolve, reject) =>{
                this.UserAPISettings.findOne({
                        where: { Client: options.clientVal, CertificateVal: options.certVal }
                    }
                ).then(
                    success => {
                        if (!success) {
                            reject();
                        }
                        resolve();
                    },
                    error => {
                        reject();
                    }
                );
            }
        );
    },

    selectDataIntegrations(options) {

        return new Promise(
            (resolve, reject) => {
                this.DataIntegrationsMasterList.findAll({ 
                    where: { Active: true }
                }).then(
                    data => {
                        resolve(data);
                    },
                    error => {
                        reject(error);
                    }
                );
            }
        );
    },

    selectDataIntegrationsByClient(options) {

        return new Promise(
            (resolve, reject) => {
                this.DataIntegrationsMasterList.findAll({ 
                    where: { Active: true, Client: options.client }
                }).then(
                    data => {
                        resolve(data);
                    },
                    error => {
                        reject(error);
                    }
                );
            }
        );

    },

    selectCountErrors(options) {
        return new Promise(
            (resolve, reject) => { this.DataIntegrationsErrors.count({ DataIntegrationsID: options.intgid }).then(
                data => {
                    resolve(data);
                },
                error => {
                    reject(error);
                }
            );
        });
    },

    selectUserData(options) {
        return new Promise(
            (resolve, reject) => {
                this.UserAPISettings.findAll({
                    attributes: ['Client','UserName','Email','AddedDate','Support','Admin']
                }
                ).then(
                    data => {
                        resolve(data);
                    },
                    error => {
                        reject(error);
                    }
                );
            }
        );
    },

    insertUser(user) {
        return new Promise(
            (resolve,reject) => {
                this.UserAPISettings.create(user).then(
                    data => {
                        resolve(data);
                    },
                    error => {
                        reject(error);
                    }
                );
            }
        );
    },

    deactivateUser(user) {
        return new Promise(
            (resolve,reject) => {
                this.UserAPISettings.update(user).then(
                    data => {
                        resolve(data);
                    },
                    error => {
                        reject(error);
                    }
                );
            }
        );
    },

    insertFileInfo(fileInfo) {
        return new Promise(
            (resolve, reject) => {
                this.DataIntegrationsFiles.create(fileInfo).then(
                    data => {
                        resolve(data);
                    },
                    error => {
                        reject(error);
                    }
                );
            }
        );
    }

}