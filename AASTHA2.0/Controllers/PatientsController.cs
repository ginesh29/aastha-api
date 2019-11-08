using AASTHA2.DTO;
using AASTHA2.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;

namespace AASTHA2._0.Controllers
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
        public ActionResult<IEnumerable<dynamic>> GetPatients(string Search, string Sort, int Skip, int Take, string Fields)
        {
            Search = "Firstname-eq-{Ginesh1} or Lastname-eq-{Tandel1} or Middlename-eq-{Balkrushana1}";
            Fields = "Firstname,Middlename,Lastname";
            Sort = "Middlename desc,Firstname asc";
            Skip = 0;
            Take = 10;
            var result = _patientService.GetPatients(Search, Sort, true, Skip, Take, Fields);
            return result.ToDynamicList();
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
        public ActionResult<PatientDTO> PostPatient(PatientDTO patient)
        {
            _patientService.PostPatient(patient);
            return CreatedAtAction("GetPatient", new { id = patient.Id }, patient);
        }
        [HttpPut]
        public ActionResult<PatientDTO> PutPatient(PatientDTO patient)
        {
            _patientService.PutPatient(patient);
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
