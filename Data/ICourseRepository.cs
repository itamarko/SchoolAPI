using SchoolAPI.Data.Entities;

namespace SchoolAPI.Data
{
    public interface ICourseRepository
    {
        IEnumerable<Course>  AllCourses { get; }
    }
}
