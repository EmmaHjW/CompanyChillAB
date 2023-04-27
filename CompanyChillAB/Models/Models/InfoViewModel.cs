using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompanyChillAB.Models
{
    public class InfoViewModel
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public string LeaveType { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        
    }
}

//[Key]
//[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//public int Id { get; set; }
//public string? LeaveType { get; set; }
//public string? Description { get; set; }
//public DateTime Start { get; set; }
//public DateTime Stop { get; set; }
//[ForeignKey("Employees")]
//public int FK_EmployeeId { get; set; }
//public virtual Employee Employees { get; set; } // Navigering
//[ForeignKey("LeaveLists")]
//public int FK_LeaveListId { get; set; }
//public virtual LeaveList? LeaveLists { get; set; }
