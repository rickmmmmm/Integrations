using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;

namespace MiddleWay_Controller.Services
{
    public class ProcessesService : IProcessesService
    {
        #region Private Variables and Properties

        private IProcessesRepository _processesRepository;
        private IClientConfiguration _clientConfiguration;
        private IConfigurationService _configurationService;

        #endregion Private Variables and Properties

        #region Constructor

        public ProcessesService(IProcessesRepository processesRepository, IClientConfiguration clientConfiguration,
                                IConfigurationService configurationService) // , IProcessTasksService processTasksService
        {
            _processesRepository = processesRepository;
            _clientConfiguration = clientConfiguration;
            _configurationService = configurationService;
        }

        #endregion Constructor

        #region Get Methods

        public int GetProcessUid()
        {
            try
            {
                var process = this._processesRepository.SelectProcess(_clientConfiguration.Client, _clientConfiguration.ProcessName);
                return process.ProcessUid;
            }
            catch
            {
                throw;
            }
        }

        public int GetProcessUid(string client, string processName)
        {
            try
            {
                var process = this._processesRepository.SelectProcess(client, processName);
                return process.ProcessUid;
            }
            catch
            {
                throw;
            }
        }

        #endregion Get Methods

        #region Add Methods

        #endregion Add Methods

        #region Update Methods

        #endregion Update Methods

        #region Delete Methods

        #endregion Delete Methods
    }
}
