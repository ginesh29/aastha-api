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
        public IEnumerable<dynamic> GetAppointments(string filter, out int totalCount, string sort="", int skip = 0, int take = 0, string includeProperties = "", string fields = "")
        {
            IEnumerable<Appointment> Appointment = _unitOfWork.Appointments.Find(null, out totalCount, filter, includeProperties, sort, skip, take);
            var mapped = _mapper.Map<IEnumerable<AppointmentDTO>>(Appointment);
            return mapped.DynamicSelect(fields).ToDynamicList();
        }
        public bool IsAppointmentExist(string filter = "")
        {
            return _unitOfWork.Appointments.FirstOrDefault(null, filter) != null;
        }
        public AppointmentDTO GetAppointment(long id, string filter = "", string includeProperties = "")
        {
            var Appointment = _unitOfWork.Appointments.FirstOrDefault(m => m.Id == id, filter, includeProperties);
            return _mapper.Map<AppointmentDTO>(Appointment);
        }
        public void PostAppointment(AppointmentDTO AppointmentDto)
        {
            var Appointment = _mapper.Map<Appointment>(AppointmentDto);
            _unitOfWork.Appointments.Create(Appointment);
            _unitOfWork.SaveChanges();
            AppointmentDto.id = Appointment.Id;
        }
        public void PutAppointment(AppointmentDTO AppointmentDto)
        {
            var Appointment = _unitOfWork.Appointments.FirstOrDefault(m => m.Id == AppointmentDto.id);
            Appointment = _mapper.Map<AppointmentDTO, Appointment>(AppointmentDto, Appointment);
            _unitOfWork.Appointments.Update(Appointment);
            _unitOfWork.SaveChanges();
        }
        public void RemoveAppointment(AppointmentDTO AppointmentDto, string filter = "", bool removePhysical = false)
        {
            var Appointment = _mapper.Map<Appointment>(AppointmentDto);
            _unitOfWork.Appointments.Delete(Appointment, removePhysical);
            _unitOfWork.SaveChanges();
        }
    }
}
