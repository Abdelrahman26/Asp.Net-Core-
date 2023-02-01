namespace MVCLec5.Models
{
    public class Course
    {
        public Course()
        {
            departments = new HashSet<Department>();
            StudentCourses = new HashSet<StudentCourses>();
        }
        public int Crs_Id { get; set; }
        public string Crs_Name{ get; set; }
        public int Crs_hours { get; set; }
        public ICollection<Department> departments { get; set; }

        // relationship between studentcourse and course 
        public ICollection<StudentCourses> StudentCourses { get; set; }
    }
}
