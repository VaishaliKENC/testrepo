using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public interface IAssetAdaptor<T>
    {
        Asset GetAssetById(Asset pEntAsset);
        Asset GetAssetRelativePathById(Asset pEntAsset);
        List<Asset> GetAssetLanguageList(Asset pEntAssets);
        Asset AddAsset(Asset pEntAsset);
        Asset EditAsset(Asset pEntAsset);
        Asset DeleteAsset(Asset pEntAsset);
        Asset SearchRelativePath(Asset pEntAsset);
        Asset DeleteAssetLanguage(Asset pEntAsset);
        List<Asset> GetAssetList(Asset pEntAsset);
        List<Asset> GetAssetListForAssignments(Asset pEntAsset);
        Asset UpdateLanguage(Asset pEntAssetMaster);
    }
}
