using Huy_.NET__baitap2_codeFirst.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Huy_.NET__baitap2_codeFirst.Entities
{
    public class InstructorInfo
    {
      
        public int ID { get; set; }

        [Required(ErrorMessage = "Last Name must be entered")]
        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Incorrect length")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Last Name must be entered")]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Incorrect length")]
        public string? FirstMidName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }
        public List<CourseAssignment>? CourseAssignments { get; set; }
        public OfficeAssignment? OfficeAssignment { get; set; }
    }
}
