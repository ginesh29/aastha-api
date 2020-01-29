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
        public IEnumerable<dynamic> GetCharges(string filter, out int totalCount, string sort="", int skip = 0, int take = 0, string includeProperties = "", string fields = "")
        {
            IEnumerable<Charge> Charge = _unitOfWork.Charges.Find(null, out totalCount, filter, includeProperties, sort, skip, take);
            var mapped = _mapper.Map<IEnumerable<ChargeDTO>>(Charge);
            return mapped.DynamicSelect(fields).ToDynamicList();
        }
        //public bool IsChargeExist(long id, string filter = "", string includeProperties="")
        //{
        //    return _unitOfWork.Charges.IsExist( => m.Id == id, filter, includeProperties);
        //}
        public ChargeDTO GetCharge(long id, string filter = "", string includeProperties = "")
        {
            var Charge = _unitOfWork.Charges.FirstOrDefault(m => m.Id == id, filter, includeProperties);
            return _mapper.Map<ChargeDTO>(Charge);
        }
        public void PostCharge(ChargeDTO ChargeDto)
        {
            var Charge = _mapper.Map<Charge>(ChargeDto);
            _unitOfWork.Charges.Create(Charge);
            _unitOfWork.SaveChanges();
            ChargeDto.id = Charge.Id;
        }
        public void PutCharge(ChargeDTO ChargeDto)
        {
            var Charge = _unitOfWork.Charges.FirstOrDefault(m => m.Id == ChargeDto.id);
            Charge = _mapper.Map<ChargeDTO, Charge>(ChargeDto, Charge);
            _unitOfWork.Charges.Update(Charge);
            _unitOfWork.SaveChanges();
        }
        public void RemoveCharge(ChargeDTO Charge, string filter = "", bool removePhysical = false)
        {
            var ChargeDto = _unitOfWork.Charges.FirstOrDefault(m => m.Id == Charge.id, filter);
            _unitOfWork.Charges.Delete(ChargeDto, removePhysical);
            _unitOfWork.SaveChanges();
        }
    }
}
