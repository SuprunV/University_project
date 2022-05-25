using Microsoft.EntityFrameworkCore;
using Project_1.Aids;
using Project_1.Data.Party;
using Project_1.Domain.Party;


namespace Project_1.Infra.Party
{
    public sealed class StudentsRepo: Repo<Student,StudentData>, IStudentsRepo
    {
        private UniversityDb? dbSet;
        private List<Country>? countries;
        protected internal override Student toDomain(StudentData d) => new Student(d);
        public StudentsRepo(UniversityDb? db) : base(db, db?.Students)
        {
            dbSet = db ?? new UniversityDb(new DbContextOptions<UniversityDb>());
        }

        internal override IQueryable<StudentData> addFilter(IQueryable<StudentData> q) {

            countries = new CountriesRepo(dbSet).GetAll(x => x?.CountryName ?? String.Empty);

            var name = CurrentFilter;
            
            if (string.IsNullOrWhiteSpace(name)) return q;

            name = name.ToLower();
                
            List<string> rightElems = new List<string>();

            foreach (var x in q) {
                var countryName = (countries.Where(y => y.ID == x.Nationality)?.FirstOrDefault())?.CountryName ?? null;
                bool contains = x.LastName!.ToLower().Contains(name)
                                || x.FirstName!.ToLower().Contains(name)
                                || (x.Sex!.Description()).ToLower().Contains(name)
                                || x.EnrollmentDate.ToString()!.ToLower().Contains(name)
                                || (countryName == null ? false : countryName.ToLower().Contains(name));
                
                if (contains) rightElems.Add(x.ID);
            }

            return q.Where(x => rightElems.Contains(x.ID));
        }
        

}
}
