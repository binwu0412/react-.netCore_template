using Microsoft.EntityFrameworkCore;
using react_.netcore_template.Domain.Entities;

namespace react_.netcore_template.Infra.Data.Context
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Dependend> Dependends { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
