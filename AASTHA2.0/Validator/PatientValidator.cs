using AASTHA2.DTO;
using FluentValidation;

namespace AASTHA2.Validator
{
    public class PatientValidator : AbstractValidator<PatientDTO>
    {
        public PatientValidator()
        {
            RuleFor(m => m.Firstname).NotEmpty();
            //RuleSet("all", () =>
            //{
            //    RuleFor(x => x.Id).Must(CheckId).WithMessage("id must greater than 0");
            //    RuleFor(x => x.Name).NotNull().When(x => !x.Id.HasValue).WithMessage("name could not be null");
            //});

            //RuleSet("id", () =>
            //{
            //    RuleFor(x => x.Id).NotNull().WithMessage("id could not be null")
            //             .GreaterThan(0).WithMessage("id must greater than 0");
            //});

            //RuleSet("name", () =>
            //{
            //    RuleFor(x => x.Name).NotNull().WithMessage("name could not be null");
            //});
        }

        private bool CheckId(int? id)
        {
            return !id.HasValue || id.Value > 0;
        }
    }
}
