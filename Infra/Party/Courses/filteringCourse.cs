using Project_1.Data.Party;
using Project_1.Infra.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1.Infra.Party.Courses {
    public static class FilteringCourse {
        public static IQueryable<CourseData> addFilter(IQueryable<CourseData> q, UniversityDb? dbSet, string? CurrentFilter, string? StudyProgramFilter, string? SemesterFilter, string? UserID) {
            var spcRepo = new StudyProgramsCoursesRepo(dbSet);
            var scRepo = new SemestersCoursesRepo(dbSet);
            var sRepo = new StudentsRepo(dbSet);
            var studyProgramsCourses = spcRepo.GetAll();
            var semesterCourses = scRepo.GetAll();
            var students = sRepo.GetAll();

            var textVal = CurrentFilter;
            var studyProgramID = StudyProgramFilter;
            var semesterID = SemesterFilter;
            string? userID = UserID;

            if (string.IsNullOrWhiteSpace(textVal) &&
                string.IsNullOrWhiteSpace(studyProgramID) &&
                string.IsNullOrWhiteSpace(userID) &&
                string.IsNullOrWhiteSpace(semesterID)) return q;

            var UserStudyProgram = students.FirstOrDefault(x => x.ID == userID)?.StudyProgramID ?? String.Empty;


            textVal = textVal?.ToLower() ?? "";

            List<string> rightElems = new List<string>();

            foreach (var x in q ) {
                var correctStudyProgram = string.IsNullOrEmpty(studyProgramID) ? true : studyProgramsCourses.FirstOrDefault(y => y.StudyProgramID == studyProgramID && y.CourseID == x.ID) != null;
                var correctSemester = string.IsNullOrEmpty(semesterID) ? true : semesterCourses.FirstOrDefault(y => y.SemesterID == semesterID && y.CourseID == x.ID) != null;

                var correctMandatory = string.IsNullOrEmpty(userID) && string.IsNullOrEmpty(UserStudyProgram) ? true : studyProgramsCourses.Where(y => y.CourseID == x.ID && UserStudyProgram == y.StudyProgramID).Count() > 0;

                bool contains = ((string.IsNullOrEmpty(textVal)) ? true : (x?.CourseTitle?.ToLower()?.Contains(textVal) ?? false)) && correctSemester && correctStudyProgram && correctMandatory;

                if (contains) rightElems.Add(x?.ID ?? String.Empty);
            }

            return q.Where(x => rightElems.Contains(x.ID));

        }
    }
}
