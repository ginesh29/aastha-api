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
        //public bool IsIpdExist(long Id, string Search = "", bool ShowDeleted = false)
        //{
        //    return _unitOfWork.Ipds.IsExist(m => m.Id == Id, Search, ShowDeleted);
        //}
        public IpdDTO GetIpd(long Id, string Search = "", bool ShowDeleted = false)
        {
            var Ipd = _unitOfWork.Ipds.FirstOrDefault(m => m.Id == Id, Search, ShowDeleted);
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
            if (IpdDto.Type == IpdType.Delivery)
            {
                var delivery = _mapper.Map<Delivery>(IpdDto.DeliveryDetail);
                delivery.IpdId = Ipd.Id;
                _unitOfWork.Deliveries.Create(delivery);
            }
            if (IpdDto.Type == IpdType.Operation)
            {
                var operation = _mapper.Map<Operation>(IpdDto.OperationDetail);
                operation.IpdId = Ipd.Id;
                _unitOfWork.Operations.Create(operation);
            }
            _unitOfWork.SaveChanges();
            IpdDto.Id = Ipd.Id;
        }
        public void PutIpd(IpdDTO IpdDto)
        {
            var Ipd = _unitOfWork.Ipds.FirstOrDefault(m => m.Id == IpdDto.Id);
            Ipd = _mapper.Map<IpdDTO, Ipd>(IpdDto, Ipd);
            _unitOfWork.Ipds.Update(Ipd);

            if (IpdDto.Type == IpdType.Delivery)
            {
                var delivery = _mapper.Map<Delivery>(IpdDto.DeliveryDetail);
                _unitOfWork.Deliveries.Update(delivery);
            }
            if (IpdDto.Type == IpdType.Operation)
            {
                var operation = _mapper.Map<Operation>(IpdDto.OperationDetail);
                _unitOfWork.Operations.Update(operation);
            }
            _unitOfWork.SaveChanges();
        }
        public void RemoveIpd(IpdDTO IpdDto, string Search = "", bool ShowDeleted = false, bool RemovePhysical = false)
        {
            var Ipd = _unitOfWork.Ipds.FirstOrDefault(m => m.Id == IpdDto.Id, Search, ShowDeleted);
            _unitOfWork.Ipds.Delete(Ipd, RemovePhysical);

            if (IpdDto.Type == IpdType.Delivery)
            {
                var delivery = _mapper.Map<Delivery>(IpdDto.DeliveryDetail);
                _unitOfWork.Deliveries.Update(delivery);
            }
            if (IpdDto.Type == IpdType.Operation)
            {
                var operation = _mapper.Map<Operation>(IpdDto.OperationDetail);
                _unitOfWork.Operations.Update(operation);
            }
            _unitOfWork.SaveChanges();
        }
    }
}
