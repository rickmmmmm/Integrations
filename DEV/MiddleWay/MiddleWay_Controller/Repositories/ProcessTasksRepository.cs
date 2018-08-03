using MiddleWay_Controller.IntegrationDatabase;
using MiddleWay_DTO.Models.MiddleWay_Controller;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiddleWay_Controller.Repositories
{
    public class ProcessTasksRepository : IProcessTasksRepository
    {
        #region Private Variables and Properties

        private IntegrationMiddleWayContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public ProcessTasksRepository(IntegrationMiddleWayContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Select Functions

        public ProcessTasksModel SelectLatestProcessTask(int processUid)
        {
            try
            {
                var processTask = (from processTasks in _context.ProcessTasks
                                   join latestProcessTask in
                                    (from processTasks in _context.ProcessTasks
                                     join processes in _context.Processes
                                                 on processTasks.ProcessUid equals processes.ProcessUid
                                     where processes.ProcessUid == processUid
                                     group processTasks by new
                                     {
                                         processes.Client,
                                         processes.ProcessName
                                     } into processTasksGroup
                                     select processTasksGroup.Max(task => task.ProcessTaskUid))
                                    on processTasks.ProcessTaskUid equals latestProcessTask
                                   select new ProcessTasksModel
                                   {
                                       ProcessTaskUid = processTasks.ProcessTaskUid,
                                       ProcessUid = processTasks.ProcessUid,
                                       StartTime = processTasks.StartTime,
                                       EndTime = processTasks.EndTime,
                                       Parameters = processTasks.Parameters,
                                       Successful = processTasks.Successful
                                   }).FirstOrDefault();

                return processTask;
            }
            catch
            {
                throw;
            }
        }

        public ProcessTasksModel SelectLatestProcessTask(string client, string processName)
        {
            try
            {
                var clientVar = (client ?? string.Empty).Trim().ToLower();
                var processNameVar = (processName ?? string.Empty).Trim().ToLower();

                var processTask = (from processTasks in _context.ProcessTasks
                                   join latestProcessTask in
                                    (from processTasks in _context.ProcessTasks
                                     join processes in _context.Processes
                                                 on processTasks.ProcessUid equals processes.ProcessUid
                                     where processes.Client.Trim().ToLower() == clientVar
                                        && processes.ProcessName.Trim().ToLower() == processNameVar
                                     group processTasks by new
                                     {
                                         processes.Client,
                                         processes.ProcessName
                                     } into processTasksGroup
                                     select processTasksGroup.Max(task => task.ProcessTaskUid))
                                    on processTasks.ProcessTaskUid equals latestProcessTask
                                   select new ProcessTasksModel
                                   {
                                       ProcessTaskUid = processTasks.ProcessTaskUid,
                                       ProcessUid = processTasks.ProcessUid,
                                       StartTime = processTasks.StartTime,
                                       EndTime = processTasks.EndTime,
                                       Parameters = processTasks.Parameters,
                                       Successful = processTasks.Successful
                                   }).FirstOrDefault();

                return processTask;
            }
            catch
            {
                throw;
            }
        }

        public ProcessTasksModel SelectProcessTask(int processTaskUid)
        {
            try
            {
                var processTask = (from processTasks in _context.ProcessTasks
                                   where processTasks.ProcessTaskUid == processTaskUid
                                   select new ProcessTasksModel
                                   {
                                       ProcessTaskUid = processTasks.ProcessTaskUid,
                                       ProcessUid = processTasks.ProcessUid,
                                       StartTime = processTasks.StartTime,
                                       EndTime = processTasks.EndTime,
                                       Parameters = processTasks.Parameters,
                                       Successful = processTasks.Successful
                                   }).FirstOrDefault();

                return processTask;
            }
            catch
            {
                throw;
            }
        }

        public List<ProcessTasksModel> SelectProcessTasks(int processUid)
        {
            try
            {
                var processTaskList = (from processTasks in _context.ProcessTasks
                                       join processes in _context.Processes
                                        on processTasks.ProcessUid equals processes.ProcessUid
                                       where processes.ProcessUid == processUid
                                       select new ProcessTasksModel
                                       {
                                           ProcessTaskUid = processTasks.ProcessTaskUid,
                                           ProcessUid = processTasks.ProcessUid,
                                           StartTime = processTasks.StartTime,
                                           EndTime = processTasks.EndTime,
                                           Parameters = processTasks.Parameters,
                                           Successful = processTasks.Successful
                                       }).ToList();

                return processTaskList;
            }
            catch
            {
                throw;
            }
        }

        public List<ProcessTasksModel> SelectProcessTasks(string client, string processName)
        {
            try
            {
                var clientVar = (client ?? string.Empty).Trim().ToLower();
                var processNameVar = (processName ?? string.Empty).Trim().ToLower();

                var processTaskList = (from processTasks in _context.ProcessTasks
                                       join processes in _context.Processes
                                        on processTasks.ProcessUid equals processes.ProcessUid
                                       where processes.Client.Trim().ToLower() == clientVar
                                          && processes.ProcessName.Trim().ToLower() == processNameVar
                                       select new ProcessTasksModel
                                       {
                                           ProcessTaskUid = processTasks.ProcessTaskUid,
                                           ProcessUid = processTasks.ProcessUid,
                                           StartTime = processTasks.StartTime,
                                           EndTime = processTasks.EndTime,
                                           Parameters = processTasks.Parameters,
                                           Successful = processTasks.Successful
                                       }).ToList();

                return processTaskList;
            }
            catch
            {
                throw;
            }
        }

        #endregion Select Functions

        #region Insert Functions

        public int InsertProcessTask(ProcessTasksModel processTask)
        {
            try
            {
                var task = new ProcessTasks
                {
                    ProcessTaskUid = 0,
                    ProcessUid = processTask.ProcessUid,
                    StartTime = processTask.StartTime,
                    EndTime = processTask.EndTime,
                    Parameters = processTask.Parameters,
                    Successful = processTask.Successful
                };

                _context.ProcessTasks.Add(task);
                _context.SaveChanges();

                return task.ProcessTaskUid;
            }
            catch
            {
                throw;
            }
        }

        public int InsertProcessTask(string client, string processName, string parameters = "")
        {
            try
            {
                var clientVar = (client ?? string.Empty).Trim().ToLower();
                var processNameVar = (processName ?? string.Empty).Trim().ToLower();

                var process = (from processes in _context.Processes
                               where processes.Client.Trim().ToLower() == clientVar
                                  && processes.ProcessName.Trim().ToLower() == processNameVar
                               select processes).FirstOrDefault();

                if (process != null)
                {
                    var task = new ProcessTasks
                    {
                        ProcessTaskUid = 0,
                        ProcessUid = process.ProcessUid,
                        StartTime = DateTime.UtcNow,
                        EndTime = null,
                        Parameters = parameters,
                        Successful = false
                    };

                    _context.ProcessTasks.Add(task);
                    _context.SaveChanges();

                    return task.ProcessTaskUid;


                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion Insert Functions

        #region Update Functions

        public bool UpdateProcessTask(ProcessTasksModel processTask)
        {
            try
            {
                if (processTask != null)
                {
                    var processTaskToUpdate = (from processTasks in _context.ProcessTasks
                                               where processTasks.ProcessTaskUid == processTask.ProcessTaskUid
                                               select processTasks).FirstOrDefault();

                    processTaskToUpdate.StartTime = processTask.StartTime;
                    processTaskToUpdate.EndTime = processTask.EndTime;
                    processTaskToUpdate.Parameters = processTask.Parameters;
                    processTaskToUpdate.Successful = processTask.Successful;

                    _context.ProcessTasks.Update(processTaskToUpdate);
                    _context.SaveChanges();

                    return true;
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

        public bool UpdateProcessTask(int processTaskUid, DateTime endTime)
        {
            try
            {
                if (processTaskUid > 0)
                {
                    var processTaskToUpdate = (from processTasks in _context.ProcessTasks
                                               where processTasks.ProcessTaskUid == processTaskUid
                                               select processTasks).FirstOrDefault();

                    processTaskToUpdate.EndTime = endTime;

                    _context.ProcessTasks.Update(processTaskToUpdate);
                    _context.SaveChanges();

                    return true;
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

        public bool UpdateProcessTask(int processTaskUid, DateTime endTime, bool successful = false)
        {
            try
            {
                if (processTaskUid > 0)
                {
                    var processTaskToUpdate = (from processTasks in _context.ProcessTasks
                                               where processTasks.ProcessTaskUid == processTaskUid
                                               select processTasks).FirstOrDefault();

                    processTaskToUpdate.EndTime = endTime;
                    processTaskToUpdate.Successful = successful;

                    _context.ProcessTasks.Update(processTaskToUpdate);
                    _context.SaveChanges();

                    return true;
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

        #endregion Update Functions

    }
}
