using System.Collections.Generic;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay_BLL
{
    public interface IAssetsService
    {
        void ProcessAssets(int processUid, List<string> commands);

        void updateFixedAssetIds();
    }
}
