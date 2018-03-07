using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using VinDemo.Api.Controllers.Base;
using VinDemo.Api.Criteria;
using VinDemo.Domain.Common.Pagination;
using VinDemo.Domain.Entities;
using VinDemo.Domain.Interfaces;

namespace VinDemo.Api.Controllers
{
    public class MembersController : GenericControllerBase<Member, MemberCriteria>
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        protected override IEnumerable<Member> GetInternal(MemberCriteria criteria)
        {
            return _memberService.GetMembers(new PagingCriteria(
                criteria.Page,
                criteria.ItemsPerPage,
                criteria.SortBy ?? "Username",
                criteria.SortByDesc), null);
        }

        protected override Member GetByIdInternal(int id)
        {
            return _memberService.GetMemberById(id);
        }

        protected override void UpdateInternal(int id, Member value)
        {
            value.MemberId = id;
            _memberService.UpdateMember(value);
        }

        protected override int InsertInternal(Member value)
        {
            return _memberService.AddMember(value);
        }

        protected override void DeleteInternal(int id)
        {
            var member = _memberService.GetMemberById(id);
            if (member == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _memberService.DeactivateMember(member);
        }
    }
}