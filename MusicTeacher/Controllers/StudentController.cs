using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicTeacher.Repos;

namespace MusicTeacher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private IMusicTeacherRepo _repo;

        public StudentController(ILogger<StudentController> logger, IMusicTeacherRepo repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet]
        //public async Task<IActionResult> GetStudents()
        public async Task<IActionResult> GetStudents()
        {
            _logger.LogInformation("GetStudents() method called");
            var repo = new MusicTeacherRepo(_logger, "data source=//users/jimmills/documents/sqlite/musicteacher.db");
            var students = await repo.GetStudents();
            //var studentList = await StudentManager.GetAll();
            return Ok(students);
        }
    }
}
