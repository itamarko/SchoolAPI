using SchoolAPI.Data.Entities;

namespace SchoolAPI.Data
{
    public interface IFinalRepository
    {
        IEnumerable<Final> AllFinals { get; }
        Final GetById(int id);
        IEnumerable<Final> GetFinalsByStudentId(int studentId);
        Final Add(Final final);
        Final Update(Final final);
    }
}
