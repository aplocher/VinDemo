using System.Collections.Generic;
using VinDemo.Domain.Common.Pagination;
using VinDemo.Domain.Entities;
using VinDemo.Domain.Interfaces;

namespace VinDemo.Web.ViewModels.Members
{
    public class MemberTableViewModel
    {
        public PagedList<Member> Members { get; set; }

        public Dictionary<string,string> ColumnFilters { get; set; }
    }
}