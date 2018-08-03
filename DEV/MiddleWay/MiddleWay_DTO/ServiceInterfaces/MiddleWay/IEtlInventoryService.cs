using MiddleWay_DTO.Models.MiddleWay_Controller;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay
{
    public interface IEtlInventoryService
    {
        List<EtlInventoryModel> Get(int processTaskUid, int offset, int limit);
        List<EtlInventoryModel> GetLatest(int offset, int limit);
        EtlInventoryModel Get(int etlInventoryUid);
        EtlInventoryModel GetByTag(int processTaskUid, string tag);
        EtlInventoryModel GetByAsset(int processTaskUid, string assetId);
        //EtlInventoryModel GetBySerial(int processTaskUid, string serial);
        EtlInventoryModel GetByInventoryId(int processTaskUid, int inventoryUid);
        int GetTotal(int processTaskUid);
        int GetTotalLatest();
        int Add(EtlInventoryModel item);
        bool AddRange(List<EtlInventoryModel> items);
        bool Edit(EtlInventoryModel item);
        bool EditRange(List<EtlInventoryModel> items);
        bool Remove(int etlInventoryUid);
        bool RemoveAll();
        bool RemoveAll(int processTaskUid);
        bool ValidateEtlInventory(int processUid, int processTaskUid, int processSource);
        bool SubmitEtlInventory(int processUid, int processTaskUid, int processSource);
    }
}
