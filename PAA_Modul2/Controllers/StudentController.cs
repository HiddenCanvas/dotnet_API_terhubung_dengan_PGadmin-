using Microsoft.AspNetCore.Mvc;
using PAA_Modul2.Context;
using PAA_Modul2.Models;

namespace PAA_Modul2.Controllers
{
    [ApiController]
    public class StudentController : ControllerBase
    {
        private string __constr;

        public StudentController(IConfiguration configuration)
        {
            __constr = configuration.GetConnectionString("WebApiDatabase")!;
        }

        [HttpGet("api/student")]
        public IActionResult ListStudent()
        {
            StudentContext context = new StudentContext(__constr);
            return Ok(context.ListStudent());
        }

        [HttpGet("api/student/{id}")]
        public IActionResult GetStudent(int id)
        {
            StudentContext context = new StudentContext(__constr);
            Student? student = context.GetStudentById(id);
            if (student == null) return NotFound(new { message = "Murid tidak ditemukan" });
            return Ok(student);
        }

        [HttpPost("api/student")]
        public IActionResult CreateStudent([FromBody] Student student)
        {
            StudentContext context = new StudentContext(__constr);
            bool success = context.CreateStudent(student);
            if (success) return Ok(new { message = "Murid berhasil ditambahkan" });
            return BadRequest(new { message = "Gagal menambahkan murid" });
        }

        [HttpPut("api/student/{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] Student student)
        {
            StudentContext context = new StudentContext(__constr);
            bool success = context.UpdateStudent(id, student);
            if (success) return Ok(new { message = "Data murid berhasil diupdate" });
            return NotFound(new { message = "Murid tidak ditemukan" });
        }

        [HttpDelete("api/student/{id}")]
        public IActionResult DeleteStudent(int id)
        {
            StudentContext context = new StudentContext(__constr);
            bool success = context.DeleteStudent(id);
            if (success) return Ok(new { message = "Murid berhasil dihapus" });
            return NotFound(new { message = "Murid tidak ditemukan" });
        }
    }
}