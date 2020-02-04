using AASTHA2.Common;
using AASTHA2.Common.Helpers;
using AASTHA2.DTO;
using AASTHA2.Entities;
using AASTHA2.Interfaces;
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
        public IEnumerable<dynamic> GetIpds(string filter, out int totalCount, string sort = "", int skip = 0, int take = 0, string includeProperties = "", string fields = "")
        {
            IEnumerable<Ipd> Ipd = _unitOfWork.Ipds.Find(null, out totalCount, filter, includeProperties, sort, skip, take);
            var mapped = _mapper.Map<IEnumerable<IpdDTO>>(Ipd);
            return mapped.DynamicSelect(fields).ToDynamicList();
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
            var Ipd = _unitOfWork.Ipds.FirstOrDefault(m => m.Id == IpdDto.id, "");
            Ipd = _mapper.Map<Ipd>(Ipd);
            //var Ipd1 = _mapper.Map<IpdDTO, Ipd>(IpdDto, Ipd);
            _unitOfWork.Ipds.Update(Ipd);
            if (IpdDto.type == IpdType.Delivery)
            {
                var delivery = _mapper.Map<Delivery>(Ipd.DeliveryDetail);
                _unitOfWork.Deliveries.Update(delivery);
            }
            if (IpdDto.type == IpdType.Operation)
            {
                var operation = _mapper.Map<Operation>(Ipd.OperationDetail);
                _unitOfWork.Operations.Update(operation);
            }
            _unitOfWork.SaveChanges();
        }
        public void RemoveIpd(IpdDTO IpdDto, string filter = "", bool removePhysical = false)
        {
            var Ipd = _unitOfWork.Ipds.FirstOrDefault(m => m.Id == IpdDto.id, filter);
            _unitOfWork.Ipds.Delete(Ipd, removePhysical);

            if (IpdDto.type == IpdType.Delivery)
            {
                var delivery = _mapper.Map<Delivery>(IpdDto.deliveryDetail);
                _unitOfWork.Deliveries.Update(delivery);
            }
            if (IpdDto.type == IpdType.Operation)
            {
                var operation = _mapper.Map<Operation>(IpdDto.operationDetail);
                _unitOfWork.Operations.Update(operation);
            }
            _unitOfWork.SaveChanges();
        }
    }
}
