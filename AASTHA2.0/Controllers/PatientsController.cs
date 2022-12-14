using AASTHA2.Common;
using AASTHA2.Services;
using AASTHA2.Services.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AASTHA2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientsController : ControllerBase
    {
        private static PatientService _patientService;
        public PatientsController(ServicesWrapper ServicesWrapper)
        {
            _patientService = ServicesWrapper.PatientService;
        }
        // GET: api/Patients
        [HttpGet]
        public ActionResult GetPatients([FromQuery] FilterModel filterModel)
        {
            var data = _patientService.GetPatients(filterModel);
            return Ok(data);
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public ActionResult<PatientDTO> GetPatient(long id, string filter)
        {
            var patient = _patientService.GetPatient(id, filter);
            if (patient == null)
            {
                return NotFound();
            }
            return patient;
        }
        [HttpPost]
        public ActionResult<PatientDTO> PostPatient(PatientDTO patientDTO, string includeProperties = "")
        {
            _patientService.PostPatient(patientDTO);
            var patient = _patientService.GetPatient(patientDTO.id, null, includeProperties);
            return CreatedAtAction("GetPatient", new { patientDTO.id }, patient);
        }
        [HttpPut]
        public ActionResult<PatientDTO> PutPatient(PatientDTO patientDTO, string includeProperties = "")
        {
            var patient = _patientService.GetPatient(patientDTO.id);
            if (patient == null)
            {
                return NotFound();
            }
            _patientService.PutPatient(patientDTO);
            patient = _patientService.GetPatient(patientDTO.id, null, includeProperties);
            return CreatedAtAction("GetPatient", new { patientDTO.id }, patient);
        }
        [HttpDelete("{id}")]
        public ActionResult<PatientDTO> DeletePatient(long id, bool isDeleted, bool removePhysical = false)
        {
            var patient = _patientService.GetPatient(id, "");
            patient.isDeleted = isDeleted;
            if (patient == null)
            {
                return NotFound();
            }
            _patientService.RemovePatient(patient, removePhysical);
            return CreatedAtAction("GetPatient", new { id }, patient);
        }
    }
}
