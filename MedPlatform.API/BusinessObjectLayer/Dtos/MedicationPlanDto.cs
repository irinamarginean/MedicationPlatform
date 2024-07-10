using BusinessObjectLayer.Entities;
using System;
using System.Collections.Generic;

namespace BusinessObjectLayer.Dtos
{
    public class MedicationPlanDto
    {
        public string Id { get; set; }
        public string IntakeIntervals { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PatientId { get; set; }
        public PatientRegisterDto Patient { get; set; }
        public List<MedicationDto> Medication { get; set; }
    }
}
