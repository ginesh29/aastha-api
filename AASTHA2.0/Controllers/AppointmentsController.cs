using AASTHA2.DTO;
using AASTHA2.Models;
using AASTHA2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;

namespace AASTHA2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AppointmentsController : ControllerBase
    {
        private static AppointmentService _AppointmentService;
        public AppointmentsController(ServicesWrapper ServicesWrapper)
        {
            _AppointmentService = ServicesWrapper.AppointmentService;
        }
        // GET: api/Appointments
        [HttpGet]
        public ActionResult GetAppointments([FromQuery]FilterModel filterModel)
        {
            var result = _AppointmentService.GetAppointments(filterModel);
            return Ok(result);
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public ActionResult<AppointmentDTO> GetAppointment(long id, string filter)
        {
            var Appointment = _AppointmentService.GetAppointment(id, filter);

            if (Appointment == null)
            {
                return NotFound();
            }
            return Appointment;
        }
        [HttpPost]
        public ActionResult<AppointmentDTO> PostAppointment(AppointmentDTO AppointmentDTO, string includeProperties = "")
        {
            _AppointmentService.PostAppointment(AppointmentDTO);
            var Appointment = _AppointmentService.GetAppointment(AppointmentDTO.id, null, includeProperties);
            return CreatedAtAction("GetAppointment", new { id = AppointmentDTO.id }, Appointment);
        }
        [HttpPut]
        public ActionResult<AppointmentDTO> PutAppointment(AppointmentDTO AppointmentDTO, string includeProperties = "")
        {
            var Appointment = _AppointmentService.GetAppointment(AppointmentDTO.id);
            if (Appointment == null)
            {
                return NotFound();
            }
            _AppointmentService.PutAppointment(AppointmentDTO);
            Appointment = _AppointmentService.GetAppointment(AppointmentDTO.id, null, includeProperties);
            return CreatedAtAction("GetAppointment", new { id = AppointmentDTO.id }, Appointment);
        }
        [HttpDelete("{id}")]
        public ActionResult<AppointmentDTO> DeleteAppointment(long id, bool isDeleted, bool removePhysical = false)
        {
            var Appointment = _AppointmentService.GetAppointment(id);
            Appointment.isDeleted = isDeleted;
            if (Appointment == null)
            {
                return NotFound();
            }
            _AppointmentService.RemoveAppointment(Appointment, "");
            return CreatedAtAction("GetAppointment", new { id = id }, Appointment);
        }
    }
}
