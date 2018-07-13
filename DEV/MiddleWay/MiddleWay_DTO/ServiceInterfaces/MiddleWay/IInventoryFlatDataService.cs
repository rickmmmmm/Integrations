using System;
using System.Collections.Generic;
using System.Text;
using MiddleWay_DTO.Models.MiddleWay;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay
{
    public interface IInventoryFlatDataService
    {
        List<InventoryFlatDataModel> Get(int offset, int limit);
        InventoryFlatDataModel Get(int inventoryFlatDataUid);
        InventoryFlatDataModel GetByAssetId(string assetId);
        InventoryFlatDataModel GetByTag(string tag);
        int GetTotal();
        bool Add(InventoryFlatDataModel inventoryFlatData);
        bool AddRange(List<InventoryFlatDataModel> inventoryFlatDataBatch);
        bool Change(InventoryFlatDataModel inventoryFlatData);
        void ClearData();
    }
}
