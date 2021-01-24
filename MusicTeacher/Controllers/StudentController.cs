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
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentManager _manager;

        public StudentController(ILogger<StudentController> logger, IStudentManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        [HttpGet]
        [Route("", Name = "AllStudents")]
        public async Task<IActionResult> GetStudents()
        {
            _logger.LogInformation("GetStudents() method called");
            var students = await _manager.GetStudents();

            //Add Links
            //TODO: Add self link (and the code to power it through the layers)
            foreach (var student in students)
            {
                student.AddLink(Url.Link("LessonsForStudent", new { studentId = student.Id }), "Lessons", "GET");
            }
            return Ok(students);
        }
    }
}
