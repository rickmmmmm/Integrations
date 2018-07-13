using MiddleWay_Controller.IntegrationDatabase;
using MiddleWay_DTO.Models.MiddleWay;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiddleWay_Controller.Repositories
{
    public class TransformationLookupRepository : ITransformationLookupRepository
    {
        #region Private Variables and Properties

        private IntegrationMiddleWayContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public TransformationLookupRepository(IntegrationMiddleWayContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Select Functions

        public TransformationLookupModel SelectTransformationLookup(int transformationLookupUid)
        {
            try
            {
                var transformationLookup = (from transformationLookups in _context.TransformationLookup
                                            where transformationLookups.TransformationLookupUid == transformationLookupUid
                                            select new TransformationLookupModel
                                            {
                                                TransformationLookupUid = transformationLookups.TransformationLookupUid,
                                                ProcessUid = transformationLookups.ProcessUid,
                                                TransformationLookupKey = transformationLookups.TransformationLookupKey,
                                                Key = transformationLookups.Key,
                                                Value = transformationLookups.Value,
                                                Enabled = transformationLookups.Enabled
                                            }).FirstOrDefault();

                return transformationLookup;
            }
            catch
            {
                throw;
            }
        }

        public string SelectTransformationLookupValue(int transformationLookupUid)
        {
            try
            {
                var transformationLookupValue = (from transformationLookups in _context.TransformationLookup
                                                 where transformationLookups.TransformationLookupUid == transformationLookupUid
                                                 select transformationLookups.Value).FirstOrDefault();

                return transformationLookupValue;
            }
            catch
            {
                throw;
            }
        }

        public TransformationLookupModel SelectTransformationLookup(int processUid, string transformationLookupKey, string key)
        {
            try
            {
                var transformationLookupKeyVal = (transformationLookupKey ?? string.Empty).Trim().ToLower();
                var keyVal = (key ?? string.Empty).Trim().ToLower();

                var transformationLookup = (from transformationLookups in _context.TransformationLookup
                                            where transformationLookups.ProcessUid == processUid
                                               && transformationLookups.TransformationLookupKey.Trim().ToLower() == transformationLookupKeyVal
                                               && transformationLookups.Key.Trim().ToLower() == keyVal
                                            select new TransformationLookupModel
                                            {
                                                TransformationLookupUid = transformationLookups.TransformationLookupUid,
                                                ProcessUid = transformationLookups.ProcessUid,
                                                TransformationLookupKey = transformationLookups.TransformationLookupKey,
                                                Key = transformationLookups.Key,
                                                Value = transformationLookups.Value,
                                                Enabled = transformationLookups.Enabled
                                            }).FirstOrDefault();

                return transformationLookup;
            }
            catch
            {
                throw;
            }
        }

        public string SelectTransformationLookupValue(int processUid, string transformationLookupKey, string key)
        {
            try
            {
                var transformationLookupKeyVal = (transformationLookupKey ?? string.Empty).Trim().ToLower();
                var keyVal = (key ?? string.Empty).Trim().ToLower();

                var transformationLookupValue = (from transformationLookups in _context.TransformationLookup
                                                 where transformationLookups.ProcessUid == processUid
                                                    && transformationLookups.TransformationLookupKey.Trim().ToLower() == transformationLookupKeyVal
                                                    && transformationLookups.Key.Trim().ToLower() == keyVal
                                                 select transformationLookups.Value).FirstOrDefault();

                return transformationLookupValue;
            }
            catch
            {
                throw;
            }
        }

        public TransformationLookupModel SelectTransformationLookup(string client, string processName, string transformationLookupKey, string key)
        {
            try
            {
                var clientVal = (client ?? string.Empty).Trim().ToLower();
                var processNameVal = (processName ?? string.Empty).Trim().ToLower();
                var transformationLookupKeyVal = (transformationLookupKey ?? string.Empty).Trim().ToLower();
                var keyVal = (key ?? string.Empty).Trim().ToLower();

                var transformationLookup = (from transformationLookups in _context.TransformationLookup
                                            join processes in _context.Processes
                                                on transformationLookups.ProcessUid equals processes.ProcessUid
                                            where processes.Client.Trim().ToLower() == clientVal
                                               && processes.ProcessName.Trim().ToLower() == processNameVal
                                               && transformationLookups.TransformationLookupKey.Trim().ToLower() == transformationLookupKeyVal
                                               && transformationLookups.Key.Trim().ToLower() == keyVal
                                            select new TransformationLookupModel
                                            {
                                                TransformationLookupUid = transformationLookups.TransformationLookupUid,
                                                ProcessUid = transformationLookups.ProcessUid,
                                                TransformationLookupKey = transformationLookups.TransformationLookupKey,
                                                Key = transformationLookups.Key,
                                                Value = transformationLookups.Value,
                                                Enabled = transformationLookups.Enabled
                                            }).FirstOrDefault();

                return transformationLookup;
            }
            catch
            {
                throw;
            }
        }

        public string SelectTransformationLookupValue(string client, string processName, string transformationLookupKey, string key)
        {
            try
            {
                var clientVal = (client ?? string.Empty).Trim().ToLower();
                var processNameVal = (processName ?? string.Empty).Trim().ToLower();
                var transformationLookupKeyVal = (transformationLookupKey ?? string.Empty).Trim().ToLower();
                var keyVal = (key ?? string.Empty).Trim().ToLower();

                var transformationLookupValue = (from transformationLookups in _context.TransformationLookup
                                                 join processes in _context.Processes
                                                     on transformationLookups.ProcessUid equals processes.ProcessUid
                                                 where processes.Client.Trim().ToLower() == clientVal
                                                    && processes.ProcessName.Trim().ToLower() == processNameVal
                                                    && transformationLookups.TransformationLookupKey.Trim().ToLower() == transformationLookupKeyVal
                                                    && transformationLookups.Key.Trim().ToLower() == keyVal
                                                 select transformationLookups.Value).FirstOrDefault();

                return transformationLookupValue;
            }
            catch
            {
                throw;
            }
        }

        public List<TransformationLookupModel> SelectTransformationLookups(int processUid, string transformationLookupKey)
        {
            try
            {
                var transformationLookupKeyVal = (transformationLookupKey ?? string.Empty).Trim().ToLower();

                var transformationLookupList = (from transformationLookups in _context.TransformationLookup
                                                where transformationLookups.ProcessUid == processUid
                                                   && transformationLookups.TransformationLookupKey.Trim().ToLower() == transformationLookupKeyVal
                                                select new TransformationLookupModel
                                                {
                                                    TransformationLookupUid = transformationLookups.TransformationLookupUid,
                                                    ProcessUid = transformationLookups.ProcessUid,
                                                    TransformationLookupKey = transformationLookups.TransformationLookupKey,
                                                    Key = transformationLookups.Key,
                                                    Value = transformationLookups.Value,
                                                    Enabled = transformationLookups.Enabled
                                                }).ToList();

                return transformationLookupList;
            }
            catch
            {
                throw;
            }
        }

        public List<TransformationLookupModel> SelectTransformationLookups(string client, string processName, string transformationLookupKey)
        {
            try
            {
                var clientVal = (client ?? string.Empty).Trim().ToLower();
                var processNameVal = (processName ?? string.Empty).Trim().ToLower();
                var transformationLookupKeyVal = (transformationLookupKey ?? string.Empty).Trim().ToLower();

                var transformationLookupList = (from transformationLookups in _context.TransformationLookup
                                                join processes in _context.Processes
                                                    on transformationLookups.ProcessUid equals processes.ProcessUid
                                                where processes.Client.Trim().ToLower() == clientVal
                                                   && processes.ProcessName.Trim().ToLower() == processNameVal
                                                   && transformationLookups.TransformationLookupKey.Trim().ToLower() == transformationLookupKeyVal
                                                select new TransformationLookupModel
                                                {
                                                    TransformationLookupUid = transformationLookups.TransformationLookupUid,
                                                    ProcessUid = transformationLookups.ProcessUid,
                                                    TransformationLookupKey = transformationLookups.TransformationLookupKey,
                                                    Key = transformationLookups.Key,
                                                    Value = transformationLookups.Value,
                                                    Enabled = transformationLookups.Enabled
                                                }).ToList();

                return transformationLookupList;
            }
            catch
            {
                throw;
            }
        }

        #endregion Select Functions

        //#region Insert Functions

        //#endregion Insert Functions

        //#region Update Functions

        //#endregion Update Functions

        //#region Delete Functions

        //#endregion Delete Functions
    }
}
