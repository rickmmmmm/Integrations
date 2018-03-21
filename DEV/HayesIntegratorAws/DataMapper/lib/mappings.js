var fs = require('fs');

var mappingActionArray = [
    { type: 'truncate', action001: truncateString },
    { type: 'concatenate', action001: concatenateStrings },
    { type: 'splitCombine', action001: splitCombineStrings },
    { type: 'linkMap', action001: mapToLinkDataTable },
    { type: 'roundDown', action001: roundToLeastInteger }
    //{ type: 'default', action001: selectDefaultValue }
]

function splitElements(inArr, elements) {
    var outArr = [];

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
    var outVal = '';

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

        var dashSplit = obj[options.field].split(options.delim);
        var oa = [];

        for (let y of dashSplit) {
            var splitArr = y.split(".");
            var outArr = splitElements(splitArr, options.combSegs);
            var val = outArr.join(".");
            oa.push(val);
        }

        outVal = oa.join(options.delim);

        return outVal.trim();

    }


    var splitArr = obj[options.field].split(".");
    var outArr = splitElements(splitArr, options.combSegs);

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

module.exports = {

    transformObj(obj, options) {

        if (options && options.default) {
            return options.defaultVal.trim();
        }

        let myAction = mappingActionArray.filter(fil => { return fil.type === options.type; })[0];

        let outVal = myAction.action001(obj, options);

        return outVal;
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
        var dest = {};

        for (let m of mapObj) {
            dest[m.toVal] = m.transformationObj ? this.transformObj(source, m.transformationObj) : source[m.fromVal] ? source[m.fromVal].toString().trim() : "";
        }
        return dest;
    }
}