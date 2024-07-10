using System.ComponentModel.DataAnnotations;

namespace BusinessObjectLayer.Entities
{
    public class MedicationMedicationPlan
    {
        [Key]
        public string Id { get; set; }
        public string MedicationId { get; set; }
        public virtual Medication Medication { get; set; }
        public string MedicationPlanId { get; set; }
        public virtual MedicationPlan MedicationPlan { get; set; }
    }
}
