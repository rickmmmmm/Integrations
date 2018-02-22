//file-tasks.js
'use strict'
var Promise = require('bluebird');
var fs = require('fs');

module.exports = {
    readHTMLFile(options) {
        let filepath = options.filepath;

        return new Promise(
            (resolve, reject) => {            
                fs.readFile(filepath,'utf-8', (error, data) => {
                    if (error) {
                        reject(error);
                    }
                    resolve(data);
                }
                );
            }
        );
    },
    flattenHTML(data, options) {
        return data.replace('\n',' ');
    }
}