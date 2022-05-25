using Project_1.Data.Connection;
using Project_1.Domain.Party;

namespace Project_1.Domain.Connection
{
    public interface IEnrollmentsRepo : IRepo<Enrollment> { }
    public sealed class Enrollment : UniqueEntity<EnrollmentData>
    {
        public Enrollment() : this(new()) { }
        public Enrollment(EnrollmentData d) : base(d) { }
        public string? StudentID => getValue(Data?.StudentID);
        public string? DegreeTakerID => getValue(Data?.DegreeTakerID);
        public string SemesterID => getValue(Data?.SemesterID);
        public string CourseID => getValue(Data?.CourseID);
        public int? Grade => getValue(Data?.Grade);

        public string? CreateStatus()
        {
            var totalStatus = string.Empty;
            if (Grade >= 1 && Grade!=9) totalStatus = "Passed";
            else if(Grade ==0)
            { totalStatus = "Not passed"; }
            else { totalStatus = "In process"; }
            return totalStatus;
        }
        public Course? Course => GetRepo.Instance<ICoursesRepo>()?.Get(CourseID);
        public Semester? Semester => GetRepo.Instance<ISemestersRepo>()?.Get(SemesterID);
        public string? StudyProgramID => GetRepo.Instance<IStudyProgramsCoursesRepo>()?.GetBy(x => x.CourseID == CourseID).StudyProgramID;
        public Lecturer? Lecturer => GetRepo.Instance<ILecturersRepo>()?.Get(DegreeTakerID ?? string.Empty);
        public Student? Student => GetRepo.Instance<IStudentsRepo>()?.Get(StudentID ?? string.Empty);
    }
}
