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

            CreateMap<Opd, OpdDTO>();
            CreateMap<OpdDTO, Opd>();

            CreateMap<Ipd, IpdDTO>();
            CreateMap<IpdDTO, Ipd>();

            CreateMap<Lookup, LookupDTO>();
            CreateMap<LookupDTO, Lookup>();

            CreateMap<Delivery, DeliveryDTO>();
            CreateMap<DeliveryDTO, Delivery>();

            CreateMap<Operation, OperationDTO>();
            CreateMap<OperationDTO, Operation>();

            CreateMap<Charge, ChargeDTO>();
            CreateMap<ChargeDTO, Charge>();

            CreateMap<Appointment, AppointmentDTO>();
            CreateMap<AppointmentDTO, Appointment>();

            CreateMap<IpdLookup, IpdLookupDTO>();
            CreateMap<IpdLookupDTO, IpdLookup>();
        }
    }
}
