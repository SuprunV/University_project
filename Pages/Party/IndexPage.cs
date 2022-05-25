using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project_1.Domain;
using Project_1.Domain.Party;
using Project_1.Facade.Party;
using Project_1.Infra;
using Project_1.Infra.Party;

namespace Project_1.Pages.Party {
    public sealed class IndexPage : PagedPage<StudentView, Student, IStudentsRepo>
    {
        private readonly UniversityDb _context = new UniversityDb(new DbContextOptions<UniversityDb>());
        [BindProperty] public string? Roll { get; set; } = "admin";
        [BindProperty] public string? UserId { get; set; } = string.Empty;
        public string[] Values { get; set; } = new string[] { "student", "lecturer", "admin" };
        public IEnumerable<SelectListItem> Rolls
            => Values.Select(x => new SelectListItem($"{x.Substring(0,1).ToUpper()}{x.Substring(1,x.Length-1)}", x));
        public IEnumerable<SelectListItem> Users { get { return getUsers(); } set { Users = value; } }


        private List<Student> students;
        private List<Lecturer> lecturers;

        public IndexPage(IStudentsRepo r, ILecturersRepo l) : base(r) {
            students = r.GetAll(x => x.ID);
            lecturers = l.GetAll(x => x.ID);
        }

        public Task<IActionResult> OnGet() {
            Roll = SessionRoll;
            UserId = SessionUserID;
            return Task.FromResult<IActionResult>(Page());
        }

        public Task<IActionResult> OnPost() {

            if (!string.IsNullOrEmpty(UserId))
            {
                SessionUserID = UserId;
                SessionUserName = getUserName();
            }

            if (!string.IsNullOrEmpty(Roll)) {
                if(Roll != SessionRoll)
                {
                    removeSession("_UserName");
                    removeSession("_UserID");
                }
                SessionRoll = Roll;
            }

            return Task.FromResult<IActionResult>(Page());
        }

        public IEnumerable<SelectListItem> getUsers() {
            switch (Roll) {
                case "student": return students.Select(x => new SelectListItem(x.FullName, x.ID));
                case "lecturer": return lecturers.Select(x => new SelectListItem(x.FullName, x.ID));
                default: return new List<SelectListItem>();
            }
        }

        public string getUserName() {
            string name = "";
            if(!string.IsNullOrEmpty(SessionUserID))
            {
                if(SessionRoll == "student")
                {
                    var student = students.FirstOrDefault(x => x.ID == SessionUserID);
                    name = student != null ? $"{ student.FirstName } { student.LastName }" : "";
                }
                else if (SessionRoll == "lecturer")
                {
                    var lecturer = lecturers.FirstOrDefault(x => x.ID == SessionUserID);
                    name = lecturer != null ? $"{ lecturer.FirstName } { lecturer.LastName }" : "";
                }
            }
            return name;
        }

        public string selectedOption(string roll) =>
            (Roll == roll) ? "selected" : "";

        protected override StudentView toView(Student? entity, bool getNullVal = true) => new StudentViewFactory().Create(entity);
        protected override Student toObject(StudentView? entity) => new StudentViewFactory().Create(entity);

    }
}
