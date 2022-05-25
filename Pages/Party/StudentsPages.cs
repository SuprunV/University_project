using System.Net.Sockets;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_1.Data.Party;
using Project_1.Aids;
using Project_1.Data.IsoEnums;
using Microsoft.AspNetCore.Mvc;
using Project_1.Domain.Connection;
using Project_1.Domain.Party;
using Project_1.Facade.Connection;
using Project_1.Facade.Party;

namespace Project_1.Pages.Party
{
    public class StudentsPages : PagedPage<StudentView, Student, IStudentsRepo>
    {
        private readonly ICountriesRepo countries;
        private readonly IStudyProgramsRepo studyPrograms;
        public StudentsPages(IStudentsRepo r, ICountriesRepo c, IStudyProgramsRepo sp) : base(r) {
            countries = c;
            studyPrograms = sp;

        } 
        protected override Student toObject(StudentView? item) => new StudentViewFactory().Create(item);
        protected override StudentView toView(Student? entity, bool getNullVal =false) => (getNullVal) ? new StudentViewFactory().Create(entity) : new StudentViewFactory().CreateUndefined(entity);
        public override string[] IndexColumns { get; } = new[] {
            nameof(StudentView.UniID),
            nameof(StudentView.FirstName),
            nameof(StudentView.LastName),
            nameof(StudentView.Sex),
            nameof(StudentView.Nationality),
            nameof(StudentView.EnrollmentDate)
        };
        public IEnumerable<SelectListItem> Nationalities 
        => countries?.GetAll(x => x?.CountryName ?? String.Empty)
            .Select(x => new SelectListItem(x.CountryName, x.ID))
            ?? new List<SelectListItem>();

        public IEnumerable<SelectListItem> StudyPrograms
        => studyPrograms?.GetAll(x => x?.StudyProgramsTitle ?? String.Empty)
            .Select(x => new SelectListItem(x.StudyProgramsTitle, x.ID))
            ?? new List<SelectListItem>();

        public string CountryName(string? countryID = null)
            => Nationalities?.FirstOrDefault(x=>x.Value == (countryID?? string.Empty))?.Text ?? "Unspecified";

        public override object? GetValue(string name, StudentView v)
        {
            var r = base.GetValue(name, v);
            if (name == nameof(StudentView.Nationality)) return CountryName(r as string);
            if (name == nameof(StudentView.Sex)) return GenderDescription((IsoGender)(r ?? IsoGender.NotApplicable));
            return r;
        }
        public IEnumerable<SelectListItem> Genders
            => Enum.GetValues<IsoGender>()?
                   .Select(x => new SelectListItem(x.Description(), x.ToString()))
               ?? new List<SelectListItem>();
        public string GenderDescription(IsoGender? x)
            => (x ?? IsoGender.NotApplicable).Description();
    }
}
