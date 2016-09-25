namespace Common.Core.Models
{
    public class BaseModel
    {
        public long Id { get; set; }
        public bool IsNew => Id <= 0;
    }
}
