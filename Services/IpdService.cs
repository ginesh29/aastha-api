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
    public class IpdService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public IpdService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public PaginationModel GetIpds(FilterModel filterModel)
        {
            var ipds = _unitOfWork.Ipds.Find(null, filterModel.filter, filterModel.includeProperties, filterModel.sort);
            var totalCount = ipds.Count();
            var paged = ipds.ToPageList(filterModel.skip, filterModel.take);
            var mapped = _mapper.Map<List<IpdDTO>>(paged).AsQueryable();
            return new PaginationModel
            {
                Data = mapped,
                StartPage = totalCount > 0 ? filterModel.skip + 1 : 0,
                EndPage = totalCount > filterModel.take ? filterModel.skip + filterModel.take : totalCount,
                TotalCount = ipds.Count()
            };
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
        public IQueryable GetIpdStatistics(int? Year = null)
        {
            return _unitOfWork.Ipds.GetStatistics(Year).AsQueryable();
        }
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
                var Ipd = _unitOfWork.IpdLookups.FirstOrDefault(m => m.Id == item.Id, filter);
                _unitOfWork.IpdLookups.Delete(Ipd, removePhysical);
            }
            _unitOfWork.SaveChanges();
        }
    }
}
