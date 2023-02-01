namespace CRUD.Models
{
    public interface IStudent
    {
        public List<Student> GetAllStudents();
        public Student GetStudentById(int id);
        public void AddStudent(Student std);
        public void EditStudent(Student std);
        public void EditStudentImage(Student std);
        public void DeleteById(int id);
        public int GetNextId();

    }
}
