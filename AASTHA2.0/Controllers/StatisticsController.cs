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
        public ActionResult<dynamic> GetPatientStatistics()
        {
            var result = new
            {

                patients = _patientService.GetPatientStatistics(),
                opds = _opdService.GetOpdStatistics(),
                ipds = _ipdService.GetIpdStatistics()
            };
            return Ok(result);
        }
        [HttpGet]
        [Route("GetOpdIpdStatistics")]
        public ActionResult<dynamic> GetStatistics()
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