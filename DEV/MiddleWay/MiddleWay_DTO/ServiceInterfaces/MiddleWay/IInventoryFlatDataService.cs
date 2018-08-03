using System;
using System.Collections.Generic;
using System.Text;
using MiddleWay_DTO.Models.MiddleWay_Controller;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay
{
    public interface IInventoryFlatDataService
    {
        List<InventoryFlatDataModel> Get(int processTaskUid, int offset, int limit);
        List<InventoryFlatDataModel> GetLatest(int offset, int limit);
        InventoryFlatDataModel Get(int inventoryFlatDataUid);
        InventoryFlatDataModel GetByAssetId(int processTaskUid, string assetId);
        InventoryFlatDataModel GetByTag(int processTaskUid, string tag);
        int GetTotal(int processTaskUid);
        int GetTotalLatest();
        bool Add(InventoryFlatDataModel inventoryFlatData);
        bool AddRange(List<InventoryFlatDataModel> inventoryFlatDataBatch);
        bool Edit(InventoryFlatDataModel inventoryFlatModel);
        bool EditRange(List<InventoryFlatDataModel> inventoryFlatData);
        void Remove(int processTaskUid);
        void ClearData();
        void ClearData(int processTaskUid);
    }
}
