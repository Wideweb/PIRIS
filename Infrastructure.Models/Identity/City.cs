using Infrastructure.Models.Attributes;
using System.Collections.Generic;

namespace Infrastructure.Models.Identity
{
    [Table(Name = "City")]
    public class City : Entity
    {
        public string Name { get; set; }
    }
}
