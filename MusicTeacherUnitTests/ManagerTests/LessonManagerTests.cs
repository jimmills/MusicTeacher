using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using MusicTeacher.Managers;
using MusicTeacher.Models;
using MusicTeacher.Models.DTO;
using MusicTeacher.Repos;
using Xunit;

namespace MusicTeacherUnitTests.ManagerTests
{
    public class LessonManagerTests
    {
        private readonly ILogger<LessonManager> _logger;
        private readonly IMusicTeacherRepo _repo;
        private LessonManager _manager;

        private List<LessonPlanDTO> _lessonPlans = new List<LessonPlanDTO>() {
            new LessonPlanDTO() { LessonID = 1, StudentID = 1, StartDate= DateTime.Now, EndDate = DateTime.Now },
            new LessonPlanDTO() { LessonID = 2, StudentID = 1, StartDate= DateTime.Now, EndDate = DateTime.Now }
        };

        public LessonManagerTests()
        {
            _logger = new Mock<ILogger<LessonManager>>().Object;
            var mockRepo = new Mock<IMusicTeacherRepo>();
            mockRepo
                .Setup(m => m.GetLessonPlans())
                .Returns(Task.FromResult<IEnumerable<LessonPlanDTO>>(_lessonPlans));
            _repo = mockRepo.Object;

            _manager = new LessonManager(_logger, _repo);
        }

        [Fact]
        public async Task GetLessonPlanDTOsGetsDTOsFromRepo()
        {
            //Arrange
            //act
            var lessonPlans = await _manager.GetLessonPlans();

            //assert
            Assert.NotEmpty(lessonPlans);
        }

        [Fact]
        public void GetLessonPlanFromDTOReturnsLessonPlan()
        {
            //Arrange
            var dto = new LessonPlanDTO()
            {
                LessonID = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                StudentID = 1
            };

            //Act
            var lessonPlan = _manager.GetLessonPlanFromDTO(dto);

            //Assert
            Assert.IsType<LessonPlan>(lessonPlan);
        }

        [Fact]
        public void GetLessonPlanFromDTOMapsDataProperly()
        {
            //Arrange
            var dto = new LessonPlanDTO()
            {
                LessonID = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                StudentID = 1
            };

            //Act
            var lessonPlan = _manager.GetLessonPlanFromDTO(dto);

            //Assert
            Assert.Equal(dto.LessonID, lessonPlan.ID);
            Assert.Equal(dto.StudentID, lessonPlan.StudentID);
            Assert.Equal(dto.StartDate, lessonPlan.StartDate);
            Assert.Equal(dto.EndDate, lessonPlan.EndDate);
        }
    }
}
