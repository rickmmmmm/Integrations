using MiddleWay_Controller.IntegrationDatabase;
using MiddleWay_DTO.Enumerations;
using MiddleWay_DTO.Models.MiddleWay_Controller;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using System.Collections.Generic;
using System.Linq;

namespace MiddleWay_Controller.Repositories
{
    public class TransformationsRepository : ITransformationsRepository
    {
        #region Private Variables and Properties

        private IntegrationMiddleWayContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public TransformationsRepository(IntegrationMiddleWayContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Select Functions

        public bool HasTransformations(int processUid, ProcessSteps stepName)
        {
            try
            {
                var stepNameVal = stepName.ToString().Trim().ToLower();

                return (from transformations in _context.Transformations
                        where transformations.ProcessUid == processUid
                           && transformations.StepName.Trim().ToLower() == stepNameVal
                           && transformations.Enabled
                        select 1).Count() > 0;
            }
            catch
            {
                throw;
            }
        }

        public bool HasTransformations(string client, string processName, ProcessSteps stepName)
        {
            try
            {
                var clientVal = (client ?? string.Empty).Trim().ToLower();
                var processNameVal = (processName ?? string.Empty).Trim().ToLower();
                var stepNameVal = stepName.ToString().Trim().ToLower();

                return (from transformations in _context.Transformations
                        join processes in _context.Processes
                            on transformations.ProcessUid equals processes.ProcessUid
                        where processes.Client.Trim().ToLower() == clientVal
                           && processes.ProcessName.Trim().ToLower() == processNameVal
                           && transformations.StepName.Trim().ToLower() == stepNameVal
                           && transformations.Enabled
                        select 1).Count() > 0;
            }
            catch
            {
                throw;
            }
        }

        public List<TransformationsModel> SelectTransformations(int processUid, ProcessSteps stepName)
        {
            try
            {
                var stepNameVal = stepName.ToString().Trim().ToLower();

                var list = (from transformations in _context.Transformations
                            where transformations.ProcessUid == processUid
                               && transformations.StepName.Trim().ToLower() == stepNameVal
                               && transformations.Enabled
                            select new TransformationsModel
                            {
                                TransformationUid = transformations.TransformationUid,
                                ProcessUid = transformations.ProcessUid,
                                StepName = transformations.StepName,
                                Function = transformations.Function,
                                Parameters = transformations.Parameters,
                                SourceColumn = transformations.SourceColumn,
                                DestinationColumn = transformations.DestinationColumn,
                                Enabled = transformations.Enabled,
                                Order = transformations.Order
                            }).ToList();

                return list;
            }
            catch
            {
                throw;
            }
        }

        public List<TransformationsModel> SelectTransformations(string client, string processName, ProcessSteps stepName)
        {
            try
            {
                var clientVal = (client ?? string.Empty).Trim().ToLower();
                var processNameVal = (processName ?? string.Empty).Trim().ToLower();
                var stepNameVal = stepName.ToString().Trim().ToLower();

                var list = (from transformations in _context.Transformations
                            join processes in _context.Processes
                                on transformations.ProcessUid equals processes.ProcessUid
                            where processes.Client.Trim().ToLower() == clientVal
                               && processes.ProcessName.Trim().ToLower() == processNameVal
                               && transformations.StepName.Trim().ToLower() == stepNameVal
                               && transformations.Enabled
                            select new TransformationsModel
                            {
                                TransformationUid = transformations.TransformationUid,
                                ProcessUid = transformations.ProcessUid,
                                StepName = transformations.StepName,
                                Function = transformations.Function,
                                Parameters = transformations.Parameters,
                                SourceColumn = transformations.SourceColumn,
                                DestinationColumn = transformations.DestinationColumn,
                                Enabled = transformations.Enabled,
                                Order = transformations.Order
                            }).ToList();

                return list;
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
