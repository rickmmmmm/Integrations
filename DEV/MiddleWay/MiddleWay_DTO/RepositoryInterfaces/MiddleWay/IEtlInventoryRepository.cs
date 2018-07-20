using MiddleWay_DTO.Models.MiddleWay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.RepositoryInterfaces.MiddleWay
{
    public interface IEtlInventoryRepository
    {
        List<EtlInventoryModel> Select(int processUid, int offset, int limit);
        List<EtlInventoryModel> Select(string client, string processName, int offset, int limit);
        EtlInventoryModel Select(int etlInventoryUid);
        EtlInventoryModel SelectByInventoryUid(string client, string processName, int inventoryUid);
        EtlInventoryModel SelectByAssetId(string client, string processName, string assetId);
        EtlInventoryModel SelectByTag(string client, string processName, string tag);
        int GetTotal(int processUid);
        int GetTotal(string client, string processName);
        int Insert(EtlInventoryModel etlInventoryData);
        bool InsertRange(List<EtlInventoryModel> etlInventoryData);
        bool Update(EtlInventoryModel etlInventoryData);
        bool UpdateRange(List<EtlInventoryModel> etlInventoryData);
        bool Delete(int processUid);
        bool Delete(string client, string processName);
    }
}
