using MiddleWay_Controller.IntegrationDatabase;
using MiddleWay_DTO.Models.MiddleWay;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiddleWay_Controller.Repositories
{
    public class ProcessesRepository : IProcessesRepository
    {
        #region Private Variables and Properties

        private IntegrationMiddleWayContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public ProcessesRepository(IntegrationMiddleWayContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Select Functions

        public ProcessesModel SelectProcess(int processUid)
        {
            try
            {
                var process = (from processes in _context.Processes
                               where processes.ProcessUid == processUid
                               select new ProcessesModel
                               {
                                   ProcessUid = processes.ProcessUid,
                                   Client = processes.Client,
                                   ProcessName = processes.ProcessName,
                                   Description = processes.Description,
                                   Enabled = processes.Enabled,
                                   CreatedDate = processes.CreatedDate
                               }).FirstOrDefault();

                return process;
            }
            catch
            {
                throw;
            }
        }

        public ProcessesModel SelectProcess(string client, string processName)
        {
            try
            {
                var clientVal = (client ?? string.Empty).Trim().ToLower();
                var processNameVal = (processName ?? string.Empty).Trim().ToLower();

                var process = (from processes in _context.Processes
                               where processes.Client.Trim().ToLower() == clientVal
                                  && processes.ProcessName.Trim().ToLower() == processNameVal
                                  && processes.Enabled
                               select new ProcessesModel
                               {
                                   ProcessUid = processes.ProcessUid,
                                   Client = processes.Client,
                                   ProcessName = processes.ProcessName,
                                   Description = processes.Description,
                                   Enabled = processes.Enabled,
                                   CreatedDate = processes.CreatedDate
                               }).FirstOrDefault();

                return process;
            }
            catch
            {
                throw;
            }
        }

        public int SelectProcessUid(string client, string processName)
        {
            try
            {
                var clientVal = (client ?? string.Empty).Trim().ToLower();
                var processNameVal = (processName ?? string.Empty).Trim().ToLower();

                var processUid = (from processes in _context.Processes
                                  where processes.Client.Trim().ToLower() == clientVal
                                     && processes.ProcessName.Trim().ToLower() == processNameVal
                                     && processes.Enabled
                                  select processes.ProcessUid).FirstOrDefault();

                return processUid;
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
