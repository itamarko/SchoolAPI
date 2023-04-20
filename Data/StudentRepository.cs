using Microsoft.EntityFrameworkCore;
using SchoolAPI.Data.Entities;

namespace SchoolAPI.Data
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolDbContext _schoolDbContext;

        public StudentRepository(SchoolDbContext schoolDbContext)
        {
            _schoolDbContext = schoolDbContext;
        }
        public IEnumerable<Student> AllStudents => _schoolDbContext.Students.Include(s=>s.Department);

        public Student Add(Student student)
        {
            _schoolDbContext.Students.Add(student);
            _schoolDbContext.SaveChanges();
            return student;
        }

        public Student GetById(int id)
        {
            return _schoolDbContext.Students.FirstOrDefault(s => s.Id == id);
        }

        public bool Remove(int id)
        {
            bool isSuccessful = false;
            Student student = GetById(id);
            if (student != null)
            {
                _schoolDbContext.Students.Remove(student);
                _schoolDbContext.SaveChanges();
                isSuccessful = true;
            }
            return isSuccessful;
        }

        public Student Update(Student student)
        {
            Student updatedStudent = _schoolDbContext.Students.FirstOrDefault(s => s.Id == student.Id);
            if (updatedStudent != null)
            {
                updatedStudent.FirstName = student.FirstName;
                updatedStudent.LastName = student.LastName;
                updatedStudent.DepartmentId = student.DepartmentId;
                _schoolDbContext.SaveChanges();
            }
            return updatedStudent;
        }
    }
}
