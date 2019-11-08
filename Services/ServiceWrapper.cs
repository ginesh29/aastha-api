using AASTHA2.Interfaces;
using AutoMapper;

namespace AASTHA2.Services
{
    public class ServicesWrapper
    {
        private IUnitOfWork _IUnitOfWork;
        private readonly IMapper _mapper;
        public ServicesWrapper(IUnitOfWork IUnitOfWork, IMapper mapper)
        {
            _IUnitOfWork = IUnitOfWork;
            _mapper = mapper;
            PatientService = new PatientService(_IUnitOfWork, _mapper);
        }
        public PatientService PatientService;
    }
}
