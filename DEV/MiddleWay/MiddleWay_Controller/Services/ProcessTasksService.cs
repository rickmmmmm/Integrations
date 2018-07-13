using MiddleWay_DTO.Models.MiddleWay;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.Services
{

    public class ProcessTasksService : IProcessTasksService
    {
        #region Private Variables and Properties

        private IProcessTasksRepository _processTasksRepository;
        private IClientConfiguration _clientConfiguration;
        private IConfigurationService _configurationService;
        private IProcessesService _processesService;

        private int processTaskUid;

        #endregion Private Variables and Properties

        #region Constructor

        public ProcessTasksService(IProcessTasksRepository processTasksRepository, IClientConfiguration clientConfiguration,
                                   IConfigurationService configurationService, IProcessesService processesService)
        {
            _processTasksRepository = processTasksRepository;
            _clientConfiguration = clientConfiguration;
            _configurationService = configurationService;
            _processesService = processesService;
        }

        #endregion Constructor

        #region Get Methods

        public ProcessTasksModel GetProcessTaskByProcessUid(int processTaskUid)
        {
            try
            {
                var processTask = _processTasksRepository.SelectProcessTask(processTaskUid);
                return processTask;
            }
            catch
            {
                throw;
            }
        }

        public List<ProcessTasksModel> GetProcessTasks(int processUid)
        {
            try
            {
                var processTasks = _processTasksRepository.SelectProcessTasks(processUid);
                return processTasks;
            }
            catch
            {
                throw;
            }
        }

        public List<ProcessTasksModel> GetProcessTasks(string client, string processName)
        {
            try
            {
                var processTasks = _processTasksRepository.SelectProcessTasks(client, processName);
                return processTasks;
            }
            catch
            {
                throw;
            }
        }

        public ProcessTasksModel GetLatestProcessTask(int processUid)
        {
            try
            {
                var processTask = _processTasksRepository.SelectLatestProcessTask(processUid);
                return processTask;
            }
            catch
            {
                throw;
            }
        }

        public ProcessTasksModel GetLatestProcessTask(string client, string processName)
        {
            try
            {
                var processTask = _processTasksRepository.SelectLatestProcessTask(client, processName);
                return processTask;
            }
            catch
            {
                throw;
            }
        }

        #endregion Get Methods

        #region Add Methods

        public bool StartProcessTask(string parameters = null)
        {
            try
            {
                //Get the process uid for the client configuration
                var processUid = _processesService.GetProcessUid(_clientConfiguration.Client, _clientConfiguration.ProcessName);

                if (processUid > 0)
                {

                    //Add a process task for that process uid
                    var processTask = new ProcessTasksModel
                    {
                        ProcessUid = processUid,
                        StartTime = DateTime.UtcNow,
                        EndTime = null,
                        Parameters = parameters,
                        Successful = false
                    };

                    processTaskUid = _processTasksRepository.InsertProcessTask(processTask);

                    //Save the ProcessTaskUid and return
                    return (processTaskUid > 0);

                }
                else
                {
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion Add Methods

        #region Update Methods

        public void EndProcessTask(bool success = false)
        {
            if (processTaskUid > 0)
            {
                try
                {
                    if (!_processTasksRepository.UpdateProcessTask(processTaskUid, DateTime.UtcNow, success))
                    {
                        //TODO: Log something
                    }
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                throw new ArgumentException("No Process Tasks to end has been started");
            }
        }

        #endregion Update Methods

    }
}
