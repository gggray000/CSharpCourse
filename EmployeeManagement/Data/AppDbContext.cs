using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Data
{
    // Manages database connection. 
    public class AppDbContext : DbContext
    {
        // Manage Employees entity.
        public DbSet<Employee> Employees { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) // passing options to DbContext's constructor, because it requires options parameter.
        { }

    }
}