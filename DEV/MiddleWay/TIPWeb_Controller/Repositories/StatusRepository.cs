using TIPWeb_Controller.DataProvider;
using TIPWeb_Controller.EF_DAL;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MiddleWay_DTO.Enumerations;

namespace TIPWeb_Controller.Repositories
{
    public class StatusRepository
    {
        #region Private Variables and Properties

        private TIPWebContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public StatusRepository(IDataProviderFactory dataProvider)
        {
            _context = dataProvider.GetContext();
        }

        #endregion Constructor

        #region Select Functions

        public int GetStatusUid(string statusName, StatusTypes statusTypeID)
        {
            try
            {
                var statusID = (from status in _context.TblStatus
                                where status.StatusDesc == statusName
                                   && status.StatusTypeUid == (int)statusTypeID
                                select status.StatusId).FirstOrDefault();

                return statusID;
            }
            catch
            {
                throw;
            }
        }

        #endregion Select Functions

        #region Insert Functions

        #endregion Insert Functions

        #region Update Functions

        #endregion Update Functions

        #region Delete Functions

        #endregion Delete Functions
    }
}
