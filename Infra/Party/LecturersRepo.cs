using Project_1.Aids;
using Project_1.Data.Party;
using Project_1.Domain.Party;

namespace Project_1.Infra.Party
{
    public sealed class LecturersRepo : Repo<Lecturer, LecturerData>, ILecturersRepo
    {
        public LecturersRepo(UniversityDb? db) : base(db, db?.Lecturers) { }
        
        protected internal override Lecturer toDomain(LecturerData d) => new (d);
        internal override IQueryable<LecturerData> addFilter(IQueryable<LecturerData> q)
        {

            var name = CurrentFilter;
            if (string.IsNullOrWhiteSpace(name)) return q;

            name = name.ToLower();

            List<string> rightElems = new List<string>();

            foreach (var x in q) {

                bool contains = x.LastName!.ToLower().Contains(name)
                                || x.FirstName!.ToLower().Contains(name)
                                || x.UniID!.ToLower().Contains(name)
                                || (x!.Sex!.Description())!.ToLower().Contains(name);

                if (contains)
                    rightElems.Add(x.ID);
            }

            return q.Where(x => rightElems.Contains(x.ID));
        }
    }
}
