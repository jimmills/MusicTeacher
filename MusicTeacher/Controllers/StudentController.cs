using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicTeacher.Managers;
using MusicTeacher.Repos;

namespace MusicTeacher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentManager _manager;

        public StudentController(ILogger<StudentController> logger, IStudentManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            _logger.LogInformation("GetStudents() method called");
            var students = await _manager.GetStudents();
            return Ok(students);
        }
    }
}
