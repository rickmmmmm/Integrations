using MiddleWay_DAL.EF_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DAL.Repositories
{
    public class ETLRejectsRepository
    {
        #region Private Variables and Properties

        private TIPWebContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public ETLRejectsRepository(TIPWebContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Select Functions

        public List<RejectedRecord> getRejectionsFromLastImport()
        {
            List<RejectedRecord> rejects = new List<RejectedRecord>();

            string returnQuery = "SELECT Reference, RejectReason, RejectedValue, ExceptionMessage, LineNumber FROM _ETL_Rejects WHERE ImportCode = " + _importCode.ToString();

            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }

            _conn.Open();

            SqlCommand returnCmd = new SqlCommand(returnQuery, _conn);

            SqlDataReader reader = returnCmd.ExecuteReader();

            while (reader.Read())
            {
                try
                {
                    rejects.Add(new RejectedRecord
                    {
                        orderNumber = (string)reader[0],
                        rejectReason = (string)reader[1],
                        rejectValue = (string)reader[2],
                        exceptionMessage = (string)reader[3],
                        LineNumber = (int)reader[4]
                    });
                }

                catch (Exception e)
                {
                    DbErrorEventArgs args = new DbErrorEventArgs();
                    args.InterfaceMessage = "Unable to get list of rejected records.";
                    args.ExceptionMessage = e.Message;
                    OnError(args);
                    break;
                }
            }

            reader.Close();
            _conn.Close();

            return rejects;
        }

        #endregion Select Functions

        #region Insert Functions

        public void logRejectRecord(List<RejectedRecord> rejections)
        {
            foreach (RejectedRecord rejection in rejections)
            {
                logRejectRecord(rejection);
            }

        }
        public void logRejectRecord(RejectedRecord rejection)
        {
            string query = "INSERT INTO _ETL_Rejects (ImportCode, Reference, RejectReason, RejectedValue, ExceptionMessage, LineNumber)";
            query += " VALUES (" + _importCode.ToString() + ",'" + rejection.orderNumber + "','" + rejection.rejectReason + "','" + rejection.rejectValue.Replace("'", "") + "','" + rejection.exceptionMessage.Replace("'", "") + "'," + rejection.LineNumber.ToString() + ")";
            SqlCommand cmd = new SqlCommand(query, _conn);

            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }

            _conn.Open();
            cmd.ExecuteNonQuery();
            _conn.Close();
        }

        #endregion Insert Functions

        #region Update Functions

        #endregion Update Functions

        #region Delete Functions

        #endregion Delete Functions
    }
}
