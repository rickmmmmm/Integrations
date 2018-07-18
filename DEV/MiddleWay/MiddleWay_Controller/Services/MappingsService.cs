using MiddleWay_DTO.Enumerations;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MiddleWay_Controller.Services
{
    public class MappingsService : IMappingsService
    {

        #region Private Variables and Properties

        private IMappingsRepository _mappingsRepository;
        private IClientConfiguration _clientConfiguration;
        private ITransformationsService _transformationsService;

        #endregion Private Variables and Properties

        #region Constructor

        public MappingsService(IMappingsRepository mappingsRepository, IClientConfiguration clientConfiguration, ITransformationsService transformationsService)
        {
            _mappingsRepository = mappingsRepository;
            _clientConfiguration = clientConfiguration;
            _transformationsService = transformationsService;
        }

        #endregion Constructor

        #region Get Methods

        public bool HasMappings(ProcessSteps stepName)
        {
            return _mappingsRepository.HasMappings(_clientConfiguration.Client, _clientConfiguration.ProcessName, stepName);
        }

        public U Map<T, U>(T item, ProcessSteps stepName) where U : new()
        {
            var itemList = Map<T, U>(new List<T> { item }, stepName);
            if (itemList != null && itemList.Count == 1)
            {
                return itemList[0];
            }
            else
            {
                return default(U);
            }
        }

        public List<U> Map<T, U>(List<T> items, ProcessSteps stepName) where U : new()
        {
            try
            {
                if (items != null && items.Count > 0)
                {
                    var mappings = _mappingsRepository.SelectMappings(_clientConfiguration.Client, _clientConfiguration.ProcessName, stepName);

                    if (mappings != null)
                    {
                        List<U> outputItems = new List<U>();

                        foreach (var item in items)
                        {
                            U outputItem = new U();
                            IDictionary<string, object> dynamicInput = null;
                            IDictionary<string, object> dynamicOutput = null;
                            bool isDynamicSourceProperty = false;
                            bool isDynamicDestinationProperty = false;
                            bool hasSourceProperty = false;
                            object sourceValue = null;
                            PropertyInfo destinationProperty = null;
                            bool hasDestinationProperty = false;

                            if (item is IDictionary<string, object>)
                            {
                                isDynamicSourceProperty = true;
                                dynamicInput = item as IDictionary<string, object>;
                            }

                            if (outputItem is IDictionary<string, object>)
                            {
                                dynamicOutput = outputItem as IDictionary<string, object>;
                                isDynamicDestinationProperty = true;
                            }

                            foreach (var mapping in mappings)
                            {
                                try
                                {
                                    hasSourceProperty = false;
                                    sourceValue = null;
                                    destinationProperty = null;
                                    hasDestinationProperty = false;

                                    if (!string.IsNullOrEmpty(mapping.SourceColumn))
                                    {
                                        if (item is IDictionary<string, object>)
                                        {
                                            if (isDynamicSourceProperty)
                                            {
                                                if (dynamicInput.ContainsKey(mapping.SourceColumn))
                                                {
                                                    hasSourceProperty = true;
                                                    sourceValue = dynamicInput[mapping.SourceColumn];
                                                }
                                            }
                                        }
                                        else
                                        {
                                            var sourceProperty = item.GetType().GetProperty(mapping.SourceColumn);
                                            hasSourceProperty = (sourceProperty != null);
                                            sourceValue = sourceProperty.GetValue(item);
                                        }
                                    }
                                    else
                                    {
                                        hasSourceProperty = true;
                                        sourceValue = null;
                                    }

                                    if (isDynamicDestinationProperty)
                                    {
                                        hasDestinationProperty = dynamicOutput.ContainsKey(mapping.DestinationColumn);
                                    }
                                    else
                                    {
                                        destinationProperty = outputItem.GetType().GetProperty(mapping.DestinationColumn);
                                        hasDestinationProperty = (destinationProperty != null);
                                    }

                                    if (isDynamicDestinationProperty)
                                    {
                                        if (hasDestinationProperty)
                                        {
                                            dynamicOutput[mapping.DestinationColumn] = sourceValue;
                                        }
                                        else
                                        {
                                            dynamicOutput.Add(mapping.DestinationColumn, sourceValue);
                                        }
                                    }
                                    else
                                    {
                                        if (hasDestinationProperty)
                                        {
                                            if (!string.IsNullOrEmpty(mapping.SourceColumn) && hasSourceProperty)
                                            {
                                                var outputValue = _transformationsService.QuickCast(sourceValue, destinationProperty.PropertyType);
                                                destinationProperty.SetValue(outputItem, outputValue);
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    System.Diagnostics.Debug.WriteLine(MiddleWay_Utilities.Utilities.ParseException(ex));
                                    //Log error
                                    continue;
                                }
                            }

                            outputItems.Add(outputItem);

                        }

                        return outputItems;
                    }
                    else
                    {
                        //TODO: Log message indicating no mappings
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
                throw;
            }
        }

        #endregion Get Methods

        #region Add Methods

        #endregion Add Methods

        #region Update Methods

        #endregion Update Methods

        #region Delete Methods

        #endregion Delete Methods
    }
}
