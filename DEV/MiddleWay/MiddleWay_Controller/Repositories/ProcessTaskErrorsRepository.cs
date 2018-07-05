using MiddleWay_Controller.IntegrationDatabase;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.Repositories
{
    public class ProcessTaskErrorsRepository : IProcessTaskErrorsRepository
    {
        #region Private Variables and Properties

        private IntegrationMiddleWayContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public ProcessTaskErrorsRepository(IntegrationMiddleWayContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Select Functions

        #endregion Select Functions

        #region Insert Functions

        #endregion Insert Functions

        #region Update Functions

        #endregion Update Functions

        #region Delete Functions

        #endregion Delete Functions
    }
}
