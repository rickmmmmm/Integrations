export function mapSQLError(error: string): string {
    let mappedVal: string = error;

    if (error.indexOf("Conversion failed when converting the nvarchar value") !== -1 && error.indexOf("to data type int") !== -1) {
        mappedVal = "Attempted to insert string or decimal into number field. Check all fields with \"Quantity\".";
    }

    else if (error.indexOf("String or binary data would be truncated") !== -1) {
        mappedVal = "Attempted to add text of greater length than field allows. See BRD for field length requirements.";
    }

    else if (error.indexOf('Violation of PRIMARY KEY') !== -1) {
        mappedVal = 'Attempted to add duplicate Purchase Order. Order Number and line number must be unique in each file.'
    }

    return mappedVal;
}