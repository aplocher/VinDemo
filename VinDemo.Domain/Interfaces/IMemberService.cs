using System.Collections.Generic;
using VinDemo.Domain.Common.Pagination;
using VinDemo.Domain.Entities;

namespace VinDemo.Domain.Interfaces
{
    public interface IMemberService
    {
        bool IsUsernameAvailable(string username, int? userId = null);
        int AddMember(Member member);
        void UpdateMember(Member member);
        Member GetMemberById(int id);
        Member GetMemberByUsername(string username);
        PagedList<Member> GetMembers(PagingCriteria criteria, Dictionary<string, string> columnFilters);
        Member GetNewMember();
        void DeactivateMember(Member member);
    }
}