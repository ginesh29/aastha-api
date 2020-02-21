﻿using AASTHA2.Common.Helpers;
using AASTHA2.DTO;
using AASTHA2.Entities;
using AASTHA2.Interfaces;
using AASTHA2.Models;
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
        public dynamic GetAppointments(FilterModel filterModel)
        {
            IEnumerable<Appointment> Appointment = _unitOfWork.Appointments.Find(null, filterModel.filter, filterModel.includeProperties, filterModel.sort, filterModel.skip, filterModel.take);
            return _mapper.Map<IEnumerable<AppointmentDTO>>(Appointment).ToPageList(filterModel.skip, filterModel.take);

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
            var Appointment = _mapper.Map<AppointmentDTO, Appointment>(AppointmentDto);
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
