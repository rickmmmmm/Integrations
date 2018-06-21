using System;
using System.Collections.Generic;
using System.Text;
using MiddleWay_DTO.Models.MiddleWay;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay
{
    public interface IInventoryFlatDataService
    {
        void ClearData();
        bool Add(InventoryFlatDataModel inventoryFlatData);
        bool AddRange(List<InventoryFlatDataModel> inventoryFlatDataBatch);
    }
}
