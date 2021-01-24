using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ILogger<LessonPlanManager> _logger;
        private readonly IMusicTeacherRepo _repo;
        private LessonPlanManager _manager;

        private List<LessonPlanDTO> _lessonPlans = new List<LessonPlanDTO>() {
            new LessonPlanDTO() { LessonID = 1, StudentID = 1, StartDate= DateTime.Now, EndDate = DateTime.Now },
            new LessonPlanDTO() { LessonID = 2, StudentID = 1, StartDate= DateTime.Now, EndDate = DateTime.Now },
            new LessonPlanDTO() { LessonID = 3, StudentID = 2, StartDate= DateTime.Now, EndDate = DateTime.Now },
        };

        private List<LessonPlanDTO> _lessonPlansForStudent = new List<LessonPlanDTO>() {
            new LessonPlanDTO() { LessonID = 1, StudentID = 1, StartDate= DateTime.Now, EndDate = DateTime.Now },
            new LessonPlanDTO() { LessonID = 2, StudentID = 1, StartDate= DateTime.Now, EndDate = DateTime.Now }
        };

        public LessonManagerTests()
        {
            _logger = new Mock<ILogger<LessonPlanManager>>().Object;

            //I'm sure there's a better way to setup these mocks than putting them in the constructor
            var mockRepo = new Mock<IMusicTeacherRepo>();
            mockRepo
                .Setup(m => m.GetLessonPlans())
                .Returns(Task.FromResult<IEnumerable<LessonPlanDTO>>(_lessonPlans));
            mockRepo
                .Setup(m => m.GetLessonPlans(1))
                .Returns(Task.FromResult<IEnumerable<LessonPlanDTO>>(_lessonPlansForStudent));
            _repo = mockRepo.Object;
            mockRepo
                .Setup(m => m.GetLessonPlans(-1))
                .Returns(Task.FromResult<IEnumerable<LessonPlanDTO>>(new List<LessonPlanDTO>().AsEnumerable()));
            _repo = mockRepo.Object;
            mockRepo
                .Setup(m => m.GetLessonPlan(1))
                .Returns(Task.FromResult<LessonPlanDTO>(new LessonPlanDTO() { LessonID = 1}));
            _repo = mockRepo.Object;
            mockRepo
                .Setup(m => m.GetLessonPlan(-1))
                .Returns(Task.FromResult<LessonPlanDTO>(null));
            _repo = mockRepo.Object;
            _manager = new LessonPlanManager(_logger, _repo);
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
            Assert.Equal(dto.LessonID, lessonPlan.Id);
            Assert.Equal(dto.StudentID, lessonPlan.StudentID);
            Assert.Equal(dto.StartDate, lessonPlan.StartDate);
            Assert.Equal(dto.EndDate, lessonPlan.EndDate);
        }

        [Fact]
        public async Task GetLessonPlansByStudentReturnsPlans()
        {
            //Arrange
            int studentID = 1;

            //act
            var lessonPlans = await _manager.GetLessonPlans(studentID);

            //assert
            Assert.NotEmpty(lessonPlans);
        }

        [Fact]
        public async Task GetLessonPlanByBadStudentReturnsEmpty()
        {
            //Arrange
            int studentID = -1;

            //act
            var lessonPlans = await _manager.GetLessonPlans(studentID);

            //assert
            Assert.Empty(lessonPlans);
        }

        [Fact]
        public async Task GetLessonPlanByIDReturnsPlan()
        {
            //Arrange
            int id = 1;

            //act
            var lessonPlan = await _manager.GetLessonPlan(id);

            //assert
            Assert.Equal(lessonPlan.Id, id);
        }
        [Fact]
        public async Task GetLessonPlanByBadIDReturnsNull()
        {
            //Arrange
            int id = -1;

            //act
            var lessonPlan = await _manager.GetLessonPlan(id);

            //assert
            Assert.Null(lessonPlan);
        }
    }
}
