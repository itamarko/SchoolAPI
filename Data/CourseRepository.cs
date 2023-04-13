using SchoolAPI.Data.Entities;

namespace SchoolAPI.Data
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SchoolDbContext _schoolDbContext;

        public CourseRepository(SchoolDbContext schoolDbContext)
        {
            _schoolDbContext = schoolDbContext;
        }
        public IEnumerable<Course> AllCourses => _schoolDbContext.Courses;
    }
}
