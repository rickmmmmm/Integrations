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

        //public List<U> Transform<T, U>(List<T> items) where U : new()
        public List<dynamic> Transform<T>(List<T> items) // where U : new()
        {
            try
            {
                if (items != null && items.Count > 0)
                {
                    var transformations = _transformationsRepository.SelectTransformations(_clientConfiguration.Client, _clientConfiguration.ProcessName);

                    if (transformations != null)
                    {
                        List<dynamic> outputItems = new List<dynamic>();
                        //var typeT = typeof(T);
                        //var typeU = typeof(U);

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

                            //U outputItem = new U();
                            var outputItem = new System.Dynamic.ExpandoObject();

                            foreach (var groupedTransformation in groupedTransformations)
                            {
                                try
                                {
                                    var sourceProperty = item.GetType().GetProperty(groupedTransformation.SourceColumn);
                                    //var destinationProperty = outputItem.GetType().GetProperty(groupedTransformation.DestinationColumn);

                                    bool hasSourceProperty = (sourceProperty != null);
                                    //bool hasDestinationProperty = (destinationProperty != null);

                                    if (hasSourceProperty)//&& hasDestinationProperty
                                    {
                                        var transformationGroup = (from transforms in transformations
                                                                   where transforms.SourceColumn == groupedTransformation.SourceColumn
                                                                   //&& transforms.DestinationColumn == groupedTransformation.DestinationColumn
                                                                   orderby transforms.Order ascending
                                                                   select transforms).ToList();

                                        var sourceType = sourceProperty.GetType();
                                        object value = sourceProperty.GetValue(item);
                                        //object result = value; //Create a copy of the value?
                                        var propertyName = groupedTransformation.SourceColumn;

                                        //For each item, map
                                        foreach (var transformation in transformationGroup)
                                        {
                                            try
                                            {
                                                //var destinationType = destinationProperty.GetType();

                                                // perhaps change this to an object <object, Type> to allow post transformation conversion
                                                //var transformedValue = ApplyTransformation(sourceType, destinationType, transformation.Function, transformation.Parameters, result);
                                                var transformedValue = ApplyTransformation(sourceType, typeof(object), transformation.Function, transformation.Parameters, value);
                                                //if (transformedValue != null)
                                                //{
                                                //    destinationProperty.SetValue(outputItem, transformedValue);
                                                //result = transformedValue;
                                                //}

                                                var expandoDict = outputItem as IDictionary<string, object>;
                                                if (expandoDict.ContainsKey(propertyName))
                                                {
                                                    expandoDict[propertyName] = transformedValue;
                                                }
                                                else
                                                {
                                                    expandoDict.Add(propertyName, transformedValue);
                                                }

                                            }
                                            catch
                                            {
                                                //TODO: log error
                                                break;
                                            }
                                        }

                                        //destinationProperty.SetValue(outputItem, result);
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

        public U ApplyTransformation<T, U>(T inputEntity, U outputEntity, string function, string parameters, T value)
        {
            //try
            //{
            var paramsList = ProcessParameters.SplitParameters(parameters);
            string stringVal = string.Empty;
            int intVal = -1;

            TransformationFunctions functionVal;
            if (Enum.TryParse<TransformationFunctions>(function, out functionVal))
            {
                switch (functionVal)
                {
                    case TransformationFunctions.DEFAULT:// "default":
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
                        intVal = RoundDown<T>(value); // paramsList
                        return QuickCast<U>(intVal);
                    case TransformationFunctions.ROUNDUP:
                        intVal = RoundUp<T>(value); // paramsList
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
            //}
            //catch
            //{
            //    throw;
            //}
        }

        public U QuickCast<T, U>(T value)
        {
            //TODO: Perform a switch statement using the types of input and perform targeted casts
            if (value is U)
            {
                return (U)Convert.ChangeType(value, typeof(U));
            }
            else
            {
                if (value == null)
                {
                    return default(U);
                }
                else
                {
                    return (U)Convert.ChangeType(value.ToString(), typeof(U));
                }
            }
        }

        public T QuickCast<T>(string value)
        {
            //TODO: Perform a switch statement using the types of input and perform targeted casts
            if (value is T)
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            else
            {
                return default(T);
            }
        }

        public T QuickCast<T>(int value)
        {
            //TODO: Perform a switch statement using the types of input and perform targeted casts
            if (value is T)
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            else
            {
                return default(T);
            }
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
                        return QuickCast<U>(parameters[0]);
                    }
                    else
                    {
                        return default(U);
                    }
                }
            }
            else
            {
                if (value == null)
                {
                    if (parameters.Count == 1 && parameters[0] != null)
                    {
                        return QuickCast<U>(parameters[0]);
                    }
                    else
                    {
                        return default(U);
                    }
                }
                else
                {
                    return QuickCast<T, U>(value);
                }
            }
            //}
            //catch
            //{
            //    throw;
            //}
        }

        public string Lookup<T>(T value, List<string> parameters)
        {
            if (parameters != null && parameters.Count == 1)
            {
                try
                {
                    var lookupValue = _transformationLookupService.GetTransformationLookupValue(parameters[0], value.ToString());

                    //if(lookupValue != null)
                    //{
                    return lookupValue;
                    //}
                    //else
                    //{
                    //    return default(U);
                    //}
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
                                if (indexMatch > 0 && indexMatch < splitResult.Length)
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
            if (parameters != null && parameters.Count == 1)
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

        #endregion Get Methods

        //#region Add Methods

        //#endregion Add Methods

        //#region Update Methods

        //#endregion Update Methods

        //#region Delete Methods

        //#endregion Delete Methods
    }
}
