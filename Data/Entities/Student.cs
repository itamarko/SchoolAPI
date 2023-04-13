namespace SchoolAPI.Data.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public List<Course> Courses { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
