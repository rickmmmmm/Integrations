var fs = require('fs');

function splitElements(inArr, elements) {
    var outArr = [];

    for (let e of elements) {
        outArr.push(inArr[e]);
    }

    return outArr;
}

module.exports = {
    
    transformObjects: function(obj, options = []) {
        if (options && options.default) {
            return options.defaultVal;
        }

        if (options && options.fields && options.type === 'concatenate') {
            var outVal = '';

            for (let i of options.fields) {

                if(obj[i]) {
                    outVal += (' ' + obj[i]);
                }
                
            }

            return outVal;
        }

        if (options && options.type === 'truncate') {
            let outVal;

            if (obj[options.field] && obj[options.field].toString().length > options.maxchars) {
                outVal = obj[options.field].substring(0,options.maxchars - 1);
            }

            else {
                outVal = obj[options.field]
                
            }

            return outVal;
            
        }

        if (options && options.type === 'splitCombine') {
            var outVal;

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

                return outVal;

            }
            

            var splitArr = obj[options.field].split(".");
            var outArr = splitElements(splitArr, options.combSegs);

            outVal = outArr.join(".");

            return outVal;

        }

        if (options && options.mapped && options.mapper) {
            //use table as map
        }

        //add maps as needed for new scenarios

        else {
            return;
        }
    },

    mapIt: function(source, mapObj) {
        var dest = {};
        
        for (let m of mapObj){
            dest[m.toVal] = m.transformationObj ? this.transformObjects(source, m.transformationObj) : source[m.fromVal];
        }
        return dest;
    }
}