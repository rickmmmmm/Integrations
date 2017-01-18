using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DataAccess;

namespace SystemTasks
{
    public class Logging
    {
        private IRepository _repo;

        public Logging(IRepository repo)
        {
            _repo = repo;
        }

        public enum ChangeType { Error, Activity, RejectRecord }
        public void log(string message, string actionName, ChangeType type)
        {

            if (type == ChangeType.Error)
            {
                _repo.logError(message, actionName, DateTime.Now);
            }

            _repo.logAction(actionName, message, DateTime.Now);
        }

    }

    public class ErrorEventArgs : EventArgs
    {
        public string message { get; set; }
        public string actionName { get; set; }
        public Logging.ChangeType type { get; set; }
        public ErrorData Data { get; set; }
    }

    public class ErrorData
    {
        public string Reference { get; set; }
        public string Reason { get; set; }
        public string ExceptionMessage { get; set; }
        public string RejectedValue { get; set; }
    }
}
