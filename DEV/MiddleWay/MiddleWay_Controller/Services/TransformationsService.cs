using MiddleWay_DTO.Enumerations;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
using MiddleWay_Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiddleWay_Controller.Services
{
    public class TransformationsService : ITransformationsService
    {
        #region Private Variables and Properties

        private ITransformationsRepository _transformationsRepository;
        private IClientConfiguration _clientConfiguration;
        private ITransformationLookupService _transformationLookupService;

        #endregion Private Variables and Properties

        #region Constructor

        public TransformationsService(ITransformationsRepository transformationsRepository, IClientConfiguration clientConfiguration,
                                      ITransformationLookupService transformationLookupService)
        {
            _transformationsRepository = transformationsRepository;
            _clientConfiguration = clientConfiguration;
            _transformationLookupService = transformationLookupService;
        }

        #endregion Constructor

        #region Get Methods

        public dynamic Transform<T>(T item, ProcessSteps stepName)
        {
            var itemList = Transform<T>(new List<T> { item }, stepName);
            if (itemList != null && itemList.Count == 1)
            {
                return itemList[0];
            }
            else
            {
                return null;
            }
        }

        public List<dynamic> Transform<T>(List<T> items, ProcessSteps stepName)
        {
            try
            {
                if (items != null && items.Count > 0)
                {
                    var transformations = _transformationsRepository.SelectTransformations(_clientConfiguration.Client, _clientConfiguration.ProcessName, stepName);

                    if (transformations != null)
                    {
                        List<dynamic> outputItems = new List<dynamic>();

                        foreach (var item in items)
                        {
                            var groupedTransformations = (from transform in transformations
                                                          group transform by new { transform.SourceColumn, transform.DestinationColumn } into tranformGroup
                                                          select new
                                                          {
                                                              tranformGroup.Key.SourceColumn //,
                                                              //tranformGroup.Key.DestinationColumn
                                                          }).ToList();

                            var outputProperties = (from transforms in transformations
                                                    group transforms by transforms.DestinationColumn into destinationTransforms
                                                    select destinationTransforms.Key).ToList();

                            var outputItem = new System.Dynamic.ExpandoObject();
                            var expandoDict = outputItem as IDictionary<string, object>;

                            foreach (var groupedTransformation in groupedTransformations)
                            {
                                try
                                {

                                    var sourceProperty = item.GetType().GetProperty(groupedTransformation.SourceColumn);

                                    bool hasSourceProperty = (sourceProperty != null);

                                    if (hasSourceProperty)
                                    {
                                        var transformationGroup = (from transforms in transformations
                                                                   where transforms.SourceColumn == groupedTransformation.SourceColumn
                                                                   orderby transforms.Order ascending
                                                                   select transforms).ToList();

                                        var sourceType = sourceProperty.GetType();
                                        object value = sourceProperty.GetValue(item);

                                        //For each item, map
                                        foreach (var transformation in transformationGroup)
                                        {
                                            try
                                            {
                                                string propertyName;
                                                Type destinationType;

                                                if (!string.IsNullOrEmpty(transformation.DestinationColumn))
                                                {
                                                    propertyName = transformation.DestinationColumn;
                                                }
                                                else
                                                {
                                                    propertyName = groupedTransformation.SourceColumn;
                                                }

                                                if (expandoDict.ContainsKey(propertyName))
                                                {
                                                    var val = expandoDict[propertyName];
                                                    destinationType = val.GetType();
                                                }
                                                else
                                                {
                                                    destinationType = sourceType;
                                                }

                                                // perhaps change this to an object <object, Type> to allow post transformation conversion

                                                var transformedValue = ApplyTransformation(transformation.Function, transformation.Parameters, value);

                                                if (expandoDict.ContainsKey(propertyName))
                                                {
                                                    expandoDict[propertyName] = transformedValue;
                                                }
                                                else
                                                {
                                                    expandoDict.Add(propertyName, transformedValue);
                                                }

                                            }
                                            catch (Exception ex)
                                            {
                                                //TODO: log error
                                                Console.WriteLine(Utilities.ParseException(ex));
                                                break;
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                    //TODO: log error
                                    continue;
                                }
                            }

                            outputItems.Add(outputItem);

                        }

                        return outputItems;
                    }
                    else
                    {
                        //TODO: Log message indicating no transformations
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                //TODO: log error
                throw;
            }
        }

        //public void ApplyTransformation<T, U>(string function, string parameters, T inputValue, out U outputValue)
        //{
        //    //try
        //    //{
        //    var paramsList = ProcessParameters.SplitParameters(parameters);
        //    string stringVal = string.Empty;
        //    int intVal = -1;

        //    TransformationFunctions functionVal;
        //    if (Enum.TryParse<TransformationFunctions>(function, true, out functionVal))
        //    {
        //        switch (functionVal)
        //        {
        //            case TransformationFunctions.DEFAULT:// "default":
        //                outputValue = Default<T, U>(inputValue, paramsList);
        //                break;
        //            case TransformationFunctions.LOOKUP:
        //                stringVal = Lookup(inputValue, paramsList);
        //                outputValue = QuickCast<U>(stringVal);
        //                break;
        //            case TransformationFunctions.SPLIT:
        //                stringVal = Split(inputValue, paramsList);
        //                outputValue = QuickCast<U>(stringVal);
        //                break;
        //            case TransformationFunctions.TRUNCATE:
        //                stringVal = Truncate(inputValue, paramsList);
        //                outputValue = QuickCast<U>(stringVal);
        //                break;
        //            //case "cast":
        //            //    return Cast<T, U>(value, paramsList);
        //            //    break;
        //            case TransformationFunctions.ROUNDDOWN:
        //                intVal = RoundDown<T>(inputValue); // paramsList
        //                outputValue = QuickCast<U>(intVal);
        //                break;
        //            case TransformationFunctions.ROUNDUP:
        //                intVal = RoundUp<T>(inputValue); // paramsList
        //                outputValue = QuickCast<U>(intVal);
        //                break;
        //            //case "concatenate":
        //            //    return Concatenate();
        //            //    break;
        //            default:
        //                outputValue = QuickCast<T, U>(inputValue);
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        outputValue = QuickCast<T, U>(inputValue);
        //    }
        //    //}
        //    //catch
        //    //{
        //    //    throw;
        //    //}
        //}

        public object ApplyTransformation(string function, string parameters, string value)
        {
            var output = new object();

            return ApplyTransformation<string, object>(value, output, function, parameters, value);
        }

        public object ApplyTransformation(string function, string parameters, int value)
        {
            var output = new object();

            return ApplyTransformation<int, object>(value, output, function, parameters, value);
        }

        public object ApplyTransformation(string function, string parameters, int? value)
        {
            var output = new object();

            return ApplyTransformation<int?, object>(value, output, function, parameters, value);
        }

        public object ApplyTransformation(string function, string parameters, double value)
        {
            var output = new object();

            return ApplyTransformation<double, object>(value, output, function, parameters, value);
        }

        public object ApplyTransformation(string function, string parameters, double? value)
        {
            var output = new object();

            return ApplyTransformation<double?, object>(value, output, function, parameters, value);
        }

        public object ApplyTransformation(string function, string parameters, bool value)
        {
            var output = new object();

            return ApplyTransformation<bool, object>(value, output, function, parameters, value);
        }

        public object ApplyTransformation(string function, string parameters, bool? value)
        {
            var output = new object();

            return ApplyTransformation<bool?, object>(value, output, function, parameters, value);
        }

        public object ApplyTransformation(string function, string parameters, object value)
        {
            var output = new object();

            return ApplyTransformation<object, object>(value, output, function, parameters, value);
        }

        public U ApplyTransformation<T, U>(T inputEntity, U outputEntity, string function, string parameters, T value)
        {
            var paramsList = ProcessParameters.SplitParameters(parameters);
            string stringVal = string.Empty;
            int intVal = -1;

            TransformationFunctions functionVal;
            if (Enum.TryParse<TransformationFunctions>(function, true, out functionVal))
            {
                switch (functionVal)
                {
                    case TransformationFunctions.DEFAULT:
                        return Default<T, U>(value, paramsList);
                    case TransformationFunctions.LOOKUP:
                        stringVal = Lookup(value, paramsList);
                        return QuickCast<U>(stringVal);
                    case TransformationFunctions.SPLIT:
                        stringVal = Split(value, paramsList);
                        return QuickCast<U>(stringVal);
                    case TransformationFunctions.TRUNCATE:
                        stringVal = Truncate(value, paramsList);
                        return QuickCast<U>(stringVal);
                    //case "cast":
                    //    return Cast<T, U>(value, paramsList);
                    case TransformationFunctions.ROUNDDOWN:
                        intVal = RoundDown<T>(value);
                        return QuickCast<U>(intVal);
                    case TransformationFunctions.ROUNDUP:
                        intVal = RoundUp<T>(value);
                        return QuickCast<U>(intVal);
                    //case "concatenate":
                    //    return Concatenate();
                    default:
                        return QuickCast<T, U>(value);
                }
            }
            else
            {
                return QuickCast<T, U>(value);
            }
        }

        public U QuickCast<T, U>(T value)
        {
            //TODO: Perform a switch statement using the types of input and perform targeted casts
            if (value == null)
            {
                return default(U);
            }
            else
            {
                Type outputType = null;

                outputType = typeof(U);
                if (Nullable.GetUnderlyingType(outputType) != null)
                {
                    outputType = Nullable.GetUnderlyingType(outputType);
                }

                try
                {
                    switch (outputType.Name)
                    {
                        case "Byte": // int
                            return (U)Convert.ChangeType(Convert.ToByte(value), outputType);
                        case "Char": // int
                            return (U)Convert.ChangeType(Convert.ToChar(value), outputType);
                        case "Int16": // int
                            return (U)Convert.ChangeType(Convert.ToInt16(value), outputType);
                        case "Int32": // int
                            return (U)Convert.ChangeType(Convert.ToInt32(value), outputType);
                        case "Int64": // long
                            return (U)Convert.ChangeType(Convert.ToInt64(value), outputType);
                        case "Boolean": //bool
                            return (U)Convert.ChangeType(Convert.ToBoolean(value), outputType);
                        case "Single": //float
                            return (U)Convert.ChangeType(Convert.ToSingle(value), outputType);
                        case "Double": // double
                            return (U)Convert.ChangeType(Convert.ToDouble(value), outputType);
                        case "Decimal": // decimal
                            return (U)Convert.ChangeType(Convert.ToDecimal(value), outputType);
                        case "String": // string
                            return (U)Convert.ChangeType(Convert.ToString(value), outputType);
                        case "DateTime": // DateTime
                            return (U)Convert.ChangeType(Convert.ToDateTime(value), outputType);
                        case "Object": // object
                            return (U)Convert.ChangeType(value, outputType);
                        default:
                            return (U)Convert.ChangeType(value, typeof(U));
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        //public void QuickCast<T, U>(T value, out U destination)
        //{
        //    destination = QuickCast<T, U>(value);
        //    //if (value is U)
        //    //{
        //    //    destination = (U)Convert.ChangeType(value, typeof(U));
        //    //}
        //    //else
        //    //{
        //    //    if (value == null)
        //    //    {
        //    //        destination = default(U);
        //    //    }
        //    //    else
        //    //    {
        //    //        destination = (U)Convert.ChangeType(value.ToString(), typeof(U));
        //    //    }
        //    //}
        //}

        /// <summary>
        /// This function takes the type passed as a parameter to determine which QuickCast
        /// variation to run and attempts to perform a QuickCast based on this considering the 
        /// possibility of the type being nullable and returning null when the input value
        /// cannot be converted
        /// </summary>
        /// <typeparam name="T">The dynamic type of hte input object</typeparam>
        /// <param name="value">Value to Cast</param>
        /// <param name="destinationType">The Type of the output to return (boxed in an object)</param>
        /// <returns></returns>
        public object QuickCast<T>(T value, Type destinationType)
        {
            var outputTypeName = string.Empty;
            bool isNullable = false;

            if (destinationType.Name == "String")
            {
                outputTypeName = "String";
            }
            else
            {
                var objectType = destinationType;
                if (Nullable.GetUnderlyingType(objectType) != null)
                {
                    objectType = Nullable.GetUnderlyingType(objectType);
                    isNullable = true;
                    outputTypeName = objectType.Name;
                }
                else
                {
                    outputTypeName = objectType.Name;
                }
            }

            try
            {
                switch (outputTypeName)
                {
                    case "Byte": // int
                        return QuickCast<T, Byte>(value);
                    case "Char": // int
                        return QuickCast<T, Char>(value);
                    case "Int16": // int
                        return QuickCast<T, Int16>(value);
                    case "Int32": // int
                        return QuickCast<T, Int32>(value);
                    case "Int64": // long
                        return QuickCast<T, Int64>(value);
                    case "Boolean": //bool
                        return QuickCast<T, Boolean>(value);
                    case "Single": //float
                        return QuickCast<T, Single>(value);
                    case "Double": // double
                        return QuickCast<T, Double>(value);
                    case "Decimal": // decimal
                        return QuickCast<T, Decimal>(value);
                    case "String": // string
                        return QuickCast<T, String>(value);
                    case "DateTime": // DateTime
                        return QuickCast<T, DateTime>(value);
                    case "Object": // object
                                   //return QuickCast < T, Object > (value);
                    default:
                        return QuickCast<T, Object>(value);
                }
            }
            catch
            {
                if (isNullable)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        public T QuickCast<T>(string value)
        {
            return QuickCast<string, T>(value);
        }

        public T QuickCast<T>(int value)
        {
            return QuickCast<int, T>(value);
        }

        public T QuickCast<T>(double value)
        {
            return QuickCast<double, T>(value);
        }

        public T QuickCast<T>(decimal value)
        {
            return QuickCast<decimal, T>(value);
        }

        public T QuickCast<T>(bool value)
        {
            return QuickCast<bool, T>(value);
        }

        public T QuickCast<T>(DateTime value)
        {
            return QuickCast<DateTime, T>(value);
        }

        public U Default<T, U>(T value, List<string> parameters)
        {
            //try
            //{
            if (parameters != null)
            {
                if (value != null)
                {
                    return QuickCast<T, U>(value);
                }
                else
                {
                    if (parameters.Count == 1 && parameters[0] != null)
                    {
                        var defaultCast = QuickCast<T>(parameters[0]); // Pre-cast the parameter to the input type
                        return QuickCast<T, U>(defaultCast);
                    }
                    else
                    {
                        return default(U);
                    }
                }
            }
            else
            {
                return QuickCast<T, U>(value);
            }
        }

        public string Lookup<T>(T value, List<string> parameters)
        {
            if (parameters != null && parameters.Count == 1 && parameters[0] != null)
            {
                try
                {
                    var lookupValue = _transformationLookupService.GetTransformationLookupValue(parameters[0], value.ToString());

                    return lookupValue;

                }
                catch
                {
                    throw;
                }
            }
            else
            {
                throw new ArgumentNullException("Parameters for Lookup are null or empty, lookup cannot be performed.");
            }
        }

        /// <summary>
        /// Return the first index of a split defined by the delimiter in parameters or
        /// a concatenated list of the indexes provided in the parameters after the split
        /// </summary>
        /// <typeparam name="T">The Type of the input value</typeparam>
        /// <param name="value">value that will be split</param>
        /// <param name="parameters">List of parameters to use in splitting the value
        ///     parameters must be in the format [delimiter] or [delimiter][indices]</param>
        /// <returns></returns>
        public string Split<T>(T value, List<string> parameters)
        {
            string[] splitResult;

            if (parameters != null && (parameters.Count == 1 || parameters.Count == 2))
            {
                var delimiter = parameters[0];

                if (!string.IsNullOrEmpty(delimiter))
                {
                    if (parameters.Count > 1)
                    {
                        var indicesString = parameters[1];
                        var indicesArray = indicesString.Split(',', StringSplitOptions.RemoveEmptyEntries); // indicesString.Split(',').ToList().ForEach(cast => { int result; Int32.TryParse(cast, out result) ? return result : -1; });

                        if (indicesArray.Length > 0)
                        {
                            var indices = new List<int>();

                            // Extract the indices to recover from the split
                            for (var i = 0; i < indicesArray.Length; i++)
                            {
                                if (Int32.TryParse(indicesArray[i], out int index))
                                {
                                    indices.Add(index);
                                }
                            }

                            splitResult = value.ToString().Split(delimiter, StringSplitOptions.None); // Keeping all sections of the input because of the indices to keep must match
                            var result = new StringBuilder();

                            // Traverse the indices to recover and concatenate the split items matched
                            foreach (var indexMatch in indices)
                            {
                                if (indexMatch >= 0 && indexMatch < splitResult.Length)
                                {
                                    result.Append(splitResult[indexMatch]);
                                }
                            }

                            return result.ToString();

                        }
                        else
                        {
                            throw new ArgumentException("Indices (Second) parameters for Split is null or empty, Split cannot be performed.");
                        }
                    }
                    else //parameters.Count == 1
                    {
                        splitResult = value.ToString().Split(delimiter, StringSplitOptions.None); // Keeping all sections of the input because of the indices to keep must match
                        return splitResult[0];
                    }
                }
                else
                {
                    throw new ArgumentException("Delimiter (First) parameters for Split is null or empty, Split cannot be performed.");
                }

            }
            else
            {
                throw new ArgumentNullException("Parameters for Split are null or empty, Split cannot be performed.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">The Type of the input value</typeparam>
        /// <param name="value">value to be truncated</param>
        /// <param name="parameters">the number of characters to truncate at</param>
        /// <returns></returns>
        public string Truncate<T>(T value, List<string> parameters)
        {
            if (parameters != null && parameters.Count == 1 && parameters[0] != null)
            {
                try
                {
                    int limit = -1;
                    if (Int32.TryParse(parameters[0], out limit))
                    {
                        if (limit > 0)
                        {
                            string result = value.ToString();
                            if (result.Length > limit)
                            {
                                return result.Substring(0, limit);
                            }
                            else
                            {
                                return result;
                            }
                        }
                        else
                        {
                            return value.ToString();
                        }
                    }
                    else
                    {
                        return value.ToString();
                    }
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                throw new ArgumentNullException("Parameters for Truncate are null or empty, Truncate cannot be performed.");
            }
        }

        //private U Cast<T, U>(T value, List<string> parameters)
        //{
        //    if (parameters != null && parameters.Count == 1)
        //    {
        //        try
        //        {

        //        }
        //        catch
        //        {
        //            throw;
        //        }
        //    }
        //    else
        //    {
        //        throw new ArgumentNullException("Parameters for Cast are null or empty, Cast cannot be performed.");
        //    }
        //}

        public int RoundDown<T>(T value) //, List<string> parameters
        {
            //if (parameters != null && parameters.Count == 1)
            //{
            if (value != null)
            {
                if (Utilities.IsANumber(value.ToString()))
                {
                    //try
                    //{
                    //Cast value to string then rounddown
                    var val = decimal.Parse(value.ToString());

                    return Convert.ToInt32(Math.Floor(val));
                    //}
                    //catch
                    //{
                    //    throw;
                    //}
                }
                else
                {
                    throw new ArgumentException("Value is not a Number.");
                }
                //}
                //else
                //{
                //    throw new ArgumentNullException("Parameters for RoundDown are null or empty, RoundDown cannot be performed.");
                //}
            }
            else
            {
                throw new ArgumentNullException("Value cannot be null.");
            }
        }

        public int RoundUp<T>(T value) //, List<string> parameters
        {
            //if (parameters != null && parameters.Count == 1)
            //{
            if (value != null)
            {
                if (Utilities.IsANumber(value.ToString()))
                {
                    //try
                    //{
                    //Cast value to string then rounddown
                    var val = decimal.Parse(value.ToString());

                    return Convert.ToInt32(Math.Ceiling(val));
                    //}
                    //catch
                    //{
                    //    throw;
                    //}
                }
                else
                {
                    throw new ArgumentException("Value is not a Number.");
                }
                //}
                //else
                //{
                //    throw new ArgumentNullException("Parameters for RoundDown are null or empty, RoundDown cannot be performed.");
                //}
            }
            else
            {
                throw new ArgumentNullException("Value cannot be null."); // return 0;
            }
        }

        //public object GetOutputVariable(System.Reflection.PropertyInfo destinationProperty)
        //{
        //    var objectType = destinationProperty.PropertyType;
        //    bool isNullable = false;
        //    if (Nullable.GetUnderlyingType(objectType) != null)
        //    {
        //        objectType = Nullable.GetUnderlyingType(objectType);
        //        isNullable = true;
        //    }

        //    if (objectType.Name == "String")
        //    {
        //        return (object)string.Empty;
        //    }
        //    else
        //    {
        //        if (isNullable)
        //        {
        //            var nullable = typeof(Nullable<>).MakeGenericType(objectType);
        //            return Activator.CreateInstance(nullable);
        //        }
        //        else
        //        {
        //            return Activator.CreateInstance(objectType);
        //        }
        //    }
        //}

        #endregion Get Methods

        //#region Add Methods

        //#endregion Add Methods

        //#region Update Methods

        //#endregion Update Methods

        //#region Delete Methods

        //#endregion Delete Methods
    }
}
