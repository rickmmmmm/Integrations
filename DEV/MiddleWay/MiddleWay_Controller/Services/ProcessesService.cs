using MiddleWay_Controller.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.Services
{
    public class ProcessesService : IProcessesService
    {
        #region Private Variables and Properties

        private IProcessesRepository _processesRepository;

        #endregion Private Variables and Properties

        #region Constructor

        public ProcessesService(IProcessesRepository processesRepository)
        {
            _processesRepository = processesRepository;
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
