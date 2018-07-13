using MiddleWay_DTO.Models.MiddleWay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay
{
    public interface IEtlInventoryService
    {
        EtlInventoryModel Get(int etlInventoryUid);
        EtlInventoryModel GetByTag(string client, string processName, string tag);
        EtlInventoryModel GetByAsset(string client, string processName, string assetId);
        //EtlInventoryModel GetBySerial(string client, string processName, string serial);
        EtlInventoryModel GetByInventoryId(string client, string processName, int inventoryUid);
        int Add(EtlInventoryModel item);
        bool AddRange(List<EtlInventoryModel> items);
        bool Edit(EtlInventoryModel item);
        bool Remove(int etlInventoryUid);
    }
}
