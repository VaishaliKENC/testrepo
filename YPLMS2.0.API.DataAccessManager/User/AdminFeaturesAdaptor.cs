using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager
{
    public class AdminFeaturesAdaptor : IDataManager<AdminFeatures>, IAdminFeaturesAdaptor<AdminFeatures>
    {
        #region Global Declaration
        string _strConnString = string.Empty;
        CustomException _expCustom;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara;
        AdminFeatures _entAdminFeature = null;
        List<AdminFeatures> _entListAdminFeatures = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.AdminFeature.ADMIN_FEATURE_ERROR;
        #endregion

        /// <summary>
        /// To get feature data by feature id
        /// </summary>
        /// <param name="pEntAdminFeature"></param>
        /// <returns>admin feature object</returns>
        public AdminFeatures GetFeatureByID(AdminFeatures pEntAdminFeature)
        {
            _entAdminFeature = new AdminFeatures();
            _entListAdminFeatures = new List<AdminFeatures>();
            try
            {
                _entListAdminFeatures = GetFeaturesList(pEntAdminFeature);
                if (_entListAdminFeatures != null && _entListAdminFeatures.Count > 0)
                    _entAdminFeature = _entListAdminFeatures[0];
            }
            catch (Exception expCommon)
            {
               _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return _entAdminFeature;
        }


        /// <summary>
        /// To edit feature object
        /// </summary>
        /// <param name="pEntAdminFeature"></param>
        /// <returns>returns updated feature object </returns>
        public AdminFeatures EditFeature(AdminFeatures pEntAdminFeature)
        {
            _entAdminFeature = new AdminFeatures();
            _entAdminFeature = Update(pEntAdminFeature, Schema.Common.VAL_UPDATE_MODE);
            return _entAdminFeature;
        }

        /// <summary>
        /// To add/update feature information
        /// </summary>
        /// <param name="pEntAdminFeature"></param>
        /// <param name="pStrUpdateMode"></param>
        /// <returns>retunrns updated/added feature object</returns>
        private AdminFeatures Update(AdminFeatures pEntAdminFeature, string pStrUpdateMode)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            int iRowsAffected = 0;
            _sqlcmd.CommandText = Schema.AdminFeatures.PROC_UPDATE_FEATURE_MASTER;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAdminFeature.ClientId);

                if (pStrUpdateMode == Schema.Common.VAL_INSERT_MODE)
                {
                    pEntAdminFeature.ID = YPLMS.Services.IDGenerator.GetUniqueKey(12);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                }
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, null);

                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminFeatures.PARA_FEATURE_ID, pEntAdminFeature.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminFeatures.PARA_FEATURE_NAME, pEntAdminFeature.FeatureName);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntAdminFeature.FeatureDescription))
                    _sqlpara = new SqlParameter(Schema.AdminFeatures.PARA_FEATURE_DESC, pEntAdminFeature.FeatureDescription);
                else
                    _sqlpara = new SqlParameter(Schema.AdminFeatures.PARA_FEATURE_DESC, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntAdminFeature.ParentFeatureId))
                {
                    _sqlpara = new SqlParameter(Schema.AdminFeatures.PARA_FEATURE_PARENT_ID, pEntAdminFeature.ParentFeatureId);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.AdminFeatures.PARA_FEATURE_PARENT_ID, null);
                }
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pEntAdminFeature.CreatedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntAdminFeature.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                iRowsAffected = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntAdminFeature;
        }

        /// <summary>
        /// To get list if feature objects 
        /// </summary>GetFeaturesList
        /// <param name="pEntAdminFeatures"></param>
        /// <returns>List of AdminFeatures objects</returns>
        public List<AdminFeatures> GetFeaturesList(AdminFeatures pEntAdminFeatures)
        {
            _sqlObject = new SQLObject();
            _entListAdminFeatures = new List<AdminFeatures>();
            _sqlcmd = new SqlCommand();
            DataSet dset = new DataSet();
            DataTable dtableFeature;
            DataTable dtableFeatureRole;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAdminFeatures.ClientId);
                if (!String.IsNullOrEmpty(pEntAdminFeatures.ID))
                {
                    _sqlcmd.CommandText = Schema.AdminFeatures.PROC_GET_FEATURE_MASTER;
                    _sqlcmd.Parameters.AddWithValue(Schema.AdminFeatures.PARA_FEATURE_ID, pEntAdminFeatures.ID);
                }
                else
                {
                    _sqlcmd.CommandText = Schema.AdminFeatures.PROC_GET_ALL_FEATURE_MASTER;
                    if (pEntAdminFeatures.IsVisible != null)
                    {
                        _sqlpara = new SqlParameter(Schema.AdminFeatures.PARA_IS_VISIBLE, pEntAdminFeatures.IsVisible);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (pEntAdminFeatures.CreatedById != null)
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntAdminFeatures.CreatedById);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                }
                dset = _sqlObject.SqlDataAdapter(_sqlcmd, _strConnString);
                dtableFeature = dset.Tables[0];
                dtableFeatureRole = dset.Tables[1];
                foreach (DataRow drow in dtableFeature.Rows)
                {
                    _entAdminFeature = new AdminFeatures();
                    _entAdminFeature = FillAdminFeatures(drow);
                    foreach (DataRow drowFeatureRole in dtableFeatureRole.Select(Schema.AdminFeatures.COL_FEATURE_ID + "='" + _entAdminFeature.ID + "'"))
                    {
                        _entAdminFeature.AdminRoleFeatures.Add(FillFeatureRole(drowFeatureRole));
                    }
                    _entListAdminFeatures.Add(_entAdminFeature);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, _strConnString);
                throw _expCustom;
            }
            finally
            { }
            return _entListAdminFeatures;
        }
        //
        /// <summary>
        /// To get list if feature objects 
        /// </summary>GetYPLSMenu
        /// <param name="pEntAdminFeatures"></param>
        /// <returns>List of AdminFeatures objects</returns>
        //public List<AdminFeatures> GetYPLSMenu(AdminFeatures pEntAdminFeatures)
        //{
        //    _sqlObject = new SQLObject();
        //    _entListAdminFeatures = new List<AdminFeatures>();
        //    List<AdminRoleFeatures> _entLstAdminRoleFeatures = new List<AdminRoleFeatures>();
        //    _sqlcmd = new SqlCommand();
        //    DataSet dset = new DataSet();
        //    DataTable dtableFeature;
        //    DataTable dtableFeatureRole;
        //    SqlConnection sqlConnection = null;
        //    try
        //    {
        //        _strConnString = _sqlObject.GetClientDBConnString(pEntAdminFeatures.ClientId);
        //        sqlConnection = new SqlConnection(_strConnString);
        //        _sqlcmd = new SqlCommand(Schema.AdminRoleFeatures.PROC_GET_ALL_MENU_YPLS, sqlConnection);

        //        _sqlpara = new SqlParameter(Schema.ProductCatalogRegRequest.PARA_SYSTEMUSERGUID, pEntAdminFeatures.SystemUserGUID.ToString());
        //        _sqlcmd.Parameters.Add(_sqlpara);

        //        _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntAdminFeatures.ClientId);
        //        _sqlcmd.Parameters.Add(_sqlpara);

        //        //_sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntAdminFeatures.l);
        //        //_sqlcmd.Parameters.Add(_sqlpara);


        //        //SqlDataReader _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
        //        //while (_sqlreader.Read())
        //        //{
        //        //    _entAdminFeature.AdminRoleFeatures.Add(FillFeatureRole(drowFeatureRole));
        //        //    _entMenuItem = FillObject(_sqlreader);
        //        //    _entListMenuItems.Add(_entMenuItem);
        //        //}
        //            //if (pEntAdminFeatures.IsVisible != null)
        //            //{
        //            //    _sqlpara = new SqlParameter(Schema.AdminFeatures.PARA_IS_VISIBLE, pEntAdminFeatures.IsVisible);
        //            //    _sqlcmd.Parameters.Add(_sqlpara);
        //            //}

        //            //if (pEntAdminFeatures.CreatedById != null)
        //            //{
        //            //    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntAdminFeatures.CreatedById);
        //            //    _sqlcmd.Parameters.Add(_sqlpara);
        //            //}
        //        //}
        //        dset = _sqlObject.SqlDataAdapter(_sqlcmd, _strConnString);
        //        dtableFeature = dset.Tables[0];
        //       // dtableFeatureRole = dset.Tables[1];
        //        foreach (DataRow drow in dtableFeature.Rows)
        //        {

        //           AdminRoleFeatures _entAdminRoleFeatures1 = new AdminRoleFeatures();
        //            _entAdminRoleFeatures1 = FillFeatureRole(drow);
        //            _entLstAdminRoleFeatures.Add(_entAdminRoleFeatures1);
        //            //foreach (DataRow drowFeatureRole in dtableFeatureRole.Select(Schema.AdminFeatures.COL_FEATURE_ID + "='" + _entAdminFeature.ID + "'"))
        //            //{
        //            //    _entAdminFeature.AdminRoleFeatures.Add(FillFeatureRole(drowFeatureRole));
        //            //}
        //          //  _entListAdminFeatures.Add(_entAdminRoleFeatures1);
        //            //_entAdminFeature = new AdminFeatures();
        //            //_entAdminFeature = FillAdminFeatures(drow);
        //            //foreach (DataRow drowFeatureRole in dtableFeatureRole.Select(Schema.AdminFeatures.COL_FEATURE_ID + "='" + _entAdminFeature.ID + "'"))
        //            //{
        //           // _entAdminFeature.AdminRoleFeatures.Add(FillFeatureRole(drow));
        //            //}
        //           // _entListAdminFeatures.Add(_entAdminFeature);
        //        }
        //    }
        //    catch (Exception expCommon)
        //    {
        //        _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
        //        throw _expCustom;
        //    }
        //    finally
        //    { }
        //    return _entListAdminFeatures;
        //}
        /// <summary>
        /// To fill feature object from reader
        /// </summary>
        /// <param name="pDRow"></param>
        /// <returns>filled feature object</returns>
        private AdminFeatures FillAdminFeatures(DataRow pDRow)
        {
            _entAdminFeature = new AdminFeatures();

            _entAdminFeature.ID = Convert.ToString(pDRow[Schema.AdminFeatures.COL_FEATURE_ID]);
            _entAdminFeature.FeatureName = Convert.ToString(pDRow[Schema.AdminFeatures.COL_FEATURE_NAME]);
            _entAdminFeature.FeatureDescription = Convert.ToString(pDRow[Schema.AdminFeatures.COL_FEATURE_DESC]);
            _entAdminFeature.ParentFeatureId = Convert.ToString(pDRow[Schema.AdminFeatures.COL_FEATURE_PARENT_ID]);
            _entAdminFeature.CreatedById = Convert.ToString(pDRow[Schema.Common.COL_CREATED_BY]);
            _entAdminFeature.LastModifiedById = Convert.ToString(pDRow[Schema.Common.COL_MODIFIED_BY]);
            _entAdminFeature.DateCreated = Convert.ToDateTime(pDRow[Schema.Common.COL_CREATED_ON]);
            _entAdminFeature.LastModifiedDate = Convert.ToDateTime(pDRow[Schema.Common.COL_MODIFIED_ON]);
            _entAdminFeature.IsCBActivateVisible = Convert.ToBoolean(pDRow[Schema.AdminFeatures.COL_IS_CB_CAN_ACTIVATE_VISIBLE]);
            _entAdminFeature.IsCBAddVisible = Convert.ToBoolean(pDRow[Schema.AdminFeatures.COL_IS_CB_CAN_ADD_VISIBLE]);
            _entAdminFeature.IsCBCopyVisible = Convert.ToBoolean(pDRow[Schema.AdminFeatures.COL_IS_CB_CAN_COPY_VISIBLE]);
            _entAdminFeature.IsCBDeActivateVisible = Convert.ToBoolean(pDRow[Schema.AdminFeatures.COL_IS_CB_CAN_DEACTIVATE_VISIBLE]);
            _entAdminFeature.IsCBDeleteVisible = Convert.ToBoolean(pDRow[Schema.AdminFeatures.COL_IS_CB_CAN_DELETE_VISIBLE]);
            _entAdminFeature.IsCBEditVisible = Convert.ToBoolean(pDRow[Schema.AdminFeatures.COL_IS_CB_CAN_EDIT_VISIBLE]);
            _entAdminFeature.IsCBEmailVisible = Convert.ToBoolean(pDRow[Schema.AdminFeatures.COL_IS_CB_CAN_EMAIL_VISIBLE]);
            _entAdminFeature.IsCBExportVisible = Convert.ToBoolean(pDRow[Schema.AdminFeatures.COL_IS_CB_CAN_EXPORT_VISIBLE]);
            _entAdminFeature.IsCBImportVisible = Convert.ToBoolean(pDRow[Schema.AdminFeatures.COL_IS_CB_CAN_IMPORT_VISIBLE]);
            _entAdminFeature.IsCBPrintVisible = Convert.ToBoolean(pDRow[Schema.AdminFeatures.COL_IS_CB_CAN_PRINT_VISIBLE]);
            _entAdminFeature.IsCBUploadVisible = Convert.ToBoolean(pDRow[Schema.AdminFeatures.COL_IS_CB_CAN_UPLOAD_VISIBLE]);
            _entAdminFeature.IsCBViewVisible = Convert.ToBoolean(pDRow[Schema.AdminFeatures.COL_IS_CB_VIEW_VISIBLE]);

            if (pDRow.Table.Columns.Contains(Schema.AdminRoleFeatures.COL_CAN_ADD))
            {
                _entAdminFeature.bCanAddVisible = Convert.ToBoolean(pDRow[Schema.AdminRoleFeatures.COL_CAN_ADD]);
            }

            if (pDRow.Table.Columns.Contains(Schema.AdminRoleFeatures.COL_CAN_ACTIVATE))
            {
                _entAdminFeature.bCanActivateVisible = Convert.ToBoolean(pDRow[Schema.AdminRoleFeatures.COL_CAN_ACTIVATE]);
            }

            if (pDRow.Table.Columns.Contains(Schema.AdminRoleFeatures.COL_CAN_COPY))
            {
                _entAdminFeature.bCanCopyVisible = Convert.ToBoolean(pDRow[Schema.AdminRoleFeatures.COL_CAN_COPY]);
            }

            if (pDRow.Table.Columns.Contains(Schema.AdminRoleFeatures.COL_CAN_DEACTIVATE))
            {
                _entAdminFeature.bCanDeActivateVisible = Convert.ToBoolean(pDRow[Schema.AdminRoleFeatures.COL_CAN_DEACTIVATE]);
            }

            if (pDRow.Table.Columns.Contains(Schema.AdminRoleFeatures.COL_CAN_DELETE))
            {
                _entAdminFeature.bCanDeleteVisible = Convert.ToBoolean(pDRow[Schema.AdminRoleFeatures.COL_CAN_DELETE]);
            }

            if (pDRow.Table.Columns.Contains(Schema.AdminRoleFeatures.COL_CAN_EDIT))
            {
                _entAdminFeature.bCanEditVisible = Convert.ToBoolean(pDRow[Schema.AdminRoleFeatures.COL_CAN_EDIT]);
            }

            if (pDRow.Table.Columns.Contains(Schema.AdminRoleFeatures.COL_CAN_EMAIL))
            {
                _entAdminFeature.bCanEmailVisible = Convert.ToBoolean(pDRow[Schema.AdminRoleFeatures.COL_CAN_EMAIL]);
            }

            if (pDRow.Table.Columns.Contains(Schema.AdminRoleFeatures.COL_CAN_EXPORT))
            {
                _entAdminFeature.bCanExportVisible = Convert.ToBoolean(pDRow[Schema.AdminRoleFeatures.COL_CAN_EXPORT]);
            }

            if (pDRow.Table.Columns.Contains(Schema.AdminRoleFeatures.COL_CAN_IMPORT))
            {
                _entAdminFeature.bCanImportVisible = Convert.ToBoolean(pDRow[Schema.AdminRoleFeatures.COL_CAN_IMPORT]);
            }

            if (pDRow.Table.Columns.Contains(Schema.AdminRoleFeatures.COL_CAN_PRINT))
            {
                _entAdminFeature.bCanPrintVisible = Convert.ToBoolean(pDRow[Schema.AdminRoleFeatures.COL_CAN_PRINT]);
            }

            if (pDRow.Table.Columns.Contains(Schema.AdminRoleFeatures.COL_CAN_UPLOAD))
            {
                _entAdminFeature.bCanUploadVisible = Convert.ToBoolean(pDRow[Schema.AdminRoleFeatures.COL_CAN_UPLOAD]);
            }

            if (pDRow.Table.Columns.Contains(Schema.AdminRoleFeatures.COL_CAN_VIEW))
            {
                _entAdminFeature.bCanViewVisible = Convert.ToBoolean(pDRow[Schema.AdminRoleFeatures.COL_CAN_VIEW]);
            }

            return _entAdminFeature;
        }

        /// <summary>
        /// To get the AdminRoleFeatures 
        /// </summary>
        /// <param name="pDRow"></param>
        /// <returns>Admin Role Features object</returns>
        private AdminRoleFeatures FillFeatureRole(DataRow pDRow)
        {
            AdminRoleFeatures entRoleFeatures = new AdminRoleFeatures();
            entRoleFeatures.RoleId = Convert.ToString(pDRow[Schema.AdminRole.COL_ROLE_ID]);
            entRoleFeatures.AdminFeatureId = Convert.ToString(pDRow[Schema.AdminFeatures.COL_FEATURE_ID]);
            if (pDRow[Schema.AdminRoleFeatures.COL_CAN_VIEW] != null)
            {
                entRoleFeatures.CanView = Convert.ToBoolean(pDRow[Schema.AdminRoleFeatures.COL_CAN_VIEW]);
            }
            if (pDRow[Schema.AdminRoleFeatures.COL_CAN_ADD] != null)
            {
                entRoleFeatures.CanAdd = Convert.ToBoolean(pDRow[Schema.AdminRoleFeatures.COL_CAN_ADD]);
            }
            if (pDRow[Schema.AdminRoleFeatures.COL_CAN_EDIT] != null)
            {
                entRoleFeatures.CanEdit = Convert.ToBoolean(pDRow[Schema.AdminRoleFeatures.COL_CAN_EDIT]);
            }
            if (pDRow[Schema.AdminRoleFeatures.COL_CAN_DELETE] != null)
            {
                entRoleFeatures.CanDelete = Convert.ToBoolean(pDRow[Schema.AdminRoleFeatures.COL_CAN_DELETE]);
            }
            if (pDRow[Schema.AdminRoleFeatures.COL_CAN_PRINT] != null)
            {
                entRoleFeatures.CanPrint = Convert.ToBoolean(pDRow[Schema.AdminRoleFeatures.COL_CAN_PRINT]);
            }
            if (pDRow[Schema.AdminRoleFeatures.COL_CAN_EXPORT] != null)
            {
                entRoleFeatures.CanExport = Convert.ToBoolean(pDRow[Schema.AdminRoleFeatures.COL_CAN_EXPORT]);
            }
            if (pDRow[Schema.AdminRoleFeatures.COL_CAN_COPY] != null)
            {
                entRoleFeatures.CanCopy = Convert.ToBoolean(pDRow[Schema.AdminRoleFeatures.COL_CAN_COPY]);
            }
            if (pDRow[Schema.AdminRoleFeatures.COL_CAN_EMAIL] != null)
            {
                entRoleFeatures.CanEmail = Convert.ToBoolean(pDRow[Schema.AdminRoleFeatures.COL_CAN_EMAIL]);
            }
            if (pDRow[Schema.AdminRoleFeatures.COL_CAN_IMPORT] != null)
            {
                entRoleFeatures.CanImport = Convert.ToBoolean(pDRow[Schema.AdminRoleFeatures.COL_CAN_IMPORT]);
            }
            if (pDRow[Schema.AdminRoleFeatures.COL_CAN_UPLOAD] != null)
            {
                entRoleFeatures.CanUpload = Convert.ToBoolean(pDRow[Schema.AdminRoleFeatures.COL_CAN_UPLOAD]);
            }
            if (pDRow[Schema.AdminRoleFeatures.COL_CAN_ACTIVATE] != null)
            {
                entRoleFeatures.CanActivate = Convert.ToBoolean(pDRow[Schema.AdminRoleFeatures.COL_CAN_ACTIVATE]);
            }
            if (pDRow[Schema.AdminRoleFeatures.COL_CAN_DEACTIVATE] != null)
            {
                entRoleFeatures.CanDeactivate = Convert.ToBoolean(pDRow[Schema.AdminRoleFeatures.COL_CAN_DEACTIVATE]);
            }
            return entRoleFeatures;
        }

        /// <summary>
        /// To Update AdminRoleFeaturesMaster IsVisible
        /// </summary>
        /// <param name="pEntAdminFeature"></param>        
        /// <returns>AdminFeatures object</returns>
        public AdminFeatures UpdateIsVisible(AdminFeatures pEntAdminFeature)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            int iRowsAffected = 0;
            _sqlcmd.CommandText = Schema.AdminFeatures.PROC_UPDATE_ISVISIBLE_FEATURE_MASTER;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAdminFeature.ClientId);

                _sqlpara = new SqlParameter(Schema.AdminFeatures.PARA_FEATURE_ID, pEntAdminFeature.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminFeatures.PARA_IS_VISIBLE, pEntAdminFeature.IsVisible);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntAdminFeature.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                iRowsAffected = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntAdminFeature;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntAdminFeatures"></param>
        /// <returns></returns>
        public AdminFeatures Get(AdminFeatures pEntAdminFeatures)
        {
            return GetFeatureByID(pEntAdminFeatures);
        }
        /// <summary>
        /// Update AdminFeatures
        /// </summary>
        /// <param name="pEntAdminFeatures"></param>
        /// <returns></returns>
        public AdminFeatures Update(AdminFeatures pEntAdminFeatures)
        {
            return EditFeature(pEntAdminFeatures);
        }
        #endregion
    }
}
