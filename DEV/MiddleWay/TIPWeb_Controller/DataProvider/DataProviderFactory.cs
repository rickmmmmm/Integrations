//using MiddleWay_Controller.Interfaces;
using TIPWeb_Controller.EF_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TIPWeb_Controller.DataProvider
{
    public interface IDataProviderFactory
    {
        TIPWebContext GetContext();

        void SetConnection(string connectionString);

        //bool HasHostConnection(string host);

        //string GetHostConnection(string host);

    }

    public class DataProviderFactory : IDataProviderFactory
    {
        #region Private Variables and Properties

        //private IConfigurationService _configurationService;
        //private Dictionary<string, string> connections;
        private string connection;

        #endregion Private Variables and Properties

        #region Constructor

        public DataProviderFactory()
        {

        }
        public DataProviderFactory(string connectionString)//IConfigurationService configurationService    IHttpContextAccessor httpContextAccessor)
        {
            //_configurationService = configurationService;
            //connections = new Dictionary<string, string>();
            connection = connectionString;
        }

        #endregion Constructor

        #region Get Functions

        public TIPWebContext GetContext()
        {
            return new TIPWebContext(connection);
        }

        //public bool HasHostConnection(string host)
        //{
        //    return connections.ContainsKey(host);
        //}

        //public string GetHostConnection(string host)
        //{
        //    var conn = connections.Where(val => val.Key == host);
        //    return conn.FirstOrDefault().Value;
        //}

        #endregion Get Functions

        #region Add Functions

        public void SetConnection(string connectionString)
        {
            //if the host/connection string is not in the collection add it
            connection = connectionString;
        }

        #endregion Add Functions

    }
}
