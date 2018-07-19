using MiddleWay_DTO.Enumerations;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
using MiddleWay_Utilities;
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

                        bool isDynamicSourceProperty = false;
                        bool isDynamicDestinationProperty = false;
                        IDictionary<string, object> dynamicInput = null;
                        IDictionary<string, object> dynamicOutput = null;
                        bool hasSourceProperty = false;
                        object sourceValue = null;
                        PropertyInfo destinationProperty = null;
                        bool hasDestinationProperty = false;


                        if (items[0] is IDictionary<string, object>)
                        {
                            isDynamicSourceProperty = true;
                        }

                        U outputItem = new U();
                        if (outputItem is IDictionary<string, object>)
                        {
                            isDynamicDestinationProperty = true;
                        }

                        foreach (var item in items)
                        {
                            try
                            {
                                outputItem = new U();
                                dynamicInput = null;
                                dynamicOutput = null;
                                hasSourceProperty = false;
                                sourceValue = null;
                                destinationProperty = null;
                                hasDestinationProperty = false;

                                dynamicInput = isDynamicSourceProperty ? item as IDictionary<string, object> : null;
                                dynamicOutput = isDynamicDestinationProperty ? outputItem as IDictionary<string, object> : null;

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
                                            //if (item is IDictionary<string, object>)
                                            //{
                                            if (isDynamicSourceProperty)
                                            {
                                                if (dynamicInput.ContainsKey(mapping.SourceColumn))
                                                {
                                                    hasSourceProperty = true;
                                                    sourceValue = dynamicInput[mapping.SourceColumn];
                                                }
                                            }
                                            //}
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
                                        //Log error
                                        if (isDynamicSourceProperty)
                                        {
                                            if (dynamicInput.ContainsKey("Rejected"))
                                            {
                                                dynamicInput["Rejected"] = true;
                                            }
                                            else
                                            {
                                                dynamicInput.Add("Rejected", true);
                                            }
                                            if (dynamicInput.ContainsKey("RejectedNotes"))
                                            {
                                                var notes = dynamicInput["RejectedNotes"];
                                                notes = (notes == null ? string.Empty : $"{notes}\n") + $"Source Property: {mapping.SourceColumn}, Destination Property: {mapping.DestinationColumn}; {(!string.IsNullOrEmpty(ex.Message) ? "Message: {ex.Message}" : Utilities.ParseException(ex))}";
                                                dynamicInput["RejectedNotes"] = notes;
                                            }
                                            else
                                            {
                                                var notes = $"Source Property: {mapping.SourceColumn}, Destination Property: {mapping.DestinationColumn}; {(!string.IsNullOrEmpty(ex.Message) ? "Message: {ex.Message}" : Utilities.ParseException(ex))}";
                                                dynamicInput.Add("RejectedNotes", notes);
                                            }
                                        }
                                        else
                                        {
                                            var rejectedProperty = item.GetType().GetProperty("Rejected");
                                            rejectedProperty.SetValue(item, true);
                                            var rejectedNotesProperty = item.GetType().GetProperty("RejectedNotes");
                                            var notes = rejectedNotesProperty.GetValue(item);
                                            notes = (notes == null ? string.Empty : $"{notes}\n") + $"Source Property: {mapping.SourceColumn}, Destination Property: {mapping.DestinationColumn}; {(!string.IsNullOrEmpty(ex.Message) ? "Message: {ex.Message}" : Utilities.ParseException(ex))}";
                                            rejectedNotesProperty.SetValue(item, notes);
                                        }

                                        continue;
                                    }
                                }

                                outputItems.Add(outputItem);

                            }
                            catch (Exception ex)
                            {
                                //Log error
                                if (isDynamicSourceProperty)
                                {
                                    if (dynamicInput.ContainsKey("Rejected"))
                                    {
                                        dynamicInput["Rejected"] = true;
                                    }
                                    else
                                    {
                                        dynamicInput.Add("Rejected", true);
                                    }
                                    if (dynamicInput.ContainsKey("RejectedNotes"))
                                    {
                                        var notes = dynamicInput["RejectedNotes"];
                                        notes = (notes == null ? string.Empty : $"{notes}\n") + $"{(!string.IsNullOrEmpty(ex.Message) ? $"Message: {ex.Message}" : Utilities.ParseException(ex))}";
                                        dynamicInput["RejectedNotes"] = notes;
                                    }
                                    else
                                    {
                                        var notes = $"{(!string.IsNullOrEmpty(ex.Message) ? $"Message: {ex.Message}" : Utilities.ParseException(ex))}";
                                        dynamicInput.Add("RejectedNotes", notes);
                                    }
                                }
                                else
                                {
                                    var rejectedProperty = item.GetType().GetProperty("Rejected");
                                    rejectedProperty.SetValue(item, true);
                                    var rejectedNotesProperty = item.GetType().GetProperty("RejectedNotes");
                                    var notes = rejectedNotesProperty.GetValue(item);
                                    notes = (notes == null ? string.Empty : $"{notes}\n") + $"{(!string.IsNullOrEmpty(ex.Message) ? $"Message: {ex.Message}" : Utilities.ParseException(ex))}";
                                    rejectedNotesProperty.SetValue(item, notes);
                                }
                                continue;
                            }
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
