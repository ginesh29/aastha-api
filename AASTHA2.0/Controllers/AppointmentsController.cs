﻿using AASTHA2.DTO;
using AASTHA2.Services;
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
        public dynamic GetAppointments(string filter, string sortOrder, int skip, int take=15, string fields="")
        {
            //Search = "Firstname-eq-{Ginesh1} or Lastname-eq-{Tandel1} or Middlename-eq-{Balkrushana1}";
            //Fields = "Firstname,Middlename,Lastname";
            //Sort = "Middlename desc,Firstname asc";
            //Skip = 0;
            //Take = 10;
            int totalCount;
            var data = _AppointmentService.GetAppointments(filter, sortOrder, true, out totalCount, skip, take, fields);
            var result = new { TotalCount = totalCount, Data = data.ToDynamicList() };
            return Ok(result);
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public ActionResult<AppointmentDTO> GetAppointment(long id, string Search)
        {
            //Search = "Firstname-eq-{Ginesh} or Lastname-eq-{Tandel1} or Middlename-eq-{Balkrushana1}";
            var Appointment = _AppointmentService.GetAppointment(id, Search, false);

            if (Appointment == null)
            {
                return NotFound();
            }
            return Appointment;
        }
        [HttpPost]
        public ActionResult<AppointmentDTO> PostAppointment(AppointmentDTO AppointmentDTO)
        {
            _AppointmentService.PostAppointment(AppointmentDTO);
            return CreatedAtAction("GetAppointment", new { id = AppointmentDTO.Id }, AppointmentDTO);
        }
        [HttpPut]
        public ActionResult<AppointmentDTO> PutAppointment(AppointmentDTO AppointmentDTO)
        {
            var Appointment = _AppointmentService.GetAppointment(AppointmentDTO.Id);
            if (Appointment == null)
            {
                return NotFound();
            }
            _AppointmentService.PutAppointment(AppointmentDTO);
            Appointment = _AppointmentService.GetAppointment(AppointmentDTO.Id);
            return Appointment;
        }
        [HttpDelete("{id}")]
        public ActionResult<AppointmentDTO> DeleteAppointment(long id, bool removePhysical = false)
        {
            var Appointment = _AppointmentService.GetAppointment(id);
            if (Appointment == null)
            {
                return NotFound();
            }
            _AppointmentService.RemoveAppointment(Appointment, null, false, removePhysical);
            return Appointment;
        }
    }
}