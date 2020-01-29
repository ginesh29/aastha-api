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
        public IEnumerable<dynamic> GetLookups(string filter, out int totalCount, string sort="", int skip = 0, int take = 0, string includeProperties = "", string fields = "")
        {
            IEnumerable<Lookup> Lookup = _unitOfWork.Lookups.Find(null, out totalCount, filter, includeProperties, sort, skip, take);
            var mapped = _mapper.Map<IEnumerable<LookupDTO>>(Lookup);
            return mapped.DynamicSelect(fields).ToDynamicList();
        }
        public bool IsLookupExist(long id, string filter = "", string includeProperties="")
        {
            return _unitOfWork.Lookups.FirstOrDefault( m=> m.Id == id, filter, includeProperties) != null;
        }
        public LookupDTO GetLookup(long id, string filter = "", string includeProperties="")
        {
            var Lookup = _unitOfWork.Lookups.FirstOrDefault(m => m.Id == id, filter, includeProperties);
            return _mapper.Map<LookupDTO>(Lookup);
        }
        //public int LookupCount(string filter = "", bool ShowDeleted = false)
        //{
        //    return _unitOfWork.Lookups.Count(null, filter, ShowDeleted);
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
        public void RemoveLookup(LookupDTO Lookup, string filter = "", bool removePhysical = false)
        {
            var LookupDto = _unitOfWork.Lookups.FirstOrDefault(m => m.Id == Lookup.Id, filter);
            _unitOfWork.Lookups.Delete(LookupDto, removePhysical);
            _unitOfWork.SaveChanges();
        }
    }
}
