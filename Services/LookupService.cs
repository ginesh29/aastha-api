using AASTHA2.Common.Helpers;
using AASTHA2.DTO;
using AASTHA2.Entities;
using AASTHA2.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;

namespace AASTHA2.Services
{
    public class LookupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LookupService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<dynamic> GetLookups(string Search, string Sort, bool ShowDeleted, out int totalCount, int Skip, int Take, string Fields)
        {
            IEnumerable<Lookup> Lookup = _unitOfWork.Lookups.Find(null, Search, ShowDeleted, out totalCount, Sort, Skip, Take);
            var mapped = _mapper.Map<IEnumerable<LookupDTO>>(Lookup);
            return mapped.DynamicSelect(Fields).ToDynamicList();
        }
        //public bool IsLookupExist(long Id, string Search = "", bool ShowDeleted = false)
        //{
        //    return _unitOfWork.Lookups.IsExist(m => m.Id == Id, Search, ShowDeleted);
        //}
        public LookupDTO GetLookup(long Id, string Search = "", bool ShowDeleted = false)
        {
            var Lookup = _unitOfWork.Lookups.FirstOrDefault(m => m.Id == Id, Search, ShowDeleted);
            return _mapper.Map<LookupDTO>(Lookup);
        }
        //public int LookupCount(string Search = "", bool ShowDeleted = false)
        //{
        //    return _unitOfWork.Lookups.Count(null, Search, ShowDeleted);
        //}
        public void PostLookup(LookupDTO LookupDto)
        {
            var Lookup = _mapper.Map<Lookup>(LookupDto);
            _unitOfWork.Lookups.Create(Lookup);
            _unitOfWork.SaveChanges();
            LookupDto.Id = Lookup.Id;
        }
        public void PutLookup(LookupDTO LookupDto)
        {
            var Lookup = _unitOfWork.Lookups.FirstOrDefault(m => m.Id == LookupDto.Id);
            Lookup = _mapper.Map<LookupDTO, Lookup>(LookupDto, Lookup);
            _unitOfWork.Lookups.Update(Lookup);
            _unitOfWork.SaveChanges();
        }
        public void RemoveLookup(LookupDTO Lookup, string Search = "", bool ShowDeleted = false, bool RemovePhysical = false)
        {
            var LookupDto = _unitOfWork.Lookups.FirstOrDefault(m => m.Id == Lookup.Id, Search, ShowDeleted);
            _unitOfWork.Lookups.Delete(LookupDto, RemovePhysical);
            _unitOfWork.SaveChanges();
        }
    }
}
