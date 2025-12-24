using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public interface IAssetLibraryAdaptor <T>
    {
        AssetLibrary GetAssetLibraryById(AssetLibrary pEntAssetLib);
        AssetLibrary GetAssetLibraryById_ForAssignment(AssetLibrary pEntAssetLib);
        AssetLibrary GetAssetLibraryChildCount(AssetLibrary pEntAssetLib);
        AssetLibrary AddAssetLibrary(AssetLibrary pEntAssetLib);
        //bool AddFolder(ref AssetLibrary pAssetLibrary);
        AssetLibrary EditAssetLibrary(AssetLibrary pEntAssetLib);
        AssetLibrary DeleteAssetLibrary(AssetLibrary pEntAssetLib);
        List<AssetLibrary> GetAssetLibraryList(AssetLibrary pEntAssetLib);

    }
}
