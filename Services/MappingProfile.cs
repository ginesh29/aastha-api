using AASTHA2.Entities.Models;
using AASTHA2.Services.DTO;
using AutoMapper;

namespace AASTHA2.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientDTO>().ReverseMap();
            CreateMap<Opd, OpdDTO>().ReverseMap();
            CreateMap<Ipd, IpdDTO>().ReverseMap();
            CreateMap<Lookup, LookupDTO>().ReverseMap();
            CreateMap<Delivery, DeliveryDTO>().ReverseMap();
            CreateMap<Operation, OperationDTO>().ReverseMap();
            CreateMap<Charge, ChargeDTO>().ReverseMap();
            CreateMap<Appointment, AppointmentDTO>().ReverseMap();
            CreateMap<IpdLookup, IpdLookupDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
