using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MusicTeacher.Controllers;
using MusicTeacher.Managers;
using MusicTeacher.Models;
using Xunit;

namespace MusicTeacherUnitTests.ControllerTests
{
    public class LessonPlanControllerTests
    {
        private readonly ILogger<LessonPlanController> _logger;
        private readonly ILessonPlanManager _manager;
        private LessonPlanController _controller;

        private List<LessonPlan> _lessonPlans = new List<LessonPlan>() {
            new LessonPlan() { Id = 1, StudentID = 1 },
            new LessonPlan() { Id = 2, StudentID = 1 }
        };

        public LessonPlanControllerTests()
        {
            _logger = new Mock<ILogger<LessonPlanController>>().Object;

            var mockManager = new Mock<ILessonPlanManager>();
            mockManager
                .Setup(m => m.GetLessonPlans())
                .Returns(() => Task.FromResult(_lessonPlans.AsEnumerable()));
            mockManager
                .Setup(m => m.GetLessonPlans(1))
                .Returns(() => Task.FromResult(_lessonPlans.AsEnumerable()));
            mockManager
                .Setup(m => m.GetLessonPlans(-1))
                .Returns(() => Task.FromResult(new List<LessonPlan>().AsEnumerable()));
            mockManager
                .Setup(m => m.GetLessonPlan(1))
                .Returns(() => Task.FromResult(new LessonPlan() { Id = 1, StudentID = 1}));
            mockManager
                .Setup(m => m.GetLessonPlan(-1))
                .Returns(() => Task.FromResult<LessonPlan>(null));
            mockManager
                .Setup(m => m.GetAssignment(1))
                .Returns(() => Task.FromResult(new Assignment() { Id = 1 }));
            mockManager
                .Setup(m => m.GetAssignment(-1))
                .Returns(() => Task.FromResult<Assignment>(null));

            _manager = mockManager.Object;

            var mockURL = new Mock<IUrlHelper>(); //Have to mock this because of the logic in the controller that creates links
            mockURL
                .Setup(m => m.Link(It.IsAny<string>(), It.IsAny<object>()))
                .Returns("/fake/link");

            _controller = new LessonPlanController(_logger, _manager);
            _controller.Url = mockURL.Object;
        }

        [Fact]
        public async Task getLessonPlansReturnsPlans()
        {
            //Arrange
            //act
            var result = await _controller.GetLessonPlans() as OkObjectResult;
            var lessonPlans = result.Value as IEnumerable<LessonPlan>;

            //assert
            Assert.NotEmpty(lessonPlans);
        }

        [Fact]
        public async Task getLessonPlansByStudentReturnsPlans()
        {
            //arrange
            //act
            var result = await _controller.GetLessonPlans("1") as OkObjectResult;
            var lessonPlans = result.Value as IEnumerable<LessonPlan>;

            //assert
            Assert.NotEmpty(lessonPlans);
        }

        [Fact]
        public async Task getLessonPlanByIdReturnsPlan()
        {
            //arrange
            string id = "1";

            //act
            var result = await _controller.GetLessonPlan(id) as OkObjectResult;
            var lessonPlan = result.Value as LessonPlan;

            //assert
            Assert.Equal(id, lessonPlan.Id.ToString());
        }

        [Fact]
        public async Task getAssignmentByIdReturnsAssignment()
        {
            //arrange
            string id = "1";

            //act
            var result = await _controller.GetAssignment(id) as OkObjectResult;
            var assignment = result.Value as Assignment;

            //assert
            Assert.Equal(id, assignment.Id.ToString());
        }
    }
}
