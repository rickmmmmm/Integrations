using MiddleWay_DTO.Models.MiddleWay_Controller;
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
        bool ValidateTags(int processUid, int processTaskUid, int sourceProcess);
        bool ValidateItems(int processUid, int processTaskUid, int sourceProcess);
        bool ValidateItemTypes(int processUid, int processTaskUid, int sourceProcess);
        bool ValidateCustomFields(int processUid, int processTaskUid, int sourceProcess);
        bool ValidateManufacturers(int processUid, int processTaskUid, int sourceProcess);
        bool ValidateAreas(int processUid, int processTaskUid, int sourceProcess);
        bool ValidateSites(int processUid, int processTaskUid, int sourceProcess);
        bool ValidateDepartments(int processUid, int processTaskUid, int sourceProcess);
        bool ValidateFundingSources(int processUid, int processTaskUid, int sourceProcess);
        bool ValidateVendors(int processUid, int processTaskUid, int sourceProcess);
        bool ValidateStatus(int processUid, int processTaskUid, int sourceProcess);
        bool ValidatePurchaseOrders(int processUid, int processTaskUid, int sourceProcess);
        bool Delete(int etlInventoryUid);
        bool DeleteAll(int processTaskUid);
        bool DeleteAll(string client, string processName);
    }
}
