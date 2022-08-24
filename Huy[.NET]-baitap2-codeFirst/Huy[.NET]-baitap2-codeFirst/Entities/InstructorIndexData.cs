using Huy_.NET__baitap2_codeFirst.Models;

namespace Huy_.NET__baitap2_codeFirst.Entities
{
    public class InstructorIndexData
    {
        public IEnumerable<InstructorInfo> InstructorsInfo { get; set; }
        public IEnumerable<CourseInfo> CoursesInfo { get; set; }
        public IEnumerable<EnrollmentInfo> EnrollmentsInfo { get; set; }
    }
}
