using System.Collections.Generic;

namespace BusinessObjectLayer.Entities
{
    public class CaregiverEntity : UserEntity
    {
        public List<PatientEntity> Patients { get; set; }
    }
}
