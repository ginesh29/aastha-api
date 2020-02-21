using AASTHA2.Common;
using AASTHA2.Common.Helpers;
using AASTHA2.DTO;
using AASTHA2.Entities;
using AASTHA2.Interfaces;
using AASTHA2.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace AASTHA2.Services
{
    public class IpdService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public IpdService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public dynamic GetIpds(FilterModel filterModel)
        {
            IEnumerable<Ipd> Ipd = _unitOfWork.Ipds.Find(null, filterModel.filter, filterModel.includeProperties, filterModel.sort, filterModel.skip, filterModel.take);
            return  _mapper.Map<IEnumerable<IpdDTO>>(Ipd).ToPageList(filterModel.skip, filterModel.take);
        }
        public bool IsIpdExist(string filter = "")
        {
            return _unitOfWork.Ipds.FirstOrDefault(null, filter) != null;
        }
        public IpdDTO GetIpd(long id, string filter = "", string includeProperties = "")
        {
            var Ipd = _unitOfWork.Ipds.FirstOrDefault(m => m.Id == id, filter, includeProperties);
            return _mapper.Map<IpdDTO>(Ipd);
        }
        public IEnumerable<dynamic> GetIpdStatistics(int? Year = null)
        {
            return _unitOfWork.Ipds.GetStatistics(Year);
        }
        public void PostIpd(IpdDTO IpdDto)
        {
            var Ipd = _mapper.Map<Ipd>(IpdDto);
            _unitOfWork.Ipds.Create(Ipd);
            if (IpdDto.type == IpdType.Delivery)
            {
                var delivery = _mapper.Map<Delivery>(IpdDto.deliveryDetail);
                delivery.IpdId = Ipd.Id;
                _unitOfWork.Deliveries.Create(delivery);
            }
            if (IpdDto.type == IpdType.Operation)
            {
                var operation = _mapper.Map<Operation>(IpdDto.operationDetail);
                operation.IpdId = Ipd.Id;
                _unitOfWork.Operations.Create(operation);
            }
            _unitOfWork.SaveChanges();
            IpdDto.id = Ipd.Id;
        }
        public void PutIpd(IpdDTO IpdDto)
        {
            var Ipd = _mapper.Map<IpdDTO, Ipd>(IpdDto);
            _unitOfWork.Ipds.Update(Ipd);
            _unitOfWork.SaveChanges();
        }
        public void RemoveIpd(IpdDTO IpdDto, string filter = "", bool removePhysical = false)
        {
            var Ipd = _mapper.Map<Ipd>(IpdDto);
            _unitOfWork.Ipds.Delete(Ipd, removePhysical);
            _unitOfWork.SaveChanges();
        }
        public void RemoveIpdLookup(IEnumerable<IpdLookupDTO> ipdLookupDTO, string filter = "", bool removePhysical = false)
        {
            foreach (var item in ipdLookupDTO)
            {
                var Ipd = _unitOfWork.IpdLookups.FirstOrDefault(m => m.Id == item.id, filter);
                _unitOfWork.IpdLookups.Delete(Ipd, removePhysical);
            }
            _unitOfWork.SaveChanges();
        }
    }
}
