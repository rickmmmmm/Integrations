using MiddleWay_DTO.Enumerations;
using System.Collections.Generic;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay_BLL
{
    public interface IAssetsService
    {
        void ProcessAssets(int processUid, List<string> commands, ProcessSources processSource);

        void updateFixedAssetIds();
    }
}
