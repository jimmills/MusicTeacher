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

        private List<AssignmentDTO> _assignments = new List<AssignmentDTO>()
        {
            new AssignmentDTO() { assignmentID = 1, lessonID = 1 },
            new AssignmentDTO() { assignmentID = 2, lessonID = 1 },
            new AssignmentDTO() { assignmentID = 3, lessonID = 1 }
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
            mockRepo
                .Setup(m => m.GetAssignments(1))
                .Returns(Task.FromResult<IEnumerable<AssignmentDTO>>(_assignments));
            _repo = mockRepo.Object;
            mockRepo
            .Setup(m => m.GetAssignments(-1))
            .Returns(Task.FromResult<IEnumerable<AssignmentDTO>>(new List<AssignmentDTO>().AsEnumerable()));
                    _repo = mockRepo.Object;
            mockRepo
            .Setup(m => m.GetAssignment(1))
            .Returns(Task.FromResult<AssignmentDTO>(new AssignmentDTO() { assignmentID = 1 }));
            _repo = mockRepo.Object;
            mockRepo
            .Setup(m => m.GetAssignment(-1))
            .Returns(Task.FromResult<AssignmentDTO>(null));
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
            var lessonPlan = _manager.GetLessonPlanFromDTO(dto, null);

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
            var assignements = new List<AssignmentDTO>()
            {
                new AssignmentDTO() { assignmentID = 1, lessonID = 1},
                new AssignmentDTO() { assignmentID = 2, lessonID = 1},
                new AssignmentDTO() { assignmentID = 3, lessonID = 2},
            };


            //Act
            var lessonPlan = _manager.GetLessonPlanFromDTO(dto, assignements.AsEnumerable());

            //Assert
            Assert.Equal(dto.LessonID, lessonPlan.Id);
            Assert.Equal(dto.StudentID, lessonPlan.StudentID);
            Assert.Equal(dto.StartDate, lessonPlan.StartDate);
            Assert.Equal(dto.EndDate, lessonPlan.EndDate);
            Assert.Equal(3, lessonPlan.Assignments.Count);
            Assert.Equal(2, lessonPlan.Assignments.Where(p => p.Id == 3).Select(s => s.LessonID).First()); //assignment = 3 when lesson = 2
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

        [Fact]
        public void AssignmentFromDTOMapsProperly()
        {
            //Arrange
            var dto = new AssignmentDTO()
            {
                assignmentID = 1,
                lessonID = 1,
                description = "description",
                practiceNotes = "notes"
            };

            //Act
            var assignment = _manager.GetAssignmentFromDTO(dto);

            //Assert
            Assert.Equal(dto.assignmentID, assignment.Id);
            Assert.Equal(dto.lessonID, assignment.LessonID);
            Assert.Equal(dto.description, assignment.Description);
            Assert.Equal(dto.practiceNotes, assignment.PracticeNotes);
        }

        [Fact]
        public async Task GetAssignmentsReturnsAssignments()
        {
            //Arrange
            int id = 1;

            //act
            var assignments = await _manager.GetAssignments(id);

            //assert
            Assert.NotEmpty(assignments);
        }

        [Fact]
        public async Task GetAssignmentsBadRequestReturnsEmptyCollection()
        {
            //Arrange
            int id = -1;

            //act
            var assignments = await _manager.GetAssignments(id);

            //assert
            Assert.Empty(assignments);
        }

        [Fact]
        public async Task GetAssignmentReturnsAssignment()
        {
            //Arrange
            int id = 1;

            //act
            var assignment = await _manager.GetAssignment(id);

            //assert
            Assert.Equal(assignment.Id, id);
        }

        [Fact]
        public async Task GetAssignmentBadRequestReturnsNull()
        {
            //Arrange
            int id = -1;

            //act
            var assignment = await _manager.GetAssignment(id);

            //assert
            Assert.Null(assignment);
        }
    }
}
