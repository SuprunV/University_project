using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Project_1.Data.Party;
using Project_1.Domain.Party;

namespace Project_1.Facade.Party {
    public sealed class StudyProgramView:UniqueView
    {
        [DisplayName("Study program title"), Required, RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$", ErrorMessage = "Word starts with a capital letter and use only english keyboard")] public string? StudyProgramsTitle { get; set; }
        [DisplayName("Program duration: (in years)"), Required, Range(1, 6)] public int? Duration { get; set; }
        [DisplayName("Description"), RegularExpression(@"^[a-zA-Z0-9 , . : ; - ""'\s-]*$", ErrorMessage = "Use only english keyboard ")] public string? StudyProgramsDescription { get; set; }
    }
    public sealed class StudyProgramViewFactory : BaseViewFactory<StudyProgramView, StudyProgram, StudyProgramData>
    {
        protected override StudyProgram toEntity(StudyProgramData d) => new(d);
    }
}
