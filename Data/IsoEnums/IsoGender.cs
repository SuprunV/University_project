using System.ComponentModel;

namespace Project_1.Data.IsoEnums
{
    public enum IsoGender {
        [Description("Not known")] NotKnown = 0,
        [Description("Male")] Male = 1,
        [Description("Female")] Female = 2,
        [Description("Not applicable")] NotApplicable = 9    
    }
}
