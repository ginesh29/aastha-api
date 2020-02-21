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
    public class OpdService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OpdService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public dynamic GetOpds(FilterModel filterModel)
        {
            IEnumerable<Opd> Opd = _unitOfWork.Opds.Find(null, filterModel.filter, filterModel.includeProperties, filterModel.sort, filterModel.skip, filterModel.take);
            return _mapper.Map<IEnumerable<OpdDTO>>(Opd).ToPageList(filterModel.skip, filterModel.take);
        }
        public bool IsOpdExist(string filter = "")
        {
            return _unitOfWork.Opds.FirstOrDefault(null, filter) != null;
        }
        public OpdDTO GetOpd(long id, string filter = "", string includeProperties = "")
        {
            var Opd = _unitOfWork.Opds.FirstOrDefault(m => m.Id == id, filter, includeProperties);
            return _mapper.Map<OpdDTO>(Opd);
        }
        public IEnumerable<dynamic> GetOpdStatistics(int? Year = null)
        {
            return _unitOfWork.Opds.GetStatistics(Year);
        }
        public void PostOpd(OpdDTO OpdDto)
        {
            var Opd = _mapper.Map<Opd>(OpdDto);
            _unitOfWork.Opds.Create(Opd);
            _unitOfWork.SaveChanges();
            OpdDto.id = Opd.Id;
        }
        public void PutOpd(OpdDTO OpdDto)
        {
           var Opd = _mapper.Map<OpdDTO, Opd>(OpdDto);
            _unitOfWork.Opds.Update(Opd);
            _unitOfWork.SaveChanges();
        }
        public void RemoveOpd(OpdDTO OpdDto, string filter = "", bool removePhysical = false)
        {
            var Opd = _mapper.Map<Opd>(OpdDto);
            _unitOfWork.Opds.Delete(Opd, removePhysical);
            _unitOfWork.SaveChanges();            
        }
    }
}
