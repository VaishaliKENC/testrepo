using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
   
    public class AssetLibraryManager
    {
        AssetLibraryAdaptor assetLibraryAdaptor = new AssetLibraryAdaptor();
        public DataSet GetAssetDataSet(Learner learner, string clientId)
        {
            XmlDocument xmlDoc = new XmlDocument();
            DataSet dsAssetFolderGet = new DataSet();

            AssetLibrary assetLib = new AssetLibrary
            {
                ClientId = clientId
            };

            if (!(learner.IsSiteAdmin() || learner.IsSuperAdmin()))
                assetLib.CreatedById = learner.ID;


            assetLib = assetLibraryAdaptor.GetAssetLibraryById(assetLib);

            if (assetLib != null)
            {
                xmlDoc = assetLib.FolderTreeXml;
                dsAssetFolderGet.ReadXml(new XmlNodeReader(xmlDoc));
            }

            return dsAssetFolderGet;
        }

        public List<AssetFolderNode> GetAssetTree(Learner learner, string clientId)
    {
        DataSet dsAssetFolders = GetAssetDataSet( learner,  clientId);
        List<AssetFolderNode> tree = new List<AssetFolderNode>();

        if (dsAssetFolders.Tables.Contains("Category"))
        {
            DataTable categoryTable = dsAssetFolders.Tables["Category"];
            DataRow[] rootFolders = categoryTable.Select("parentid=0");

            foreach (var row in rootFolders)
            {
                var rootNode = new AssetFolderNode
                {
                    Title = row["title"].ToString(),
                    UnitId = row["UnitId"].ToString(),
                    ParentId = "",
                    FolderDescription = row["AssetFolderDescription"].ToString(),
                    Children = GetChildFolders(categoryTable, row["UnitId"].ToString())
                };

                tree.Add(rootNode);
            }
        }

        return tree;
    }


        public string GetFolderPath(Learner plearner, AssetLibrary pEntAssetLib)
        {
            string strFolderPath = string.Empty;
            string strCategoryId = string.Empty;
            DataRow[] findRowCategory;
            DataRow[] findRowLevel;


            DataSet _dsAssetFolders = GetAssetDataSet(plearner, pEntAssetLib.ClientId);

            //check if DataSet contains tables in it
            if (_dsAssetFolders.Tables.Count > 0)
            {
                //check if DataSet contains Category table
                if (_dsAssetFolders.Tables.Contains("Category"))
                {
                    //check if Category table contains rows
                    if (_dsAssetFolders.Tables["Category"].Rows.Count > 0)
                    {
                        findRowCategory = _dsAssetFolders.Tables["Category"].Select("UnitId='" + pEntAssetLib.ParentFolderId + "'");

                        //check if UnitId is found
                        if (findRowCategory != null && findRowCategory.Length > 0)
                        {
                            strCategoryId = Convert.ToString(findRowCategory[0]["Category_Id"]);
                        }
                    }
                }

                //check if strCategoryId is not Null or Empty
                if (!string.IsNullOrEmpty(strCategoryId))
                {
                    //check DataSet contains Levels tables
                    if (_dsAssetFolders.Tables.Contains("Levels"))
                    {
                        //check if Levels table contains rows in it
                        if (_dsAssetFolders.Tables["Levels"].Rows.Count > 0)
                        {
                            findRowLevel = _dsAssetFolders.Tables["Levels"].Select("Category_Id='" + strCategoryId + "'");

                            //check if strCategoryId is found in Levels table
                            if (findRowLevel != null && findRowLevel.Length > 0)
                            {
                                strFolderPath = Convert.ToString(findRowLevel[0]["pathval"]);
                            }
                        }
                    }
                }
            }


            return strFolderPath;

        }

        private List<AssetFolderNode> GetChildFolders(DataTable categoryTable, string parentId)
    {
        List<AssetFolderNode> children = new List<AssetFolderNode>();
        DataRow[] subFolders = categoryTable.Select($"ParentUnitId='{parentId}'", "title ASC");

        foreach (var row in subFolders)
        {
            var node = new AssetFolderNode
            {
                Title = row["title"].ToString(),
                UnitId = row["UnitId"].ToString(),
                ParentId=parentId,
                FolderDescription= row["AssetFolderDescription"].ToString(),
                Children = GetChildFolders(categoryTable, row["UnitId"].ToString())
            };

            children.Add(node);
        }

        return children;
    }
    }

    public class AssetFolderNode
    {
        public string Title { get; set; }
        public string UnitId { get; set; }
        public string ParentId { get; set; }
        public string FolderDescription { get; set; }
        public List<AssetFolderNode> Children { get; set; } = new List<AssetFolderNode>();
        
    }
}
