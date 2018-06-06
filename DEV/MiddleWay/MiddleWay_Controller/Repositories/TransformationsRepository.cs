using MiddleWay_Controller.IntegrationDatabase;
using MiddleWay_Controller.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.Repositories
{
    public class TransformationsRepository : ITransformationsRepository
    {
        #region Private Variables and Properties

        private IntegrationMiddleWayContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public TransformationsRepository(IntegrationMiddleWayContext context)
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
