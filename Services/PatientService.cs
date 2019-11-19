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
        public IEnumerable<dynamic> GetPatients(string Search, string Sort, bool ShowDeleted, int Skip, int Take, string Fields)
        {
            IEnumerable<Patient> patient = _unitOfWork.Patients.Find(null, Search, ShowDeleted, Sort, Skip, Take);
            var mapped = _mapper.Map<IEnumerable<PatientDTO>>(patient);
            return mapped.DynamicSelect(Fields).ToDynamicList();
        }
        public bool IsPatientExist(long Id, string Search = "", bool ShowDeleted = false)
        {
            return _unitOfWork.Patients.IsExist(m => m.Id == Id, Search, ShowDeleted);
        }
        public PatientDTO GetPatient(long Id, string Search = "", bool ShowDeleted = false)
        {
            var patient = _unitOfWork.Patients.FirstOrDefault(m => m.Id == Id, Search, ShowDeleted);
            return _mapper.Map<PatientDTO>(patient);
        }
        public int PatientCount(string Search = "", bool ShowDeleted = false)
        {
            return _unitOfWork.Patients.Count(null, Search, ShowDeleted);
        }
        public void PostPatient(PatientDTO patientDto)
        {
            var patient = _mapper.Map<Patient>(patientDto);
            _unitOfWork.Patients.Create(patient);
            _unitOfWork.SaveChanges();
        }
        public void PutPatient(PatientDTO patientDto)
        {
            var patient = _unitOfWork.Patients.FirstOrDefault(m => m.Id == patientDto.Id);
            patient = _mapper.Map<PatientDTO, Patient>(patientDto, patient);
            _unitOfWork.Patients.Update(patient);
            _unitOfWork.SaveChanges();
        }
        public void RemovePatient(PatientDTO patient, string Search = "", bool ShowDeleted = false, bool RemovePhysical = false)
        {
            var patientDto = _unitOfWork.Patients.FirstOrDefault(m => m.Id == patient.Id, Search, ShowDeleted);
            _unitOfWork.Patients.Delete(patientDto, RemovePhysical);
            _unitOfWork.SaveChanges();
        }
    }
}
