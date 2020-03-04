﻿using AASTHA2.Common;
using AASTHA2.Common.Helpers;
using AASTHA2.DTO;
using AASTHA2.Entities;
using AASTHA2.Interfaces;
using AASTHA2.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace AASTHA2.Services
{
    public class PatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PatientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public PaginationModel GetPatients(FilterModel filterModel)
        {
            IEnumerable<Patient> patient = _unitOfWork.Patients.Find(null, filterModel.filter, filterModel.includeProperties, filterModel.sort);
            return _mapper.Map<IEnumerable<PatientDTO>>(patient).ToPageList(filterModel.skip, filterModel.take);
        }
        public IQueryable GetPatientStatistics(int? Year = null)
        {
            return _unitOfWork.Patients.GetStatistics(Year).AsQueryable();
        }
        public bool IsPatientExist(string filter = "")
        {
            return _unitOfWork.Patients.FirstOrDefault(null, filter) != null;
        }
        public PatientDTO GetPatient(long id, string filter = "", string includeProperties = "")
        {
            var patient = _unitOfWork.Patients.FirstOrDefault(m => m.Id == id, filter, includeProperties);
            return _mapper.Map<PatientDTO>(patient);
        }

        public void PostPatient(PatientDTO patientDto)
        {
            var patient = _mapper.Map<Patient>(patientDto);
            _unitOfWork.Patients.Create(patient);
            _unitOfWork.SaveChanges();
            patientDto.id = patient.Id;
        }
        public void PutPatient(PatientDTO patientDto)
        {
            var patient = _mapper.Map<PatientDTO, Patient>(patientDto);
            _unitOfWork.Patients.Update(patient);
            _unitOfWork.SaveChanges();
        }
        public void RemovePatient(PatientDTO patientDto, string filter = "", bool removePhysical = false)
        {
            var patient = _mapper.Map<Patient>(patientDto);
            _unitOfWork.Patients.Delete(patient, removePhysical);
            _unitOfWork.SaveChanges();
        }
    }
}
