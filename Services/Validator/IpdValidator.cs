using AASTHA2.Common;
using AASTHA2.Common.Helpers;
using AASTHA2.Services;
using AASTHA2.Services.DTO;
using FluentValidation;

namespace AASTHA2.Validator
{
    public class IpdValidator : AbstractValidator<IpdDTO>
    {
        private static PatientService _patientService;
        private static IpdService _ipdService;
        public IpdValidator(ServicesWrapper ServicesWrapper)
        {
            _patientService = ServicesWrapper.PatientService;
            _ipdService = ServicesWrapper.IpdService;
            RuleFor(m => m.Type).NotEmpty().When(m => m.Id < 1).WithMessage("Ipd Type is required").IsInEnum();
            RuleFor(m => m.RoomType).NotEmpty().When(m => m.Id < 1).WithMessage("Room Type is required").IsInEnum();
            RuleFor(m => m.PatientId).NotEmpty().When(m => m.Id < 1).WithMessage("Select Patient")
            .Must((ipd, cancellation) =>
            {
                return _patientService.IsPatientExist($"Id-eq-{{{ipd.PatientId}}}");
            }).WithMessage("Patient not valid.");
            RuleFor(m => new { m.Id, m.UniqueId }).NotEmpty().When(m => m.Id < 1).WithMessage("Select Invoice No.")
            .Must((ipd, cancellation) =>
            {
                string filter = $"Id-neq-{{{ipd.Id}}} and UniqueId-eq-{{{ipd.UniqueId}}} and isDeleted-neq-{{{true}}}";
                return !_ipdService.IsIpdExist(filter);
            }).WithMessage("Invoice No. already exist.");
            RuleFor(m => m.AddmissionDate).NotEmpty().When(m => m.Id < 1).WithMessage("Addmission Date is required");
            RuleFor(m => m.OperationDetail).NotNull().When(m => m.Id < 1 && m.Type == IpdType.Operation)
            .SetValidator(new OperationDetailValidator()).When(m => m.Id < 1 && m.Type == IpdType.Operation);

            RuleFor(m => m.DeliveryDetail).NotNull().When(m => m.Id < 1 && m.Type == IpdType.Delivery)
            .SetValidator(new DeliveryDetailValidator()).When(m => m.Id < 1 && m.Type == IpdType.Delivery);

            RuleFor(m => m.IpdLookups).NotEmpty().When(m => m.Id < 1);
            RuleFor(m => m.Charges).NotEmpty().When(m => m.Id < 1).WithMessage("please enter charges detail");

            RuleForEach(m => m.Charges).Must(collection => collection.LookupId > 0).When(m => m.Id < 1).WithMessage("Charges Details not valid");
            RuleForEach(m => m.IpdLookups).Must(collection => collection.LookupId > 0).When(m => m.Id < 1).WithMessage("Lookups Details not valid");
        }
    }
    public class OperationDetailValidator : AbstractValidator<OperationDTO>
    {
        public OperationDetailValidator()
        {
            RuleFor(x => x.Date).NotEmpty();
        }
    }
    public class DeliveryDetailValidator : AbstractValidator<DeliveryDTO>
    {
        public DeliveryDetailValidator()
        {
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Time).NotEmpty()
                                .Must(ValidateHelper.IsValidTime).WithMessage("Invalid Delivery Time");
            RuleFor(x => x.Gender).NotEmpty();
            RuleFor(x => x.BabyWeight).NotEmpty();
        }


    }
    public class LookupCheckValidator : AbstractValidator<LookupDTO>
    {
        private static LookupService _lookupService;
        public LookupCheckValidator(ServicesWrapper ServicesWrapper)
        {
            _lookupService = ServicesWrapper.LookupService;
            RuleFor(m => m.Id).Must((ipd, cancellation) =>
            {
                return _lookupService.IsLookupExist($"Id-eq-{{{ipd.Id}}}");
            }).WithMessage("Lookup not valid.");
        }
    }
}
