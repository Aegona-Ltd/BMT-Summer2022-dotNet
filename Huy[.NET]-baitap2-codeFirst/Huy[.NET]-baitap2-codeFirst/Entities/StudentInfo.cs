using Huy_.NET__baitap2_codeFirst.Models;
using System.ComponentModel.DataAnnotations;

namespace Huy_.NET__baitap2_codeFirst.Entities
{
    public class StudentInfo
    {
        
        public int ID { get; set; }
        [Required(ErrorMessage = "Last Name must be entered")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Incorrect length")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "First Name must be entered")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Incorrect length")]
        public string? FirstMidName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstMidName;
            }
        }
        public DateTime EnrollmentDate { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
