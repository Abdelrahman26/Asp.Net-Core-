using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.PortableExecutable;

namespace MVCLec5.Models
{
    [Table("dept")] // table name in database  
    public class Department
    {
        public Department()
        {
            // create object from Student, to make me can add objects to it
            // list & hashset both are inhert from Icollection 
            // without this line, object of student equlas NULL, can't say NULL.add(val)
            students = new HashSet<Student>();
            courses  = new HashSet<Course>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // may be choose computed or Identity rather than identity
        public int DeptId { get; set; }

        [StringLength(15, MinimumLength = 3)]
        // MinmumLenugh not throw to database, but i gonna use it next in mvc 
        public string DeptName { get; set; }
        
        //Navigation property 
        // refrence of students is null, until create object from it 
        // in constructor 
        public  ICollection<Student> students { get; set; }
        public ICollection <Course> courses { get; set; }
    }
}
