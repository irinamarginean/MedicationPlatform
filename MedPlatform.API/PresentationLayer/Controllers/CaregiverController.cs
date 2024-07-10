using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjectLayer;
using BusinessObjectLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedicationPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Constants.CaregiverRole)]
    public class CaregiverController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMapper mapper;

        public CaregiverController(UserManager<IdentityUser> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentCaregiver()
        {
            var handler = new JwtSecurityTokenHandler();
            string authHeader = Request.Headers["Authorization"];
            authHeader = authHeader.Replace("Bearer ", "");
            var jsonToken = handler.ReadToken(authHeader);

            if (!(jsonToken is JwtSecurityToken jwtSecurityToken))
            {
                return BadRequest();
            }

            var email = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            BaseUserEntity user = userManager.Users.SingleOrDefault(r => r.Email == email) as BaseUserEntity;

            //UserUpdateDto userDto = mapper.Map<UserUpdateDto>(user);

            return Ok(user);
        }

        [HttpGet("patients/all/{caregiverId}")]
        public async Task<IActionResult> GetAllPatients(string caregiverId)
        {
            var patients = await userManager.GetUsersInRoleAsync(Constants.PatientRole);
            IList<PatientEntity> patientEntities = new List<PatientEntity>();

            foreach (var patient in patients)
            {
                if (patient is PatientEntity patientEntity && patientEntity.CaregiverId == caregiverId)
                {
                    patientEntities.Add(patientEntity);
                }
            }

            if (patientEntities.Count > 0) return Ok(patientEntities);

            return Ok();
        }
    }
}
