using System.ComponentModel.DataAnnotations.Schema;

namespace MVCLec5.Models
{
    public class StudentCourses
    {
        [ForeignKey("Student")]
        public int StdId { get; set; }

        [ForeignKey("Course")]
        public int CrsId { get; set; }

        //attribure on relation ship
        public int Degree { get; set; }

        public Course Course { get; set; }

        public Student Student { get; set; }
    }
}
