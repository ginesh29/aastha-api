using AASTHA2.Common.Helpers;
using AASTHA2.DTO;
using AASTHA2.Entities;
using AASTHA2.Interfaces;
using AutoMapper;
using System;
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
        public IEnumerable<dynamic> GetOpds(string filter, out int totalCount, string sort, int skip = 0, int take = 0, string includeProperties = "", string fields = "")
        {
            IEnumerable<Opd> Opd = _unitOfWork.Opds.Find(null, out totalCount, filter, includeProperties, sort, skip, take);
            var mapped = _mapper.Map<IEnumerable<OpdDTO>>(Opd);
            return mapped.DynamicSelect(fields).ToDynamicList();
        }
        //public bool IsOpdExist(long id, string filter = "", string includeProperties="")
        //{
        //    return _unitOfWork.Opds.IsExist( => m.Id == id, filter, includeProperties);
        //}
        public OpdDTO GetOpd(long id, string filter = "", string includeProperties = "")
        {
            var Opd = _unitOfWork.Opds.FirstOrDefault(m => m.Id == id, filter, includeProperties);
            return _mapper.Map<OpdDTO>(Opd);
        }
        public IEnumerable<dynamic> GetOpdStatistics(string filter, out int totalCount)
        {
            return _unitOfWork.Opds.GetStatistics(out totalCount,filter);
        }
        //public int OpdCount(string filter = "", bool ShowDeleted = false)
        //{
        //    return _unitOfWork.Opds.Count(null, filter, ShowDeleted);
        //}
        public void PostOpd(OpdDTO OpdDto)
        {
            var Opd = _mapper.Map<Opd>(OpdDto);
            _unitOfWork.Opds.Create(Opd);
            _unitOfWork.SaveChanges();
            OpdDto.id = Opd.Id;
        }
        public void PutOpd(OpdDTO OpdDto)
        {
            var Opd = _unitOfWork.Opds.FirstOrDefault(m => m.Id == OpdDto.id);
            Opd = _mapper.Map<OpdDTO, Opd>(OpdDto, Opd);
            _unitOfWork.Opds.Update(Opd);
            _unitOfWork.SaveChanges();
        }
        public void RemoveOpd(OpdDTO Opd, string filter = "", bool removePhysical = false)
        {
            var OpdDto = _unitOfWork.Opds.FirstOrDefault(m => m.Id == Opd.id, filter);
            _unitOfWork.Opds.Delete(OpdDto, removePhysical);
            _unitOfWork.SaveChanges();
        }
    }
}
