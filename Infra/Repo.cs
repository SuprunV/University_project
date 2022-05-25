using Microsoft.EntityFrameworkCore;
using Project_1.Data;
using Project_1.Domain;

namespace Project_1.Infra
{
    public abstract class Repo<TDomain, TData> : PagedRepo<TDomain, TData>
        where TDomain : UniqueEntity<TData>, new() where TData : UniqueData, new() {
        protected Repo(DbContext? c, DbSet<TData>? s) : base(c, s) { }
    }
}
