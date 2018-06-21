using MiddleWay_Controller.IntegrationDatabase;
using MiddleWay_DTO.Models.MiddleWay;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using System;
using System.Collections.Generic;
using System.Text;

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
                //var list = (from transformations in _context.Transformations
                //            where transformations.ProcessUid == processUid
                //               && transformations.Enabled
                //            select new TransformationsModel
                //            {
                //                TransformationUid = transformations.TransformationUid,
                //                ProcessUid = transformations.ProcessUid,
                //                Function = transformations.Function,
                //                Parameters = transformations.Parameters,
                //                SourceColumn = transformations.SourceColumn,
                //                DestinationColumn = transformations.DestinationColumn,
                //                Enabled = transformations.Enabled,
                //                Order = transformations.Order
                //            }).ToList();

                //return list;
                return null;
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
                //var list = (from transformations in _context.Transformations
                //            where transformations.ProcessUid == processUid
                //               && transformations.Enabled
                //            select new TransformationsModel
                //            {
                //                TransformationUid = transformations.TransformationUid,
                //                ProcessUid = transformations.ProcessUid,
                //                Function = transformations.Function,
                //                Parameters = transformations.Parameters,
                //                SourceColumn = transformations.SourceColumn,
                //                DestinationColumn = transformations.DestinationColumn,
                //                Enabled = transformations.Enabled,
                //                Order = transformations.Order
                //            }).ToList();

                //return list;
                return null;
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
                var clientVal = client.Trim().ToLower();
                var processNameVal = processName.Trim().ToLower();

                //var list = (from transformations in _context.Transformations
                //            where transformations.ProcessUid == processUid
                //               && transformations.Enabled
                //            select new TransformationsModel
                //            {
                //                TransformationUid = transformations.TransformationUid,
                //                ProcessUid = transformations.ProcessUid,
                //                Function = transformations.Function,
                //                Parameters = transformations.Parameters,
                //                SourceColumn = transformations.SourceColumn,
                //                DestinationColumn = transformations.DestinationColumn,
                //                Enabled = transformations.Enabled,
                //                Order = transformations.Order
                //            }).ToList();

                //return list;
                return null;
            }
            catch
            {
                throw;
            }
        }

        public string SelectTransformationLookup(int processUid, string transformationLookupKey, string key)
        {
            try
            {
                //var list = (from transformations in _context.Transformations
                //            where transformations.ProcessUid == processUid
                //               && transformations.Enabled
                //            select new TransformationsModel
                //            {
                //                TransformationUid = transformations.TransformationUid,
                //                ProcessUid = transformations.ProcessUid,
                //                Function = transformations.Function,
                //                Parameters = transformations.Parameters,
                //                SourceColumn = transformations.SourceColumn,
                //                DestinationColumn = transformations.DestinationColumn,
                //                Enabled = transformations.Enabled,
                //                Order = transformations.Order
                //            }).ToList();

                //return list;
                return null;
            }
            catch
            {
                throw;
            }
        }

        public string SelectTransformationLookup(string client, string processName, string transformationLookupKey, string key)
        {
            try
            {
                var clientVal = client.Trim().ToLower();
                var processNameVal = processName.Trim().ToLower();

                //var list = (from transformations in _context.Transformations
                //            where transformations.ProcessUid == processUid
                //               && transformations.Enabled
                //            select new TransformationsModel
                //            {
                //                TransformationUid = transformations.TransformationUid,
                //                ProcessUid = transformations.ProcessUid,
                //                Function = transformations.Function,
                //                Parameters = transformations.Parameters,
                //                SourceColumn = transformations.SourceColumn,
                //                DestinationColumn = transformations.DestinationColumn,
                //                Enabled = transformations.Enabled,
                //                Order = transformations.Order
                //            }).ToList();

                //return list;
                return null;
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
