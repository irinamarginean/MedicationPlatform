using System.Collections.Generic;

namespace BusinessObjectLayer.Entities
{
    public class PatientEntity : UserEntity
    {
        public string MedicalRecord { get; set; }
        public string CaregiverId { get; set; }
        public CaregiverEntity Caregiver { get; set; }
        public List<MedicationPlan> MedicationPlans { get; set; }
        public List<ActivityEntity> Activities { get; set; }
    }
}
