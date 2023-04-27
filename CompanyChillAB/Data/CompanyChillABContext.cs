using CompanyChillAB.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyChillAB.Data
{
    public class CompanyChillABContext : DbContext
    {
        public CompanyChillABContext(DbContextOptions<CompanyChillABContext> options)
            : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveList> LeaveLists { get; set; }
        //public DbSet<InfoViewModel> InfoViewModel { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<InfoViewModel>(entity =>
        //    {
        //        entity.HasNoKey();
        //    });
        //}
    }
}
