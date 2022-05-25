using Microsoft.EntityFrameworkCore;
using Project_1.Data.Connection;
using Project_1.Domain.Connection;
using Project_1.Domain.Party;
using Project_1.Infra.Party;
using Project_1.Infra.Party.Courses;

namespace Project_1.Infra.Connection
{
    public sealed class StudyProgramsCoursesRepo : Repo<StudyProgramCourse, StudyProgramCourseData>, IStudyProgramsCoursesRepo
    {
        private List<StudyProgram>? studyProgram;
        private List<Course>? courses;
        private UniversityDb? dbSet ;

        public StudyProgramsCoursesRepo(UniversityDb? db) : base(db, db?.StudyProgramsCourses)
        {
            dbSet = db ;
        }
        
        protected internal override StudyProgramCourse toDomain(StudyProgramCourseData d) => new(d);

        internal override IQueryable<StudyProgramCourseData> addFilter(IQueryable<StudyProgramCourseData> q)
        {

            studyProgram = new StudyProgramsRepo(dbSet).GetAll(x => x.StudyProgramsTitle );
            courses = new CoursesRepo(dbSet).GetAll(x => x?.CreateTotalString() ?? String.Empty);

            var name = CurrentFilter;
            List<string> rightElems = new List<string>();
            if (string.IsNullOrWhiteSpace(name)) return q;
            name = name.ToLower();
            foreach (var x in q)
            {
                var semestersName = (studyProgram?.Where(y => y.ID == x.StudyProgramID).FirstOrDefault())?.StudyProgramsTitle ?? null;
                var coursesName = (courses?.Where(y => y.ID == x.CourseID).FirstOrDefault())?.CreateTotalString() ??
                                  null;
                bool contains = (semestersName != null && semestersName.ToLower().Contains(name))
                                || (coursesName != null && coursesName.ToLower().Contains(name));

                if (contains) rightElems.Add(x.ID);
            }
            return q.Where(x => rightElems.Contains(x.ID));
        }
    }
}
    

   

