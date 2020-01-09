using AASTHA2.DTO;
using AASTHA2.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;

namespace AASTHA2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class IpdsController : ControllerBase
    {
        private static IpdService _IpdService;
        public IpdsController(ServicesWrapper ServicesWrapper)
        {
            _IpdService = ServicesWrapper.IpdService;
        }
        // GET: api/Ipds
        [HttpGet]
        public dynamic GetIpds(string filter, string sort, int skip, int take, string includeProperties = "", string fields = "")
        {
            int totalCount;
            var data = _IpdService.GetIpds(filter, out totalCount, sort, skip, take, includeProperties,fields);

            var result = new { TotalCount = totalCount, Data = data.ToDynamicList() };
            return Ok(result);
        }

        // GET: api/Ipds/5
        [HttpGet("{id}")]
        public ActionResult<IpdDTO> GetIpd(long id, string filter)
        {
            var Ipd = _IpdService.GetIpd(id, filter);

            if (Ipd == null)
            {
                return NotFound();
            }
            return Ipd;
        }
        [HttpPost]
        public ActionResult<IpdDTO> PostIpd(IpdDTO IpdDTO)
        {
            _IpdService.PostIpd(IpdDTO);
            return CreatedAtAction("GetIpd", new { id = IpdDTO.id }, IpdDTO);
        }
        [HttpPut]
        public ActionResult<IpdDTO> PutIpd(IpdDTO IpdDTO)
        {
            var Ipd = _IpdService.GetIpd(IpdDTO.id);
            if (Ipd == null)
            {
                return NotFound();
            }
            _IpdService.PutIpd(IpdDTO);
            Ipd = _IpdService.GetIpd(IpdDTO.id);
            return CreatedAtAction("GetIpd", new { id = IpdDTO.id }, Ipd);
        }
        [HttpDelete("{id}")]
        public ActionResult<IpdDTO> DeleteIpd(long id, bool removePhysical = false)
        {
            var Ipd = _IpdService.GetIpd(id);
            if (Ipd == null)
            {
                return NotFound();
            }
            _IpdService.RemoveIpd(Ipd, "", removePhysical);
            return CreatedAtAction("GetIpd", new { id = id }, Ipd);
        }
    }
}
