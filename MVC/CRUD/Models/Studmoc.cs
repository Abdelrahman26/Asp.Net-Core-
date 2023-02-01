using System.Net;

namespace CRUD.Models 
{
    // represents student table 
    // operation crud
    // eather than real table
    public class Studmoc:IStudent
    {
        // class member
         static List<Student> students = new List<Student>()
         {
             new Student()
             {
                 Id   = 1,
                 Name = "Abdelrahman",
                 Age  = 24,
                 Stdimg = "1.png"
             },
             new Student()
             {
                 Id   = 2,
                 Name = "Mohamed",
                 Age  = 26,
                 Stdimg = "2.png"
             }
         };
          
        public List<Student> GetAllStudents()
        {
            return students;
        }

        public Student GetStudentById(int id)
        {
            return students.FirstOrDefault(a => a.Id == id);
        }

        public void AddStudent(Student std)
        {
            students.Add(std);
        }

        public void EditStudent(Student std)
        {
            Student oldstd = students.FirstOrDefault(a => a.Id == std.Id);
            oldstd.Name    = std.Name;
            oldstd.Age = std.Age;
        }

        public void EditStudentImage(Student std)
        {
            Student oldstd = students.FirstOrDefault(a => a.Id == std.Id);
            oldstd.Stdimg = std.Stdimg;
        }

        public void DeleteById(int id)
        {
            students.Remove(GetStudentById(id));
        }



        public int GetNextId()
        {
            return students.Max(x => x.Id) + 1;
        }
    }
}
