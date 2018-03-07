using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using VinDemo.Domain.Common.Pagination;
using VinDemo.Domain.Entities;
using VinDemo.Domain.Interfaces;
using VinDemo.Web.ExtensionMethods;
using VinDemo.Web.ViewModels.Members;

namespace VinDemo.Web.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        // GET: Member
        public ActionResult Index(Dictionary<string, string> filters = null, string sort = "Username", bool desc = false, int page = 1)
        {
            return View(new IndexViewModel
            {
                MemberTableViewModel = new MemberTableViewModel
                {
                    Members = _memberService.GetMembers(new PagingCriteria(page, 10, sort, desc), filters),
                    ColumnFilters = filters
                },
                PageNumber = 1,
                SortBy = "Username",
                SortByDescending = false
            });
        }

        [HttpPost]
        public JsonResult GetMemberTablePage(Dictionary<string, string> filters = null, string sort = "Username",
            bool desc = false, int page = 1)
        {
            var members = _memberService.GetMembers(new PagingCriteria(page, 10, sort, desc), filters);
            return Json(new 
            {
                PageCount = members.PageCount,
                PageNumber = members.PageNumber,
                HasPreviousPage = members.HasPreviousPage,
                HasNextPage = members.HasNextPage,
                SortBy = members.SortBy,
                SortByDescending = members.SortByDescending,
                MembersView = this.RenderRazorViewToString("MemberTable", new MemberTableViewModel
                {
                    Members = _memberService.GetMembers(new PagingCriteria(page, 10, sort, desc), filters),
                    ColumnFilters = filters
                })
            });
        }

        //public ActionResult MemberTable(Dictionary<string, string> filters = null, string sort = "Username", bool desc = false, int page = 1)
        //{
        //    return PartialView(nameof(MemberTable), new MemberTableViewModel
        //    {
        //        Members = _memberService.GetMembers(new PagingCriteria(page, 10, sort, desc), filters),
        //        ColumnFilters = filters
        //    });
        //}

        [HttpPost]
        public ActionResult Detail(int? id)
        {
            return PartialView("Detail", new DetailViewModel
            {
                Member = id.HasValue
                    ? _memberService.GetMemberById(id.Value)
                    : _memberService.GetNewMember()
            });
        }

        [HttpPost]
        public ActionResult DeleteMember(int id)
        {
            var member = _memberService.GetMemberById(id);
            if (member == null)
                return Json(new { status = "error", message = "Member not found" });

            _memberService.DeactivateMember(member);
            return Json(new { status = "success", message = "Member deactivated" });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SaveMember(DetailViewModel viewModel)
        {
            if (!_memberService.IsUsernameAvailable(viewModel.Member.Username, viewModel.Member.MemberId))
            {
                ModelState.AddModelError("Member.Username", "Username is already being used. Try another.");
            }

            if (ModelState.IsValid)
            {
                var isEditMode = viewModel.Member.MemberId.GetValueOrDefault(0) > 0;

                if (isEditMode)
                    _memberService.UpdateMember(viewModel.Member);
                else
                {
                    var newId = _memberService.AddMember(viewModel.Member);
                    return Detail(newId);
                }
            }

            if (!ModelState.IsValid && Request.IsAjaxRequest())
                return PartialView("Detail");

            return Detail(viewModel.Member.MemberId);
        }

        [HttpPost]
        public JsonResult IsUsernameAvailable(int id, string username)
        {
            return Json(_memberService.IsUsernameAvailable(username, id));
        }
    }
}