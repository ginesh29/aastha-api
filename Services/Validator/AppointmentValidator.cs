using AASTHA2.DTO;
using AASTHA2.Services;
using FluentValidation;
using System;

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
        }
    }
}
