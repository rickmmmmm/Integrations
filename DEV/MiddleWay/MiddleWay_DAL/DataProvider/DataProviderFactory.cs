using MiddleWay_Controller.Interfaces;
using MiddleWay_DAL.EF_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiddleWay_DAL.DataProvider
{
    public interface IDataProviderFactory
    {
        TIPWebContext GetContext();

        //bool HasHostConnection(string host);

        //string GetHostConnection(string host);

        //void AddHostConnection(string host, string connection);

    }

    public class DataProviderFactory : IDataProviderFactory
    {
        #region Private Variables and Properties

        private IConfigurationService _configurationService;
        //private Dictionary<string, string> connections;

        #endregion Private Variables and Properties

        #region Constructor

        public DataProviderFactory(IConfigurationService configurationService)//IHttpContextAccessor httpContextAccessor)
        {
            _configurationService = configurationService;
            //connections = new Dictionary<string, string>();
        }

        #endregion Constructor

        #region Get Functions

        public TIPWebContext GetContext()
        {
            return new TIPWebContext(_configurationService.TIPWebConnection);
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

        //public void AddHostConnection(string host, string connection)
        //{
        //    //if the host/connection string is not in the collection add it
        //    connections.Add(host, connection);
        //}

        #endregion Add Functions

    }
}
