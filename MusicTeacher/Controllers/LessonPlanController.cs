using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicTeacher.Managers;
using MusicTeacher.Models;
using MusicTeacher.Models.DTO;

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

            foreach (LessonPlan plan in lessonPlans)
            {
                plan.Links = this.BuildLessonPlanLinks(plan);
                foreach(var assignment in plan.Assignments)
                {
                    assignment.Links = this.BuildAssignmentLinks(assignment);
                }
            }

            return Ok(lessonPlans);
        }

        [Route("Student/{studentId}", Name = "LessonsForStudent")]
        [HttpGet]
        public async Task<IActionResult> GetLessonPlans(string studentId)
        {
            _logger.LogInformation($"GetLessonPlans({studentId}) method called");

            var lessonPlans = await _manager.GetLessonPlans(Convert.ToInt32(studentId));

            foreach(LessonPlan plan in lessonPlans)
            {
                plan.Links = this.BuildLessonPlanLinks(plan);
                foreach (var assignment in plan.Assignments)
                {
                    assignment.Links = this.BuildAssignmentLinks(assignment);
                }
            }

            return Ok(lessonPlans);
        }

        [Route("{Id}", Name = "GetLessonPlan")]
        [HttpGet]
        public async Task<IActionResult> GetLessonPlan(string Id)
        {
            _logger.LogInformation($"GetLessonPlan({Id}) method called");
            var plan = await _manager.GetLessonPlan(Convert.ToInt32(Id));

            if(plan == null) { return NotFound(); }

            plan.Links = this.BuildLessonPlanLinks(plan);
            foreach (var assignment in plan.Assignments)
            {
                assignment.Links = this.BuildAssignmentLinks(assignment);
            }

            return Ok(plan);
        }

        [Route("", Name = "PostLessonPlan")]
        [HttpPost]
        public async Task<IActionResult> PostLessonPlan([FromBody] LessonPlanDTO lessonPlan)
        {
            _logger.LogInformation($"PostLessonPlan() method called: {lessonPlan.StudentID}, {lessonPlan.StartDate.ToString()}, {lessonPlan.EndDate.ToString()}");
            var newLessonPlan = await _manager.InsertLessonPlan(lessonPlan);
            newLessonPlan.Links = BuildLessonPlanLinks(newLessonPlan);

            return Created(newLessonPlan.Links.Where(p => p.Rel == "self").FirstOrDefault().Href, newLessonPlan);
        }

        [Route("{Id}", Name = "DeleteLessonPlan")]
        [HttpDelete]
        public async Task<IActionResult> DeleteLessonPlan(int Id)
        {
            _logger.LogInformation($"DeleteLessonPlan({Id}) method called");
            await _manager.DeleteLessonPlan(Id);
            return NoContent();
        }


        [Route("Assignment/{Id}", Name ="GetAssignment")]
        [HttpGet]
        public async Task<IActionResult> GetAssignment(string Id)
        {
            _logger.LogInformation($"GetAssignment({Id}) method called");
            var assignment = await _manager.GetAssignment(Convert.ToInt32(Id));

            if (assignment == null) { return NotFound(); }

            assignment.Links = BuildAssignmentLinks(assignment);

            return Ok(assignment);
        }

        [Route("Assignment", Name = "PostAssignment")]
        [HttpPost]
        public async Task<IActionResult> PostAssignment([FromBody] AssignmentDTO assignment)
        {
            _logger.LogInformation($"PostAssignment() method called: {assignment.lessonID}, {assignment.description}, {assignment.practiceNotes}");
            var newAssignment = await _manager.InsertAssignment(assignment);
            newAssignment.Links = BuildAssignmentLinks(newAssignment);

            return Created(newAssignment.Links.Where(p => p.Rel == "self").FirstOrDefault().Href, newAssignment);
        }

        [Route("Assignment/{Id}", Name = "DeleteAssignment")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAssignment(int Id)
        {
            _logger.LogInformation($"DeleteAssignment({Id}) method called");
            await _manager.DeleteAssignment(Id);
            return NoContent();
        }


        [Route("{lessonId}/Assignment", Name = "AssignmentsForLesson")]
        [HttpGet]
        public async Task<IActionResult> GetAssignments(string lessonId)
        {
            _logger.LogInformation($"GetAssignments({lessonId}) method called");

            var lessonPlan = await _manager.GetLessonPlan(Convert.ToInt32(lessonId));
            if(lessonPlan == null) { return NotFound(); }

            foreach(var assignment in lessonPlan.Assignments)
            {
                assignment.Links = BuildAssignmentLinks(assignment);
            }

            return Ok(lessonPlan.Assignments);
        }

        private List<Link> BuildLessonPlanLinks(LessonPlan lessonPlan)
        {
            List<Link> links = new List<Link>();

            //Self
            links.Add(new Link(Url.Link("GetLessonPlan", new { Id = lessonPlan.Id }), "self", "GET"));

            //Assignments
            links.Add(new Link(Url.Link("AssignmentsForLesson", new { lessonId = lessonPlan.Id }), "assignments", "GET"));

            return links;
        }

        private List<Link> BuildAssignmentLinks(Assignment assignment)
        {
            List<Link> links = new List<Link>();

            //Self
            links.Add(new Link(Url.Link("GetAssignment", new { Id = assignment.Id }), "self", "GET"));

            //Delete
            links.Add(new Link(Url.Link("DeleteAssignment", new { Id = assignment.Id }), "delete", "DELETE"));

            return links;
        }
    }
}
