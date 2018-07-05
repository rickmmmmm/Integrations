using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.Services
{
    public class ProcessTaskErrorsService : IProcessTaskErrorsService
    {
        #region Private Variables and Properties

        private IProcessTaskErrorsRepository _processTaskErrorsRepository;

        #endregion Private Variables and Properties

        #region Constructor

        public ProcessTaskErrorsService(IProcessTaskErrorsRepository processTaskErrorsRepository)
        {
            _processTaskErrorsRepository = processTaskErrorsRepository;
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
