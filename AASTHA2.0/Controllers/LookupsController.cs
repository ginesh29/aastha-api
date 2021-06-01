using AASTHA2.DTO;
using AASTHA2.Models;
using AASTHA2.Services;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult GetLookups([FromQuery]FilterModel filterModel)
          {
            var result = _LookupService.GetLookups(filterModel);
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
        public ActionResult<LookupDTO> PostLookup(LookupDTO LookupDTO, string includeProperties = "")
        {
            _LookupService.PostLookup(LookupDTO);
            var lookup = _LookupService.GetLookup(LookupDTO.id, null, includeProperties);
            return CreatedAtAction("GetLookup", new { id = LookupDTO.id }, lookup);
        }
        [HttpPut]
        public ActionResult<LookupDTO> PutLookup(LookupDTO LookupDTO, string includeProperties = "")
        {
            var Lookup = _LookupService.GetLookup(LookupDTO.id);
            if (Lookup == null)
            {
                return NotFound();
            }
            _LookupService.PutLookup(LookupDTO);
            Lookup = _LookupService.GetLookup(LookupDTO.id, null, includeProperties);
            return CreatedAtAction("GetLookup", new { id = LookupDTO.id }, Lookup);
        }
        [HttpDelete("{id}")]
        public ActionResult<LookupDTO> DeleteLookup(long id, bool isDeleted, bool removePhysical = false)
        {
            var Lookup = _LookupService.GetLookup(id);
            Lookup.isDeleted = isDeleted;
            if (Lookup == null)
            {
                return NotFound();
            }
            _LookupService.RemoveLookup(Lookup, "", removePhysical);
            return CreatedAtAction("GetLookup", new { id = id }, Lookup);
        }
    }
}
