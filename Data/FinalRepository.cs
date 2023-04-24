using Microsoft.EntityFrameworkCore;
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
            return _schoolDbContext.Finals.Include(f => f.Course).FirstOrDefault(f => f.Id == id);
        }

        public IEnumerable<Final> GetFinalsByStudentId(int studentId)
        {
            return _schoolDbContext.Finals.Include(f => f.Course).Where(f => f.StudentId == studentId);
        }

        public Final Update(Final final)
        {
            Final updatedFinal = _schoolDbContext.Finals
                .FirstOrDefault(f => f.Id == final.Id && f.StudentId == final.StudentId);
            if (updatedFinal != null)
            {
                updatedFinal.Mark = final.Mark;
                updatedFinal.Name = final.Name;
                _schoolDbContext.SaveChanges();
            }

            return updatedFinal;
        }
    }
}
