using Project_1.Data.Party;
using Project_1.Domain.Party;

namespace Project_1.Infra.Party
{
    public sealed class CountriesRepo : Repo<Country, CountryData>, ICountriesRepo
    {
        public CountriesRepo(UniversityDb? db) : base(db, db?.Countries) {}
        protected internal override Country toDomain(CountryData d) => new(d);
    }
}
