﻿using AASTHA2.DTO;
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
        public dynamic GetPatients(string filter, string sort, int skip, int take, bool isDeleted, string includeProperties = "", string fields = "")
        {
            int totalCount;
            var data = _patientService.GetPatients(filter, out totalCount, sort, skip, take, includeProperties, fields);
            var result = new { TotalCount = totalCount, Data = data.ToDynamicList() };
            return Ok(result);
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
            return CreatedAtAction("GetPatient", new { id = patientDTO.id }, patient);
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
            patient = _patientService.GetPatient(patientDTO.id,null,includeProperties);
            return CreatedAtAction("GetPatient", new { id = patientDTO.id }, patient);
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
            _patientService.RemovePatient(patient, "", removePhysical);
            return CreatedAtAction("GetPatient", new { id = id }, patient);
        }       
    }
}
