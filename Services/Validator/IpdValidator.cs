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

            RuleFor(m => m.type).NotEmpty().When(m => m.id < 1).WithMessage("Ipd Type is required")
                                .IsInEnum();
            RuleFor(m => m.roomType).NotEmpty().When(m => m.id < 1).WithMessage("Room Type is required")
                                    .IsInEnum();
            //RuleFor(m => m.patientId).NotEmpty().When(m => m.id < 1).WithMessage("Select Patient")
            //                         .SetValidator(new ValidPatientValidator(ServicesWrapper));
            //RuleFor(m =>new {m.id, m.uniqueId }).NotEmpty().When(m => m.id < 1).WithMessage("Select Invoice No.")
            //                         .SetValidator(new ExistUniqueIdValidator(ServicesWrapper)).WithMessage("Invoice No. already exist.");
            RuleFor(m => m.addmissionDate).NotEmpty().When(m => m.id < 1).WithMessage("Addmission Date is required");
            RuleFor(m => m.dischargeDate).NotEmpty().When(m => m.id < 1).WithMessage("Discharge Date is required");
            RuleFor(m => m.operationDetail).NotNull().When(m => m.id < 1 && m.type == IpdType.Operation)
                                           .SetValidator(new OperationDetailValidator()).When(m => m.id < 1 && m.type == IpdType.Operation);

            RuleFor(m => m.deliveryDetail).NotNull().When(m => m.id < 1 && m.type == IpdType.Delivery)
                                          .SetValidator(new DeliveryDetailValidator()).When(m => m.id < 1 && m.type == IpdType.Delivery);

            RuleFor(m => m.ipdLookups).NotEmpty().When(m => m.id < 1);
            RuleFor(m => m.charges).NotEmpty().When(m => m.id < 1).WithMessage("please enter charges detail");

            //RuleForEach(m => m.charges).Must(collection => collection.lookupId > 0).When(m => m.id < 1).WithMessage("Charges Details not valid")
            //                           .Must(collection => collection.lookupId > 0).SetValidator(new ValidLookupValidator(ServicesWrapper));

            //RuleForEach(m => m.ipdLookups).Must(collection => collection.lookupId > 0).When(m => m.id < 1).WithMessage("Lookups Details not valid")
            //                              .Must(collection => collection.lookupId > 0).SetValidator(new ValidLookupValidator(ServicesWrapper));
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

}
