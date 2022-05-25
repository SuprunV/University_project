using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Project_1.Facade
{
    public abstract class UniqueView
    { 
        [DisplayName("ID"),Required, /*StringLength(6, MinimumLength = 6), RegularExpression(@"^[A-Z]+[A-Z0-9""'\s-]*$", ErrorMessage = "ID consists only uppercase letters and numbers")*/] public string ID { get; set; } = Guid.NewGuid().ToString();
        [Required] public byte[] Token { get; set; } = Array.Empty<byte>();
    }

}
