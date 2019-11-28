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
            OpdService = new OpdService(_IUnitOfWork, _mapper);
            IpdService = new IpdService(_IUnitOfWork, _mapper);
            LookupService = new LookupService(_IUnitOfWork, _mapper);
        }
        public PatientService PatientService;
        public OpdService OpdService;
        public IpdService IpdService;
        public LookupService LookupService;
    }
}
