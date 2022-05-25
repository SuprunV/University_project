using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_1.Aids;
using Project_1.Domain.Connection;
using Project_1.Domain.Party;
using Project_1.Facade.Connection;
using Project_1.Facade.Party;
using Project_1.Infra.Party.Courses;

namespace Project_1.Pages.Party.Courses {
    public class CoursesPages : PagedPage<CourseView, Course, ICoursesRepo> {

        public ICourseLecturerRepo courseLecturerRepo;
        public ISemesterCourseRepo semesterCourseRepo;
        public IStudyProgramsCoursesRepo studyProgramCoursesRepo;
        public List<CourseLecturer> courseLecturersList;
        public List<SemesterCourse> semesterCoursesList;
        public List<StudyProgram> studyProgramsList;
        public List<StudyProgramCourse> studyProgramCoursesList;
        public List<Lecturer> lecturersList;
        public List<Course> coursesList;
        public List<Semester> semestersList;
        public override Dictionary<string, IEnumerable<SelectListItem>> SelectFilters { get; }

        public CoursesPages(ICoursesRepo c, ICourseLecturerRepo cl, ILecturersRepo l, ISemestersRepo s, ISemesterCourseRepo sC, IStudyProgramsRepo sp, IStudyProgramsCoursesRepo spcr) : base(c) {

            courseLecturerRepo = cl;
            semesterCourseRepo = sC;
            studyProgramCoursesRepo = spcr;

            courseLecturersList = cl.GetAll(x => x.CourseID);
            semesterCoursesList = sC.GetAll(x => x.CourseID);
            studyProgramsList = sp.GetAll(x => x.StudyProgramsTitle ?? String.Empty);
            studyProgramCoursesList = spcr.GetAll(x => x.CourseID ?? String.Empty);
            lecturersList = l.GetAll(x => x.FullName ?? String.Empty);
            coursesList= c.GetAll(x => x.ID);
            semestersList= s.GetAll(x => x.ID);
            SelectFilters = new()
            {
                { nameof(CourseView.StudyProgramID), studyProgramsList.Select(x => new SelectListItem(x.StudyProgramsTitle, x.ID)) },
                { nameof(CourseView.SemesterID), semestersList.Select(x => new SelectListItem(x.SeasonFull(), x.ID)) },
            };
        }        
        public override string[] IndexColumns { get; } = new[] {
            nameof(CourseView.CourseTitle),
            nameof(CourseView.EAP),
            nameof(CourseView.StudyProgramID),
            nameof(CourseView.SemesterID),
            nameof(CourseView.OwnerID),
            nameof(CourseView.Description)
        };
        public IEnumerable<SelectListItem> Semesters
            => semestersList.Select(x => new SelectListItem(x.SeasonFull(), x.ID))
               ?? new List<SelectListItem>();
        public IEnumerable<SelectListItem> Lecturer
            => lecturersList.Select(x => new SelectListItem(x.FullName, x.ID))
               ?? new List<SelectListItem>();
        public IEnumerable<SelectListItem> StudyPrograms
            => studyProgramsList.Select(x => new SelectListItem(x.StudyProgramsTitle, x.ID))
            ?? new List<SelectListItem>();
        public List<string?> lecturersOnCourse {
            get {
                var neededLecturersID = courseLecturersList.Where(x => x.CourseID == Item.ID).Select(x => x.LecturerID);
                return lecturersList.Where(x => neededLecturersID.Contains(x.ID)).Select(x => x.FullName).ToList();
            }
            set { }
        }
        public IEnumerable<SelectListItem> allStudyPrograms
            => studyProgramsList
                   .Select(x => new SelectListItem(x.StudyProgramsTitle, x.ID))
               ?? new List<SelectListItem>();
        public string SemesterName(string? semID = null)
            => Semesters?.FirstOrDefault(x => x.Value == (semID ?? string.Empty))?.Text ?? "Unspecified";
        public string LecturerName(string? lecID = null)
            => Lecturer?.FirstOrDefault(x => x.Value == (lecID ?? string.Empty))?.Text ?? "Unspecified";
        public string StudyProgramName(string? spID = null)
            => StudyPrograms?.FirstOrDefault(x => x.Value == (spID ?? string.Empty))?.Text ?? "Unspecified";
        public override object? GetValue(string name, CourseView v) {
            var r = base.GetValue(name, v);
            switch(name) {
                case nameof(CourseView.SemesterID): return SemesterName(r as string);
                case nameof(CourseView.StudyProgramID): return StudyProgramName(r as string);
                case nameof(CourseView.OwnerID): return LecturerName(r as string);
                default: return r;
            }
        }
        
        protected override async Task<IActionResult> getItemPage(string id) {
            Item = await getItem(id);
            Item.SemesterID = semesterCoursesList.FirstOrDefault(x => x.CourseID == Item.ID)?.SemesterID;
            Item.StudyProgramID = studyProgramCoursesList.FirstOrDefault(x => x.CourseID == Item.ID)?.StudyProgramID;
            return Item == null ? NotFound() : Page();
        }
        public SemesterCourseView createSemesterCourseView(string id = "")
        {
            var s = new SemesterCourseView()
            {
                CourseID = Item.ID,
                SemesterID = Item.SemesterID ?? String.Empty
            };
            if (!string.IsNullOrEmpty(id)) s.ID = id;
            return s;
        }
        public StudyProgramCourseView createStudyProgramCourseView(string id = "")
        {
            var v = new StudyProgramCourseView()
            {
                StudyProgramID = Item.StudyProgramID ?? String.Empty,
                CourseID = Item.ID
            };
            if (!string.IsNullOrEmpty(id)) v.ID = id;
            return v;
        }
        protected override async Task<IActionResult> postCreateAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            await repo.AddAsync(toObject(Item));
            var sC = createSemesterCourseView();
            var spcv = createStudyProgramCourseView();
            await semesterCourseRepo.AddAsync(new SemesterCourseViewFactory().Create(sC));
            await studyProgramCoursesRepo.AddAsync(new StudyProgramCourseViewFactory().Create(spcv));
            return redirectToIndex("MyCoursesIndex");
        }
        protected override async Task<IActionResult> postEditAsync()
        {
            var sC_ID = semesterCoursesList.FirstOrDefault(x => x.CourseID == Item.ID)?.ID ?? "";
            var spcv_ID = studyProgramCoursesList.FirstOrDefault(x => x.CourseID == Item.ID)?.ID ?? "";

            var semCou = new SemesterCourseViewFactory().Create(createSemesterCourseView(sC_ID));
            var spcv = new StudyProgramCourseViewFactory().Create(createStudyProgramCourseView(spcv_ID));
            
            var o = repo.Get(Item.ID);
            if (ConcurrencyToken.ToStr(o.Token) == ConcurrencyToken.ToStr())
            {
                ModelState.AddModelError(string.Empty, "Unable to save. The item was deleted by another user.");
                return Page();
            }
            var oToken = ConcurrencyToken.ToStr(o.Token);
            var itemToken = ConcurrencyToken.ToStr(Item.Token);
            if (oToken != itemToken) return redirectToEdit(Item);



            if (!ModelState.IsValid) return Page();
            var obj = toObject(Item);


            var updated = await repo.UpdateNeededEntities(obj, semCou, spcv);
            return !updated ? NotFound() : redirectToIndex("MyCoursesIndex");
        }


        public async Task<IActionResult> OnGetJoinAsync(string id, int pageIndex = 0, int? itemsNr = null, string? currentFilter = null, string? sortOrder = null, string? StudyProgramID = null, string? SemesterID = null, string? Status = null, string? IsMandatory = null, string? nativePage = null) {
            CourseLecturerView cl = new CourseLecturerView() {
                CourseID = id,
                LecturerID = SessionUserID ?? String.Empty
            };

            CourseLecturer clObj = new CourseLecturerViewFactory().Create(cl);

            var allIDcou = courseLecturerRepo.GetAll(x => x.ID).Where(elem => (elem.CourseID == cl.CourseID) && (elem.LecturerID == cl.LecturerID)).FirstOrDefault();

            if (allIDcou == null) await courseLecturerRepo.AddAsync(clObj);
            else await courseLecturerRepo.DeleteAsync(allIDcou.ID);

            setAttributes(pageIndex,currentFilter,sortOrder,itemsNr,StudyProgramID,SemesterID,Status, getMandatoryCourse(IsMandatory), nativePage);
            
            return redirectToIndex(toNativePage:false);
        }
        public override string? additionalControlString(string courseID) {
            return ControllJoined.isJoinedToCourse(courseID, courseLecturersList, SessionUserID) ? "Unjoin" : "Join";
        }
        protected override Course toObject(CourseView? item) => new CourseViewFactory().Create(item);
        protected override CourseView toView(Course? entity, bool getNullVal = false) => (getNullVal) ? new CourseViewFactory().Create(entity) : new CourseViewFactory().CreateUndefined(entity);
    }
}
