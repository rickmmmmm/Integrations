using MiddleWay_DTO.Models.MiddleWay;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.Services
{
    public class TransformationLookupService : ITransformationLookupService
    {
        #region Private Variables and Properties

        private ITransformationLookupRepository _transformationLookupRepository;
        private IClientConfiguration _clientConfiguration;

        #endregion Private Variables and Properties

        #region Constructor

        public TransformationLookupService(ITransformationLookupRepository transformationLookupRepository, IClientConfiguration clientConfiguration)
        {
            _transformationLookupRepository = transformationLookupRepository;
            _clientConfiguration = clientConfiguration;
        }

        #endregion Constructor

        #region Get Methods

        public List<TransformationLookupModel> GetTransformationLookupData(string transformationLookupKey)
        {
            try
            {
                var transformationLookups = _transformationLookupRepository.SelectTransformationLookups(_clientConfiguration.Client, _clientConfiguration.ProcessName, transformationLookupKey);
                return transformationLookups;
            }
            catch
            {
                throw;
            }
        }

        public string LookupValue(string transformationLookupKey, string key)
        {
            try
            {
                var lookupValue = _transformationLookupRepository.SelectTransformationLookup(_clientConfiguration.Client, _clientConfiguration.ProcessName, transformationLookupKey, key);
                return lookupValue;
            }
            catch
            {
                throw;
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
