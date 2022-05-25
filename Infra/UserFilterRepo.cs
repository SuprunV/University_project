using Microsoft.EntityFrameworkCore;
using Project_1.Data;
using Project_1.Domain;
namespace Project_1.Infra
{
    //public abstract class UserFilter<TDomain, TData> : CrudRepo<TDomain, TData> where TDomain : UniqueEntity<TData>, new()
    //    where TData : UniqueData, new()
    //{
    //    protected UserFilter(DbContext? c, DbSet<TData>? s) : base(c, s) { }
    //    public string? UserID { get; set; }
    //    protected internal override IQueryable<TData> createSql() => addAdvancedFilter(base.createSql());
    //    internal virtual IQueryable<TData> addAdvancedFilter(IQueryable<TData> q) => q;

    //}
}
