using KROS_REST_API.Models;
using Microsoft.EntityFrameworkCore;

namespace KROS_REST_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasOne(x => x.Director).WithMany(x => x.CompaniesChief).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Employee>().HasOne(x => x.CompanyWork).WithMany(x => x.Employees).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Division>().HasOne(x => x.DivisionChief).WithMany(x => x.DivisionsChief).OnDelete(DeleteBehavior.SetNull);
            //modelBuilder.Entity<Division>().HasOne(x => x.Company).WithMany(x => x.Divisions).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Project>().HasOne(x => x.ProjectChief).WithMany(x => x.ProjectsChief).OnDelete(DeleteBehavior.SetNull);
            //modelBuilder.Entity<Project>().HasOne(x => x.Division).WithMany(x => x.Projects).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Department>().HasOne(x => x.DepartmentChief).WithMany(x => x.DepartmentsChief).OnDelete(DeleteBehavior.SetNull);
            //modelBuilder.Entity<Department>().HasOne(x => x.Project).WithMany(x => x.Departments).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
