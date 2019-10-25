using AASTHA2.Entities;
using AASTHA2.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AASTHA2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        public PatientsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Patients
        [HttpGet]
        public ActionResult<IEnumerable<Patient>> GetPatients()
        {
            return _unitOfWork.Patients.Find(m => !m.IsDeleted).ToList();
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public ActionResult<Patient> GetPatient(long id)
        {
            var patient = _unitOfWork.Patients.FirstOrDefault(m => m.Id == id && !m.IsDeleted);

            if (patient == null)
            {
                return NotFound();
            }
            return patient;
        }

        // PUT: api/Patients/5
        [HttpPut("{id}")]
        public ActionResult<Patient> PutPatient(long id, Patient patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
                _unitOfWork.Patients.Update(patient);
            else
                return BadRequest();

            try
            {
                _unitOfWork.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Patients
        [HttpPost]
        public ActionResult<Patient> PostPatient(Patient patient)
        {
            _unitOfWork.Patients.Create(patient);
            _unitOfWork.SaveChanges();
            return CreatedAtAction("GetPatient", new { id = patient.Id }, patient);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public ActionResult<Patient> DeletePatient(long id)
        {
            var patient = _unitOfWork.Patients.FirstOrDefault(m => m.Id == id && !m.IsDeleted);
            if (patient == null)
            {
                return NotFound();
            }
            _unitOfWork.Patients.Delete(patient);
            _unitOfWork.SaveChanges();

            return patient;
        }
        private bool PatientExists(long id)
        {
            return _unitOfWork.Patients.IsExist(m => m.Id == id && !m.IsDeleted);
        }
    }
}
