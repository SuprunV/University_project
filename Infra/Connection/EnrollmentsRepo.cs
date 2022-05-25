using Microsoft.EntityFrameworkCore;
using Project_1.Data.Connection;
using Project_1.Data.IsoEnums;
using Project_1.Data.Party;
using Project_1.Domain.Connection;
using Project_1.Infra.Party;

namespace Project_1.Infra.Connection
{
    public sealed class EnrollmentsRepo : Repo<Enrollment, EnrollmentData>, IEnrollmentsRepo
    {
        private UniversityDb dbSet ;
        public EnrollmentsRepo(UniversityDb? db) : base(db, db?.Enrollments) {
            dbSet = db  ?? new UniversityDb(new DbContextOptions<UniversityDb>());
        }

        
        protected internal override Enrollment toDomain(EnrollmentData d) => new (d);

        internal override IQueryable<EnrollmentData> addFilter(IQueryable<EnrollmentData> q) {
            
            var courses = dbSet.Courses?.ToList<CourseData>();
            var semesterCourses = dbSet.SemestersCourse?.ToList<SemesterCourseData>();

            var textVal = CurrentFilter;
            var studyProgramID = StudyProgramFilter ;
            var semesterID = SemesterFilter;
            string? statusGrade = statusFilter;

            if (string.IsNullOrWhiteSpace(textVal) &&
                string.IsNullOrWhiteSpace(studyProgramID) &&
                string.IsNullOrWhiteSpace(semesterID) &&
                string.IsNullOrWhiteSpace(statusGrade)) return q;



            textVal = textVal?.ToLower() ?? "";

            List<string> rightElems = new List<string>();

            foreach (var x in q)
            {
                bool correctText = string.IsNullOrEmpty(textVal) ? true : (courses?.FirstOrDefault(y => y.ID == x.CourseID)?.CourseTitle?.ToLower() ?? string.Empty).Contains(textVal);
                bool correctSemester = string.IsNullOrEmpty(semesterID) ? true : semesterID  == x.SemesterID;
                bool correctStatus = string.IsNullOrEmpty(statusGrade) ? true : getStatusByNumber(x.Grade) == statusGrade;

                bool contains = correctText && correctSemester && correctStatus;
                //bool contains = true;

                if (contains) rightElems.Add(x.ID);
            }

            return q.Where(x => rightElems.Contains(x.ID));
        }

        internal override IQueryable<EnrollmentData> addAdvancedFilter(IQueryable<EnrollmentData> q) {
            if (SessionUserID == null || SessionRoll == null) return q;

            if (SessionRoll == "lecturer") return q.Where(x => x.DegreeTakerID == SessionUserID);
            else if (SessionRoll == "student") return q.Where(x => x.StudentID == SessionUserID);
            return q;
        }
        private string getStatusByNumber(int? number) {
            if (number > 0 && number <= 5) return IsoCourseStatus.Passed.ToString();
            else return (((IsoCourseStatus)number)).ToString();
        }
    }
}
