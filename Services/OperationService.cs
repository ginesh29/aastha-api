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
    public class OperationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OperationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public PaginationModel GetOperations(FilterModel filterModel)
        {
            var operations = _unitOfWork.Operations.Find(null, filterModel.filter, filterModel.includeProperties, filterModel.sort);
            var totalCount = operations.Count();
            var paged = operations.ToPageList(filterModel.skip, filterModel.take);
            var mapped = _mapper.Map<List<OperationDTO>>(paged).AsQueryable();
            return new PaginationModel
            {
                Data = mapped,
                StartPage = totalCount > 0 ? filterModel.skip + 1 : 0,
                EndPage = totalCount > filterModel.take ? filterModel.skip + filterModel.take : totalCount,
                TotalCount = operations.Count()
            };
        }
        //public bool IsOperationExist(long id, string filter = "", string includeProperties="")
        //{
        //    return _unitOfWork.Operations.IsExist( => m.Id == id, filter, includeProperties);
        //}
        public OperationDTO GetOperation(long id, string filter = "", string includeProperties = "")
        {
            var Operation = _unitOfWork.Operations.FirstOrDefault(m => m.Id == id, filter, includeProperties);
            return _mapper.Map<OperationDTO>(Operation);
        }
        //public int OperationCount(string filter = "", bool ShowDeleted = false)
        //{
        //    return _unitOfWork.Operations.Count(null, filter, ShowDeleted);
        //}
        public void PostOperation(OperationDTO OperationDto)
        {
            var Operation = _mapper.Map<Operation>(OperationDto);
            _unitOfWork.Operations.Create(Operation);

            _unitOfWork.SaveChanges();
            OperationDto.id = Operation.Id;
        }
        public void PutOperation(OperationDTO OperationDto)
        {
            var Operation = _mapper.Map<OperationDTO, Operation>(OperationDto);
            _unitOfWork.Operations.Update(Operation);
            _unitOfWork.SaveChanges();
        }
        public void RemoveOperation(OperationDTO OperationDto, bool removePhysical = false)
        {
            var Operation = _mapper.Map<Operation>(OperationDto);
            _unitOfWork.Operations.Delete(Operation, removePhysical);
            _unitOfWork.SaveChanges();
        }
    }
}