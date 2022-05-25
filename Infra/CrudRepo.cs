using Microsoft.EntityFrameworkCore;
using Project_1.Data;
using Project_1.Domain;
using System.Linq.Expressions;

namespace Project_1.Infra
{


    public abstract class CrudRepo<TDomain, TData> : BaseRepo<TDomain, TData> where TDomain : UniqueEntity<TData>, new()
        where TData : UniqueData, new()
    {
        protected CrudRepo(DbContext? c, DbSet<TData>? s) : base(c, s) { }

        public override bool Add(TDomain obj) => AddAsync(obj).GetAwaiter().GetResult();

        public override async Task<bool> AddAsync(TDomain obj) {
            try {
                var d = obj.Data;
                _ = (set is null) ? null : await set.AddAsync(d);
                _ = (db is null) ? 0 : await db.SaveChangesAsync();
                return true;
            }
            catch {
                return false;
            }
        }

        public override bool Delete(string id) => DeleteAsync(id).GetAwaiter().GetResult();

        public override async Task<bool> DeleteAsync(string id) {
            try {
                var d = (set is null) ? null : await set.FindAsync(id);
                if (d == null) return false;
                _ = set?.Remove(d);
                _ = (db is null) ? 0 : await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override List<TDomain> Get() => GetAsync().GetAwaiter().GetResult();
        public override TDomain Get(string id) => GetAsync(id).GetAwaiter().GetResult();

        public override TDomain GetBy(Func<TDomain, bool>? condition)
        {
            var r = new List<TDomain>();
            if (set is null) return new();
            foreach (var d in set) r.Add(toDomain(d));
            return r?.FirstOrDefault(condition!) ?? new();
        }
        public override List<TDomain> GetAll(Func<TDomain, dynamic>? orderBy = null) {
            var r = new List<TDomain>();
            if (set is null) return r;
            foreach (var d in set) r.Add(toDomain(d));
            return (orderBy is null) ? r : r.OrderBy(orderBy).ToList();
        }

        public virtual async Task<int> getItemsCount() {
            var list = await runSql(from s in set select s);
            return list.Count;
        }

        public override async Task<List<TDomain>> GetAsync() {
            try {
                var query = createSql();
                var dataItems = await runSql(query);
                var iCount = await getItemsCount();
                ListItemsCount = iCount;
                var items = new List<TDomain>();
                foreach (var d in dataItems) items.Add(toDomain(d));
                return items;
            }
            catch { return new List<TDomain>(); }
        }

        internal async Task<List<TData>> runSql(IQueryable<TData> query) => await query.AsNoTracking().ToListAsync();
        protected internal virtual IQueryable<TData> createSql() => from s in set select s;

        public override async Task<TDomain> GetAsync(string id) {
            try
            {
                if (id == null) return new TDomain();
                var d = (set is null) ? null : await set.FirstOrDefaultAsync(x => x.ID == id);
                return d == null ? new TDomain() : toDomain(d);
            }
            catch { return new TDomain(); }
        }

        public override bool Update(TDomain obj) => UpdateAsync(obj).GetAwaiter().GetResult();

        public override async Task<bool> UpdateAsync(TDomain obj) {
            try {
                if (db is null) return false;
                var d = obj.Data;
                db.ChangeTracker.Clear();

                db.Attach(d).State = EntityState.Modified;
                
                _ = await db.SaveChangesAsync();
                return true;
            }
            catch {
                return false;
            }
        }

        protected internal abstract TDomain toDomain(TData d);
    }
}