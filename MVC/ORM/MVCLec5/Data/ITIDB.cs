using Microsoft.EntityFrameworkCore;
using MVCLec5.Models;

namespace MVCLec5.Data
{

    public class ITIDB:DbContext
    {
        public ITIDB()
        {

        }

        // pass options object to base class (DBContext) 
        public ITIDB(DbContextOptions options): base(options)
        {
            
        }
        // mandatory to create table in database and manipulate it then 
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet <Course> Courses { get; set; }
        public DbSet <StudentCourses> StudentCourses { get; set; }
        // override OnConfiguaing
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // if database doesnot exists gonna create it, otherwise will gonna connect on it
            optionsBuilder.UseSqlServer("Server=.; Database=alexv5; Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
            // open package manager console from tools
            // add-migration migrationNamr
            // add-migration initial
            // this this commands show what is gonna happens on database without excute it

        }
        // Configuration using fluent API
        // override onmodelcreating 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite Primary Key
            modelBuilder.Entity<StudentCourses>()
                .HasKey(a => new { a.StdId, a.CrsId });
            modelBuilder.Entity<Course>().HasKey(a => a.Crs_Id);
            modelBuilder.Entity<Course>().Property(a => a.Crs_Name)
                .HasMaxLength(10)
                .IsRequired();
            //modelBuilder.Entity<Department>().HasKey(a => a.DeptId);
            //modelBuilder.Entity<Department>().Property(a => a.DeptName).HasMaxLength(30)
            //    .IsRequired();

            base.OnModelCreating(modelBuilder);
        }

    }
}
