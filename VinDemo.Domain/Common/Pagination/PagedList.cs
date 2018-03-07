using System;
using System.Collections.Generic;
using System.Linq;

namespace VinDemo.Domain.Common.Pagination
{
    public class PagedList<T> : List<T>
    {
        public PagedList(PagingCriteria criteria)
        {
            PageNumber = criteria.PageNumber;
            PageSize = criteria.PageSize;
            PageCount = 1;
            HasNextPage = false;
            HasPreviousPage = false;
            SortBy = criteria.SortBy;
            SortByDescending = criteria.SortByDescending;
        }

        public PagedList(IQueryable<T> items, PagingCriteria criteria)
            : base(
                items
                    .OrderBy(
                        criteria.SortBy, criteria.SortByDescending)
                    .Skip(criteria.PageSize * (criteria.PageNumber - 1))
                    .Take(criteria.PageSize)
                    .ToList())
        {
            PageNumber = criteria.PageNumber;
            PageSize = criteria.PageSize;
            PageCount = (int) Math.Ceiling((decimal) items.Count() / criteria.PageSize);
            HasNextPage = PageNumber < PageCount;
            HasPreviousPage = PageNumber > 1;
            SortBy = criteria.SortBy;
            SortByDescending = criteria.SortByDescending;
        }

        /// <summary>
        ///     Current page number
        /// </summary>
        public int PageNumber { get; }

        /// <summary>
        ///     Total number of pages
        /// </summary>
        public int PageCount { get; }

        /// <summary>
        ///     Records per page
        /// </summary>
        public int PageSize { get; }

        public bool HasNextPage { get; }

        public bool HasPreviousPage { get; }

        public string SortBy { get; }

        public bool SortByDescending { get; }

        /// <summary>
        ///     Small helper method to return no more than x number of pages.  If there are more than x number of pages total, it
        ///     will show the pages surrounding PageNumber
        /// </summary>
        /// <param name="maxPagesToShow"></param>
        /// <returns></returns>
        public int[] GetPagesToShow(int maxPagesToShow)
        {
            if (PageCount > maxPagesToShow)
            {
                return
                    PageNumber > maxPagesToShow / 2
                        ? Enumerable.Range(PageNumber - maxPagesToShow / 2, maxPagesToShow).ToArray()
                        : Enumerable.Range(1, maxPagesToShow).ToArray();
            }

            return Enumerable.Range(1, PageCount).ToArray();
        }
    }
}