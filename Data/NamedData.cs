using Project_1.Data.IsoEnums;

namespace Project_1.Data
{
    public abstract class NamedData:UniqueData
    {
        public string? UniID { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public IsoGender? Sex { get; set; }
    }
}
