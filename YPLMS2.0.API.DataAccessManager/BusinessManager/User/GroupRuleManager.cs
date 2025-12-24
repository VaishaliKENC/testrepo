using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager
{
    public class GroupRuleManager
    {
                
        public DataView GetClientFieldsFilter(string left, string right, string condition, GroupRule groupRule)
        {
            try
            {
                DataSet ds = GetClientFields(groupRule.ClientId, ImportAction.Report);
                if (ds != null)
                {
                    DataView vw = ds.Tables[0].DefaultView;
                    if (right.Equals(BusinessRuleReportsKeys.STANDARD_CUSTOM))
                    {
                        vw.RowFilter = left + " " + condition + "'" + right + "'" +
                       " OR FieldName='HireDate' AND FieldTypes='StandardCustom'" +
                       " OR FieldName='ManagerName' AND FieldTypes='Standard' " +
                       " OR FieldName='CurrentRegionView' AND FieldTypes='Standard'";
                    }
                    else
                        vw.RowFilter = left + " " + condition + "'" + right + "'";

                    return vw;
                }
            }
            catch (CustomException ex)
            {
                
            }
            return null;
        }


        static public DataSet GetClientFields(string pstrClientId, ImportAction pImpAction)
        {
            try
            {
                DataSet dsImpDef = new DataSet();
               ImportDefinitionAdaptor _importDefinitionAdaptor = new ImportDefinitionAdaptor();
               ImportDefination entImpDef = new ImportDefination();
                List<ImportDefination> entListImpdef = new List<ImportDefination>();
                entImpDef.ClientId = pstrClientId;
                entImpDef.ImportAction = pImpAction;
                entListImpdef = _importDefinitionAdaptor.GetImportDefinationList(entImpDef);
                API.YPLMS.Services.Converter dsConverter = new API.YPLMS.Services.Converter();
                 dsImpDef = dsConverter.ConvertToDataSet<ImportDefination>(entListImpdef);

                if (dsImpDef != null)
                {
                    if (dsImpDef.Tables[0].Rows.Count > 0 && dsImpDef.Tables.Count > 0)
                    {
                        foreach (DataRow dr in dsImpDef.Tables[0].Rows)
                        {
                            if (dr["FieldName"].ToString() == "DateOfRegistration")
                                dr["FieldName"] = "HireDate";
                        }
                    }
                }

                return dsImpDef;
            }
            catch
            {
                return null;
            }
        }

    }
}
