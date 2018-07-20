﻿using MiddleWay_DTO.Models.MiddleWay;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
using System.Collections.Generic;

namespace MiddleWay_Controller.Services
{
    public class EtlInventoryService : IEtlInventoryService
    {
        #region Private Variables and Properties

        private IEtlInventoryRepository _etlInventoryRepository;
        private IClientConfiguration _clientConfiguration;
        private IProcessesService _processesService;

        #endregion Private Variables and Properties

        #region Constructor

        public EtlInventoryService(IEtlInventoryRepository etlInventoryRepository, IClientConfiguration clientConfiguration,
                                   IProcessesService processesService)
        {
            _etlInventoryRepository = etlInventoryRepository;
            _clientConfiguration = clientConfiguration;
            _processesService = processesService;
        }

        #endregion Constructor

        #region Get Methods

        public EtlInventoryModel Get(int etlInventoryUid)
        {
            try
            {
                return _etlInventoryRepository.Select(etlInventoryUid);
            }
            catch
            {
                throw;
            }
        }

        public EtlInventoryModel GetByTag(string client, string processName, string tag)
        {
            try
            {
                return _etlInventoryRepository.SelectByTag(client, processName, tag);
            }
            catch
            {
                throw;
            }
        }

        public EtlInventoryModel GetByAsset(string client, string processName, string assetId)
        {
            try
            {
                return _etlInventoryRepository.SelectByAssetId(client, processName, assetId);
            }
            catch
            {
                throw;
            }
        }

        //public EtlInventoryModel GetBySerial(string client, string processName, string serial)
        //{
        //    try
        //    {
        //        return _etlInventoryRepository.SelectBySerial(client, processName, serial);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public EtlInventoryModel GetByInventoryId(string client, string processName, int inventoryUid)
        {
            try
            {
                return _etlInventoryRepository.SelectByInventoryUid(client, processName, inventoryUid);
            }
            catch
            {
                throw;
            }
        }

        #endregion Get Methods

        #region Add Methods

        public int Add(EtlInventoryModel item)
        {
            try
            {
                var processUid = _processesService.GetProcessUid();
                item.ProcessUid = processUid;
                return _etlInventoryRepository.Insert(item);
            }
            catch
            {
                throw;
            }
        }

        public bool AddRange(List<EtlInventoryModel> items)
        {
            try
            {
                var processUid = _processesService.GetProcessUid();
                items.ForEach(row => row.ProcessUid = processUid);
                return _etlInventoryRepository.InsertRange(items);
                //TODO: Log count of items saved?
            }
            catch
            {
                throw;
            }
        }

        #endregion Add Methods

        #region Change Methods

        public bool Edit(EtlInventoryModel item)
        {
            try
            {
                var processUid = _processesService.GetProcessUid();
                item.ProcessUid = processUid;
                return _etlInventoryRepository.Update(item);
            }
            catch
            {
                throw;
            }
        }

        public bool EditRange(List<EtlInventoryModel> items)
        {
            try
            {
                var processUid = _processesService.GetProcessUid();
                items.ForEach(x => x.ProcessUid = processUid);
                return _etlInventoryRepository.UpdateRange(items);
            }
            catch
            {
                throw;
            }
        }

        public bool ValidateEtlInventory()
        {
            try
            {
                var processUid = _processesService.GetProcessUid();

                //Validate Tag/Asset/Serial
                //TODO: Get Configuration for use Tag in Notes

                //Validate Product Name
                //TODO: Get Configuration for Create Products

                //Validate Product Type
                //TODO: Get Configuration for Default Product Type

                //Validate Custom Fields

                //Validate Manufacturers
                //TODO: Get Configuration for Create Manufacturers

                //Validate Funding Sources
                //TODO: Get Configuration for Create Funding Sources

                //Validate Vendor
                //TODO: Get Configuration for Create Vendors

                //Validate EntityType

                //Validate Entity (EntityName/EntityID)

                //Validate Status

                //Validate Department

                //Validate Purchase Order (OrderNumber, LineNumber, AccountCode, PurchasePrice, PurchaseDate)
                //TODO: Get Configuration for Create Purchase Order
                //TODO: Get Configuration for Create Purchase Details
                //TODO: Get Configuration for Default Line Number

                //Validate Invoice Data
                //TODO: Get Configuration for Invoice For Shipment or Order....????

                //Validate Parent Tag

                //TODO: Log count of items saved?

                return false;
            }
            catch
            {
                throw;
            }
        }

        public bool SubmitEtlInventory()
        {
            try
            {
                var processUid = _processesService.GetProcessUid();

                //Submit Products

                //Submit Manufacturers

                //Submit Funding Sources

                //Submit Vendors

                //Submit Inventory

                //Submit Custom Fields

                //Submit Parent Data

                //Submit Purchase Orders

                //Submit Purchase Details

                //Submit Purchase Shipments

                //Submit Purchase Inventory

                //Submit Invoice Data

                //TODO: Log count of items saved?

                return false;
            }
            catch
            {
                throw;
            }
        }

        #endregion Change Methods

        #region Delete Methods

        public bool Remove(int etlInventoryUid)
        {
            try
            {
                return _etlInventoryRepository.Delete(etlInventoryUid);
            }
            catch
            {
                throw;
            }
        }

        #endregion Delete Methods
    }
}
