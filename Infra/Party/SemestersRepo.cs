using Project_1.Aids;
using Project_1.Data.Party;
using Project_1.Domain.Party;

namespace Project_1.Infra.Party
{
    public sealed class SemestersRepo : Repo<Semester, SemesterData>, ISemestersRepo
    {
        public SemestersRepo(UniversityDb? db) : base(db, db?.Semesters) { }
        protected internal override Semester toDomain(SemesterData d) => new (d);
        internal override IQueryable<SemesterData> addFilter(IQueryable<SemesterData> q)
        {

            var name = CurrentFilter;

            if (string.IsNullOrWhiteSpace(name))
                return q;

            name = name.ToLower();

            List<string> rightElems = new List<string>();

            foreach (var x in q) {

                bool contains = x!.Season!.Description()!.ToLower().Contains(name)
                                || x.DateStart.ToString()!.ToLower().Contains(name)
                                || x.DateEnd.ToString()!.ToLower().Contains(name);

                if (contains)
                    rightElems.Add(x.ID);
            }

            return q.Where(x => rightElems.Contains(x.ID));
        }
    }
}
