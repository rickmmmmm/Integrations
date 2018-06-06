using MiddleWay_Controller.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.Services
{
    public class TransformationsService : ITransformationsService
    {
        #region Private Variables and Properties

        private ITransformationsRepository _transformationsRepository;

        #endregion Private Variables and Properties

        #region Constructor

        public TransformationsService(ITransformationsRepository transformationsRepository)
        {
            _transformationsRepository = transformationsRepository;
        }

        #endregion Constructor

        #region Get Methods

        #endregion Get Methods

        #region Add Methods

        #endregion Add Methods

        #region Update Methods

        #endregion Update Methods

        #region Delete Methods

        #endregion Delete Methods
    }
}
