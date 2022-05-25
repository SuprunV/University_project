using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Project_1.Data.IsoEnums;
using Project_1.Data.Party;


namespace Project_1.Facade
{
    public abstract class NamedView:UniqueView
    {
        [DisplayName("Uni ID"), Required, StringLength(6, MinimumLength = 6), RegularExpression(@"^[a-z]+[a-z""'\s-]*$",ErrorMessage = "Word use only with a lowercase letter and english keyboard")] public string? UniID { get; set; }
        [DisplayName("Last name"),Required, RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Word starts with a capital letter and use only english keyboard")] public string? LastName { get; set; }
        [DisplayName("First name"), RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Word starts with a capital letter and use only english keyboard")] public string? FirstName { get; set; }
        [DisplayName("Sex")] public IsoGender? Sex { get; set; }
        [DisplayName("Full name")] public string? FullName { get; set; }
    }
}
