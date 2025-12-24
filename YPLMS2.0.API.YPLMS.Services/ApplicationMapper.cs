using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.YPLMS.Services.Messages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.Entity.ViewModel;

namespace YPLMS2._0.API.YPLMS.Services
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper() 
        {
            CreateMap<LearnerVM, Learner>().ReverseMap();
            CreateMap<SearchVM, Search>().ReverseMap();
            CreateMap<GroupRuleVM, Entity.GroupRule>().ReverseMap();        
            CreateMap<ImportDefinationVM, ImportDefination>().ReverseMap();
            CreateMap<ContentModuleVM,Entity.ContentModule>().ReverseMap();
            CreateMap<ContentModuleLanguagesVM, ContentModuleLanguages>().ReverseMap();
            CreateMap<ContentModuleUploadVM, Entity.ContentModule>().ReverseMap();
            CreateMap<ContentModuleSessionVM, Entity.ContentModuleSession>().ReverseMap();
            CreateMap<ActivityAssignmentVM, Entity.ActivityAssignment>().ReverseMap();
        }
    }
}
