using System.ComponentModel.DataAnnotations;

namespace BusinessObjectLayer.Dtos
{
    public class UserUpdateDto
    {
        public string Id { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
    }
}
