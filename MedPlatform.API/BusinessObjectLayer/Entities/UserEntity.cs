using System;

namespace BusinessObjectLayer.Entities
{
    public class UserEntity : BaseUserEntity
    {
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
    }
}
