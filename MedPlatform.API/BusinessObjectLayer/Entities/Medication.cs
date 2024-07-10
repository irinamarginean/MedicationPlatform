using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjectLayer.Entities
{
    public class Medication
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string SideEffects { get; set; }
        public string Dosage { get; set; }
        public virtual List<MedicationMedicationPlan> MedicationMedicationPlans { get; set; }
    }
}
