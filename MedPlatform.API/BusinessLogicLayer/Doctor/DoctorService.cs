using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjectLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Doctor
{
    public class DoctorService : IDoctorService
    {
        private readonly IRepository<Medication> medicationRepository;
        private readonly IRepository<MedicationPlan> medicationPlanRepository;

        public DoctorService(IRepository<Medication> medicationRepository, IRepository<MedicationPlan> medicationPlanRepository)
        {
            this.medicationRepository = medicationRepository;
            this.medicationPlanRepository = medicationPlanRepository;
        }

        public async Task<ICollection<Medication>> GetAllMedications()
        {
            return await medicationRepository.GetAll();
        }

        public async Task<Medication> GetMedicationById(string id)
        {
            var medications = await medicationRepository.GetAll();

            return medications.FirstOrDefault(x => x.Id == id);
        }

        public async Task UpdateMedication(Medication medicationToUpdate)
        {
            await medicationRepository.Update(medicationToUpdate);
        }

        public async Task DeleteMedication(Medication medicationToDelete)
        {
            await medicationRepository.Delete(medicationToDelete);
        }

        public async Task<MedicationPlan> GetMedicationPlanById(string id)
        {
            var medications = await medicationPlanRepository.GetAll();

            return medications.FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<MedicationPlan>> GetMedicationPlansByPatientId(string patientId)
        {
            var medicationPlansSet = medicationPlanRepository.GetDbSet();

            var meds =  medicationPlansSet
                .Include(x => x.Patient)
                .Include(x => x.MedicationMedicationPlans).
                ThenInclude(x => x.Medication);
            return meds.Where(x => x.PatientId == patientId).ToList();
        }

        public async Task CreateMedicationPlan(MedicationPlan medicationPlan)
        {
            await medicationPlanRepository.Insert(medicationPlan);
        }

        public async Task UpdateMedicationPlan(MedicationPlan medicationPlan)
        {
            await medicationPlanRepository.Update(medicationPlan);
        }

        public async Task AddMedication(Medication medication)
        {
            await medicationRepository.Insert(medication);
        }
    }
}
