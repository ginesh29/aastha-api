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
        public IEnumerable<dynamic> GetDeliveries(string filter, out int totalCount, string sort = "", int skip = 0, int take = 0, string includeProperties = "", string fields = "")
        {
            IEnumerable<Delivery> Delivery = _unitOfWork.Deliveries.Find(null, out totalCount, filter, includeProperties, sort, skip, take);
            var mapped = _mapper.Map<IEnumerable<DeliveryDTO>>(Delivery);
            return mapped.DynamicSelect(fields).ToDynamicList();
        }
        //public bool IsDeliveryExist(long id, string filter = "", string includeProperties="")
        //{
        //    return _unitOfWork.Deliverys.IsExist( => m.Id == id, filter, includeProperties);
        //}
        public DeliveryDTO GetDelivery(long id, string filter = "", string includeProperties = "")
        {
            var Delivery = _unitOfWork.Deliveries.FirstOrDefault(m => m.Id == id, filter, includeProperties);
            return _mapper.Map<DeliveryDTO>(Delivery);
        }
        //public int DeliveryCount(string filter = "", bool ShowDeleted = false)
        //{
        //    return _unitOfWork.Deliverys.Count(null, filter, ShowDeleted);
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
            var Delivery = _mapper.Map<DeliveryDTO, Delivery>(DeliveryDto);
            _unitOfWork.Deliveries.Update(Delivery);
            _unitOfWork.SaveChanges();
        }
        public void RemoveDelivery(DeliveryDTO DeliveryDto, string filter = "", bool removePhysical = false)
        {
            var Delivery = _mapper.Map<Delivery>(DeliveryDto);
            _unitOfWork.Deliveries.Delete(Delivery, removePhysical);
            _unitOfWork.SaveChanges();
        }
    }
}
