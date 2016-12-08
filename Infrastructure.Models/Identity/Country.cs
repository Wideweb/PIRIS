using Infrastructure.Models.Attributes;

namespace Infrastructure.Models.Identity
{
    [Table(Name = "Country")]
    public class Country : Entity
    {
        public string Name { get; set; }
    }
}
