using AASTHA2.Services;
using Microsoft.AspNetCore.Mvc;

namespace AASTHA2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private static PatientService _patientService;
        private static OpdService _opdService;
        private static IpdService _ipdService;
        public StatisticsController(ServicesWrapper ServicesWrapper)
        {
            _patientService = ServicesWrapper.PatientService;
            _opdService = ServicesWrapper.OpdService;
            _ipdService = ServicesWrapper.IpdService;
        }
        [HttpGet]
        [Route("GetPatientStatistics")]
        public ActionResult<dynamic> GetPatientStatistics(string filter)
        {
            int totalCount;
            var result = new
            {
                patients = _patientService.GetPatientStatistics(filter, out totalCount),
                opds = _opdService.GetOpdStatistics(filter, out totalCount),
                ipds = _ipdService.GetIpdStatistics(filter, out totalCount)
            };
            return Ok(result);
        }
        [HttpGet]
        [Route("GetOpdIpdStatistics")]
        public ActionResult<dynamic> GetStatistics(string filter)
        {
            int totalCount;
            var result = new
            {
                opds = _opdService.GetOpdStatistics(filter, out totalCount),
                ipds = _ipdService.GetIpdStatistics(filter, out totalCount)
            };
            return Ok(result);
        }
    }
}