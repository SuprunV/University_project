using Microsoft.EntityFrameworkCore;
using Project_1.Data;

namespace Project_1.Infra.Initializers {
    public abstract class BaseInitializer<TData> where TData : UniqueData {
        protected internal DbContext? db;
        protected internal DbSet<TData>? set;
        protected BaseInitializer(DbContext? c, DbSet<TData>? s) {
            db = c;
            set = s;
        }

        public void Init() {
            if (set?.Any() ?? true)
                return;
            set.AddRange(getEntities);
            db?.SaveChanges();
        }
        protected abstract IEnumerable<TData> getEntities { get; }
        internal static bool isCorrectIsoCode(string id) => !string.IsNullOrWhiteSpace(id) && char.IsLetter(id[0]);
    }
}
