using Project_1.Data.Connection;
using Project_1.Domain.Party;

namespace Project_1.Domain.Connection {
    public interface ICourseLecturerRepo : IRepo<CourseLecturer> { }
    public sealed class CourseLecturer : UniqueEntity<CourseLecturerData> {
        public CourseLecturer() : this(new ()) { }
        public CourseLecturer(CourseLecturerData d) : base (d) { }
        public string CourseID => getValue(Data?.CourseID);
        public string LecturerID => getValue(Data?.LecturerID);
        public Course? Course => GetRepo.Instance<ICoursesRepo>()?.Get(CourseID);
        public Lecturer? Lecturer => GetRepo.Instance<ILecturersRepo>()?.Get(LecturerID);
    }
}