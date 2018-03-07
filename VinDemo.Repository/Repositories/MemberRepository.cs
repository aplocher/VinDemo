using VinDemo.Domain.Entities;
using VinDemo.Domain.Interfaces;
using VinDemo.Repository.Repositories.Base;

namespace VinDemo.Repository.Repositories
{
    public class MemberRepository : RepositoryBase<Member>
    {
        public MemberRepository(IVinDemoDbContext context) : base(context)
        {
        }
    }
}