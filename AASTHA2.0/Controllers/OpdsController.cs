using AASTHA2.Common;
using AASTHA2.DTO;
using AASTHA2.Services;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public dynamic GetOpds(string filter, string sortOrder, int skip, int take = 15, string fields="")
        {
            int totalCount;
            var data = _OpdService.GetOpds(filter, sortOrder, true, out totalCount, skip, take, fields);

            var result = new { TotalCount = totalCount, Data = data.ToDynamicList() };
            return Ok(result);
        }

        // GET: api/Opds/5
        [HttpGet("{id}")]
        public ActionResult<OpdDTO> GetOpd(long id, string Search)
        {
            var Opd = _OpdService.GetOpd(id, Search, false);

            string stringValue = Enum.GetName(typeof(CaseType), Opd.CaseType);
            if (Opd == null)
            {
                return NotFound();
            }
            return Opd;
        }
        [HttpPost]
        public ActionResult<OpdDTO> PostOpd(OpdDTO OpdDTO)
        {
            _OpdService.PostOpd(OpdDTO);
            return CreatedAtAction("GetOpd", new { id = OpdDTO.Id }, OpdDTO);
        }
        [HttpPut]
        public ActionResult<OpdDTO> PutOpd(OpdDTO OpdDTO)
        {
            var Opd = _OpdService.GetOpd(OpdDTO.Id);
            if (Opd == null)
            {
                return NotFound();
            }
            _OpdService.PutOpd(OpdDTO);
            Opd = _OpdService.GetOpd(OpdDTO.Id);
            return CreatedAtAction("GetOpd", new { id = OpdDTO.Id }, Opd);
        }
        [HttpDelete("{id}")]
        public ActionResult<OpdDTO> DeleteOpd(long id, bool removePhysical = false)
        {
            var Opd = _OpdService.GetOpd(id);
            if (Opd == null)
            {
                return NotFound();
            }
            _OpdService.RemoveOpd(Opd, null, false, removePhysical);
            return CreatedAtAction("GetOpd", new { id = id }, Opd);
        }
    }
}
