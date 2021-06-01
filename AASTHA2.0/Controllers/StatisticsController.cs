using AASTHA2.Services;
using Microsoft.AspNetCore.Mvc;

namespace AASTHA2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
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
        public ActionResult GetPatientStatistics(int? Year)
        {
            var result = new
            {
                patients = _patientService.GetPatientStatistics(Year),
                opds = _opdService.GetOpdStatistics(Year),
                ipds = _ipdService.GetIpdStatistics(Year)
            };
            return Ok(result);
        }
        [HttpGet]
        [Route("GetOpdIpdStatistics")]
        public ActionResult GetStatistics()
        {
            var result = new
            {
                opds = _opdService.GetOpdStatistics(),
                ipds = _ipdService.GetIpdStatistics()
            };
            return Ok(result);
        }
    }
}