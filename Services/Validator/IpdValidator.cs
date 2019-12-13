using AASTHA2.Common;
using AASTHA2.Common.Helpers;
using AASTHA2.DTO;
using AASTHA2.Services;
using FluentValidation;
using System;

namespace AASTHA2.Validator
{
    public class IpdValidator : AbstractValidator<IpdDTO>
    {
        public IpdValidator(ServicesWrapper ServicesWrapper)
        {
            RuleFor(m => m.Type).NotEmpty().When(m => m.Id < 1)
                                .IsInEnum();
            RuleFor(m => m.RoomType).NotEmpty().When(m => m.Id < 1)
                                    .IsInEnum();
            RuleFor(m => m.PatientId).NotEmpty().When(m => m.Id < 1)
                                     .SetValidator(new ValidPatientValidator(ServicesWrapper));

            RuleFor(m => m.AddmissionDate).NotEmpty().When(m => m.Id < 1);
            RuleFor(m => m.DischargeDate).NotEmpty().When(m => m.Id < 1);
            RuleFor(m => m.OperationDetail).NotNull().When(m => m.Id < 1 && m.Type == IpdType.Operation)
                                           .SetValidator(new OperationDetailValidator()).When(m => m.Id < 1 && m.Type == IpdType.Operation);
            RuleFor(m => m.DeliveryDetail).NotNull().When(m => m.Id < 1 && m.Type == IpdType.Delivery)
                                          .SetValidator(new DeliveryDetailValidator()).When(m => m.Id < 1 && m.Type == IpdType.Delivery);
            RuleForEach(m => m.Charges).Must(collection => collection.LookupId > 0).When(m => m.Id < 1).WithMessage("Charges Details not valid")
                                       .Must(collection => collection.LookupId > 0).SetValidator(new ValidLookupValidator(ServicesWrapper));
            RuleForEach(m => m.IpdLookups).Must(collection => collection.LookupId > 0).When(m => m.Id < 1).WithMessage("Lookups Details not valid")
                                          .Must(collection => collection.LookupId > 0).SetValidator(new ValidLookupValidator(ServicesWrapper));
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

}
