using MiddleWay_DAL.EF_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DAL.Repositories
{
    public class AreasRepository
    {
        #region Private Variables and Properties

        private TIPWebContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public AreasRepository(TIPWebContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Select Functions
        public int getAreaUIDFromName(string areaName)
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
