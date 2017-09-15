//file-tasks.js

var fs = require('fs');
var Promise = require('bluebird');
var configuration = require('./configuration.js');


module.exports = {
    getDataFile: function(fileName) {

        return new Promise((resolve, reject) => { 
            
            fs.readFile(fileName,'utf-8', (error, data) => {

                console.log(data);

                if (error) {
                    reject(error);
                }
                resolve(data);
            }
            );
        }
        );
    
    },
}