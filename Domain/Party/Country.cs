using Project_1.Data.Party;

namespace Project_1.Domain.Party
{
    public sealed class Country : UniqueEntity<CountryData>
    {
        public Country() : this(new CountryData()) { }
        public Country(CountryData d) : base(d) { }

        public string? ISO => getValue(Data?.ISO);
        public string? CountryName => getValue(Data?.CountryName);
        public string? NativeName => getValue(Data?.NativeName);
    }
    public interface ICountriesRepo : IRepo<Country>
    { }
}
