using Project_1.Data;
using Project_1.Data.IsoEnums;
using Project_1.Data.Party;

namespace Project_1.Domain
{
    public abstract class NamedEntity<TData> : UniqueEntity<TData> where TData : NamedData, new()
    {
        public NamedEntity() : this(new TData()) { }
        public NamedEntity(TData d) : base(d) { }
        public string? UniID=> getValue(Data?.UniID);
        public string? FirstName => getValue(Data?.FirstName);
        public string? LastName => getValue(Data?.LastName);
        public IsoGender Sex => getValue(Data?.Sex);
        public string CreateTotalString() => $"{FirstName} {LastName}";
        public string? FullName => CreateTotalString();

    }
}
