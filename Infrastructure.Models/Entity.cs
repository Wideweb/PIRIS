namespace Infrastructure.Models
{
    public class Entity
    {
        public long Id { get; set; }

        public bool IsNew => Id <= 0;
    }
}
