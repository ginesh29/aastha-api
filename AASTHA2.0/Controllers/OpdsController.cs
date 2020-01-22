using AASTHA2.Common;
using AASTHA2.DTO;
using AASTHA2.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace AASTHA2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OpdsController : ControllerBase
    {
        private static OpdService _OpdService;
        public OpdsController(ServicesWrapper ServicesWrapper)
        {
            _OpdService = ServicesWrapper.OpdService;
        }
        // GET: api/Opds
        [HttpGet]
        public dynamic GetOpds(string filter, string sort, int skip, int take, string includeProperties = "", string fields = "")
        {
            int totalCount;
            var data = _OpdService.GetOpds(filter, out totalCount, sort, skip, take, includeProperties, fields);

            var result = new { TotalCount = totalCount, Data = data.ToDynamicList() };
            return Ok(result);
        }

        // GET: api/Opds/5
        [HttpGet("{id}")]
        public ActionResult<OpdDTO> GetOpd(long id, string filter)
        {
            var Opd = _OpdService.GetOpd(id, filter);

            string stringValue = Enum.GetName(typeof(CaseType), Opd.caseType);
            if (Opd == null)
            {
                return NotFound();
            }
            return Opd;
        }
        [HttpGet]
        [Route("GetStatistics")]
        public ActionResult<dynamic> GetStatistics(string filter)
        {
            int totalCount;
            var result = _OpdService.GetOpdStatistics(filter, out totalCount);
            return Ok(result);
        }
        [HttpPost]
        public ActionResult<OpdDTO> PostOpd(OpdDTO OpdDTO)
        {
            _OpdService.PostOpd(OpdDTO);
            return CreatedAtAction("GetOpd", new { id = OpdDTO.id }, OpdDTO);
        }
        [HttpPut]
        public ActionResult<OpdDTO> PutOpd(OpdDTO OpdDTO)
        {
            var Opd = _OpdService.GetOpd(OpdDTO.id);
            if (Opd == null)
            {
                return NotFound();
            }
            _OpdService.PutOpd(OpdDTO);
            Opd = _OpdService.GetOpd(OpdDTO.id);
            return CreatedAtAction("GetOpd", new { id = OpdDTO.id }, Opd);
        }
        [HttpDelete("{id}")]
        public ActionResult<OpdDTO> DeleteOpd(long id, bool removePhysical = false)
        {
            var Opd = _OpdService.GetOpd(id);
            if (Opd == null)
            {
                return NotFound();
            }
            _OpdService.RemoveOpd(Opd, "", removePhysical);
            return CreatedAtAction("GetOpd", new { id = id }, Opd);
        }        
    }
}
