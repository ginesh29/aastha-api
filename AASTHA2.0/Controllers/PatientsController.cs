using AASTHA2.DTO;
using AASTHA2.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;

namespace AASTHA2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PatientsController : ControllerBase
    {
        private static PatientService _patientService;
        public PatientsController(ServicesWrapper ServicesWrapper)
        {
            _patientService = ServicesWrapper.PatientService;
        }
        // GET: api/Patients
        [HttpGet]
        public dynamic GetPatients(string filter, string sortOrder, int skip, int take, string fields)
        {
            //Search = "Firstname-eq-{Ginesh1} or Lastname-eq-{Tandel1} or Middlename-eq-{Balkrushana1}";
            //Fields = "Firstname,Middlename,Lastname";
            //Sort = "Middlename desc,Firstname asc";
            //Skip = 0;
            //Take = 10;
            int totalCount;
            var data = _patientService.GetPatients(filter, sortOrder, true, out totalCount, skip, take, fields);
            var result = new { TotalCount = totalCount, Data = data.ToDynamicList() };
            return Ok(result);
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public ActionResult<PatientDTO> GetPatient(long id, string Search)
        {
            Search = "Firstname-eq-{Ginesh} or Lastname-eq-{Tandel1} or Middlename-eq-{Balkrushana1}";
            var patient = _patientService.GetPatient(id, Search, false);

            if (patient == null)
            {
                return NotFound();
            }
            return patient;
        }
        [HttpPost]
        public ActionResult<PatientDTO> PostPatient(PatientDTO patientDTO)
        {
            _patientService.PostPatient(patientDTO);
            return CreatedAtAction("GetPatient", new { id = patientDTO.Id }, patientDTO);
        }
        [HttpPut]
        public ActionResult<PatientDTO> PutPatient(PatientDTO patientDTO)
        {
            var patient = _patientService.GetPatient(patientDTO.Id);
            if (patient == null)
            {
                return NotFound();
            }
            _patientService.PutPatient(patientDTO);
            return CreatedAtAction("GetPatient", new { id = patient.Id }, patient);
        }
        [HttpDelete("{id}")]
        public ActionResult<PatientDTO> DeletePatient(long id, bool removePhysical = false)
        {
            var patient = _patientService.GetPatient(id);
            if (patient == null)
            {
                return NotFound();
            }
            _patientService.RemovePatient(patient, null, false, removePhysical);
            return patient;
        }
    }
}
