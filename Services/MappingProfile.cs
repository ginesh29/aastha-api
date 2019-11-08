using AASTHA2.DTO;
using AASTHA2.Entities;
using AutoMapper;

namespace AASTHA2.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientDTO>();
            CreateMap<PatientDTO, Patient>();
        }
    }
}
