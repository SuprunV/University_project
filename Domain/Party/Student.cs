using Project_1.Data.Party;

namespace Project_1.Domain.Party
{
    public sealed class Student:NamedEntity<StudentData>
    {
        public Student() : this(new ()) { }
        public Student(StudentData d):base(d){}

        public DateTime? EnrollmentDate => getValue(Data?.EnrollmentDate);
        public string? Nationality => getValue(Data?.Nationality);
        public string? StudyProgramID => getValue(Data?.StudyProgramID);

    }
    public interface IStudentsRepo : IRepo<Student> { }
}
