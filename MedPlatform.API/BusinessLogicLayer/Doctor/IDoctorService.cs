using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjectLayer.Entities;

namespace BusinessLogicLayer.Doctor
{
    public interface IDoctorService
    {
        Task<ICollection<Medication>> GetAllMedications();
        Task<Medication> GetMedicationById(string id);
        Task AddMedication(Medication medication);
        Task UpdateMedication(Medication medicationToUpdate);
        Task DeleteMedication(Medication medicationToDelete);
        Task<MedicationPlan> GetMedicationPlanById(string id);
        Task<List<MedicationPlan>> GetMedicationPlansByPatientId(string patientId);
        Task CreateMedicationPlan(MedicationPlan medicationPlan);
        Task UpdateMedicationPlan(MedicationPlan medicationPlan);
    }
}
