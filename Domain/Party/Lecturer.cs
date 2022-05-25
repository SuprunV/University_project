using Project_1.Data.Party;

namespace Project_1.Domain.Party
{
    public sealed class Lecturer:NamedEntity<LecturerData>
    {
        //Add value for data using constructors
        public Lecturer() : this(new LecturerData()) { }
        public Lecturer(LecturerData d):base(d){}

    }
    public interface ILecturersRepo : IRepo<Lecturer>
    { }
}
