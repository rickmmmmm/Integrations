using MiddleWay_DTO.RepositoryInterfaces;
using MiddleWay_DTO.ServiceInterfaces;

namespace MiddleWay_BLL.Services
{
    public class InventoryService : IInventoryService
    {
        #region Private Variables and Properties

        private IInventoryRepository _inventoryRepository;

        #endregion Private Variables and Properties

        #region Constructor

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        #endregion Constructor

        #region Get Functions

        #endregion Get Functions

        #region Add Functions

        #endregion Add Functions

        #region Change Functions

        public void updateFixedAssetIds()
        {
            _inventoryRepository.updateFixedAssetIds();
        }

        #endregion Change Functions

        #region Remove Functions

        #endregion Remove Functions
    }
}
