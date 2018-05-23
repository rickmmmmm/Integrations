//file-tasks.js

const fs = require('fs');
const Promise = require('bluebird');

/**
 * File tasks
 * @namespace FileTasks
 */
module.exports = {

    /**
     * Get file content
     * @param {string} fileName File name.
     * @returns {Promise<string>} File content.
     */
    getDataFile(fileName) {
        return new Promise((resolve, reject) => {
            fs.readFile(fileName, 'utf-8', (error, data) => {
                if (error) {
                    reject(error);
                }
                resolve(data);
            });
        });
    },

    /**
     * Write a file.
     * @param {string} fileName File name.
     * @param {string} data File content.
     * @returns {Promise<string>} A message warning that the file was successfully written.
     */
    writeDataFile(fileName, data) {
        return new Promise((resolve, reject) => {
            const writeStream = fs.createWriteStream(fileName);

            writeStream.on('finish', () => {
                resolve(`File ${fileName} was successful written`);
            });

            writeStream.on('error', (err) => {
                console.log(err.message);
                reject(err);
            });

            writeStream.write(data);
            writeStream.end();
        });
    },

    mkdir(path) {
        return new Promise((resolve, reject) => {
            fs.mkdir(path, (err) => {
                if (err) {
                    reject(err);
                    console.log(err.message);
                }
                resolve(`Directory '${path}' was successfully created.`)
            })
        })
    }
};