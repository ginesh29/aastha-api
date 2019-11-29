using AASTHA2.Common;
using AASTHA2.DTO;
using FluentValidation;

namespace AASTHA2.Validator
{
    public class IpdValidator : AbstractValidator<IpdDTO>
    {
        public IpdValidator()
        {
            RuleFor(m => m.Type).IsInEnum();
            RuleFor(m => m.RoomType).IsInEnum();
            RuleFor(m => m.PatientId).NotEmpty().When(m => m.Id < 1);
            RuleFor(m => m.AddmissionDate).NotEmpty().When(m => m.Id < 1);
            RuleFor(m => m.DischargeDate).NotEmpty().When(m => m.Id < 1);

            RuleFor(m => m.OperationDetail).NotEmpty().When(m => m.Id < 1 && m.Type == IpdType.Operation);
            RuleFor(m => m.OperationDetail.Date).NotEmpty().When(m => m.Id < 1);

            RuleFor(m => m.DeliveryDetail).NotEmpty().When(m => m.Id < 1 && m.Type == IpdType.Delivery);
            RuleFor(m => m.DeliveryDetail.Date).NotEmpty().When(m => m.Id < 1);
            RuleFor(m => m.DeliveryDetail.Gender).NotEmpty().When(m => m.Id < 1 && m.Type == IpdType.Delivery);
            RuleFor(m => m.DeliveryDetail.BabyWeight).NotEmpty().When(m => m.Id < 1 && m.Type == IpdType.Delivery);

            RuleForEach(m => m.Charges).Must(collection => collection.LookupId < 0).When(m => m.Id < 1);
            RuleForEach(m => m.IpdLookups).Must(collection => collection.LookupId < 0).When(m => m.Id < 1);
        }
    }
}
