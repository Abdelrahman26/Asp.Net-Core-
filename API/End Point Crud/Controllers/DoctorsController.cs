using Hospital.BL;
using Microsoft.AspNetCore.Mvc;
namespace WebApplication_Lec2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        //private readonly HospitalContext _context;
        private readonly IDoctorManager _doctorManager; 
        public DoctorsController(IDoctorManager doctorManager)
        {
            _doctorManager = doctorManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DoctorReadDTO>> GetDoctors()
        {
            return _doctorManager.GetAllDoctors();
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public ActionResult<DoctorReadDTO> GetDoctor(Guid id)
        {
            var doctorDTO = _doctorManager.GetDoctorById(id);
            if (doctorDTO == null)
                return NotFound();

            return doctorDTO;
        }

        [HttpPost]
        public ActionResult<DoctorReadDTO> PostDoctor(DoctorWriteDTO doctorWriteDTO)
        {
            DoctorReadDTO doctorReadDTO = _doctorManager.AddDoctor(doctorWriteDTO);
            // Return ReadDTO in body
            // id of doctorReadDTO, generated randomly
            return CreatedAtAction("GetDoctor", new { id = doctorWriteDTO.Id }, doctorWriteDTO);
        }
        [HttpPut]
        public ActionResult PutDoctor(DoctorWriteDTO doctorWriteDTO)
        {
            var result = _doctorManager.EditDoctor(doctorWriteDTO);
            if (result)
                return NoContent();

            return BadRequest();
        }

        [HttpDelete]
        [Route("id")]
        public ActionResult DeleteDoctor(DoctorWriteDTO doctorWriteDTO)
        {
            _doctorManager.DeleteDoctor(doctorWriteDTO.Id);
            return NoContent();
        }

    }
}
