using Microsoft.AspNetCore.Identity;

namespace BusinessObjectLayer.Entities
{
    public class BaseUserEntity : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
