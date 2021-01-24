using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicTeacher.Managers;
using MusicTeacher.Models;

namespace MusicTeacher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LessonPlanController : Controller
    {
        private readonly ILogger<LessonPlanController> _logger;
        private readonly ILessonPlanManager _manager;

        public LessonPlanController(ILogger<LessonPlanController> logger, ILessonPlanManager manager)
        {
            _logger = logger;
            _manager = manager;
        }


        [HttpGet]
        public async Task<IActionResult> GetLessonPlans()
        {
            _logger.LogInformation("GetLessonPlans() method called");
            var lessonPlans = await _manager.GetLessonPlans();
            return Ok(lessonPlans);
        }

        [Route("Student/{studentId}")]
        [HttpGet]
        public async Task<IActionResult> GetLessonPlans(string studentId)
        {
            _logger.LogInformation($"GetLessonPlans({studentId}) method called");
            var lessonPlans = await _manager.GetLessonPlans(Convert.ToInt32(studentId));
            return Ok(lessonPlans);
        }

        [Route("{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetLessonPlan(string Id)
        {
            _logger.LogInformation($"GetLessonPlan({Id}) method called");
            var lessonPlan = await _manager.GetLessonPlan(Convert.ToInt32(Id));
            return Ok(lessonPlan);
        }

        [Route("Assignment/{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetAssignment(string Id)
        {
            _logger.LogInformation($"GetAssignment({Id}) method called");
            var assignment = await _manager.GetAssignment(Convert.ToInt32(Id));
            return Ok(assignment);
        }

        [Route("{lessonId}/Assignment")]
        [HttpGet]
        public async Task<IActionResult> GetAssignments(string lessonId)
        {
            _logger.LogInformation($"GetAssignments({lessonId}) method called");
            var assignments = await _manager.GetAssignments(Convert.ToInt32(lessonId));
            return Ok(assignments);
        }
    }
}
