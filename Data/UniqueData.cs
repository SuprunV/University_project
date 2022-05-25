using System.ComponentModel.DataAnnotations;

namespace Project_1.Data
{
    public abstract class UniqueData
    {
        public static string NewID => Guid.NewGuid().ToString();
        protected static byte[] empty => Array.Empty<byte>();
        public string ID { get; set; } = NewID;
        [Timestamp] public byte[] Token { get; set; } = empty;
    }
}
