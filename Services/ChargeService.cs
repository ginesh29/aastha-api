using AASTHA2.Common;
using AASTHA2.Common.Helpers;
using AASTHA2.DTO;
using AASTHA2.Entities;
using AASTHA2.Interfaces;
using AASTHA2.Models;
using AutoMapper;
using System.Collections.Generic;

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
        public PaginationModel GetCharges(FilterModel filterModel)
        {
            IEnumerable<Charge> Charge = _unitOfWork.Charges.Find(null, filterModel.filter, filterModel.includeProperties, filterModel.sort);
            return _mapper.Map<IEnumerable<ChargeDTO>>(Charge).ToPageList(filterModel.skip, filterModel.take);
        }
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
            var Charge = _mapper.Map<ChargeDTO, Charge>(ChargeDto);
            _unitOfWork.Charges.Update(Charge);
            _unitOfWork.SaveChanges();
        }
        public void RemoveCharge(ChargeDTO ChargeDto, string filter = "", bool removePhysical = false)
        {
            var Charge = _mapper.Map<Charge>(ChargeDto);
            _unitOfWork.Charges.Delete(Charge, removePhysical);
            _unitOfWork.SaveChanges();
        }
    }
}
