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
    public class LookupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LookupService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public PaginationModel GetLookups(FilterModel filterModel)
        {
            var lookups = _unitOfWork.Lookups.Find(null, filterModel.filter, filterModel.includeProperties, filterModel.sort);
            var totalCount = lookups.Count();
            var paged = lookups.ToPageList(filterModel.skip, filterModel.take);
            var mapped = _mapper.Map<List<LookupDTO>>(paged).AsQueryable();
            return new PaginationModel
            {
                Data = mapped,
                StartPage = totalCount > 0 ? filterModel.skip + 1 : 0,
                EndPage = totalCount > filterModel.take ? filterModel.skip + filterModel.take : totalCount,
                TotalCount = lookups.Count()
            };
        }

        public bool IsLookupExist(string filter = "")
        {
            return _unitOfWork.Lookups.FirstOrDefault(null, filter) != null;
        }
        public LookupDTO GetLookup(long id, string filter = "", string includeProperties = "")
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
            LookupDto.id = Lookup.Id;
        }
        public void PutLookup(LookupDTO LookupDto)
        {
            var Lookup = _mapper.Map<LookupDTO, Lookup>(LookupDto);
            _unitOfWork.Lookups.Update(Lookup);
            _unitOfWork.SaveChanges();
        }
        public void RemoveLookup(LookupDTO LookupDto, bool removePhysical = false)
        {
            var Lookup = _mapper.Map<Lookup>(LookupDto);
            _unitOfWork.Lookups.Delete(Lookup, removePhysical);
            _unitOfWork.SaveChanges();
        }
    }
}
