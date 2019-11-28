using AASTHA2.Common.Helpers;
using AASTHA2.DTO;
using AASTHA2.Entities;
using AASTHA2.Interfaces;
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
        public IEnumerable<dynamic> GetOpds(string Search, string Sort, bool ShowDeleted, out int totalCount, int Skip, int Take, string Fields)
        {
            IEnumerable<Opd> Opd = _unitOfWork.Opds.Find(null, Search, ShowDeleted, out totalCount, Sort, Skip, Take, m => m.Patient);
            var mapped = _mapper.Map<IEnumerable<OpdDTO>>(Opd);
            return mapped.DynamicSelect(Fields).ToDynamicList();
        }
        //public bool IsOpdExist(long Id, string Search = "", bool ShowDeleted = false)
        //{
        //    return _unitOfWork.Opds.IsExist(m => m.Id == Id, Search, ShowDeleted);
        //}
        public OpdDTO GetOpd(long Id, string Search = "", bool ShowDeleted = false)
        {
            var Opd = _unitOfWork.Opds.FirstOrDefault(m => m.Id == Id, Search, ShowDeleted);
            return _mapper.Map<OpdDTO>(Opd);
        }
        //public int OpdCount(string Search = "", bool ShowDeleted = false)
        //{
        //    return _unitOfWork.Opds.Count(null, Search, ShowDeleted);
        //}
        public void PostOpd(OpdDTO OpdDto)
        {
            var Opd = _mapper.Map<Opd>(OpdDto);
            _unitOfWork.Opds.Create(Opd);
            _unitOfWork.SaveChanges();
            OpdDto.Id = Opd.Id;
        }
        public void PutOpd(OpdDTO OpdDto)
        {
            var Opd = _unitOfWork.Opds.FirstOrDefault(m => m.Id == OpdDto.Id);
            Opd = _mapper.Map<OpdDTO, Opd>(OpdDto, Opd);
            _unitOfWork.Opds.Update(Opd);
            _unitOfWork.SaveChanges();
        }
        public void RemoveOpd(OpdDTO Opd, string Search = "", bool ShowDeleted = false, bool RemovePhysical = false)
        {
            var OpdDto = _unitOfWork.Opds.FirstOrDefault(m => m.Id == Opd.Id, Search, ShowDeleted);
            _unitOfWork.Opds.Delete(OpdDto, RemovePhysical);
            _unitOfWork.SaveChanges();
        }
    }
}
