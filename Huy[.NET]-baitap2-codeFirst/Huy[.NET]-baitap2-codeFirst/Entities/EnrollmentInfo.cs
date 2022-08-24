using Huy_.NET__baitap2_codeFirst.Models;
using System.ComponentModel.DataAnnotations;
namespace Huy_.NET__baitap2_codeFirst.Entities
{
    public enum Grade
    {
        A, B, C, D, F
    }
    public class EnrollmentInfo
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade? Grade { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }
        public string FullName { get; set; }
    }
}
