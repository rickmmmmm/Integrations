using Dapper;
using MiddleWay_DTO.Enumerations;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay_BLL;
using MiddleWay_Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MiddleWay_EDS.Services
{
    public class ExternalDataSourceService : IInputReaderService
    {
        #region Private Variables

        private string connectionString;
        private string querySelect;
        private string queryBody;
        private string queryWhere;
        private string queryGroup;
        private string queryOrder;
        private string queryOffset;
        private int readOffset = 0;
        private int readLimit = 1000;
        private DataSourceTypes sourceType;

        #endregion Private Variables 

        #region Properties
        public string Select
        {
            get
            {
                return querySelect;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    querySelect = value;
                }
                else
                {
                    throw new ArgumentException("Select cannot be null or empty");
                }
            }
        }
        public string Body
        {
            get
            {
                return queryBody;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    queryBody = value;
                }
                else
                {
                    throw new ArgumentException("Body cannot be null or empty");
                }
            }
        }
        public string Where
        {
            get
            {
                return queryWhere;
            }
            set
            {
                queryWhere = value;
            }
        }
        public string Group
        {
            get
            {
                return queryGroup;
            }
            set
            {
                queryGroup = value;
            }
        }
        public string Order
        {
            get
            {
                return queryOrder;
            }
            set
            {
                queryOrder = value;
            }
        }
        public string Offset
        {
            get
            {
                return queryOffset;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    queryOffset = value;
                }
                else
                {
                    throw new ArgumentException("Offset cannot be null or empty");
                }

            }
        }
        public int ReadOffset
        {
            get
            {
                return readOffset;
            }
            set
            {
                if (value >= 0)
                {
                    readOffset = value;
                }
                else
                {
                    readOffset = 0;
                }

            }
        }
        public int ReadLimit
        {
            get
            {
                return readLimit;
            }
            set
            {
                if (value > 0 && value < 10000)
                {
                    readLimit = value;
                }
                else
                {
                    readLimit = 1000;
                }

            }
        }

        #endregion Properties

        #region Constructor

        public ExternalDataSourceService() // : base()
        {
        }

        public ExternalDataSourceService(string connection) // : base()
        {
            connectionString = connection;
        }

        #endregion Constructor

        #region Set Functions

        public void SetConnection(string connection)
        {
            //if the host/connection string is not in the collection add it
            connectionString = connection;
        }

        #endregion Set Functions

        #region Get Functions

        public bool HasNext(int total)
        {
            return readOffset < total;
        }

        public int GetCount()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var query = new StringBuilder();
                    query.AppendLine("SELECT 1");
                    query.AppendLine(queryBody);
                    if (!string.IsNullOrEmpty(queryWhere))
                    {
                        query.AppendLine(queryWhere);
                    }
                    if (!string.IsNullOrEmpty(queryGroup))
                    {
                        query.AppendLine(queryGroup);
                    }

                    var command = Utilities.RemoveWhiteSpaceCharacters(query);

                    var total = db.Query<int>(command).Count();

                    return total;
                }
            }
            catch
            {
                throw;
            }
        }

        public List<T> ReadInput<T>()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var query = new StringBuilder();
                    query.AppendLine(querySelect);
                    query.AppendLine(queryBody);
                    if (!string.IsNullOrEmpty(queryWhere))
                    {
                        query.AppendLine(queryWhere);
                    }
                    if (!string.IsNullOrEmpty(queryGroup))
                    {
                        query.AppendLine(queryGroup);
                    }
                    if (!string.IsNullOrEmpty(queryOrder))
                    {
                        query.AppendLine(queryOrder);
                    }
                    query.AppendLine(queryOffset.Replace("@OFFSET", readOffset.ToString()).Replace("@LIMIT", readLimit.ToString()));

                    var command = Utilities.RemoveWhiteSpaceCharacters(query);

                    var result = db.Query<T>(command).ToList();

                    return result;
                }
            }
            catch
            {
                throw;
            }
        }

        public List<T> ReadNext<T>()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var query = new StringBuilder();
                    query.AppendLine(querySelect);
                    query.AppendLine(queryBody);
                    if (!string.IsNullOrEmpty(queryWhere))
                    {
                        query.AppendLine(queryWhere);
                    }
                    if (!string.IsNullOrEmpty(queryGroup))
                    {
                        query.AppendLine(queryGroup);
                    }
                    if (!string.IsNullOrEmpty(queryOrder))
                    {
                        query.AppendLine(queryOrder);
                    }
                    query.AppendLine(queryOffset.Replace("@OFFSET", readOffset.ToString()).Replace("@LIMIT", readLimit.ToString()));

                    var command = Utilities.RemoveWhiteSpaceCharacters(query);

                    var result = db.Query<T>(command).ToList();

                    readOffset += readLimit; //Increase the offset to the next group

                    return result;
                }
            }
            catch
            {
                throw;
            }
        }

        public List<T> Read<T>(int offset, int limit)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var query = new StringBuilder();
                    query.AppendLine(querySelect);
                    query.AppendLine(queryBody);
                    if (!string.IsNullOrEmpty(queryWhere))
                    {
                        query.AppendLine(queryWhere);
                    }
                    if (!string.IsNullOrEmpty(queryGroup))
                    {
                        query.AppendLine(queryGroup);
                    }
                    if (!string.IsNullOrEmpty(queryOrder))
                    {
                        query.AppendLine(queryOrder);
                    }
                    query.AppendLine(queryOffset.Replace("@OFFSET", (offset > 0 ? offset : 0).ToString()).Replace("@LIMIT", (limit > 0 ? limit : 1000).ToString()));

                    var command = Utilities.RemoveWhiteSpaceCharacters(query);

                    var result = db.Query<T>(command).ToList();

                    return result;
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion Get Functions

    }
}
