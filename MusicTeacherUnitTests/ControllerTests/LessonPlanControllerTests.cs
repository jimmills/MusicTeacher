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
            new LessonPlan() { Id = 1 },
            new LessonPlan() { Id = 2 }
        };

        public LessonPlanControllerTests()
        {
            _logger = new Mock<ILogger<LessonPlanController>>().Object;

            var mockManager = new Mock<ILessonPlanManager>();
            mockManager
                .Setup(m => m.GetLessonPlans())
                .Returns(() => Task.FromResult(_lessonPlans.AsEnumerable()));
            mockManager
                .Setup(m => m.GetLessonPlans(It.IsAny<int>()))
                .Returns(() => Task.FromResult(_lessonPlans.AsEnumerable()));
            _manager = mockManager.Object;

            _controller = new LessonPlanController(_logger, _manager);

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
    }
}
