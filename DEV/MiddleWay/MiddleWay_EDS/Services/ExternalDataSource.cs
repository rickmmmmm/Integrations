using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;

namespace MiddleWay_EDS.Services
{
    public class ExternalDataSource
    {
        private string _connectionString;

        public ExternalDataSource(string connectionString) : base()
        {
            _connectionString = connectionString;

        }

        public void Exec()
        {

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SELECT AP.PO_XREF AS PO_NUMBER, " +
                    "AP.DATE_PURCHASED AS PO_DATE, " +
                    "AP.PO_line_number AS LINE_NUMBER, " +
                    "ISNULL(A.OEM_MODEL, 'UNKNOWN') AS PRODUCT_NAME, " +
                    "CASE WHEN A.DEVICE_ROLE = 'DC' THEN 'SV' WHEN A.DEVICE_ROLE IS NULL THEN 'UN' ELSE A.DEVICE_ROLE END AS PRODUCT_TYPE, " +
                    "ISNULL(A.OEM_MODEL, 'UNKNOWN') AS MODEL, " +
                    "ISNULL(A.OEM_MODEL, 'UNKNOWN') AS MANUFACTURER, " +
                    "D.ORACLE_ID AS SHIPPEDTOSITE, " +
                    "A.ASSET_ID AS ASSETUID, " +
                    "A.ASSET_TAG AS TAG, " +
                    "A.SERIAL_NUMBER AS SERIALNUMBER, " +
                    "A.LOC_ROOM_NUM AS LOCATION, " +
                    "A.LOC_ROOM_TYPE AS LOCATIONTYPE, " +
                    "A.DEPARTMENT_ID AS DEPARTMENT, " +
                    "A.[HOST_NAME] AS HOSTNAME, " +
                    "A.LAST_LOGON_USER AS LASTLOGON, " +
                    "A.DATE_COLLECTED_MSB AS LASTSCANDATE " +
                    "FROM TECHXL_ASSETS A WITH(NOLOCK) " +
                    "LEFT OUTER JOIN TECHXL_ASSET_PURCHASE_INFO AP WITH(NOLOCK) " +
                    "ON AP.ASSET_ID = A.ASSET_ID " +
                    "LEFT OUTER JOIN TECHXL_DEPARTMENTS D WITH(NOLOCK) " +
                    "ON D.DEPARTMENT_ID = A.DEPARTMENT_ID " +
                    "LEFT OUTER JOIN dbo.TEMP_JAMF TJ WITH(NOLOCK) " +
                    "ON A.SERIAL_NUMBER = TJ.serial_number " +
                    "WHERE CAST(DATE_COLLECTED_SMS AS date) > DATEADD(day, -2, CAST(GETDATE() AS date)) " +
                    "OR CAST(DATE_COLLECTED_AU AS date) > DATEADD(day, -2, CAST(GETDATE() AS date)) " +
                    "OR CAST(DATE_COLLECTED_AV AS date) > DATEADD(day, -1, CAST(GETDATE() AS date)) " +
                    "OR CAST(DATE_COLLECTED_MSB AS date) > DATEADD(day, -1, CAST(GETDATE() AS date)) " +
                    "OR CAST(DATE_COLLECTED_DHC AS date) > DATEADD(day, -1, CAST(GETDATE() AS date)) " +
                    "OR(CAST(LOST_ASSET_SVC_EXPIRE AS date) > DATEADD(day, 1010, CAST(GETDATE() AS date)) " +
                    "AND TJ.serial_number IS NOT NULL)";

                var data = db.Query<Object>(query).ToList();

                
            }
        }
    }
}
