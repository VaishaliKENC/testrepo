using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager.Content
{

    public class FilesystemContentModuleRetriever : IContentModuleRetriever
    {
        private readonly ICacheProvider _cache;
        private readonly IContentModuleRepository _contentModuleRepositorys;
        private readonly ICourseConfigurationRepository _courseConfigurationRepository;
        ContentModuleAdaptor _contentModuleRepository = new ContentModuleAdaptor();

        public FilesystemContentModuleRetriever() { }
        public FilesystemContentModuleRetriever(ICacheProvider cacheProvider, IContentModuleRepository contentModuleRepositorys, ICourseConfigurationRepository courseConfigurationRepository)
        {
            _cache = cacheProvider;
            _contentModuleRepositorys = contentModuleRepositorys;
            _courseConfigurationRepository = courseConfigurationRepository;
        }

        public ContentModule GetContentModule(string clientId, string courseId)
        {
            string cacheKey = "ContentModule-" + clientId + "-" + courseId;
            if (_cache.ContainsKey(cacheKey))
            {
                return _cache.Get<ContentModule>(cacheKey);
            }

            var course = _contentModuleRepository.GetByIdForCoursePlayer(clientId, courseId);

            // course.MasteryScore = _courseConfigurationRepository.GetMasteryScore(clientId); this statement commenetd by sarita bez now mastery score will included in course table itself.

            var manifestPath = GetManifestPath(clientId, course);
            //commented by santosh for Local testing
            //  manifestPath = "\\\\\\\\santoshlpc\\\\TestYPLMS_Content\\\\Courses\\\\" + courseId + "\\\\imsmanifest.xml";
            var contentType = (ActivityContentType)Enum.Parse(typeof(ActivityContentType), course.ContentModuleTypeId);
            course.Sections = ReadSectionsFromManifest(manifestPath, course.MasteryScore, contentType);
            //commented by santosh for Local testing
            //_cache.Add(cacheKey, course, manifestPath);

            return course;
        }
        public ContentModule GetContentModuleForScrom2004(string clientId, string courseId)
        {
            string cacheKey = "ContentModule-" + clientId + "-" + courseId;
            if (_cache.ContainsKey(cacheKey))
            {
                return _cache.Get<ContentModule>(cacheKey);
            }

            var course = _contentModuleRepository.GetByIdForCoursePlayer(clientId, courseId);

            // course.MasteryScore = _courseConfigurationRepository.GetMasteryScore(clientId); this statement commenetd by sarita bez now mastery score will included in course table itself.


            var manifestPath = GetManifestPath(clientId, course);
            //commented by santosh for Local testing
            //  manifestPath = "\\\\\\\\santoshlpc\\\\TestYPLMS_Content\\\\Courses\\\\" + courseId + "\\\\imsmanifest.xml";
            var contentType = (ActivityContentType)Enum.Parse(typeof(ActivityContentType), course.ContentModuleTypeId);
            course.Sections = ReadSectionsFromManifestForScrom2004(manifestPath, course.MasteryScore, contentType);
            //commented by santosh for Local testing
            //_cache.Add(cacheKey, course, manifestPath);

            return course;
        }

        public ContentModule GetMetaData(string clientId, string courseId)
        {
            return _contentModuleRepository.GetById(clientId, courseId);
        }

        private string GetManifestPath(string clientId, ContentModule course)
        {
            var fileHandler = new FileHandler(clientId);
            string rootSharedPath = fileHandler.RootSharedPath;
            string contentFolderPath = rootSharedPath + course.ContentModuleURL.Replace("/", @"\\");
            course.AbsoluteFolderUrl = fileHandler.RootHTTPUrl + course.ContentModuleURL;

            if (String.IsNullOrEmpty(course.ImanifestUrl))
            {
                return contentFolderPath + "\\\\imsmanifest.xml";
            }
            return rootSharedPath + course.ImanifestUrl.Replace("/", @"\");
        }

        private Dictionary<string, CourseSection> ReadSectionsFromManifest(string manifestPath, int masteryScore, ActivityContentType courseType)
        {
            var sections = new Dictionary<string, CourseSection>();
            var manifestXml = new XmlDocument();

            try
            {
                manifestXml.Load(manifestPath);
                manifestXml = new XMLLib().StripDocumentNamespace(manifestXml);
            }
            catch (Exception ex)
            {
                var cex = new CustomException("Could not load manifest XML: " + manifestPath, "XML load failure",
                                              ExceptionSeverityLevel.Critical, ex, true);
                return null;
            }

            XmlNodeList sectionNodeList =
                manifestXml.SelectNodes("/manifest/organizations/organization");
            if (sectionNodeList == null) return sections;

            var lessonReaderFactory = new LessonReaderFactory(courseType);

            int sectionNumber = 1;

            foreach (XmlNode sectionNode in sectionNodeList)
            {
                var titleNode = sectionNode.SelectSingleNode("title");
                string title = titleNode == null ? String.Empty : titleNode.InnerText;
                string identifier = ((XmlElement)sectionNode).GetAttribute("identifier");
                var lessons = new Dictionary<string, Lesson>();
                XmlNodeList lessonNodeList = sectionNode.SelectNodes("//item[@identifierref | @resourceref]");
                if (lessonNodeList != null)
                {
                    int lessonNumber = 1;
                    foreach (XmlNode lessonNode in lessonNodeList)
                    {
                        var lessonReader = lessonReaderFactory.GetLessonReader(lessonNode, masteryScore);
                        var lesson = lessonReader.ReadLesson();
                        lesson.SortOrder = lessonNumber;
                        lessons.Add(lesson.Identifier, lesson);
                        lessonNumber++;
                    }
                }

                sections.Add(identifier, new CourseSection
                {
                    Lessons = lessons,
                    Identifier = identifier,
                    Title = title,
                    SortOrder = sectionNumber
                });
                sectionNumber++;
            }
            return sections;
        }

        private Dictionary<string, CourseSection> ReadSectionsFromManifestForScrom2004(string manifestPath, int masteryScore, ActivityContentType courseType)
        {
            var sections = new Dictionary<string, CourseSection>();
            var manifestXml = new XmlDocument();

            var mgr = new XmlNamespaceManager(manifestXml.NameTable);
            mgr.AddNamespace("imsss", "http://www.imsglobal.org/xsd/imsss");

            try
            {
                manifestXml.Load(manifestPath);
                manifestXml = new XMLLib().StripDocumentNamespace(manifestXml);
            }
            catch (Exception ex)
            {
                var cex = new CustomException("Could not load manifest XML: " + manifestPath, "XML load failure",
                                              ExceptionSeverityLevel.Critical, ex, true);
                return null;
            }

            XmlNodeList sectionNodeList =
                manifestXml.SelectNodes("/manifest/organizations/organization");
            if (sectionNodeList == null) return sections;

            var lessonReaderFactory = new LessonReaderFactory(courseType);

            int sectionNumber = 1;

            foreach (XmlNode sectionNode in sectionNodeList)
            {
                var titleNode = sectionNode.SelectSingleNode("title");
                string title = titleNode == null ? String.Empty : titleNode.InnerText;
                string identifier = ((XmlElement)sectionNode).GetAttribute("identifier");
                var lessons = new Dictionary<string, Lesson>();


                XmlNodeList lessonNodeList = sectionNode.SelectNodes("//item[@identifierref | @resourceref]");
                if (lessonNodeList != null)
                {
                    int lessonNumber = 1;
                    foreach (XmlNode lessonNode in lessonNodeList)
                    {
                        var objectives = new List<ObjectiveTracking>();
                        var lessonReader = lessonReaderFactory.GetLessonReader(lessonNode, masteryScore);
                        var lesson = lessonReader.ReadLesson();
                        lesson.SortOrder = lessonNumber;
                        //lessons.Add(lesson.Identifier, lesson);
                        //lessonNumber++;


                        XmlNodeList objectiveNodeList = lessonNode.SelectNodes("imsss:sequencing/imsss:objectives/imsss:primaryObjective", mgr);

                        if (objectiveNodeList != null)
                        {
                            // int objectiveNumber = 1;
                            foreach (XmlNode objectiveNode in objectiveNodeList)
                            {
                                string strIdentifier = objectiveNode.Attributes["objectiveID"].Value;
                                if (!string.IsNullOrEmpty(strIdentifier))
                                {

                                    ObjectiveTracking obj = new ObjectiveTracking();
                                    obj.Identifier = strIdentifier;
                                    obj.enumObjectiveType = ObjectiveType.Primary;
                                    objectives.Add(obj);
                                }
                            }
                            lesson.Objectives = objectives;
                        }


                        XmlNodeList objectiveNList = lessonNode.SelectNodes("imsss:sequencing/imsss:objectives/imsss:objective", mgr);

                        if (objectiveNList != null)
                        {
                            // int objectiveNumber = 1;
                            foreach (XmlNode objectiveNode in objectiveNList)
                            {
                                string strIdentifier = objectiveNode.Attributes["objectiveID"].Value;
                                if (!string.IsNullOrEmpty(strIdentifier))
                                {

                                    ObjectiveTracking obj = new ObjectiveTracking();
                                    obj.Identifier = strIdentifier;
                                    obj.enumObjectiveType = ObjectiveType.Normal;
                                    objectives.Add(obj);
                                }
                            }
                            lesson.Objectives = objectives;
                        }


                        lessons.Add(lesson.Identifier, lesson);
                        lessonNumber++;
                    }
                }

                sections.Add(identifier, new CourseSection
                {
                    Lessons = lessons,
                    Identifier = identifier,
                    Title = title,
                    SortOrder = sectionNumber
                });
                sectionNumber++;
            }
            return sections;
        }
    }
}
