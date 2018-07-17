using MiddleWay_Controller.IntegrationDatabase;
using MiddleWay_DTO.Enumerations;
using MiddleWay_DTO.Models.MiddleWay;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using System.Collections.Generic;
using System.Linq;

namespace MiddleWay_Controller.Repositories
{
    public class MappingsRepository : IMappingsRepository
    {
        #region Private Variables and Properties

        private IntegrationMiddleWayContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public MappingsRepository(IntegrationMiddleWayContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Select Functions

        public List<MappingsModel> SelectMappings(int processUid, ProcessSteps stepName)
        {
            try
            {
                var stepNameVal = stepName.ToString().Trim().ToLower();

                var list = (from mappings in _context.Mappings
                            where mappings.ProcessUid == processUid
                               && mappings.StepName.Trim().ToLower() == stepNameVal
                               && mappings.Enabled
                            select new MappingsModel
                            {
                                MappingsUid = mappings.MappingsUid,
                                ProcessUid = mappings.ProcessUid,
                                SourceColumn = mappings.SourceColumn,
                                DestinationColumn = mappings.DestinationColumn,
                                Enabled = mappings.Enabled
                            }).ToList();

                return list;
            }
            catch
            {
                throw;
            }
        }

        public List<MappingsModel> SelectMappings(string client, string processName, ProcessSteps stepName)
        {
            try
            {
                var clientVal = (client ?? string.Empty).Trim().ToLower();
                var processNameVal = (processName ?? string.Empty).Trim().ToLower();
                var stepNameVal = stepName.ToString().Trim().ToLower();

                var list = (from mappings in _context.Mappings
                            join processes in _context.Processes
                                on mappings.ProcessUid equals processes.ProcessUid
                            where processes.Client.Trim().ToLower() == clientVal
                               && processes.ProcessName.Trim().ToLower() == processNameVal
                               && mappings.StepName.Trim().ToLower() == stepNameVal
                               && mappings.Enabled
                            select new MappingsModel
                            {
                                MappingsUid = mappings.MappingsUid,
                                ProcessUid = mappings.ProcessUid,
                                SourceColumn = mappings.SourceColumn,
                                DestinationColumn = mappings.DestinationColumn,
                                Enabled = mappings.Enabled
                            }).ToList();

                return list;
            }
            catch
            {
                throw;
            }
        }

        public MappingsModel SelectMappings(int processUid, ProcessSteps stepName, string sourceColumn)
        {
            try
            {
                var sourceColumnVal = (sourceColumn ?? string.Empty).Trim().ToLower();
                var stepNameVal = stepName.ToString().Trim().ToLower();

                var data = (from mappings in _context.Mappings
                            where mappings.ProcessUid == processUid
                               && mappings.SourceColumn.Trim().ToLower() == sourceColumnVal
                               && mappings.StepName.Trim().ToLower() == stepNameVal
                            select new MappingsModel
                            {
                                MappingsUid = mappings.MappingsUid,
                                ProcessUid = mappings.ProcessUid,
                                SourceColumn = mappings.SourceColumn,
                                DestinationColumn = mappings.DestinationColumn,
                                Enabled = mappings.Enabled
                            }).FirstOrDefault();

                return data;
            }
            catch
            {
                throw;
            }
        }

        public MappingsModel SelectMappings(string client, string processName, ProcessSteps stepName, string sourceColumn)
        {
            try
            {
                var clientVal = (client ?? string.Empty).Trim().ToLower();
                var processNameVal = (processName ?? string.Empty).Trim().ToLower();
                var sourceColumnVal = (sourceColumn ?? string.Empty).Trim().ToLower();
                var stepNameVal = stepName.ToString().Trim().ToLower();

                var data = (from mappings in _context.Mappings
                            join processes in _context.Processes
                                on mappings.ProcessUid equals processes.ProcessUid
                            where processes.Client.Trim().ToLower() == clientVal
                               && processes.ProcessName.Trim().ToLower() == processNameVal
                               && mappings.SourceColumn.Trim().ToLower() == sourceColumnVal
                               && mappings.StepName.Trim().ToLower() == stepNameVal
                            select new MappingsModel
                            {
                                MappingsUid = mappings.MappingsUid,
                                ProcessUid = mappings.ProcessUid,
                                SourceColumn = mappings.SourceColumn,
                                DestinationColumn = mappings.DestinationColumn,
                                Enabled = mappings.Enabled
                            }).FirstOrDefault();

                return data;
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
