using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using VinDemo.Domain.Common.Dictionary;
using VinDemo.Domain.Common.Pagination;
using VinDemo.Domain.Entities;
using VinDemo.Domain.Interfaces;

namespace VinDemo.Model.Services
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {
            Debug.Assert(unitOfWork != null);
            _unitOfWork = unitOfWork;
        }

        public bool IsUsernameAvailable(string username, int? memberId = null)
        {
            var usernameLowered = username.ToLower();
            var memberIdInt = memberId.GetValueOrDefault(-1);

            // TODO: Should I be including deactivated in the check here?  Am currently due to unique constraint on table.
            return !_unitOfWork.MemberRepository
                .GetByCriteria(x => x.Username.ToLower().Equals(usernameLowered) && x.MemberId != memberIdInt, true).Any();
        }

        public int AddMember(Member member)
        {
            _unitOfWork.MemberRepository.Add(member);
            _unitOfWork.Commit();

            return member.MemberId.GetValueOrDefault(0);
        }

        public void UpdateMember(Member member)
        {
            _unitOfWork.MemberRepository.Update(member);
            _unitOfWork.Commit();
        }

        public Member GetMemberById(int id)
        {
            return _unitOfWork.MemberRepository.GetById(id);
        }

        public Member GetMemberByUsername(string username)
        {
            return _unitOfWork.MemberRepository
                .GetByCriteria(x => x.Username.Equals(username, StringComparison.OrdinalIgnoreCase), false, true).FirstOrDefault();
        }

        public PagedList<Member> GetMembers(PagingCriteria pageCriteria, Dictionary<string, string> columnFilters)
        {
            var usernameFilter = columnFilters.GetValue("Username", String.Empty).ToLower();
            var firstNameFilter = columnFilters.GetValue("FirstName", String.Empty).ToLower();
            var lastNameFilter = columnFilters.GetValue("LastName", String.Empty).ToLower();
            var emailFilter = columnFilters.GetValue("Email", String.Empty).ToLower();
            var phoneNumberFilter = columnFilters.GetValue("PhoneNumber", String.Empty).ToLower();
            var dateOfBirthFilter = columnFilters.GetValue("DateOfBirth", String.Empty).ToLower();

            var hasFilters = columnFilters != null && columnFilters.Count > 0;

            var members = _unitOfWork.MemberRepository.GetByCriteria(x => !hasFilters || (
                (string.IsNullOrEmpty(usernameFilter) || x.Username.ToLower().StartsWith(usernameFilter)) &&
                (string.IsNullOrEmpty(firstNameFilter) || x.FirstName.ToLower().StartsWith(firstNameFilter)) &&
                (string.IsNullOrEmpty(lastNameFilter) || x.LastName.ToLower().StartsWith(lastNameFilter)) &&
                (string.IsNullOrEmpty(emailFilter) || x.Email.ToLower().StartsWith(emailFilter)) &&
                (string.IsNullOrEmpty(phoneNumberFilter) || x.PhoneNumber.ToLower().StartsWith(phoneNumberFilter))
));
            /*&&
            (string.IsNullOrEmpty(dateOfBirthFilter) || x.DateOfBirth
                 .GetValueOrDefault(DateTime.MaxValue)
                 .Date
                 .Equals(DateTime.Parse(columnFilters["DateOfBirth"]).Date))
        ));*/

            // TODO: pagination and sorting should probably be handled 
            // by the data access code in the repo for performance reasons
            var list = new PagedList<Member>(members, pageCriteria);
            return list;
        }

        public void DeactivateMember(Member member)
        {
            _unitOfWork.MemberRepository.Delete(member, false);
            _unitOfWork.Commit();
        }

        public Member GetNewMember()
        {
            return new Member();
        }
        
    }
}