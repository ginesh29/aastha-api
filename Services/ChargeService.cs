using AASTHA2.Common;
using AASTHA2.Common.Helpers;
using AASTHA2.Entities.Models;
using AASTHA2.Repositories.Interfaces;
using AASTHA2.Services.DTO;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

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
            var charges = _unitOfWork.Charges.Find(null, filterModel.filter, filterModel.includeProperties, filterModel.sort);
            var totalCount = charges.Count();
            var paged = charges.ToPageList(filterModel.skip, filterModel.take);
            var mapped = _mapper.Map<List<ChargeDTO>>(paged).AsQueryable();
            return new PaginationModel
            {
                Data = mapped,
                StartPage = totalCount > 0 ? filterModel.skip + 1 : 0,
                EndPage = totalCount > filterModel.take ? filterModel.skip + filterModel.take : totalCount,
                TotalCount = charges.Count()
            };
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
            ChargeDto.Id = Charge.Id;
        }
        public void PutCharge(ChargeDTO ChargeDto)
        {
            var Charge = _mapper.Map<ChargeDTO, Charge>(ChargeDto);
            _unitOfWork.Charges.Update(Charge);
            _unitOfWork.SaveChanges();
        }
        public void RemoveCharge(ChargeDTO ChargeDto, bool removePhysical = false)
        {
            var Charge = _mapper.Map<Charge>(ChargeDto);
            _unitOfWork.Charges.Delete(Charge, removePhysical);
            _unitOfWork.SaveChanges();
        }
    }
}
