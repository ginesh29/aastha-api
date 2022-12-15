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
            RuleFor(m => m.type).NotEmpty().When(m => m.id < 1).WithMessage("Ipd Type is required").IsInEnum();
            RuleFor(m => m.roomType).NotEmpty().When(m => m.id < 1).WithMessage("Room Type is required").IsInEnum();
            RuleFor(m => m.patientId).NotEmpty().When(m => m.id < 1).WithMessage("Select Patient")
            .Must((ipd, cancellation) =>
            {
                return _patientService.IsPatientExist($"Id-eq-{{{ipd.patientId}}}");
            }).WithMessage("Patient not valid.");
            RuleFor(m => new { m.id, m.uniqueId }).NotEmpty().When(m => m.id < 1).WithMessage("Select Invoice No.")
            .Must((ipd, cancellation) =>
            {
                string filter = $"Id-neq-{{{ipd.id}}} and UniqueId-eq-{{{ipd.uniqueId}}} and isDeleted-neq-{{{true}}}";
                return !_ipdService.IsIpdExist(filter);
            }).WithMessage("Invoice No. already exist.");
            RuleFor(m => m.addmissionDate).NotEmpty().When(m => m.id < 1).WithMessage("Addmission Date is required");
            RuleFor(m => m.dischargeDate).NotEmpty().When(m => m.id < 1).WithMessage("Discharge Date is required");
            RuleFor(m => m.operationDetail).NotNull().When(m => m.id < 1 && m.type == IpdType.Operation)
            .SetValidator(new OperationDetailValidator()).When(m => m.id < 1 && m.type == IpdType.Operation);

            RuleFor(m => m.deliveryDetail).NotNull().When(m => m.id < 1 && m.type == IpdType.Delivery)
            .SetValidator(new DeliveryDetailValidator()).When(m => m.id < 1 && m.type == IpdType.Delivery);

            RuleFor(m => m.ipdLookups).NotEmpty().When(m => m.id < 1);
            RuleFor(m => m.charges).NotEmpty().When(m => m.id < 1).WithMessage("please enter charges detail");

            RuleForEach(m => m.charges).Must(collection => collection.lookupId > 0).When(m => m.id < 1).WithMessage("Charges Details not valid");
            RuleForEach(m => m.ipdLookups).Must(collection => collection.lookupId > 0).When(m => m.id < 1).WithMessage("Lookups Details not valid");
        }
    }
    public class OperationDetailValidator : AbstractValidator<OperationDTO>
    {
        public OperationDetailValidator()
        {
            RuleFor(x => x.date).NotEmpty();
        }
    }
    public class DeliveryDetailValidator : AbstractValidator<DeliveryDTO>
    {
        public DeliveryDetailValidator()
        {
            RuleFor(x => x.date).NotEmpty();
            RuleFor(x => x.time).NotEmpty()
                                .Must(ValidateHelper.IsValidTime).WithMessage("Invalid Delivery Time");
            RuleFor(x => x.gender).NotEmpty();
            RuleFor(x => x.babyWeight).NotEmpty();
        }


    }
    public class LookupCheckValidator : AbstractValidator<LookupDTO>
    {
        private static LookupService _lookupService;
        public LookupCheckValidator(ServicesWrapper ServicesWrapper)
        {
            _lookupService = ServicesWrapper.LookupService;
            RuleFor(m => m.id).Must((ipd, cancellation) =>
            {
                return _lookupService.IsLookupExist($"Id-eq-{{{ipd.id}}}");
            }).WithMessage("Lookup not valid.");
        }
    }
}
