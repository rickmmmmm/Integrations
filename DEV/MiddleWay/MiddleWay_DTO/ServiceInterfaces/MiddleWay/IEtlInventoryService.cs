using MiddleWay_DTO.Models.MiddleWay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay
{
    public interface IEtlInventoryService
    {
        int Add(EtlInventoryModel item);
        bool AddRange(List<EtlInventoryModel> items);
    }
}
