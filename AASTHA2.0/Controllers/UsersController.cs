using AASTHA2.DTO;
using AASTHA2.Models;
using AASTHA2.Services;
using Microsoft.AspNetCore.Mvc;

namespace AASTHA2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static UserService _userService;
        public UsersController(ServicesWrapper ServicesWrapper)
        {
            _userService = ServicesWrapper.UserService;
        }
        // GET: api/Users
        [HttpGet]
        public ActionResult GetUsers([FromQuery]FilterModel filterModel)
        {
            var result = _userService.GetUsers(filterModel);
            return Ok(result);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public ActionResult<UserDTO> GetUser(long id, string filter)
        {
            var patient = _userService.GetUser(id, filter);           
            if (patient == null)
            {
                return NotFound();
            }
            return patient;
        }
        [HttpPost]
        public ActionResult<UserDTO> PostUser(UserDTO patientDTO, string includeProperties = "")
        {
            _userService.PostUser(patientDTO);
            var patient = _userService.GetUser(patientDTO.id, null, includeProperties);
            return CreatedAtAction("GetUser", new { id = patientDTO.id }, patient);
        }
        [HttpPut]
        public ActionResult<UserDTO> PutUser(UserDTO patientDTO, string includeProperties = "")
        {
            var patient = _userService.GetUser(patientDTO.id);
            if (patient == null)
            {
                return NotFound();
            }
            _userService.PutUser(patientDTO);
            patient = _userService.GetUser(patientDTO.id, null, includeProperties);
            return CreatedAtAction("GetUser", new { id = patientDTO.id }, patient);
        }
        [HttpDelete("{id}")]
        public ActionResult<UserDTO> DeleteUser(long id, bool isDeleted, bool removePhysical = false)
        {
            var patient = _userService.GetUser(id, "");
            patient.isDeleted = isDeleted;
            if (patient == null)
            {
                return NotFound();
            }
            _userService.RemoveUser(patient, "", removePhysical);
            return CreatedAtAction("GetUser", new { id = id }, patient);
        }
    }
}
