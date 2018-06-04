using MiddleWay_DAL.DataProvider;
using MiddleWay_DAL.EF_DAL;
using MiddleWay_DTO.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DAL.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        #region Private Variables and Properties

        private TIPWebContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public EmailRepository(IDataProviderFactory dataProvider)
        {
            _context = dataProvider.GetContext();
        }

        #endregion Constructor

        #region Select Functions

        #endregion Select Functions

        #region Insert Functions

        public void sendEmail(string profileName, string recipients, string subject, string body, string attachment = null)
        {
            //TODO: Send DBMail
            string query = "EXEC msdb.dbo.sp_send_dbmail @profile_name = '" + profileName + "', @recipients='" + recipients + "', @subject='" + subject + "', @body='" + body + "', @body_format='HTML'";

            if (!string.IsNullOrEmpty(attachment))
            {
                query += " , @file_attachments = '" + attachment + "'";
            }

            //if (_conn.State == ConnectionState.Closed)
            //{
            //    _conn.Open();
            //}

            //SqlCommand cmd = new SqlCommand(query, _conn);

            //cmd.ExecuteNonQuery();

            //_conn.Close();
        }

        #endregion Insert Functions

        #region Update Functions

        #endregion Update Functions

        #region Delete Functions

        #endregion Delete Functions
    }
}
