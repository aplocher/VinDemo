using VinDemo.Domain.Entities;
using VinDemo.Domain.Interfaces;
using VinDemo.Repository.Repositories;

namespace VinDemo.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IVinDemoDbContext _context;

        private IRepository<Member, int> _memberRepository;

        public UnitOfWork(IVinDemoDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public IRepository<Member, int> MemberRepository =>
            _memberRepository ?? (_memberRepository = new MemberRepository(_context));
    }
}