var fs = require('fs');
var errorFile = './errors/log.json';

module.exports = {

    logErrorToFile(error) {
        //log application level errors to external .json file
        fs.createWriteStream(errorFile).write(error);
    }

};