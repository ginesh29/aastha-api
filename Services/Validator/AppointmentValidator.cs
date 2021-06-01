using AASTHA2.DTO;
using AASTHA2.Services;
using FluentValidation;

namespace AASTHA2.Validator
{
    public class AppointmentValidator : AbstractValidator<AppointmentDTO>
    {
        public AppointmentValidator(ServicesWrapper ServicesWrapper)
        {
            RuleFor(m => m.date).NotEmpty().When(m => m.id < 1);
            RuleFor(m => m.type).NotEmpty().When(m => m.id < 1)
                                .IsInEnum();
            //RuleFor(m => m.patientId).NotNull().When(m => m.id < 1)
            //                         .SetValidator(new ValidPatientValidator(ServicesWrapper));
            //RuleFor(m => new {m.id, m.date, m.patientId }).SetValidator(new ExistAppointmentValidator(ServicesWrapper)).WithMessage("Appointment already exist.");
        }
    }
}
