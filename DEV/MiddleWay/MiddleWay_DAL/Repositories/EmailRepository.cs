using MiddleWay_DAL.EF_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DAL.Repositories
{
    public class EmailRepository
    {
        #region Private Variables and Properties

        private TIPWebContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public EmailRepository(TIPWebContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Select Functions

        #endregion Select Functions

        #region Insert Functions

        public void sendEmail(string ProfileName, string Recipients, string Subject, string Body, string Attachment = null)
        {
            string query = "EXEC msdb.dbo.sp_send_dbmail @profile_name = '" + ProfileName + "', @recipients='" + Recipients + "', @subject='" + Subject + "', @body='" + Body + "', @body_format='HTML'";

            if (!string.IsNullOrEmpty(Attachment))
            {
                query += " , @file_attachments = '" + Attachment + "'";
            }

            if (_conn.State == ConnectionState.Closed)
            {
                _conn.Open();
            }

            SqlCommand cmd = new SqlCommand(query, _conn);

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
