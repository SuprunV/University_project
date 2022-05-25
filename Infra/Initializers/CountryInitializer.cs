using System.Globalization;
using Project_1.Data;
using Project_1.Data.Party;
using Project_1.Domain;

namespace Project_1.Infra.Initializers
{
    public sealed class CountryInitializer : BaseInitializer<CountryData>
    {
        public CountryInitializer(UniversityDb? db) : base(db, db?.Countries) { }
        protected override IEnumerable<CountryData> getEntities
        {
            get
            {
                var l = new List<CountryData>();
                foreach (CultureInfo cul in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
                {
                    var c = new RegionInfo(new CultureInfo(cul.Name, false).LCID);
                    var id = c.ThreeLetterISORegionName;
                    if (!isCorrectIsoCode(id)) continue;
                    if (l.FirstOrDefault(x => x.ID == id) is not null) continue;
                    var d = createCountry(id, c.EnglishName, c.NativeName);
                    l.Add(d);
                }
                return l;
            }
        }

        internal static CountryData createCountry(string code, string name, string nativeName)
            => new()
            {
                ID = code ?? UniqueData.NewID,
                ISO = code ?? UniqueEntity.defaultStr,
                CountryName = name,
                NativeName = nativeName
            };
    }
}
