using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DataAccess;
using System.Configuration;

namespace Services
{
    public class SqlDbMailService : ISender
    {
        private IRepository _repo;
        public SqlDbMailService(IRepository repo)
        {
            _repo = repo;
        }
        public void send(IMessage message)
        {
            _repo.sendEmail(ConfigurationManager.AppSettings["SqlServerDbMailProfileName"], message.Receivers.ToString(), message.Subject, message.Body, message.fileAttachment);
        }

        public void sendAsync(IMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
