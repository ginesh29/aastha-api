using AASTHA2.Common.Helpers;
using AASTHA2.DTO;
using AASTHA2.Entities;
using AASTHA2.Interfaces;
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
        public IEnumerable<dynamic> GetOperations(string Search, string Sort, bool ShowDeleted, out int totalCount, int Skip, int Take, string Fields)
        {
            IEnumerable<Operation> Operation = _unitOfWork.Operations.Find(null, Search, ShowDeleted, out totalCount, Sort, Skip, Take);
            var mapped = _mapper.Map<IEnumerable<OperationDTO>>(Operation);
            return mapped.DynamicSelect(Fields).ToDynamicList();
        }
        //public bool IsOperationExist(long Id, string Search = "", bool ShowDeleted = false)
        //{
        //    return _unitOfWork.Operations.IsExist(m => m.Id == Id, Search, ShowDeleted);
        //}
        public OperationDTO GetOperation(long Id, string Search = "", bool ShowDeleted = false)
        {
            var Operation = _unitOfWork.Operations.FirstOrDefault(m => m.Id == Id, Search, ShowDeleted);
            return _mapper.Map<OperationDTO>(Operation);
        }
        //public int OperationCount(string Search = "", bool ShowDeleted = false)
        //{
        //    return _unitOfWork.Operations.Count(null, Search, ShowDeleted);
        //}
        public void PostOperation(OperationDTO OperationDto)
        {
            var Operation = _mapper.Map<Operation>(OperationDto);
            _unitOfWork.Operations.Create(Operation);

            _unitOfWork.SaveChanges();
            OperationDto.Id = Operation.Id;
        }
        public void PutOperation(OperationDTO OperationDto)
        {
            var Operation = _unitOfWork.Operations.FirstOrDefault(m => m.Id == OperationDto.Id);
            Operation = _mapper.Map<OperationDTO, Operation>(OperationDto, Operation);
            _unitOfWork.Operations.Update(Operation);
            _unitOfWork.SaveChanges();
        }
        public void RemoveOperation(OperationDTO Operation, string Search = "", bool ShowDeleted = false, bool RemovePhysical = false)
        {
            var OperationDto = _unitOfWork.Operations.FirstOrDefault(m => m.Id == Operation.Id, Search, ShowDeleted);
            _unitOfWork.Operations.Delete(OperationDto, RemovePhysical);
            _unitOfWork.SaveChanges();
        }
    }
}
