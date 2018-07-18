using MiddleWay_Controller.IntegrationDatabase;
using MiddleWay_DTO.Enumerations;
using MiddleWay_DTO.Models.MiddleWay;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiddleWay_Controller.Repositories
{
    public class ProcessTaskStepsRepository : IProcessTaskStepsRepository
    {
        #region Private Variables and Properties

        private IntegrationMiddleWayContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public ProcessTaskStepsRepository(IntegrationMiddleWayContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Select Functions

        public ProcessTaskStepsModel SelectProcessTaskStep(int processTaskStepUid)
        {
            try
            {
                return (from processTaskSteps in _context.ProcessTaskSteps
                        where processTaskSteps.ProcessTaskStepsUid == processTaskStepUid
                        select new ProcessTaskStepsModel
                        {
                            ProcessTaskStepsUid = processTaskSteps.ProcessTaskStepsUid,
                            ProcessTaskUid = processTaskSteps.ProcessTaskUid,
                            StepName = processTaskSteps.StepName,
                            StartDate = processTaskSteps.StartDate,
                            EndDate = processTaskSteps.EndDate,
                            Successful = processTaskSteps.Successful
                        }).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public ProcessTaskStepsModel SelectProcessTaskStep(int processTaskUid, ProcessSteps stepName)
        {
            try
            {
                var stepNameVal = stepName.ToString().ToLower();

                return (from processTaskSteps in _context.ProcessTaskSteps
                        join processTasks in _context.ProcessTasks
                            on processTaskSteps.ProcessTaskUid equals processTasks.ProcessTaskUid
                        where processTasks.ProcessTaskUid == processTaskUid
                           && processTaskSteps.StepName.Trim().ToLower() == stepNameVal
                        select new ProcessTaskStepsModel
                        {
                            ProcessTaskStepsUid = processTaskSteps.ProcessTaskStepsUid,
                            ProcessTaskUid = processTaskSteps.ProcessTaskUid,
                            StepName = processTaskSteps.StepName,
                            StartDate = processTaskSteps.StartDate,
                            EndDate = processTaskSteps.EndDate,
                            Successful = processTaskSteps.Successful
                        }).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public List<ProcessTaskStepsModel> SelectProcessTaskSteps(int processTaskUid)
        {
            try
            {
                return (from processTaskSteps in _context.ProcessTaskSteps
                        where processTaskSteps.ProcessTaskUid == processTaskUid
                        select new ProcessTaskStepsModel
                        {
                            ProcessTaskStepsUid = processTaskSteps.ProcessTaskStepsUid,
                            ProcessTaskUid = processTaskSteps.ProcessTaskUid,
                            StepName = processTaskSteps.StepName,
                            StartDate = processTaskSteps.StartDate,
                            EndDate = processTaskSteps.EndDate,
                            Successful = processTaskSteps.Successful
                        }).ToList();
            }
            catch
            {
                throw;
            }
        }

        public ProcessTaskStepsModel SelectLatestProcessTaskStep(string client, string processName, ProcessSteps stepName)
        {
            try
            {
                var clientVar = (client ?? string.Empty).Trim().ToLower();
                var processNameVar = (processName ?? string.Empty).Trim().ToLower();
                var stepNameVar = stepName.ToString().Trim().ToLower();

                return (from latestProcessTaskSteps in
                            (from processTaskSteps in _context.ProcessTaskSteps
                             join processTasks in _context.ProcessTasks
                                 on processTaskSteps.ProcessTaskUid equals processTasks.ProcessTaskUid
                             join processes in _context.Processes
                                         on processTasks.ProcessUid equals processes.ProcessUid
                             where processes.Client.Trim().ToLower() == clientVar
                                && processes.ProcessName.Trim().ToLower() == processNameVar
                                && processTaskSteps.StepName.Trim().ToLower() == stepNameVar
                             group processTaskSteps by new
                             {
                                 processes.Client,
                                 processes.ProcessName,
                                 processTaskSteps.StepName
                             } into processTaskStepsGroup
                             select processTaskStepsGroup.Max(steps => steps.ProcessTaskStepsUid))
                        join processTaskSteps in _context.ProcessTaskSteps
                            on latestProcessTaskSteps equals processTaskSteps.ProcessTaskStepsUid
                        select new ProcessTaskStepsModel
                        {
                            ProcessTaskStepsUid = processTaskSteps.ProcessTaskStepsUid,
                            ProcessTaskUid = processTaskSteps.ProcessTaskUid,
                            StepName = processTaskSteps.StepName,
                            StartDate = processTaskSteps.StartDate,
                            EndDate = processTaskSteps.EndDate,
                            Successful = processTaskSteps.Successful
                        }).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public List<ProcessTaskStepsModel> SelectLatestProcessTaskSteps(int processUid)
        {
            try
            {
                return (from processTaskSteps in _context.ProcessTaskSteps
                        join latestProcessTaskSteps in
                            (from processTaskSteps in _context.ProcessTaskSteps
                             join processTasks in _context.ProcessTasks
                                 on processTaskSteps.ProcessTaskUid equals processTasks.ProcessTaskUid
                             join processes in _context.Processes
                                         on processTasks.ProcessUid equals processes.ProcessUid
                             where processes.ProcessUid == processUid
                             group processTaskSteps by processTasks.ProcessUid into processTaskStepsGroup
                             //group processTaskSteps by new { processes.Client, processes.ProcessName } into processTaskStepsGroup
                             select processTaskStepsGroup.Max(steps => steps.ProcessTaskUid))
                            //select new { MaxProcessTaskUid = processTaskStepsGroup.Max(steps => steps.ProcessTaskUid) })
                            on processTaskSteps.ProcessTaskUid equals latestProcessTaskSteps
                        select new ProcessTaskStepsModel
                        {
                            ProcessTaskStepsUid = processTaskSteps.ProcessTaskStepsUid,
                            ProcessTaskUid = processTaskSteps.ProcessTaskUid,
                            StepName = processTaskSteps.StepName,
                            StartDate = processTaskSteps.StartDate,
                            EndDate = processTaskSteps.EndDate,
                            Successful = processTaskSteps.Successful
                        }).ToList();
            }
            catch
            {
                throw;
            }
        }

        public List<ProcessTaskStepsModel> SelectLatestProcessTaskSteps(string client, string processName)
        {
            try
            {
                var clientVar = (client ?? string.Empty).Trim().ToLower();
                var processNameVar = (processName ?? string.Empty).Trim().ToLower();

                return (from processTaskSteps in _context.ProcessTaskSteps
                        join latestProcessTaskSteps in
                            (from processTaskSteps in _context.ProcessTaskSteps
                             join processTasks in _context.ProcessTasks
                                 on processTaskSteps.ProcessTaskUid equals processTasks.ProcessTaskUid
                             join processes in _context.Processes
                                         on processTasks.ProcessUid equals processes.ProcessUid
                             where processes.Client.Trim().ToLower() == clientVar
                                && processes.ProcessName.Trim().ToLower() == processNameVar
                             group processTaskSteps by processTasks.ProcessUid into processTaskStepsGroup
                             //group processTaskSteps by new { processes.Client, processes.ProcessName } into processTaskStepsGroup
                             select processTaskStepsGroup.Max(steps => steps.ProcessTaskUid))
                            //select new { MaxProcessTaskUid = processTaskStepsGroup.Max(steps => steps.ProcessTaskUid) })
                            on processTaskSteps.ProcessTaskUid equals latestProcessTaskSteps
                        select new ProcessTaskStepsModel
                        {
                            ProcessTaskStepsUid = processTaskSteps.ProcessTaskStepsUid,
                            ProcessTaskUid = processTaskSteps.ProcessTaskUid,
                            StepName = processTaskSteps.StepName,
                            StartDate = processTaskSteps.StartDate,
                            EndDate = processTaskSteps.EndDate,
                            Successful = processTaskSteps.Successful
                        }).ToList();
            }
            catch
            {
                throw;
            }
        }

        #endregion Select Functions

        #region Insert Functions

        public int InsertProcessTaskStep(ProcessTaskStepsModel processTaskStep)
        {
            try
            {
                var taskStep = new ProcessTaskSteps
                {
                    ProcessTaskStepsUid = processTaskStep.ProcessTaskStepsUid,
                    ProcessTaskUid = processTaskStep.ProcessTaskUid,
                    StepName = processTaskStep.StepName,
                    StartDate = processTaskStep.StartDate,
                    EndDate = processTaskStep.EndDate,
                    Successful = processTaskStep.Successful
                };

                _context.ProcessTaskSteps.Add(taskStep);
                _context.SaveChanges();

                return taskStep.ProcessTaskStepsUid;
            }
            catch
            {
                throw;
            }
        }

        public int InsertProcessTaskStep(int processTaskUid, ProcessSteps stepName)
        {
            try
            {
                var taskStep = new ProcessTaskSteps
                {
                    ProcessTaskStepsUid = 0,
                    ProcessTaskUid = processTaskUid,
                    StepName = stepName.ToString(),
                    StartDate = DateTime.UtcNow,
                    EndDate = null,
                    Successful = false
                };

                _context.ProcessTaskSteps.Add(taskStep);
                _context.SaveChanges();

                return taskStep.ProcessTaskStepsUid;
            }
            catch
            {
                throw;
            }
        }

        #endregion Insert Functions

        #region Update Functions

        public bool UpdateProcessTaskStep(ProcessTaskStepsModel processTaskStep)
        {
            try
            {
                var taskStep = (from processTaskSteps in _context.ProcessTaskSteps
                                where processTaskSteps.ProcessTaskStepsUid == processTaskStep.ProcessTaskStepsUid
                                select processTaskSteps).FirstOrDefault();

                taskStep.EndDate = processTaskStep.EndDate;
                taskStep.Successful = processTaskStep.Successful;

                _context.ProcessTaskSteps.Update(taskStep);
                var result = _context.SaveChanges();

                return result == 1;
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateProcessTaskStep(int processTaskStepsUid, DateTime endDate, bool success)
        {
            try
            {
                var taskStep = (from processTaskSteps in _context.ProcessTaskSteps
                                where processTaskSteps.ProcessTaskStepsUid == processTaskStepsUid
                                select processTaskSteps).FirstOrDefault();

                taskStep.EndDate = endDate;
                taskStep.Successful = success;

                _context.ProcessTaskSteps.Update(taskStep);
                var result = _context.SaveChanges();

                return result == 1;
            }
            catch
            {
                throw;
            }
        }

        #endregion Update Functions

        //#region Delete Functions

        //#endregion Delete Functions
    }
}
