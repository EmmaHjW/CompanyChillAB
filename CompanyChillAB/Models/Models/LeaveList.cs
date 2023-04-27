using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyChillAB.Models
{
    public class LeaveList // Connection Table
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeaveListId { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public string? LeaveType { get; set; }
        [StringLength(50)]
        public string? Description { get; set; }

        [ForeignKey("Employees")]
        public int FK_EmployeeId { get; set; }
        public virtual Employee Employees { get; set; } // Navigering
 
    }
}
