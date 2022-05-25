using System.ComponentModel;

namespace Project_1.Data.IsoEnums {
    public enum IsoSemester {
        [Description("Autumn")] Autumn = 0,
        [Description("Spring")] Spring = 1,
        [Description("Not applicable")] NotApplicable = 9
    }
}
