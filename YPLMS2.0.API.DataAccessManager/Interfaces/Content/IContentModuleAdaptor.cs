using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public interface IContentModuleAdaptor<T>
    {
        ContentModule GetContentModuleByID(ContentModule pEntContModule);
        ContentModule CheckLockCourseAssessment(ContentModule pEntContModule);
        ContentModule GetContentModuleByID_CoursePlayer(ContentModule pEntContModule);
        ContentModule GetContentModuleURL(ContentModule pEntContModule);
        ContentModule SearchContentModuleURL(ContentModule pEntContModule);
        ContentModule GetContentModuleByID_Learner(ContentModule pEntContModule);
        ContentModule FindContentModuleByName(ContentModule pEntContModule);
        ContentModule AddContentModule(ContentModule pEntContModule);
        ContentModule EditContentModule(ContentModule pEntContModule);
        List<ContentModule> BulkDelete(List<ContentModule> pEntListContentModule);
        ContentModule UpdateCourseDetails(ContentModule pEntContModule);
        List<ContentModule> GetContentModuleList(ContentModule pEntContentModule);
        List<ContentModule> GetContentModuleListNotCompletedList(ContentModule pEntContentModule);
        List<ContentModule> GetContentModuleListAdminHome(ContentModule pEntContentModule);
        List<ContentModule> AddAllModules(List<ContentModule> pEntListContentModule);
        List<ContentModuleLanguages> AddAllModuleLanguages(List<ContentModuleLanguages> pEntListContentModuleLanguages);
        List<YPLMS2._0.API.Entity.ContentModule> FindContentModule(Search pEntSearch);
    }
}
