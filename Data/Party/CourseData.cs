

namespace Project_1.Data.Party
{
    public sealed class CourseData:UniqueData { 
        public string? CourseTitle { get; set; }
        public int? EAP { get; set; } = 0;
        public string? Description { get; set; }
        public string? OwnerID { get; set; } = string.Empty;
    }
}
