using Microsoft.EntityFrameworkCore;
using Project_1.Data;
using Project_1.Domain;

namespace Project_1.Infra
{
    public abstract class FilteredRepo<TDomain, TData> : CrudRepo<TDomain, TData> where TDomain : UniqueEntity<TData>, new()
        where TData : UniqueData, new()
    {
        protected FilteredRepo(DbContext? c, DbSet<TData>? s) : base(c, s) { }
        public string? CurrentFilter { get; set; }
        public string? StudyProgramFilter { get; set; }
        public string? SemesterFilter { get; set; }
        public string? UserID { get; set; }
        public string? statusFilter { get; set; }
        //protected internal override IQueryable<TData> createSql() => addFilter(base.createSql());
        protected internal override IQueryable<TData> createSql() => addFilter(addAdvancedFilter(base.createSql()));
        internal virtual IQueryable<TData> addAdvancedFilter(IQueryable<TData> q) => q;
        internal virtual IQueryable<TData> addFilter(IQueryable<TData> q) => q;
        public override async Task<int> getItemsCount() {
            var list = await runSql(addFilter(addAdvancedFilter(from s in set select s)));
            return list.Count;
        }

    }

}
