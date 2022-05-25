using Project_1.Data;
using Project_1.Data.IsoEnums;
using Project_1.Data.Party;
using Project_1.Data.Connection;


namespace Project_1.Domain
{
    public abstract class UniqueEntity
    {
        public const string defaultStr = "Undefined";
        public const bool defaultBool = false;
        public static DateTime defaultDate => DateTime.MinValue;
        public const int defaultInt = 0;
        protected static string getValue(string? v) => v ?? defaultStr;
        protected static bool getValue(bool? v) => v ?? defaultBool;
        protected static DateTime getValue(DateTime? v) => v ?? defaultDate;
        protected static IsoGender getValue(IsoGender? v) => v ?? IsoGender.NotApplicable;
        protected static IsoSemester getValue(IsoSemester? v) => v ?? IsoSemester.NotApplicable;
        protected static IsoCourseStatus getValue(IsoCourseStatus? v) => v ?? IsoCourseStatus.Unknown;
        protected static int getValue(int? v) => v ?? defaultInt;
        public abstract string ID { get; }
        public abstract byte[] Token { get; }
    }
    public abstract class UniqueEntity<TData> : UniqueEntity where TData :UniqueData, new()
    {
        private readonly TData data;
        public TData Data => data;
        public UniqueEntity() : this(new TData()) { }
        public UniqueEntity(TData d) => data = d;
        public override string ID => getValue(Data?.ID);
        public override byte[] Token => Data?.Token ?? Array.Empty<byte>();
    }
}
