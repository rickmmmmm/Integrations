const fs = require('fs');

const mappingActionArray = [
    { type: 'truncate', action001: truncateString },
    { type: 'concatenate', action001: concatenateStrings },
    { type: 'splitCombine', action001: splitCombineStrings },
    { type: 'linkMap', action001: mapToLinkDataTable },
    { type: 'roundDown', action001: roundToLeastInteger },
    { type: 'toDate', action001: stringToISODate },
    { type: 'parseNumber', action001: removeCharFromNumberValues }
    //{ type: 'default', action001: selectDefaultValue }
];


/**
 * Remove char from number values.
 * @param {string} value A value.
 * @returns {number} The result number.
 * @memberOf Mappings
 */
function removeCharFromNumberValues(obj, options) {//(value) {
    if (!obj[options.field]) {
        return null;
    } else {
        let value = obj[options.field];
        try {
            let number = Number(value.replace(/[^\d.-]/g, ''));
            if (isNaN(number)) {
                number = 0;
            }
            return number;
        } catch (error) {
            console.log('Invalid Number value:' + value);
            return string;
        }
    }
}

/**
 * Converts a string into a ISO Date string.
 * @param {string} string Date string.
 * @returns {string} ISO Date string.
 * @memberOf Mappings
 */
function stringToISODate(obj, options) {//(string) {
    if (!obj[options.field]) {
        return null;
    } else {
        let input = obj[options.field];
        try {
            return new Date(input).toISOString();
        } catch (error) {
            console.log('Invalid Date Input:' + input);
            return string;
        }
    }
}


function splitElements(inArr, elements) {
    const outArr = [];

    for (let e of elements) {
        outArr.push(inArr[e]);
    }

    return outArr;
}

function truncateString(obj, options) {
    let outVal;

    if (obj[options.field] && obj[options.field].toString().trim().length > options.maxchars) {
        outVal = obj[options.field].trim().substring(0, options.maxchars - 1);
    } else {
        outVal = obj[options.field] ? obj[options.field] : "";
    }

    return outVal.trim();
}

function concatenateStrings(obj, options) {
    let outVal = '';

    for (let i of options.fields) {

        if (obj[i]) {
            outVal += (' ' + obj[i]);
        }

    }

    return outVal.trim();
}

function splitCombineStrings(obj, options) {
    let outVal;

    if (options.delim) {

        if (!obj[options.field]) {
            outVal = '';
            return outVal;
        }

        const dashSplit = obj[options.field].split(options.delim);
        const oa = [];

        for (let y of dashSplit) {
            const splitArr = y.split(".");
            const outArr = splitElements(splitArr, options.combSegs);
            const val = outArr.join(".");
            oa.push(val);
        }

        outVal = oa.join(options.delim);

        return outVal.trim();

    }


    const splitArr = obj[options.field].split(".");
    let outArr = splitElements(splitArr, options.combSegs);

    outVal = outArr.join(".");

    return outVal.trim();

}

function mapToLinkDataTable(obj, options) {

    if (!options || !options.mapLinkData || !options.field) {
        return;
    }

    let outVal;
    let mapArr = require(options.mapLinkData);
    let foundArr = mapArr.filter(fil => { return fil[options.mapField] === obj[options.field]; });

    if (foundArr.length === 0) {
        return obj[options.field];
    }

    outVal = foundArr[0][options.outField];

    return outVal.trim();

}

function roundToLeastInteger(obj, options) {

    if (!options || !options.field) {
        return;
    }

    let outVal;

    outVal = Math.floor(obj[options.field]);

    return outVal;

}

/**
 * Utilities service.
 * @namespace Mappings
 */
module.exports = {

    transformObj(obj, options) {

        if (options && options.default) {
            if (options.defaultVal.toLowerCase() === "null") {
                return null;
            } else {
                return options.defaultVal.trim();
            }
        }

        let myAction = mappingActionArray.filter(fil => { return fil.type === options.type; })[0];

        return myAction.action001(obj, options);
    },

    // transformObjects: function(obj, options = []) {
    //     if (options && options.default) {
    //         return options.defaultVal;
    //     }

    //     if (options && options.fields && options.type === 'concatenate') {
    //         var outVal = '';

    //         for (let i of options.fields) {

    //             if(obj[i]) {
    //                 outVal += (' ' + obj[i]);
    //             }

    //         }

    //         return outVal;
    //     }

    //     if (options && options.type === 'truncate') {
    //         let outVal;

    //         if (obj[options.field] && obj[options.field].toString().length > options.maxchars) {
    //             outVal = obj[options.field].substring(0,options.maxchars - 1);
    //         }

    //         else {
    //             outVal = obj[options.field]

    //         }

    //         return outVal;

    //     }

    //     if (options && options.type === 'splitCombine') {
    //         var outVal;

    //         if (options.delim) {

    //             if (!obj[options.field]) {
    //                 outVal = '';
    //                 return outVal;
    //             }

    //             var dashSplit = obj[options.field].split(options.delim);
    //             var oa = [];

    //             for (let y of dashSplit) {
    //                 var splitArr = y.split(".");
    //                 var outArr = splitElements(splitArr, options.combSegs);
    //                 var val = outArr.join(".");
    //                 oa.push(val);
    //             }

    //             outVal = oa.join(options.delim);

    //             return outVal;

    //         }


    //         var splitArr = obj[options.field].split(".");
    //         var outArr = splitElements(splitArr, options.combSegs);

    //         outVal = outArr.join(".");

    //         return outVal;

    //     }

    //     if (options && options.mapped && options.mapper) {
    //         //use table as map
    //     }

    //     //add maps as needed for new scenarios

    //     else {
    //         return;
    //     }
    // },

    mapIt: function (source, mapObj) {
        const dest = {};

        for (let m of mapObj) {
            dest[m.toVal] = m.transformationObj ? this.transformObj(source, m.transformationObj) : source[m.fromVal] ? source[m.fromVal].toString().trim() : "";
        }
        return dest;
    },

	/**
	 * Remove char from number values.
	 * @param {string} value A value.
	 * @returns {number} The result number.
	 * @memberOf Mappings
	 */
    removeCharFromNumberValues(value) {
        let number = Number(value.replace(/[^\d.-]/g, ''));
        if (isNaN(number)) {
            number = 0;
        }
        return number;
    },

	/**
	 * Converts a string into a ISO Date string.
	 * @param {string} string Date string.
	 * @returns {string} ISO Date string.
	 * @memberOf Mappings
	 */
    stringToISODate(string) {
        try {
            return new Date(string).toISOString();
        } catch (error) {
            console.log('Invalid Date Input:' + string);
            return string;
        }
    },

    formatDate: function (date) {
        const year = date.getFullYear();
        const month = (date.getMonth() < 10 ? '0' + date.getMonth().toString() : date.getMonth().toString());
        const day = (date.getDate() < 10 ? '0' + date.getDate().toString() : date.getDate().toString());
        const hours = (date.getHours() < 10 ? '0' + date.getHours().toString() : date.getHours().toString());
        const minutes = (date.getMinutes() < 10 ? '0' + date.getMinutes().toString() : date.getMinutes().toString());
        const seconds = (date.getSeconds() < 10 ? '0' + date.getSeconds().toString() : date.getSeconds().toString());
        const milliseconds = (date.getMilliseconds() < 10 ? '00' + date.getMilliseconds().toString() : date.getMilliseconds() < 100 ? '0' + date.getMilliseconds().toString() : date.getMilliseconds().toString());

        return year + '/' + month + '/' + day + ' ' + hours + ':' + minutes + ':' + seconds + '.' + milliseconds;
    },

    getCurrentDate: function () {
        const currentDate = new Date(Date.now());
        return this.formatDate(currentDate);
    },

    getCurrentShortDate: function () {
        const currentDate = new Date(Date.now());
        return currentDate.toLocaleDateString();
    },

	/**
	 * Normalize a given CSV replacing wrong chars that are used on the fields to correct ones.
	 * @param {string} csv CSV string.
	 * @returns {string} Normalized CSV String.
	 * @memberOf Mappings
	 */
    normalizeCSV(csv) {
        return csv
            // change wrong single quotes inches to correct double quotes inches
            .replace(/\d'/g, match => {
                return match.replace(`'`, '"');
            })
            // change single quote for real apostrophe
            .replace(/'/g, '’')
            // changing string delimiters to be single quote between fields
            .replace(/","/g, `','`)
            // changing string delimiters from the end of record to be single quote
            .replace(/"\r\n"/g, `'\r\n'`)
            .replace(/"\n"/g, `'\n'`)
            // changing string delimiter from the begining of the record to be single quote
            .replace(/^"/g, `'`)
            // changing string delimiter from the end of the last record to be single quote.
            .replace(/"$/g, `'`)
            .replace(/"\n$/g, `'`)
            .replace(/"\r\n$/g, `'`)
            //remove uneeded line breaks that not follows string delimiter character (')
            .replace(/[^']\n/g, match => {
                return match.replace('\n', ' ');
            })
    }
};