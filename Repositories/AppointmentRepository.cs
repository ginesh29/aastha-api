using AASTHA2.Entities;
using AASTHA2.Interfaces;

namespace AASTHA2.Repositories
{
    public class AppointmentRepository : RepositoryBase<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(AASTHA2Context AASTHA2Context)
            : base(AASTHA2Context)
        {
        }
    }
}
