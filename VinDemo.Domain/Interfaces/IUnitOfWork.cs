using VinDemo.Domain.Entities;

namespace VinDemo.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Member, int> MemberRepository { get; }
        void Commit();
    }
}