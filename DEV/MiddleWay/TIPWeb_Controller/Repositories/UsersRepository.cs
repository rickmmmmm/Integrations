using TIPWeb_Controller.DataProvider;
using TIPWeb_Controller.EF_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace TIPWeb_Controller.Repositories
{
    public class UsersRepository
    {
        #region Private Variables and Properties

        private TIPWebContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public UsersRepository(IDataProviderFactory dataProvider)
        {
            _context = dataProvider.GetContext();
        }

        #endregion Constructor

        #region Select Functions

        public int getUserIdFromName(string userName)
        {
            throw new NotImplementedException();
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
