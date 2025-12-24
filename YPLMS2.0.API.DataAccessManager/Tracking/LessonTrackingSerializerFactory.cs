using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public interface ILessonTrackingSerializerFactory
    {
        ILessonTrackingSerializer Create(ActivityContentType contentType, string courseId, string learnerId, string learnerName);
        ILessonTrackingSerializer Create(string contentType, string courseId, string learnerId, string learnerName);
    }
    public class LessonTrackingSerializerFactory : ILessonTrackingSerializerFactory
    {
        public ILessonTrackingSerializer Create(ActivityContentType contentType, string courseId, string learnerId, string learnerName)
        {
            switch (contentType)
            {
                case ActivityContentType.AICC:
                    return new AuTrackingSerializer
                    {
                        CourseId = courseId,
                        LearnerId = learnerId,
                        LearnerName = learnerName
                    };
                case ActivityContentType.Scorm12:
                case ActivityContentType.Scorm2004:
                    return new ScoTrackingSerializer();
                default:
                    throw new ArgumentException("No serializer available for specified content type.");
            }
        }

        public ILessonTrackingSerializer Create(string contentType, string courseId, string learnerId, string learnerName)
        {
            try
            {
                var type = (ActivityContentType)Enum.Parse(typeof(ActivityContentType), contentType, true);
                return Create(type, courseId, learnerId, learnerName);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }
    }
}
