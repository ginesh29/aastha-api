//using AASTHA2.DTO;
//using AASTHA2.Services;
//using FluentValidation;
//using FluentValidation.Validators;

//namespace AASTHA2.Validator
//{
//    public class OpdValidator : AbstractValidator<OpdDTO>
//    {
//        public OpdValidator(ServicesWrapper ServicesWrapper)
//        {
//            RuleFor(m => m.caseType).NotEmpty().When(m => m.id < 1).WithMessage("Case Type is required")
//                                    .IsInEnum();
//            RuleFor(m => m.date).NotEmpty().When(m => m.id < 1).WithMessage("Opd Date is required");
//            RuleFor(m => m.patientId).NotNull().When(m => m.id < 1).WithMessage("Select Patient")
//                                       .SetValidator(new ValidPatientValidator(ServicesWrapper)).WithMessage("Select valid Patient");
//            RuleFor(m => new { m.id,m.patientId, m.date}).SetValidator(new ExistOpdValidator(ServicesWrapper)).WithMessage("Opd already exist.");
//        }        
//    }
//}
