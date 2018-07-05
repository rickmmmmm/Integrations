using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.Services
{
    public class ProcessTaskStepsService : IProcessTaskStepsService
    {
        #region Private Variables and Properties

        private IProcessTaskStepsRepository _processTaskStepsRepository;
        //private IClientConfiguration _clientConfiguration;
        //private IConfigurationService _configurationService;
        private IProcessTasksService _processTasksService;

        private int processTaskUid;

        #endregion Private Variables and Properties

        #region Constructor

        public ProcessTaskStepsService(IProcessTaskStepsRepository processTaskStepsRepository, IProcessTasksService processTasksService)
                                   //IClientConfiguration clientConfiguration, IConfigurationService configurationService, )
        {
            _processTaskStepsRepository = processTaskStepsRepository;
            //_clientConfiguration = clientConfiguration;
            //_configurationService = configurationService;
            _processTasksService = processTasksService;
        }

        #endregion Constructor

        #region Get Methods

        #endregion Get Methods

        #region Add Methods

        #endregion Add Methods

        #region Update Methods

        #endregion Update Methods

    }
}
