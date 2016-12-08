using Infrastructure.Models.Attributes;

namespace Infrastructure.Models.Identity
{
    [Table(Name = "Disability")]
    public class Disability : Entity
    {
        public string Name { get; set; }
    }
}
