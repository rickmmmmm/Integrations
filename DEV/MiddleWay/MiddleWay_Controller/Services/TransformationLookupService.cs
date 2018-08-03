using MiddleWay_DTO.Models.MiddleWay_Controller;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
using System.Collections.Generic;

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

        public TransformationLookupModel GetTransformationLookup(int transformationLookupUid)
        {
            try
            {
                var transformationLookup = _transformationLookupRepository.SelectTransformationLookup(transformationLookupUid);
                return transformationLookup;
            }
            catch
            {
                throw;
            }
        }

        public string GetTransformationLookupValue(int transformationLookupUid)
        {
            try
            {
                var value = _transformationLookupRepository.SelectTransformationLookupValue(transformationLookupUid);
                return value;
            }
            catch
            {
                throw;
            }
        }

        public TransformationLookupModel GetTransformationLookup(int processUid, string transformationLookupKey, string key)
        {
            try
            {
                var transformationLookup = _transformationLookupRepository.SelectTransformationLookup(processUid, transformationLookupKey, key);
                return transformationLookup;
            }
            catch
            {
                throw;
            }
        }

        public string GetTransformationLookupValue(int processUid, string transformationLookupKey, string key, bool keepKeyValue = false)
        {
            try
            {
                var value = _transformationLookupRepository.SelectTransformationLookupValue(_clientConfiguration.Client, _clientConfiguration.ProcessName, transformationLookupKey, key);
                if (value == null && keepKeyValue)
                {
                    return key;
                }
                else
                {
                    return value;
                }
            }
            catch
            {
                throw;
            }
        }

        public TransformationLookupModel GetTransformationLookup(string transformationLookupKey, string key)
        {
            try
            {
                var transformationLookup = _transformationLookupRepository.SelectTransformationLookup(_clientConfiguration.Client, _clientConfiguration.ProcessName, transformationLookupKey, key);
                return transformationLookup;
            }
            catch
            {
                throw;
            }
        }

        public string GetTransformationLookupValue(string transformationLookupKey, string key, bool keepKeyValue = false)
        {
            try
            {
                var value = _transformationLookupRepository.SelectTransformationLookupValue(_clientConfiguration.Client, _clientConfiguration.ProcessName, transformationLookupKey, key);
                if (value == null && keepKeyValue)
                {
                    return key;
                }
                else
                {
                    return value;
                }
            }
            catch
            {
                throw;
            }
        }

        public List<TransformationLookupModel> GetTransformationLookupData(int processUid, string transformationLookupKey)
        {
            try
            {
                var transformationLookups = _transformationLookupRepository.SelectTransformationLookups(processUid, transformationLookupKey);
                return transformationLookups;
            }
            catch
            {
                throw;
            }
        }

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

        #endregion Get Methods

        //#region Add Methods

        //#endregion Add Methods

        //#region Update Methods

        //#endregion Update Methods

        //#region Delete Methods

        //#endregion Delete Methods

    }
}
