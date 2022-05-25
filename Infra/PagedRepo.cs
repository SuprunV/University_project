using Microsoft.EntityFrameworkCore;
using Project_1.Data;
using Project_1.Domain;

namespace Project_1.Infra
{
    public abstract class PagedRepo<TDomain, TData> : OrderedRepo<TDomain, TData>, IPagedRepo<TDomain>
        where TDomain : UniqueEntity<TData>, new() where TData : UniqueData, new()
    {
        protected PagedRepo(DbContext? c, DbSet<TData>? s) : base(c, s) {}
        internal int skippedItemsCount => PageSize * PageIndex;
        public int PageIndex { get; set; }
        public int TotalPages => totalPages;
        public bool HasNextPage => PageIndex < TotalPages - 1;
        public bool HasPreviousPage => PageIndex > 0;
        public int PageSize { get; set; }
        protected internal override IQueryable<TData> createSql() => addSkipAndTake(base.createSql());
        internal IQueryable<TData> addSkipAndTake(IQueryable<TData> q) => q.Skip(skippedItemsCount).Take(PageSize);
        internal int totalPages => (int)Math.Ceiling(countPages);
        internal double countPages => itemsCount / ((double)PageSize <= 0 ? 10 : (double)PageSize);
        internal int itemsCount => ListItemsCount ?? 0;
    }
}