namespace Project_1.Infra
{
    public abstract class SessionRepo {
        public string SessionUserName { get; set; } = string.Empty;
        public string SessionRoll { get; set; } = string.Empty;
        public string SessionUserID { get; set; } = string.Empty;
    }
}
