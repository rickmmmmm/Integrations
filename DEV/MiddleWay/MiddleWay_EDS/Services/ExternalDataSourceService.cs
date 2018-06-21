using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using MiddleWay_DTO.Enumerations;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay_BLL;

namespace MiddleWay_EDS.Services
{
    public class ExternalDataSourceService : IInputReaderService
    {
        #region Private Variables

        private string connectionString;
        private string querySelect;
        private string queryBody;
        private string queryWhere;
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

        //public void Exec()
        //{
        //    using (IDbConnection db = new SqlConnection(connectionString))
        //    {
        //        var query = "SELECT AP.PO_XREF AS PO_NUMBER, " +
        //            "AP.DATE_PURCHASED AS PO_DATE, " +
        //            "AP.PO_line_number AS LINE_NUMBER, " +
        //            "ISNULL(A.OEM_MODEL, 'UNKNOWN') AS PRODUCT_NAME, " +
        //            "CASE WHEN A.DEVICE_ROLE = 'DC' THEN 'SV' WHEN A.DEVICE_ROLE IS NULL THEN 'UN' ELSE A.DEVICE_ROLE END AS PRODUCT_TYPE, " +
        //            "ISNULL(A.OEM_MODEL, 'UNKNOWN') AS MODEL, " +
        //            "ISNULL(A.OEM_MODEL, 'UNKNOWN') AS MANUFACTURER, " +
        //            "D.ORACLE_ID AS SHIPPEDTOSITE, " +
        //            "A.ASSET_ID AS ASSETUID, " +
        //            "A.ASSET_TAG AS TAG, " +
        //            "A.SERIAL_NUMBER AS SERIALNUMBER, " +
        //            "A.LOC_ROOM_NUM AS LOCATION, " +
        //            "A.LOC_ROOM_TYPE AS LOCATIONTYPE, " +
        //            "A.DEPARTMENT_ID AS DEPARTMENT, " +
        //            "A.[HOST_NAME] AS HOSTNAME, " +
        //            "A.LAST_LOGON_USER AS LASTLOGON, " +
        //            "A.DATE_COLLECTED_MSB AS LASTSCANDATE " +
        //            "FROM TECHXL_ASSETS A WITH(NOLOCK) " +
        //            "LEFT OUTER JOIN TECHXL_ASSET_PURCHASE_INFO AP WITH(NOLOCK) " +
        //            "ON AP.ASSET_ID = A.ASSET_ID " +
        //            "LEFT OUTER JOIN TECHXL_DEPARTMENTS D WITH(NOLOCK) " +
        //            "ON D.DEPARTMENT_ID = A.DEPARTMENT_ID " +
        //            "LEFT OUTER JOIN dbo.TEMP_JAMF TJ WITH(NOLOCK) " +
        //            "ON A.SERIAL_NUMBER = TJ.serial_number " +
        //            "WHERE CAST(DATE_COLLECTED_SMS AS date) > DATEADD(day, -2, CAST(GETDATE() AS date)) " +
        //            "OR CAST(DATE_COLLECTED_AU AS date) > DATEADD(day, -2, CAST(GETDATE() AS date)) " +
        //            "OR CAST(DATE_COLLECTED_AV AS date) > DATEADD(day, -1, CAST(GETDATE() AS date)) " +
        //            "OR CAST(DATE_COLLECTED_MSB AS date) > DATEADD(day, -1, CAST(GETDATE() AS date)) " +
        //            "OR CAST(DATE_COLLECTED_DHC AS date) > DATEADD(day, -1, CAST(GETDATE() AS date)) " +
        //            "OR(CAST(LOST_ASSET_SVC_EXPIRE AS date) > DATEADD(day, 1010, CAST(GETDATE() AS date)) " +
        //            "AND TJ.serial_number IS NOT NULL)";
        //        var data = db.Query<Object>(query).ToList();
        //    }
        //}

        public int GetCount()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var query = new StringBuilder();
                    query.Append("SELECT 1 ");
                    query.Append(queryBody);
                    if (!string.IsNullOrEmpty(queryWhere))
                    {
                        query.Append(queryWhere);
                    }

                    var total = db.Query<int>(query.ToString()).Count();

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
                    query.Append(querySelect);
                    query.Append(queryBody);
                    if (!string.IsNullOrEmpty(queryWhere))
                    {
                        query.Append(queryWhere);
                    }
                    query.Append(queryOffset.Replace("@OFFSET", readOffset.ToString()).Replace("@LIMIT", readLimit.ToString()));

                    var result = db.Query<T>(query.ToString()).ToList();

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
                    query.Append(querySelect);
                    query.Append(queryBody);
                    if (!string.IsNullOrEmpty(queryWhere))
                    {
                        query.Append(queryWhere);
                    }
                    query.Append(queryOffset.Replace("@OFFSET", readOffset.ToString()).Replace("@LIMIT", readLimit.ToString()));

                    var result = db.Query<T>(query.ToString()).ToList();

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
                    query.Append(querySelect);
                    query.Append(queryBody);
                    if (!string.IsNullOrEmpty(queryWhere))
                    {
                        query.Append(queryWhere);
                    }
                    query.Append(queryOffset.Replace("@OFFSET", (offset > 0 ? offset : 0).ToString()).Replace("@LIMIT", (limit > 0 ? limit : 1000).ToString()));

                    var result = db.Query<T>(query.ToString()).ToList();

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
