using Microsoft.EntityFrameworkCore;
using Project_1.Data;
using Project_1.Domain;

namespace Project_1.Infra
{
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public abstract class BaseRepo<TDomain, TData> : SessionRepo, IBaseRepo<TDomain>
        where TDomain : UniqueEntity<TData>, new()
        where TData : UniqueData, new()
    {
        protected internal DbContext? db { get; }
        protected internal DbSet<TData>? set { get; }
        protected internal virtual int? ListItemsCount { get; set; } = 0;
        protected BaseRepo(DbContext? c, DbSet<TData>? s) {
            db = c;
            set = s;
        }

        internal void clear()  {
            set?.RemoveRange(set);
            db?.SaveChanges();
        }
        public abstract bool Add(TDomain obj);
        public abstract List<TDomain> Get();
        public abstract TDomain Get(string ID);
        public abstract bool Update(TDomain obj);
        public abstract bool Delete(string ID);
        public abstract Task<bool> AddAsync(TDomain obj);
        public abstract Task<List<TDomain>> GetAsync();
        public abstract List<TDomain> GetAll(Func<TDomain, dynamic>? orderBy = null);
        public abstract Task<TDomain> GetAsync(string ID);
        public abstract Task<bool> UpdateAsync(TDomain obj);
        public abstract Task<bool> DeleteAsync(string ID);
        public abstract TDomain GetBy(Func<TDomain, bool>? condition);
    }
}
