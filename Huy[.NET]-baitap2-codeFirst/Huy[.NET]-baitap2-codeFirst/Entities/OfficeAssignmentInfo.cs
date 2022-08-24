using Huy_.NET__baitap2_codeFirst.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Huy_.NET__baitap2_codeFirst.Entities
{
    public class OfficeAssignmentInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InstructorID { get; set; }
        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string Location { get; set; }
        public Instructor? Instructor { get; set; }
    }
}
