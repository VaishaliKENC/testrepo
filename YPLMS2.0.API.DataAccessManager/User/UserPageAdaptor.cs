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
    public class UserPageAdaptor : IDataManager<UserPage>
    {
        #region Declaration
        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlCommand _sqlcmd = null;
        UserPage entUserPage = null;
        List<UserPage> entListPages = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.UserPage.BL_ERROR;
        string _strConnString = string.Empty;
        #endregion

        /// <summary>
        /// Get User Page By Id
        /// </summary>
        /// <param name="pEntUserPage"></param>
        /// <returns></returns>
        public UserPage GetUserPageById(UserPage pEntUserPage)
        {
            entListPages = new List<UserPage>();
            entUserPage = new UserPage();
            List<UserPageElementLanguage> entListElement = new List<UserPageElementLanguage>();
            UserPageElementLanguage entElement = new UserPageElementLanguage();
            UserPageElementLanguageAdaptor elementAdaptor = new UserPageElementLanguageAdaptor();
            entListPages = GetUserPageList(pEntUserPage);
            if (entListPages.Count > 0)
            {
                entUserPage = entListPages[0];
                entElement.ID = entUserPage.ID;
                entElement.ClientId = pEntUserPage.ClientId;
                entElement.LanguageID = pEntUserPage.ParaLanguageId;
                entListElement = elementAdaptor.GetPageElementList(entElement);
                if (entListElement.Count > 0)
                    entUserPage.PageElementLanguage.AddRange(entListElement);
            }
            return entUserPage;
        }

        /// <summary>
        /// Get User Page List
        /// </summary>
        /// <param name="pEntUserPage"></param>
        /// <returns></returns>
        public List<UserPage> GetUserPageList(UserPage pEntUserPage)
        {
            SqlConnection sqlConnection = null;
            entListPages = new List<UserPage>();
            entUserPage = new UserPage();
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            SqlParameter _sqlpara = null;
            try
            {
                if (string.IsNullOrEmpty(pEntUserPage.ID))
                    _sqlcmd.CommandText = Schema.UserPage.PROC_GET_ALL_USER_PAGES;
                else
                {
                    _sqlcmd.CommandText = Schema.UserPage.PROC_GET_USER_PAGE;
                    _sqlcmd.Parameters.AddWithValue(Schema.UserPage.PARA_PAGE_ID, pEntUserPage.ID);
                }
                if (pEntUserPage.ListRange != null)
                {
                    if (!string.IsNullOrEmpty(pEntUserPage.ListRange.SortExpression))
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntUserPage.ListRange.SortExpression);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                }
                _strConnString = _sqlObject.GetClientDBConnString(pEntUserPage.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);

                while (_sqlreader.Read())
                {
                    entUserPage = FillObject(_sqlreader);
                    entUserPage.ParaLanguageId = pEntUserPage.ParaLanguageId;
                    entListPages.Add(entUserPage);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed)
                    _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return entListPages;
        }

        /// <summary>
        /// Fill Object
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns></returns>
        private UserPage FillObject(SqlDataReader pReader)
        {
            UserPage entUserpage = new UserPage();
            int iIndex;
            if (pReader.HasRows)
            {
                iIndex = pReader.GetOrdinal(Schema.UserPage.COL_PAGE_ID);
                if (!pReader.IsDBNull(iIndex))
                    entUserpage.ID = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.UserPage.COL_PAGE_ENGLISH_NAME);
                if (!pReader.IsDBNull(iIndex))
                    entUserpage.PageEnglishName = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.UserPage.COL_PAGE_FILE_URL);
                if (!pReader.IsDBNull(iIndex))
                    entUserpage.PageFileURL = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.UserPage.COL_DISPLAY_ORDER);
                if (!pReader.IsDBNull(iIndex))
                    entUserpage.DisplayOrder = pReader.GetInt32(iIndex);

                iIndex = pReader.GetOrdinal(Schema.UserPage.COL_CONFIG_TYPE);
                if (!pReader.IsDBNull(iIndex))
                    entUserpage.ConfigType = (UserPage.UserPagesConfigType)Enum.Parse(typeof(UserPage.UserPagesConfigType), pReader.GetString(iIndex));
            }
            return entUserpage;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntUserPage"></param>
        /// <returns></returns>
        public UserPage Get(UserPage pEntUserPage)
        {
            return GetUserPageById(pEntUserPage);
        }
        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="pEntUserPage"></param>
        /// <returns></returns>
        public UserPage Update(UserPage pEntUserPage)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
