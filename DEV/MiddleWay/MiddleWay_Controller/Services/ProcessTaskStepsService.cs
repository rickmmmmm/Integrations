using MiddleWay_DTO.Enumerations;
using MiddleWay_DTO.Models.MiddleWay;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.Services
{
    public class ProcessTaskStepsService : IProcessTaskStepsService
    {
        #region Private Variables and Properties

        private IProcessTaskStepsRepository _processTaskStepsRepository;
        //private IClientConfiguration _clientConfiguration;
        private IProcessTasksService _processTasksService;
        //private IConfigurationService _configurationService;

        private int processTaskUid;

        #endregion Private Variables and Properties

        #region Constructor

        public ProcessTaskStepsService(IProcessTaskStepsRepository processTaskStepsRepository, IProcessTasksService processTasksService) //IClientConfiguration clientConfiguration, IConfigurationService configurationService, )
        {
            _processTaskStepsRepository = processTaskStepsRepository;
            //_clientConfiguration = clientConfiguration;
            //_configurationService = configurationService;
            _processTasksService = processTasksService;
        }

        #endregion Constructor

        #region Get Methods

        public ProcessTaskStepsModel GetTaskStep(int processTaskStepUid)
        {
            try
            {
                return _processTaskStepsRepository.SelectProcessTaskStep(processTaskStepUid);
            }
            catch
            {
                throw;
            }
        }

        public List<ProcessTaskStepsModel> GetTaskSteps(int processTaskUid)
        {
            try
            {
                return _processTaskStepsRepository.SelectProcessTaskSteps(processTaskUid);
            }
            catch
            {
                throw;
            }
        }

        public ProcessTaskStepsModel GetLatestTaskStep(string client, string processName, ProcessSteps stepName)
        {
            try
            {
                return _processTaskStepsRepository.SelectLatestProcessTaskStep(client, processName, stepName);
            }
            catch
            {
                throw;
            }
        }

        public List<ProcessTaskStepsModel> GetLatestProcessTaskSteps(int processUid)
        {
            try
            {
                return _processTaskStepsRepository.SelectLatestProcessTaskSteps(processUid);
            }
            catch
            {
                throw;
            }
        }

        public List<ProcessTaskStepsModel> GetLatestProcessTaskSteps(string client, string processName)
        {
            try
            {
                return _processTaskStepsRepository.SelectLatestProcessTaskSteps(client, processName);
            }
            catch
            {
                throw;
            }
        }

        #endregion Get Methods

        #region Add Methods

        public int BeginTaskStep(int processTaskUid, ProcessSteps stepName)
        {
            try
            {
                Console.WriteLine("Is _processTasksService.GetProcessTaskUid (" + _processTasksService.GetProcessTaskUid  + ") equal to processTaskUid (" + processTaskUid + "): " + (_processTasksService.GetProcessTaskUid == processTaskUid).ToString());
                return _processTaskStepsRepository.InsertProcessTaskStep(processTaskUid, stepName);
            }
            catch
            {
                throw;
            }
        }

        #endregion Add Methods

        #region Update Methods

        public bool EndTaskStep(int processTaskStepUid, bool success)
        {
            try
            {
                //var taskStep = _processTaskStepsRepository.SelectProcessTaskStep(processTaskStepUid);
                //taskStep.EndDate = DateTime.UtcNow;
                //taskStep.Successful = success;
                //return _processTaskStepsRepository.UpdateProcessTaskStep(taskStep);
                return _processTaskStepsRepository.UpdateProcessTaskStep(processTaskStepUid, DateTime.UtcNow, success);
            }
            catch
            {
                throw;
            }
        }

        public bool EndTaskStep(int processTaskUid, ProcessSteps stepName, bool success)
        {
            try
            {
                var taskStep = _processTaskStepsRepository.SelectProcessTaskStep(processTaskUid, stepName);
                taskStep.EndDate = DateTime.UtcNow;
                taskStep.Successful = success;
                return _processTaskStepsRepository.UpdateProcessTaskStep(taskStep);
                //return _processTaskStepsRepository.UpdateProcessTaskStep(processTaskStepUid, DateTime.UtcNow, success);
            }
            catch
            {
                throw;
            }
        }

        #endregion Update Methods

    }
}
