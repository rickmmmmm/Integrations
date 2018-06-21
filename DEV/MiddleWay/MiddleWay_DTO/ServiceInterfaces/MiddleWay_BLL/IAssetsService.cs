using System.Collections.Generic;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay_BLL
{
    public interface IAssetsService
    {
        void ProcessAssets(List<string> options, string parameters = null);

        void updateFixedAssetIds();
    }
}
