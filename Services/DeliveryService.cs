using AASTHA2.Common;
using AASTHA2.Common.Helpers;
using AASTHA2.Entities.Models;
using AASTHA2.Repositories.Interfaces;
using AASTHA2.Services.DTO;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
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
        public PaginationModel GetDeliveries(FilterModel filterModel)
        {
            var deliveries = _unitOfWork.Deliveries.Find(null, filterModel.filter, filterModel.includeProperties, filterModel.sort);
            var totalCount = deliveries.Count();
            var paged = deliveries.ToPageList(filterModel.skip, filterModel.take);
            var mapped = _mapper.Map<List<DeliveryDTO>>(paged).AsQueryable();
            return new PaginationModel
            {
                Data = mapped,
                StartPage = totalCount > 0 ? filterModel.skip + 1 : 0,
                EndPage = totalCount > filterModel.take ? filterModel.skip + filterModel.take : totalCount,
                TotalCount = deliveries.Count()
            };
        }
        public DeliveryDTO GetDelivery(long id, string filter = "", string includeProperties = "")
        {
            var Delivery = _unitOfWork.Deliveries.FirstOrDefault(m => m.Id == id, filter, includeProperties);
            return _mapper.Map<DeliveryDTO>(Delivery);
        }
        public void PostDelivery(DeliveryDTO DeliveryDto)
        {
            var Delivery = _mapper.Map<Delivery>(DeliveryDto);
            _unitOfWork.Deliveries.Create(Delivery);

            _unitOfWork.SaveChanges();
            DeliveryDto.Id = Delivery.Id;
        }
        public void PutDelivery(DeliveryDTO DeliveryDto)
        {
            var Delivery = _mapper.Map<DeliveryDTO, Delivery>(DeliveryDto);
            _unitOfWork.Deliveries.Update(Delivery);
            _unitOfWork.SaveChanges();
        }
        public void RemoveDelivery(DeliveryDTO DeliveryDto, bool removePhysical = false)
        {
            var Delivery = _mapper.Map<Delivery>(DeliveryDto);
            _unitOfWork.Deliveries.Delete(Delivery, removePhysical);
            _unitOfWork.SaveChanges();
        }
    }
}
