using Microsoft.EntityFrameworkCore;
using Project_1.Data.Party;
using Project_1.Domain.Connection;
using Project_1.Domain.Party;
using Project_1.Infra.Connection;

namespace Project_1.Infra.Party.Courses
{
    public sealed class JoinedCoursesRepo : Repo<Course, CourseData>, IJoinedCoursesRepo
    {
        readonly List<CourseLecturer>? courseLecturersList;
        readonly UniversityDb? dbSet;
        public JoinedCoursesRepo(UniversityDb? c) : base(c, c?.Courses) {
            dbSet = c;
            courseLecturersList = new CourseLecturersRepo(c ?? new UniversityDb(new DbContextOptions<UniversityDb>())).GetAll(x => x.ID);
        }

        protected internal override Course toDomain(CourseData d) => new(d);

        internal override IQueryable<CourseData> addFilter(IQueryable<CourseData> q) => FilteringCourse.addFilter(q, dbSet, CurrentFilter, StudyProgramFilter, SemesterFilter, UserID);

        internal override IQueryable<CourseData> addAdvancedFilter(IQueryable<CourseData> q) {

            var correctItems = new List<string>();
            foreach(var i in q)  if (ControllJoined.isJoinedToCourse(i.ID, courseLecturersList, SessionUserID)) correctItems.Add(i.ID);
            
            return q.Where(i => correctItems.Contains(i.ID));
        }
    }
    public static class ControllJoined {
        public static bool isJoinedToCourse(string? courseID, List<CourseLecturer>? cl, string? SessionUserID) {
            var allIDs = cl?.Where(elem => (elem.CourseID == courseID) && (elem.LecturerID == SessionUserID))?.FirstOrDefault() ?? null;
            return allIDs != null;
        }

    }
}
