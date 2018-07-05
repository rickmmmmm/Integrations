using System;
using System.Collections.Generic;
using System.Text;
//using CsvHelper;
//using CsvHelper.Configuration;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;

namespace MiddleWay_Controller.Services
{
    public class MappingsService : IMappingsService
    {
        //}

        //public sealed class PurchaseOrderClassMap : ClassMap<PurchaseOrderFile>
        //{

        #region Private Variables and Properties

        private IMappingsRepository _mappingsRepository;
        private IClientConfiguration _clientConfiguration;

        #endregion Private Variables and Properties

        #region Constructor

        //public MappingsService() //PurchaseOrderClassMap() //Convert to a more generic format
        //{
        //    //Map(u => u.OrderNumber).Name(ConfigurationManager.AppSettings["OrderNumber"]);
        //    //Map(u => u.OrderDate).Name(ConfigurationManager.AppSettings["OrderDate"]);
        //    //Map(u => u.VendorName).Name(ConfigurationManager.AppSettings["VendorName"]);
        //    //Map(u => u.ProductName).Name(ConfigurationManager.AppSettings["ProductName"]);
        //    //Map(u => u.Description).Name(ConfigurationManager.AppSettings["Description"]);
        //    //Map(u => u.ProductType).Name(ConfigurationManager.AppSettings["ProductType"]);
        //    //Map(u => u.Model).Name(ConfigurationManager.AppSettings["Model"]);
        //    //Map(u => u.Manufacturer).Name(ConfigurationManager.AppSettings["Manufacturer"]);
        //    //Map(u => u.Quantity).Name(ConfigurationManager.AppSettings["Quantity"]);
        //    //Map(u => u.PurchasePrice).Name(ConfigurationManager.AppSettings["PurchasePrice"]);
        //    //Map(u => u.FundingSource).Name(ConfigurationManager.AppSettings["FundingSource"]);
        //    //Map(u => u.AccountCode).Name(ConfigurationManager.AppSettings["AccountCode"]);
        //    //Map(u => u.LineNumber).Name(ConfigurationManager.AppSettings["LineNumber"]);
        //    //Map(u => u.ShippedToSite).Name(ConfigurationManager.AppSettings["ShippedToSite"]);
        //    //Map(u => u.QuantityShipped).Name(ConfigurationManager.AppSettings["QuantityShipped"]);
        //    //Map(u => u.Notes).Name(ConfigurationManager.AppSettings["Notes"]);
        //}

        public MappingsService(IMappingsRepository mappingsRepository, IClientConfiguration clientConfiguration)
        {
            _mappingsRepository = mappingsRepository;
            _clientConfiguration = clientConfiguration;
        }

        #endregion Constructor

        #region Get Methods

        public List<U> Map<T, U>(List<T> items) where U : new()
        {
            try
            {
                if (items != null && items.Count > 0)
                {
                    var mappings = _mappingsRepository.SelectMappings(_clientConfiguration.Client, _clientConfiguration.ProcessName);

                    if (mappings != null)
                    {
                        List<U> outputItems = new List<U>();

                        foreach (var item in items)
                        {

                            U outputItem = new U();

                            foreach (var mapping in mappings)
                            {
                                try
                                {
                                    var sourceProperty = item.GetType().GetProperty(mapping.SourceColumn);
                                    var destinationProperty = outputItem.GetType().GetProperty(mapping.DestinationColumn);

                                    bool hasSourceProperty = (sourceProperty != null);
                                    bool hasDestinationProperty = (destinationProperty != null);

                                    //var sourceType = sourceProperty.GetType();
                                    //var destinationType = destinationProperty.GetType();

                                    if (hasSourceProperty && hasDestinationProperty)
                                    {
                                        var value = sourceProperty.GetValue(item);

                                        destinationProperty.SetValue(outputItem, value);
                                    }
                                }
                                catch
                                {
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
