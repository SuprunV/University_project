using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;
using Project_1.Data.Connection;
using Project_1.Data.Party;
using Project_1.Domain;
using Project_1.Domain.Connection;
using Project_1.Domain.Party;
using Project_1.Infra;
using Project_1.Infra.Connection;
using Project_1.Infra.Party.Courses;

namespace Project_1.Tests.Infra.Party {
    [TestClass] public class CoursesRepoTests 
        : SealedRepoTests<CoursesRepo, Repo<Course, CourseData>, Course, CourseData> {
        private CourseData? d;
        private SemesterCourseData? semCou;
        private StudyProgramCourseData? stCou;



        protected override CoursesRepo createObj() => new (GetRepo.Instance<UniversityDb>());
        protected SemestersCoursesRepo createObjSC() => new (GetRepo.Instance<UniversityDb>());
        protected StudyProgramsCoursesRepo createObjSPC() => new (GetRepo.Instance<UniversityDb>());
        protected override object? getSet(UniversityDb db) => db.Courses;
        [TestMethod]
        public async Task UpdateNeededEntitiesTest()
        {

            var objSC = createObjSC();
            var objSPC = createObjSPC();

            d = GetRandom.Value<CourseData>();
            semCou = GetRandom.Value<SemesterCourseData>();
            stCou = GetRandom.Value<StudyProgramCourseData>();

            _ = obj.Add(new Course(d));
            _ = objSC.Add(new SemesterCourse(semCou));
            _ = objSPC.Add(new StudyProgramCourse(stCou));

            var dCou = GetRandom.Value<CourseData>() as CourseData;
            var dSemCou = GetRandom.Value<SemesterCourseData>() as SemesterCourseData;
            var dstCX = GetRandom.Value<StudyProgramCourseData>() as StudyProgramCourseData;
            
            isNotNull(d);
            isNotNull(semCou);
            isNotNull(stCou);
            isNotNull(dstCX);
            isNotNull(dSemCou);
            isNotNull(dCou);
            dCou.ID = d.ID;
            dSemCou.ID = semCou.ID;
            dstCX.ID = stCou.ID;

            var aX = new Course(dCou);
            var aSemCouX = new SemesterCourse(dSemCou);
            var aStCouX = new StudyProgramCourse(dstCX);
            
            isTrue(await obj.UpdateNeededEntities(aX, aSemCouX, aStCouX));
        }
    }
}
