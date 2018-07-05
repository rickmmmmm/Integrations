using MiddleWay_DTO.Models.MiddleWay;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.Services
{
    public class EtlInventoryService : IEtlInventoryService
    {
        #region Private Variables and Properties

        private IEtlInventoryRepository _etlInventoryRepository;
        private IClientConfiguration _clientConfiguration;

        #endregion Private Variables and Properties

        #region Constructor

        public EtlInventoryService(IEtlInventoryRepository etlInventoryRepository, IClientConfiguration clientConfiguration)
        {
            _etlInventoryRepository = etlInventoryRepository;
            _clientConfiguration = clientConfiguration;
        }

        #endregion Constructor

        #region Get Methods

        #endregion Get Methods

        #region Add Methods

        public int Add(EtlInventoryModel item)
        {
            try
            {
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
                return _etlInventoryRepository.InsertRange(items);
                //TODO: Log count of items saved?
            }
            catch
            {
                throw;
            }
        }

        #endregion Add Methods

        #region CHange Methods

        #endregion Change Methods

        #region Delete Methods

        #endregion Delete Methods
    }
}
