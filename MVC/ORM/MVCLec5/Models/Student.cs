using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace MVCLec5.Models
{
    public class Student
    {
        public Student()
        {
            StudentCourses = new HashSet<StudentCourses>();
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "*")] // data annotation 
        [StringLength(20, MinimumLength = 3, ErrorMessage ="lenght must be between 3 and 20")]
        public string Name { get; set; }

        [Range(10, 20, ErrorMessage ="age must be between 10 and 20")] // MVC validator, there is no relation with DB
        public int Age { get; set; }

        //[RegularExpression(@"[a-zA-Z0-9]+@[a-zA-Z]+.[a-zA-Z]{2,4}")]
        public string Email { get; set; }
        
        [Required]
        [Remote("checkusername", "student", AdditionalFields ="Id")] // [Remote(Action, Controller)]
        public string Username { get; set; }


        [DataType(DataType.Password)]
        public string Password { get; set; }
        [NotMapped] // don't add coulmn in data base(during migration time)
        [Compare("Password")]
        [DataType(DataType.Password)] // UI HEAD
        public string CPassword { get; set; }
        // create column which represents relatioship between student and department 
        [ForeignKey("Department")] // refrence on navigation property 
        public int DeptNo { get; set; }

        // navigation property to set relationship
        public Department Department { get; set; }

        public ICollection <StudentCourses> StudentCourses { get; set; }
    }
}
