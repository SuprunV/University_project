namespace Project_1.Data.Connection
{
    public sealed class EnrollmentData:UniqueData
    {
        public string? StudentID { get; set; }
        public string? DegreeTakerID { get; set; }
        public string? SemesterID { get; set; }
        public string? CourseID { get; set; }
        public int? Grade { get; set; }
   
    }
}
