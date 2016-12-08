using Infrastructure.Models.Attributes;

namespace Infrastructure.Models.Identity
{
    [Table(Name = "Role")]
    public class Role : Entity
    {
        public string Name { get; set; }
    }
}
