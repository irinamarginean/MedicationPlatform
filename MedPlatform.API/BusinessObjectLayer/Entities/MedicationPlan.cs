using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjectLayer.Entities
{
    public class MedicationPlan
    {
        [Key]
        public string Id { get; set; }
        public string IntakeIntervals { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string  PatientId { get; set; }
        public PatientEntity Patient { get; set; }
        public virtual List<MedicationMedicationPlan> MedicationMedicationPlans { get; set; }
    }
}
