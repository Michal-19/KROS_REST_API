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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasOne(x => x.Employee).WithMany(x => x.Companies).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Division>().HasOne(x => x.Employee).WithMany(x => x.Divisions).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
