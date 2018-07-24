using MiddleWay_DTO.Models.MiddleWay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.RepositoryInterfaces.MiddleWay
{
    public interface IEtlInventoryRepository
    {
        List<EtlInventoryModel> Select(int processTaskUid, int offset, int limit);
        List<EtlInventoryModel> SelectLatest(string client, string processName, int offset, int limit);
        EtlInventoryModel Select(int etlInventoryUid);
        EtlInventoryModel SelectByInventoryUid(int processTaskUid, int inventoryUid);
        EtlInventoryModel SelectByAssetId(int processTaskUid, string assetId);
        EtlInventoryModel SelectByTag(int processTaskUid, string tag);
        int GetTotal(int processTaskUid);
        int GetTotalLatest(string client, string processName);
        int Insert(EtlInventoryModel etlInventoryData);
        bool InsertRange(List<EtlInventoryModel> etlInventoryData);
        bool Update(EtlInventoryModel etlInventoryData);
        bool UpdateRange(List<EtlInventoryModel> etlInventoryData);
        bool Delete(int etlInventoryUid);
        bool DeleteAll(int processTaskUid);
        bool DeleteAll(string client, string processName);
    }
}
