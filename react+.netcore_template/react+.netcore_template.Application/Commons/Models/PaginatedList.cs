using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace react_.netcore_template.Application.Commons.Models
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; }
        public int PageIndex { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            if (pageIndex < 0) throw new ArgumentException("Page index can't lower than 0.");
            if (pageSize <= 0) throw new ArgumentException("Page size can't lower than or equal to 0.");

            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Items = items;
        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            if (pageIndex < 0) throw new ArgumentException("Page index can't lower than 0.");
            if (pageSize <= 0) throw new ArgumentException("Page size can't lower than or equal to 0.");

            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
