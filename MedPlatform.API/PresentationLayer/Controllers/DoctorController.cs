using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Doctor;
using BusinessObjectLayer;
using BusinessObjectLayer.Dtos;
using BusinessObjectLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedicationPlatform.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Constants.DoctorRole)]
    public class DoctorController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IDoctorService doctorService;

        public DoctorController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IDoctorService doctorService)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.doctorService = doctorService;
        }

        [HttpPost("users/caregivers/register")]
        public async Task<IActionResult> RegisterCaregiver([FromBody] UserRegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isBirthDateValid = DateTime.TryParse(model.BirthDate, out var userBirthDate);

            if (!isBirthDateValid) return BadRequest($"Birth date {model.BirthDate} is not valid");

            var user = new CaregiverEntity
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                Gender = model.Gender,
                Address = model.Address,
                BirthDate = userBirthDate
            };

            await EnsureRolesAsync(model.Role);

            var result = await userManager.CreateAsync(user, model.Password);
            await userManager.AddToRoleAsync(user, model.Role);

            if (result.Succeeded) return Ok();
            
            return BadRequest(result.Errors);
        }

        [HttpPost("users/patients/register")]
        public async Task<IActionResult> RegisterPatient([FromBody] PatientRegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isBirthDateValid = DateTime.TryParse(model.BirthDate, out var userBirthDate);

            if (!isBirthDateValid) return BadRequest($"Birth date {model.BirthDate} is not valid");

            var user = new PatientEntity
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                Gender = model.Gender,
                Address = model.Address,
                BirthDate = userBirthDate,
                MedicalRecord =  model.MedicalRecord
            };

            await EnsureRolesAsync(model.Role);

            var result = await userManager.CreateAsync(user, model.Password);
            await userManager.AddToRoleAsync(user, model.Role);

            if (result.Succeeded) return Ok();
            
            return BadRequest(result.Errors);
        }

        [HttpGet("users/patients/all")]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await userManager.GetUsersInRoleAsync(Constants.PatientRole);
            IList<PatientEntity> patientEntities = new List<PatientEntity>();

            foreach (var patient in patients)
            {
                if (patient is PatientEntity patientEntity)
                {
                    patientEntities.Add(patientEntity);
                }
            }

            if (patientEntities.Count > 0) return Ok(patientEntities);

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("users/patients/{id}")]
        public async Task<IActionResult> GetPatientById(string id)
        {
            var patients = await userManager.GetUsersInRoleAsync(Constants.PatientRole);

            if (patients.FirstOrDefault(x => x.Id == id) is PatientEntity patient) return Ok(patient);

            return NotFound();
        }

        [HttpGet("users/caregivers/all")]
        public async Task<IActionResult> GetAllCaregivers()
        {
            var caregivers = await userManager.GetUsersInRoleAsync(Constants.CaregiverRole);
            IList<CaregiverEntity> caregiverEntities = new List<CaregiverEntity>();

            foreach (var caregiver in caregivers)
            {
                if (caregiver is CaregiverEntity caregiverEntity)
                {
                    caregiverEntities.Add(caregiverEntity);
                }
            }

            if (caregiverEntities.Count > 0) return Ok(caregiverEntities);

            return NotFound();
        }

        [HttpGet("users/caregivers/{id}")]
        public async Task<IActionResult> GetCaregiverById(string id)
        {
            var caregivers = await userManager.GetUsersInRoleAsync(Constants.CaregiverRole);

            if (caregivers.FirstOrDefault(x => x.Id == id) is CaregiverEntity caregiver) return Ok(caregiver);

            return NotFound();
        }

        [HttpPut("users/caregivers/update")]
        public async Task<IActionResult> UpdateUser(UserUpdateDto userUpdateDto)
        {
            if (await userManager.FindByIdAsync(userUpdateDto.Id) is UserEntity user)
            {
                user.UserName = userUpdateDto.UserName ?? user.UserName;
                user.Email = userUpdateDto.Email ?? user.Email;
                user.Address = userUpdateDto.Address ?? user.Address;
                user.FirstName = userUpdateDto.FirstName ?? user.FirstName;
                user.LastName = userUpdateDto.LastName ?? user.LastName;

                IdentityResult result = await userManager.UpdateAsync(user);

                if (result.Succeeded) return Ok();
                    
                return BadRequest(result.Errors);
                
            }
            return NotFound();
        }

        [HttpPut("users/patients/update")]
        public async Task<IActionResult> UpdatePatient(PatientUpdateDto patientUpdateDto)
        {
            if (await userManager.FindByIdAsync(patientUpdateDto.Id) is PatientEntity patient)
            {
                patient.UserName = patientUpdateDto.UserName ?? patient.UserName;
                patient.Email = patientUpdateDto.Email ?? patient.Email;
                patient.Address = patientUpdateDto.Address ?? patient.Address;
                patient.FirstName = patientUpdateDto.FirstName ?? patient.FirstName;
                patient.LastName = patientUpdateDto.LastName ?? patient.LastName;
                patient.MedicalRecord = patientUpdateDto.MedicalRecord ?? patient.MedicalRecord;

                if (!string.IsNullOrEmpty(patientUpdateDto.CaregiverId) && 
                    await userManager.FindByIdAsync(patientUpdateDto.CaregiverId) is CaregiverEntity caregiver)
                {
                    patient.Caregiver = caregiver;
                }

                IdentityResult result = await userManager.UpdateAsync(patient);

                if (result.Succeeded) return Ok();
                    
                return BadRequest(result.Errors);
                
            }
            return NotFound();
        }

        [HttpDelete("users/delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (await userManager.FindByIdAsync(id) is UserEntity user)
            {
                IdentityResult result = await userManager.DeleteAsync(user);

                if (result.Succeeded) return Ok();

                return BadRequest(result.Errors);
            }

            return NotFound();
        }

        [AllowAnonymous]
        [HttpGet("medications/all")]
        public async Task<IActionResult> GetAllMedications()
        {
            return Ok(await doctorService.GetAllMedications());
        }

        [HttpGet("medications/{id}")]
        public async Task<IActionResult> GetMedicationById(string id)
        {
            return Ok(await doctorService.GetMedicationById(id));
        }

        
        [HttpPost("medications/add")]
        public async Task<IActionResult> AddMedication(MedicationDto medicationDto)
        {
            var medication = new Medication
            {
                Id = Guid.NewGuid().ToString(),
                Name = medicationDto.Name,
                Dosage = medicationDto.Dosage,
                SideEffects = medicationDto.SideEffects
            };

            await doctorService.AddMedication(medication);

            return Ok();
        }

        [HttpPost("medication-plan/add")]
        public async Task<IActionResult> AddMedicationPlan(MedicationPlanDto medicationPlanDto)
        {
            var medicationPlan = new MedicationPlan
            {
                Id = Guid.NewGuid().ToString(),
                IntakeIntervals = medicationPlanDto.IntakeIntervals,
                StartDate = medicationPlanDto.StartDate,
                EndDate = medicationPlanDto.EndDate,
                PatientId = medicationPlanDto.Id,
            };

            await doctorService.CreateMedicationPlan(medicationPlan);
            medicationPlan = await doctorService.GetMedicationPlanById(medicationPlan.Id);

            medicationPlan.MedicationMedicationPlans = new List<MedicationMedicationPlan>();

            foreach (var med in medicationPlanDto.Medication)
            {
                var id = Guid.NewGuid().ToString();
                medicationPlan.MedicationMedicationPlans.Add(new MedicationMedicationPlan
                {
                    Id = Guid.NewGuid().ToString(),
                    MedicationId = med.Id,
                    MedicationPlanId = medicationPlan.Id
                });
            }

            await doctorService.UpdateMedicationPlan(medicationPlan);

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("medication-plan/{patientId}")]
        public async Task<IActionResult> GetMedicationPlans(string patientId)
        {
            var medicationPlans = await doctorService.GetMedicationPlansByPatientId(patientId);
            var medicationPlansDto = new List<MedicationPlanDto>();

            foreach (var plan in medicationPlans)
            {
                var medicationPlanDto = new MedicationPlanDto
                {
                    Id = plan.Id,
                    IntakeIntervals = plan.IntakeIntervals,
                    EndDate = plan.EndDate,
                    StartDate = plan.StartDate,
                    PatientId = plan.PatientId,
                };

                var medications = plan.MedicationMedicationPlans.Select(m => m.Medication).ToList();
                var medicationsDto = new List<MedicationDto>();

                foreach (var m in medications)
                {
                    var mDto = new MedicationDto
                    {
                        Id = m.Id,
                        Dosage = m.Dosage,
                        Name = m.Name,
                        SideEffects = m.SideEffects
                    };
                    medicationPlanDto.Medication = medicationsDto;

                    medicationsDto.Add(mDto);
                }


                medicationPlansDto.Add(medicationPlanDto);
            };

            return Ok(medicationPlansDto);
        }

        [HttpPut("medications/update")]
        public async Task<IActionResult> UpdateMedication(Medication medicationToUpdate)
        {
            var medication = await doctorService.GetMedicationById(medicationToUpdate.Id);

            if (medication == null) return NotFound("The medication was not found!");

            medication.Name = medicationToUpdate.Name;
            medication.Dosage = medicationToUpdate.Dosage;
            medication.SideEffects = medicationToUpdate.SideEffects;

            await doctorService.UpdateMedication(medication);

            return Ok();
        }

        [HttpDelete("medications/delete/{id}")]
        public async Task<IActionResult> DeleteMedication(string id)
        {
            var medication = await doctorService.GetMedicationById(id);

            if (medication == null) return NotFound("The medication was not found!");

            await doctorService.DeleteMedication(medication);

            return Ok();
        }

        private async Task EnsureRolesAsync(string role)
        {
            var alreadyExists = await roleManager
                .RoleExistsAsync(role);

            if (alreadyExists) return;

            await roleManager.CreateAsync(
                new IdentityRole(role));
        }
    }
}
