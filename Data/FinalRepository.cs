using SchoolAPI.Data.Entities;

namespace SchoolAPI.Data
{
    public class FinalRepository : IFinalRepository
    {
        private readonly SchoolDbContext _schoolDbContext;

        public FinalRepository(SchoolDbContext schoolDbContext)
        {
            _schoolDbContext = schoolDbContext;
        }

        public IEnumerable<Final> AllFinals => _schoolDbContext.Finals;

        public Final Add(Final final)
        {
            _schoolDbContext.Finals.Add(final);
            _schoolDbContext.SaveChanges();

            return final;
        }

        public Final GetById(int id)
        {
            return _schoolDbContext.Finals.FirstOrDefault(f => f.Id == id);
        }

        public IEnumerable<Final> GetFinalsByStudentId(int studentId)
        {
            return _schoolDbContext.Finals.Where(f => f.StudentId == studentId);
        }
    }
}
