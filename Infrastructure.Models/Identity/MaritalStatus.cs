using Infrastructure.Models.Attributes;

namespace Infrastructure.Models.Identity
{
    [Table(Name = "MaritalStatus")]
    public class MaritalStatus : Entity
    {
        public string Name { get; set; }
    }
}
