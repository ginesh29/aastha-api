using AASTHA2.Common.Helpers;
using AASTHA2.DTO;
using AASTHA2.Entities;
using AASTHA2.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;

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
        public IEnumerable<dynamic> GetPatients(string filter, out int totalCount, string sort = "", int skip = 0, int take = 0, string includeProperties = "", string fields = "")
        {
            IEnumerable<Patient> patient = _unitOfWork.Patients
                .Find(m => !string.IsNullOrEmpty(m.Firstname)
                && !string.IsNullOrEmpty(m.Middlename)
                && !string.IsNullOrEmpty(m.Lastname), out totalCount, filter, includeProperties, sort, skip, take);
            var mapped = _mapper.Map<IEnumerable<PatientDTO>>(patient);
            return mapped.DynamicSelect(fields).ToDynamicList();
        }
        public IEnumerable<dynamic> GetPatientStatistics(int? Year = null)
        {
            return _unitOfWork.Patients.GetStatistics(Year);
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
            var patient = _unitOfWork.Patients.FirstOrDefault(m => m.Id == patientDto.id);
            patient = _mapper.Map<PatientDTO, Patient>(patientDto, patient);
            _unitOfWork.Patients.Update(patient);
            _unitOfWork.SaveChanges();
        }
        public void RemovePatient(PatientDTO patient, string filter = "", bool removePhysical = false)
        {
            var patientDto = _unitOfWork.Patients.FirstOrDefault(m => m.Id == patient.id);
            _unitOfWork.Patients.Delete(patientDto, removePhysical);
            _unitOfWork.SaveChanges();
        }
    }
}
