using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompanyChillAB.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        [Required]
        public string? Name { get; set; }
        [StringLength(60)]
        public string? Email { get; set; }
        [StringLength(50)]
        public string? Phone { get; set; }
        public string? Address { get; set; }
        [StringLength(50)]
        public string? City { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public string? Department { get; set; }

        public virtual ICollection<LeaveList>? LeaveLists { get; set; }// Navigation
    }
}
