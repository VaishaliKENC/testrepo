using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public interface ILanguageDAM<T>
    {
        Language GetLanguageByID(Language pEntLanguage);
        List<Language> GetMasterLanguages();
        Language GetMasterLanguageByID(Language pEntLanguage);
        List<Language> GetClientLanguages(Language pLanguage);
        List<Language> AddSelectedClientLanguages(List<Language> pEntListLanguage);
    }
}
