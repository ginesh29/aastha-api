using AASTHA2.Common.Helpers;
using AASTHA2.DTO;
using AASTHA2.Entities;
using AASTHA2.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;

namespace AASTHA2.Services
{
    public class AppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<dynamic> GetAppointments(string Search, string Sort, bool ShowDeleted, out int totalCount, int Skip, int Take, string Fields)
        {
            IEnumerable<Appointment> Appointment = _unitOfWork.Appointments.Find(null, Search, ShowDeleted, out totalCount, Sort, Skip, Take, m => m.Patient);
            var mapped = _mapper.Map<IEnumerable<AppointmentDTO>>(Appointment);
            return mapped.DynamicSelect(Fields).ToDynamicList();
        }
        //public bool IsAppointmentExist(long Id, string Search = "", bool ShowDeleted = false)
        //{
        //    return _unitOfWork.Appointments.IsExist(m => m.Id == Id, Search, ShowDeleted);
        //}
        public AppointmentDTO GetAppointment(long Id, string Search = "", bool ShowDeleted = false)
        {
            var Appointment = _unitOfWork.Appointments.FirstOrDefault(m => m.Id == Id, Search, ShowDeleted);
            return _mapper.Map<AppointmentDTO>(Appointment);
        }
        //public int AppointmentCount(string Search = "", bool ShowDeleted = false)
        //{
        //    return _unitOfWork.Appointments.Count(null, Search, ShowDeleted);
        //}
        public void PostAppointment(AppointmentDTO AppointmentDto)
        {
            var Appointment = _mapper.Map<Appointment>(AppointmentDto);
            _unitOfWork.Appointments.Create(Appointment);
            _unitOfWork.SaveChanges();
            AppointmentDto.Id = Appointment.Id;
        }
        public void PutAppointment(AppointmentDTO AppointmentDto)
        {
            var Appointment = _unitOfWork.Appointments.FirstOrDefault(m => m.Id == AppointmentDto.Id);
            Appointment = _mapper.Map<AppointmentDTO, Appointment>(AppointmentDto, Appointment);
            _unitOfWork.Appointments.Update(Appointment);
            _unitOfWork.SaveChanges();
        }
        public void RemoveAppointment(AppointmentDTO Appointment, string Search = "", bool ShowDeleted = false, bool RemovePhysical = false)
        {
            var AppointmentDto = _unitOfWork.Appointments.FirstOrDefault(m => m.Id == Appointment.Id, Search, ShowDeleted);
            _unitOfWork.Appointments.Delete(AppointmentDto, RemovePhysical);
            _unitOfWork.SaveChanges();
        }
    }
}
