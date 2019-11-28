using AASTHA2.DTO;
using FluentValidation;
using System;

namespace AASTHA2.Validator
{
    public class IpdValidator : AbstractValidator<IpdDTO>
    {
        public IpdValidator()
        {
            RuleFor(m => m.Type).IsInEnum();
            RuleFor(m => m.RoomType).IsInEnum();
            RuleFor(m => m.PatientId).NotEmpty().When(m => m.Id < 1);
        }
    }
}
