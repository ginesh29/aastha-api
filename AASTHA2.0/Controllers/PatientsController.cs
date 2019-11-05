using AASTHA2.Common.Helpers;
using AASTHA2.Entities;
using AASTHA2.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace AASTHA2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PatientsController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        public PatientsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Patients
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        [HttpGet]
        public ActionResult<IEnumerable<dynamic>> GetPatients(string Search = "", string Fields = "", string Sort = "", int Skip = 0, int Take = 0)
        {
            Search = "Firstname.Contains({G})";// Firstname-eq-{Ginesh} or Lastname-eq-{Tandel1} or Middlename-eq-{Balkrushana1}";

            string query;
            object[] param;
            DynamicLinqHelper.DynamicSearchQuery(Search, out query, out param);
            Search = "Firstname = \"Ginesh\"";
            //Fields = "Firstname,Middlename,Lastname,CreaterInfo,CreaterInfo.Username";
            //Sort = "Middlename desc,Firstname asc";
            //Skip = 0;
            //Take = 10;

            IQueryable result = _unitOfWork.Patients.Find(m => !m.IsDeleted, Sort, Skip, Take);

            if (!string.IsNullOrEmpty(query))
                result = result.DynamicSearch(query, param);

            if (!string.IsNullOrEmpty(Fields))
                result = result.DynamicSelect(Fields);
            return result.ToDynamicList();
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
