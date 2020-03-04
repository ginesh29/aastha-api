using AASTHA2.Common;
using AASTHA2.Common.Helpers;
using AASTHA2.DTO;
using AASTHA2.Entities;
using AASTHA2.Interfaces;
using AASTHA2.Models;
using AutoMapper;
using System.Collections.Generic;
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
            IEnumerable<Operation> Operation = _unitOfWork.Operations.Find(null, filterModel.filter, filterModel.includeProperties, filterModel.sort);
            return _mapper.Map<IEnumerable<OperationDTO>>(Operation).ToPageList(filterModel.skip, filterModel.take);
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
        public void RemoveOperation(OperationDTO OperationDto, string filter = "", bool removePhysical = false)
        {
            var Operation = _mapper.Map<Operation>(OperationDto);
            _unitOfWork.Operations.Delete(Operation, removePhysical);
            _unitOfWork.SaveChanges();
        }
    }
}
