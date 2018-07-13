using MiddleWay_DTO.Models.MiddleWay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.RepositoryInterfaces.MiddleWay
{
    public interface IInventoryFlatDataRepository
    {
        List<InventoryFlatDataModel> Select(int processUid, int offset, int limit);
        List<InventoryFlatDataModel> Select(string client, string processName, int offset, int limit);
        InventoryFlatDataModel Select(int inventoryFlatDataUid);
        InventoryFlatDataModel SelectByAssetId(string client, string processName, string assetId);
        InventoryFlatDataModel SelectByTag(string  client, string  processName, string  tag);
        int GetTotal(int processUid);
        int GetTotal(string client, string processName);
        int Insert(InventoryFlatDataModel inventoryFlatData);
        bool InsertRange(List<InventoryFlatDataModel> inventoryFlatDataBatch);
        bool Update(InventoryFlatDataModel inventoryFlatData);
        bool Delete(int processUid);
        bool Delete(string client, string processName);
    }
}
