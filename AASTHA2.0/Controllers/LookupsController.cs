using AASTHA2.DTO;
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
        public dynamic GetLookups(string filter, string sort, int skip, int take, string includeProperties = "", string fields = "")
        {
            int totalCount;
            var data = _LookupService.GetLookups(filter, out totalCount, sort,  skip, take, includeProperties,fields);

            var result = new { TotalCount = totalCount, Data = data.ToDynamicList() };
            return Ok(result);
        }

        // GET: api/Lookups/5
        [HttpGet("{id}")]
        public ActionResult<LookupDTO> GetLookup(long id, string filter)
        {
            var Lookup = _LookupService.GetLookup(id, filter);

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
            Lookup = _LookupService.GetLookup(LookupDTO.Id);
            return CreatedAtAction("GetLookup", new { id = LookupDTO.Id }, Lookup);
        }
        [HttpDelete("{id}")]
        public ActionResult<LookupDTO> DeleteLookup(long id, bool removePhysical = false)
        {
            var Lookup = _LookupService.GetLookup(id);
            if (Lookup == null)
            {
                return NotFound();
            }
            _LookupService.RemoveLookup(Lookup,"", removePhysical);
            return CreatedAtAction("GetLookup", new { id =id }, Lookup);
        }
    }
}
