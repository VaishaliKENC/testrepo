using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public interface IAutoEmailTemplateSettingAdaptor<T>
    {
        AutoEmailTemplateSetting GetEmailEventById(AutoEmailTemplateSetting pEntEmailEvent);
        AutoEmailTemplateSetting GetEmailTempId(AutoEmailTemplateSetting pEntEmailEvent);
        List<AutoEmailTemplateSetting> GetEmailTemplateSettingList(AutoEmailTemplateSetting pEntEmailEvent);

    }
}
