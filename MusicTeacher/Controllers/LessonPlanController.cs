﻿using System;
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
            plan.Links = this.BuildLessonPlanLinks(plan);
            foreach (var assignment in plan.Assignments)
            {
                assignment.Links = this.BuildAssignmentLinks(assignment);
            }

            return Ok(plan);
        }

        [Route("Assignment/{Id}", Name ="GetAssignment")]
        [HttpGet]
        public async Task<IActionResult> GetAssignment(string Id)
        {
            _logger.LogInformation($"GetAssignment({Id}) method called");
            var assignment = await _manager.GetAssignment(Convert.ToInt32(Id));
            assignment.Links = BuildAssignmentLinks(assignment);

            return Ok(assignment);
        }

        [Route("{lessonId}/Assignment", Name = "AssignmentsForLesson")]
        [HttpGet]
        public async Task<IActionResult> GetAssignments(string lessonId)
        {
            _logger.LogInformation($"GetAssignments({lessonId}) method called");
            var assignments = await _manager.GetAssignments(Convert.ToInt32(lessonId));

            foreach(var assignment in assignments)
            {
                assignment.Links = BuildAssignmentLinks(assignment);
            }

            return Ok(assignments);
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

            return links;
        }
    }
}
