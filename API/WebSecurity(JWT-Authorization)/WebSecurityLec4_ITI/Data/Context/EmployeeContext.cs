using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebSecurityLec4_ITI.Data.Models;

namespace WebSecurityLec4_ITI.Data.Context;

public class EmployeeContext : IdentityDbContext <Employee>
{
    public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
    {

    }
    // Convert table name from AspNetUsers to "Employees"
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Employee>().ToTable("Employees");
    }
}
