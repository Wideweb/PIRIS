using Common.Core.Models;

namespace Identity.Core.Features.User.Models
{
    public class ClientListModel : BaseModel
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string MiddleName { get; set; }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }
    }
}
