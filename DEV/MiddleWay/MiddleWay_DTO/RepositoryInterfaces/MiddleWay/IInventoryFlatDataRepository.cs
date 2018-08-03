using MiddleWay_DTO.Models.MiddleWay_Controller;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.RepositoryInterfaces.MiddleWay
{
    public interface IInventoryFlatDataRepository
    {
        List<InventoryFlatDataModel> Select(int processTaskUid, int offset, int limit);
        List<InventoryFlatDataModel> SelectLatest(string client, string processName, int offset, int limit);
        InventoryFlatDataModel Select(int inventoryFlatDataUid);
        InventoryFlatDataModel SelectByAssetId(int processTaskUid, string assetId);
        InventoryFlatDataModel SelectByTag(int processTaskUid, string tag);
        int GetTotal(int processTaskUid);
        int GetTotalLatest(string client, string processName);
        int Insert(InventoryFlatDataModel inventoryFlatData);
        bool InsertRange(List<InventoryFlatDataModel> inventoryFlatDataBatch);
        bool Update(InventoryFlatDataModel inventoryFlatData);
        bool UpdateRange(List<InventoryFlatDataModel> inventoryFlatData);
        bool Delete(int inventoryFlatDataUid);
        bool DeleteAll(int processTaskUid);
        bool DeleteAll(string client, string processName);
    }
}
