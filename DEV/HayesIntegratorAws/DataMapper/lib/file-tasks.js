//file-tasks.js

var fs = require('fs');
var Promise = require('bluebird');


module.exports = {
    getDataFile: function (fileName) {

        return new Promise((resolve, reject) => {

            fs.readFile(fileName, 'utf-8', (error, data) => {

                if (error) {
                    reject(error);
                }
                resolve(data);
            });
        });
    },
}