using Huy_.NET__baitap2_codeFirst.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Huy_.NET__baitap2_codeFirst.Entities
{
    public class CourseInfo
    {
        

        [Required(ErrorMessage = "Number must be entered")]
        [Display(Name = "Number")]
        public int CourseID { get; set; }
        [Required(ErrorMessage = "Title must be entered")]

        [StringLength(50, MinimumLength = 3,ErrorMessage = "Incorect Length")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Credits must be entered")]
        [Range(0, 5, ErrorMessage = "Out range")]
        public int Credits { get; set; }
        [Required(ErrorMessage = "invalid")]
        public int DepartmentID { get; set; }
        public string? DepartmentName { get; set; }
        public Department? Department { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
        public ICollection<CourseAssignment>? CourseAssignments { get; set; }


    }
}
