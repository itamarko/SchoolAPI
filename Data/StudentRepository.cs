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
    }
}
