using AASTHA2.Common.Helpers;
using AASTHA2.DTO;
using AASTHA2.Entities;
using AASTHA2.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;

namespace AASTHA2.Services
{
    public class ChargeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ChargeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<dynamic> GetCharges(string Search, string Sort, bool ShowDeleted, out int totalCount, int Skip, int Take, string Fields)
        {
            IEnumerable<Charge> Charge = _unitOfWork.Charges.Find(null, Search, ShowDeleted, out totalCount, Sort, Skip, Take);
            var mapped = _mapper.Map<IEnumerable<ChargeDTO>>(Charge);
            return mapped.DynamicSelect(Fields).ToDynamicList();
        }
        //public bool IsChargeExist(long Id, string Search = "", bool ShowDeleted = false)
        //{
        //    return _unitOfWork.Charges.IsExist(m => m.Id == Id, Search, ShowDeleted);
        //}
        public ChargeDTO GetCharge(long Id, string Search = "", bool ShowDeleted = false)
        {
            var Charge = _unitOfWork.Charges.FirstOrDefault(m => m.Id == Id, Search, ShowDeleted);
            return _mapper.Map<ChargeDTO>(Charge);
        }
        public void PostCharge(ChargeDTO ChargeDto)
        {
            var Charge = _mapper.Map<Charge>(ChargeDto);
            _unitOfWork.Charges.Create(Charge);

            _unitOfWork.SaveChanges();
            ChargeDto.Id = Charge.Id;
        }
        public void PutCharge(ChargeDTO ChargeDto)
        {
            var Charge = _unitOfWork.Charges.FirstOrDefault(m => m.Id == ChargeDto.Id);
            Charge = _mapper.Map<ChargeDTO, Charge>(ChargeDto, Charge);
            _unitOfWork.Charges.Update(Charge);
            _unitOfWork.SaveChanges();
        }
        public void RemoveCharge(ChargeDTO Charge, string Search = "", bool ShowDeleted = false, bool RemovePhysical = false)
        {
            var ChargeDto = _unitOfWork.Charges.FirstOrDefault(m => m.Id == Charge.Id, Search, ShowDeleted);
            _unitOfWork.Charges.Delete(ChargeDto, RemovePhysical);
            _unitOfWork.SaveChanges();
        }
    }
}
