using Huy_.NET__baitap2_codeFirst.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Huy_.NET__baitap2_codeFirst.Entities
{
    public class CourseAssignmentInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InstructorID { get; set; }
        public int CourseID { get; set; }
        public Instructor? Instructor { get; set; }
        public Course? Course { get; set; }
    }
}
