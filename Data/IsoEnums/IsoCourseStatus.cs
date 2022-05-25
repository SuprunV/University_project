using System.ComponentModel;

namespace Project_1.Data.IsoEnums
{
    public enum IsoCourseStatus {
        [Description("Failed")] Failed = 0,
        [Description("Passed")] Passed = 5,
        [Description("In process")] InProcess = 9,
        [Description("Unknown")] Unknown = 11
    }

}
