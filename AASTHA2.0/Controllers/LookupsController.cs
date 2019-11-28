﻿using AASTHA2.DTO;
using AASTHA2.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;

namespace AASTHA2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class LookupsController : ControllerBase
    {
        private static LookupService _LookupService;
        public LookupsController(ServicesWrapper ServicesWrapper)
        {
            _LookupService = ServicesWrapper.LookupService;
        }
        // GET: api/Lookups
        [HttpGet]
        public dynamic GetLookups(string filter, string sortOrder, int skip, int take = 15, string fields="")
        {
            int totalCount;
            var data = _LookupService.GetLookups(filter, sortOrder, true, out totalCount, skip, take, fields);

            var result = new { TotalCount = totalCount, Data = data.ToDynamicList() };
            return Ok(result);
        }

        // GET: api/Lookups/5
        [HttpGet("{id}")]
        public ActionResult<LookupDTO> GetLookup(long id, string Search)
        {
            var Lookup = _LookupService.GetLookup(id, Search, false);

            if (Lookup == null)
            {
                return NotFound();
            }
            return Lookup;
        }
        [HttpPost]
        public ActionResult<LookupDTO> PostLookup(LookupDTO LookupDTO)
        {
            _LookupService.PostLookup(LookupDTO);
            return CreatedAtAction("GetLookup", new { id = LookupDTO.Id }, LookupDTO);
        }
        [HttpPut]
        public ActionResult<LookupDTO> PutLookup(LookupDTO LookupDTO)
        {
            var Lookup = _LookupService.GetLookup(LookupDTO.Id);
            if (Lookup == null)
            {
                return NotFound();
            }
            _LookupService.PutLookup(LookupDTO);
            return CreatedAtAction("GetLookup", new { id = Lookup.Id }, Lookup);
        }
        [HttpDelete("{id}")]
        public ActionResult<LookupDTO> DeleteLookup(long id, bool removePhysical = false)
        {
            var Lookup = _LookupService.GetLookup(id);
            if (Lookup == null)
            {
                return NotFound();
            }
            _LookupService.RemoveLookup(Lookup, null, false, removePhysical);
            return Lookup;
        }
    }
}
