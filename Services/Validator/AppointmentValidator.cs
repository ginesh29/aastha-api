using AASTHA2.DTO;
using FluentValidation;
using System;

namespace AASTHA2.Validator
{
    public class AppointmentValidator : AbstractValidator<AppointmentDTO>
    {
        public AppointmentValidator()
        {
            RuleFor(m => m.Date).NotEmpty().When(m => m.Id < 1);
            RuleFor(m => m.Type).NotEmpty().When(m => m.Id < 1)
                                .IsInEnum();
            RuleFor(m => m.PatientId).NotEmpty().When(m => m.Id < 1);
        }
    }
}
