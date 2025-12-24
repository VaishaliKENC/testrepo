using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager.Content
{
    public class AssetManager:IManager<Asset, Asset.Method, Asset.ListMethod>
    {
        /// <summary>
        /// Default constructor for AssetManager
        /// </summary>
        public AssetManager()
        {

        }

        /// <summary>
        /// Use for Read,Add,Update,Delete Asset transactions
        /// </summary>
        /// <param name="pEntAsset"></param>
        /// <param name="pMethod"></param>
        /// <returns>Asset object</returns>
        public Asset Execute(Asset pEntAsset, Asset.Method pMethod)
        {
            Asset entAssetReturn = null;
            AssetAdaptor adaptorAsset = new AssetAdaptor();
            switch (pMethod)
            {
                case Asset.Method.GetRelativePath:
                    entAssetReturn = adaptorAsset.GetAssetRelativePathById(pEntAsset);
                    break;
                case Asset.Method.Get:
                    entAssetReturn = adaptorAsset.GetAssetById(pEntAsset);
                    break;
                case Asset.Method.Add:
                    entAssetReturn = adaptorAsset.AddAsset(pEntAsset);
                    break;
                case Asset.Method.Update:
                    entAssetReturn = adaptorAsset.EditAsset(pEntAsset);
                    break;
                case Asset.Method.Delete:
                    entAssetReturn = adaptorAsset.DeleteAsset(pEntAsset);
                    break;
                case Asset.Method.UpdateLanguage:
                    entAssetReturn = adaptorAsset.UpdateLanguage(pEntAsset);
                    break;
                case Asset.Method.DeleteLanguage:
                    entAssetReturn = adaptorAsset.DeleteAssetLanguage(pEntAsset);
                    break;
                case Asset.Method.SearchRelativePath:
                    entAssetReturn = adaptorAsset.SearchRelativePath(pEntAsset);
                    break;
                default:
                    entAssetReturn = null;
                    break;
            }
            return entAssetReturn;
        }

        /// <summary>
        /// Used for finding Asset List and Get all Asset List
        /// </summary>
        /// <param name="pEntAsset"></param>
        /// <param name="pMethod"></param>
        /// <returns>List of Asset objects</returns>
        public List<Asset> Execute(Asset pEntAsset, Asset.ListMethod pMethod)
        {
            List<Asset> entListAsset = new List<Asset>();
            AssetAdaptor adaptorAsset = new AssetAdaptor();
            switch (pMethod)
            {
                case Asset.ListMethod.GetAll:
                    entListAsset = adaptorAsset.GetAssetList(pEntAsset);
                    break;
                case Asset.ListMethod.GetAllForAssignments:
                    entListAsset = adaptorAsset.GetAssetListForAssignments(pEntAsset);
                    break;
                case Asset.ListMethod.GetAssetLanguages:
                    entListAsset = adaptorAsset.GetAllProgramLanguage(pEntAsset);
                    break;
                default:
                    break;
            }
            return entListAsset;
        }


        /// <summary>
        /// Returns Dataset as per method
        /// </summary>
        /// <typeparam name="Asset"></typeparam>
        /// <param name="pEntBase">Asset object</param>
        /// <param name="pMethod">Asset.ListMethod</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(Asset pEntBase, Asset.ListMethod pMethod)
        {
            DataSet dataSet = null;
            List<Asset> listAsset = Execute(pEntBase, pMethod);
            YPLMS.Services.Converter dsConverter = new YPLMS.Services.Converter();
            dataSet = dsConverter.ConvertToDataSet<Asset>(listAsset);
            return dataSet;

        }
    }
}
