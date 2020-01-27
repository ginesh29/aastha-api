using AASTHA2.DTO;
using AASTHA2.Services;
using FluentValidation;

namespace AASTHA2.Validator
{
    public class AppointmentValidator : AbstractValidator<AppointmentDTO>
    {
        public AppointmentValidator(ServicesWrapper ServicesWrapper)
        {
            RuleFor(m => m.Date).NotEmpty().When(m => m.Id < 1);
            RuleFor(m => m.Type).NotEmpty().When(m => m.Id < 1)
                                .IsInEnum();
            RuleFor(m => m.PatientId).NotNull().When(m => m.Id < 1)
                                     .SetValidator(new ValidPatientValidator(ServicesWrapper));
            RuleFor(m => new { m.Date, m.PatientId }).SetValidator(new ExistAppointmentValidator(ServicesWrapper)).WithMessage("Appointment already exist.");
        }
    }
}
