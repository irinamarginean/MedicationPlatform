using BusinessObjectLayer.Entities;
using DataAccessLayer.Repositories;
using Grpc.Core;
using MedicationPlatform.API.Protos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicationPlatform.API.Services
{
    public class MedicationPlanDownloaderService : MedicationPlanDownloader.MedicationPlanDownloaderBase
    {
        private readonly ILogger<MedicationPlanDownloaderService> _logger;
        private readonly IRepository<MedicationPlan> medicationPlanRepository;
        private readonly IRepository<MedicationMedicationPlan> medicationMedicationPlanRepository;

        public MedicationPlanDownloaderService(ILogger<MedicationPlanDownloaderService> logger, 
            IRepository<MedicationPlan> medicationPlanRepository, 
            IRepository<MedicationMedicationPlan> medicationMedicationPlanRepository)
        {
            _logger = logger;
            this.medicationPlanRepository = medicationPlanRepository;
            this.medicationMedicationPlanRepository =  medicationMedicationPlanRepository;
        }

        public async override Task<MedicationPlanReply> RequestMedicationPlanDownload(MedicationPlanRequest request, ServerCallContext context)
        {
            var medicationPlans = await medicationPlanRepository.GetAll();
            var medicationPlan = medicationPlans.FirstOrDefault(x => x.PatientId == request.Id);
            var medications = medicationMedicationPlanRepository.GetDbSet().Include(x => x.Medication).ToList();
            var medicationList = String.Join(", ", medications.Select(x => x.Medication.Name));

            return await Task.FromResult(new MedicationPlanReply
            {
                //IntakeIntervals = (float)medicationPlan.IntakeIntervals,
                StartDate = medicationPlan.StartDate.ToString(),
                EndTime = medicationPlan.EndDate.ToString(),
                Medications = medicationList
            });
        }
    }
}
