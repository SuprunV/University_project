using Project_1.Data.Connection;
using Project_1.Domain.Party;

namespace Project_1.Domain.Connection
{
    public interface IStudyProgramsCoursesRepo : IRepo<StudyProgramCourse> { }
    public sealed class StudyProgramCourse : UniqueEntity<StudyProgramCourseData>
    {
        public StudyProgramCourse() : this(new()) { }
        public StudyProgramCourse(StudyProgramCourseData d) : base(d) { }
        public string? StudyProgramID => getValue(Data?.StudyProgramID);
        public string? CourseID => getValue(Data?.CourseID);
        public Course? Course => GetRepo.Instance<ICoursesRepo>()?.Get(CourseID ?? String.Empty);
        public StudyProgram? StudyProgram => GetRepo.Instance<IStudyProgramsRepo>()?.Get(StudyProgramID ?? String.Empty);
    }
}