using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicTeacher.Managers;
using MusicTeacher.Models;
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
            foreach (var student in students)
            {
                student.Links = BuildStudentLinks(student);
            }
            return Ok(students);
        }

        [HttpGet]
        [Route("{id}", Name = "GetStudent")]
        public async Task<IActionResult> GetStudent(int id)
        {
            _logger.LogInformation($"GetStudent({id}) method called");
            var student = await _manager.GetStudent(id);

            if(student == null) { return NotFound(); }

            student.Links = BuildStudentLinks(student);

            return Ok(student);
        }

        private List<Link> BuildStudentLinks(Student student)
        {
            List<Link> links = new List<Link>();

            //Self
            links.Add(new Link(Url.Link("GetStudent", new { id = student.Id }), "self", "GET"));

            //LessonPlan
            links.Add(new Link(Url.Link("LessonsForStudent", new { studentId = student.Id }), "Lessons", "GET"));

            return links;
        }
    }
}
