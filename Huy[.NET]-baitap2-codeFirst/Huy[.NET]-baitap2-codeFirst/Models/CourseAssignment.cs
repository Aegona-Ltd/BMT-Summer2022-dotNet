using System.ComponentModel.DataAnnotations.Schema;

namespace Huy_.NET__baitap2_codeFirst.Models
{
    public class CourseAssignment
    {

        public int InstructorID { get; set; }
        public int CourseID { get; set; }
        public Instructor? Instructor { get; set; }
        public Course? Course { get; set; }
    }
}
