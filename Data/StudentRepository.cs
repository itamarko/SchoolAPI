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
        public IEnumerable<Student> AllStudents => _schoolDbContext.Students.Include(s=>s.Courses);

        public Student GetById(int id)
        {
            return _schoolDbContext.Students.FirstOrDefault(s => s.Id == id);
        }
    }
}
