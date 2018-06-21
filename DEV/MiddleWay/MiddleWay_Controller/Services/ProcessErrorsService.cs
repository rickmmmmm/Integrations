using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.Services
{
    public class ProcessErrorsService : IProcessErrorsService
    {
        #region Private Variables and Properties

        private IProcessErrorsRepository _processErrorsRepository;

        #endregion Private Variables and Properties

        #region Constructor

        public ProcessErrorsService(IProcessErrorsRepository processErrorsRepository)
        {
            _processErrorsRepository = processErrorsRepository;
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
