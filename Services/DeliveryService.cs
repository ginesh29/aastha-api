using AASTHA2.Common.Helpers;
using AASTHA2.DTO;
using AASTHA2.Entities;
using AASTHA2.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;

namespace AASTHA2.Services
{
    public class DeliveryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeliveryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<dynamic> GetDeliverys(string Search, string Sort, bool ShowDeleted, out int totalCount, int Skip, int Take, string Fields)
        {
            IEnumerable<Delivery> Delivery = _unitOfWork.Deliveries.Find(null, Search, ShowDeleted, out totalCount, Sort, Skip, Take);
            var mapped = _mapper.Map<IEnumerable<DeliveryDTO>>(Delivery);
            return mapped.DynamicSelect(Fields).ToDynamicList();
        }
        //public bool IsDeliveryExist(long Id, string Search = "", bool ShowDeleted = false)
        //{
        //    return _unitOfWork.Deliverys.IsExist(m => m.Id == Id, Search, ShowDeleted);
        //}
        public DeliveryDTO GetDelivery(long Id, string Search = "", bool ShowDeleted = false)
        {
            var Delivery = _unitOfWork.Deliveries.FirstOrDefault(m => m.Id == Id, Search, ShowDeleted);
            return _mapper.Map<DeliveryDTO>(Delivery);
        }
        //public int DeliveryCount(string Search = "", bool ShowDeleted = false)
        //{
        //    return _unitOfWork.Deliverys.Count(null, Search, ShowDeleted);
        //}
        public void PostDelivery(DeliveryDTO DeliveryDto)
        {
            var Delivery = _mapper.Map<Delivery>(DeliveryDto);
            _unitOfWork.Deliveries.Create(Delivery);

            _unitOfWork.SaveChanges();
            DeliveryDto.id = Delivery.Id;
        }
        public void PutDelivery(DeliveryDTO DeliveryDto)
        {
            var Delivery = _unitOfWork.Deliveries.FirstOrDefault(m => m.Id == DeliveryDto.id);
            Delivery = _mapper.Map<DeliveryDTO, Delivery>(DeliveryDto, Delivery);
            _unitOfWork.Deliveries.Update(Delivery);
            _unitOfWork.SaveChanges();
        }
        public void RemoveDelivery(DeliveryDTO Delivery, string Search = "", bool ShowDeleted = false, bool RemovePhysical = false)
        {
            var DeliveryDto = _unitOfWork.Deliveries.FirstOrDefault(m => m.Id == Delivery.id, Search, ShowDeleted);
            _unitOfWork.Deliveries.Delete(DeliveryDto, RemovePhysical);
            _unitOfWork.SaveChanges();
        }
    }
}
