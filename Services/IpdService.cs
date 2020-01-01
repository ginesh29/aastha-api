using AASTHA2.Common;
using AASTHA2.Common.Helpers;
using AASTHA2.DTO;
using AASTHA2.Entities;
using AASTHA2.Interfaces;
using AutoMapper;
using System.Collections.Generic;
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
        public IEnumerable<dynamic> GetIpds(string Search, string Sort, bool ShowDeleted, out int totalCount, int Skip, int Take, string Fields)
        {
            IEnumerable<Ipd> Ipd = _unitOfWork.Ipds.Find(null, Search, ShowDeleted, out totalCount, Sort, Skip, Take, m => m.Charges);
            var mapped = _mapper.Map<IEnumerable<IpdDTO>>(Ipd);
            return mapped.DynamicSelect(Fields).ToDynamicList();
        }
        public bool IsIpdExist(long Id, string Search = "", bool ShowDeleted = false)
        {
            return _unitOfWork.Ipds.FirstOrDefault(m => m.Id == Id, Search, ShowDeleted) != null;
        }
        public IpdDTO GetIpd(long Id, string Search = "", bool ShowDeleted = false)
        {
            var Ipd = _unitOfWork.Ipds.FirstOrDefault(m => m.Id == Id, Search, ShowDeleted, c => c.Charges, i => i.IpdLookups, d => d.DeliveryDetail, o => o.OperationDetail);
            return _mapper.Map<IpdDTO>(Ipd);
        }
        //public int IpdCount(string Search = "", bool ShowDeleted = false)
        //{
        //    return _unitOfWork.Ipds.Count(null, Search, ShowDeleted);
        //}
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
            var Ipd = _unitOfWork.Ipds.FirstOrDefault(m => m.Id == IpdDto.id, "", false, c => c.Charges, i => i.IpdLookups, d => d.DeliveryDetail, o => o.OperationDetail);
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
        public void RemoveIpd(IpdDTO IpdDto, string Search = "", bool ShowDeleted = false, bool RemovePhysical = false)
        {
            var Ipd = _unitOfWork.Ipds.FirstOrDefault(m => m.Id == IpdDto.id, "", false, c => c.Charges, i => i.IpdLookups, d => d.DeliveryDetail, o => o.OperationDetail);
            _unitOfWork.Ipds.Delete(Ipd, RemovePhysical);

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
