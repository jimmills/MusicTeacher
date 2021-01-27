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
    public class StudentManagerTests
    {
        private readonly ILogger<StudentManager> _logger;
        private readonly IMusicTeacherRepo _repo;
        private StudentManager _manager;
        private List<StudentDTO> _students;

        public StudentManagerTests()
        {
            _logger = new Mock<ILogger<StudentManager>>().Object;

            _students = new List<StudentDTO>() {
                new StudentDTO() { StudentID = 1, FirstName = "test1first"},
                new StudentDTO() { StudentID = 2, FirstName = "test2first"}
            };

            var mockRepo = new Mock<IMusicTeacherRepo>();
            mockRepo
                .Setup(m => m.GetStudents())
                .Returns(Task.FromResult<IEnumerable<StudentDTO>>(_students));
            mockRepo
                .Setup(m => m.GetStudent(1))
                .Returns(Task.FromResult<StudentDTO>(new StudentDTO() { StudentID = 1 }));
            mockRepo
                .Setup(m => m.GetStudent(-1))
                .Returns(Task.FromResult<StudentDTO>(null));

            _repo = mockRepo.Object;
            _manager = new StudentManager(_logger, _repo);
        }

        [Fact]
        public async Task GetStudentDTOsGetsDTOsFromRepo()
        {
            //Arrange
            //act
            var student = await _manager.GetStudents();

            //assert
            Assert.NotEmpty(student);
        }

        [Fact]
        public void GetStudentFromDTOReturnsStudent()
        {
            //Arrange
            var studentDTO = new StudentDTO()
            {
                StudentID = 1,
                FirstName = "firstName",
                LastName = "lastName",
                Instrument = "piano",
                LessonWindow = "right now"
            };

            //Act
            var student = _manager.GetStudentFromDTO(studentDTO);

            //Assert
            Assert.IsType<Student>(student);
        }

        [Fact]
        public void GetStudentFromDTOMapsDataProperly()
        {
            //Arrange
            var studentDTO = new StudentDTO()
            {
                StudentID = 1,
                FirstName = "firstName",
                LastName = "lastName",
                Instrument = "piano",
                LessonWindow = "right now"
            };

            //Act
            var student = _manager.GetStudentFromDTO(studentDTO);

            //Assert
            Assert.Equal(studentDTO.StudentID, student.Id);
            Assert.Equal(studentDTO.FirstName, student.FirstName);
            Assert.Equal(studentDTO.LastName, student.LastName);
            Assert.Equal(studentDTO.Instrument, student.Instrument);
            Assert.Equal(studentDTO.LessonWindow, student.LessonWindow);
        }

        [Fact]
        public async Task GetStudentGetsStudentDTO()
        {
            //Arrange
            var studentID = 1;

            //act
            var student = await _manager.GetStudent(studentID);

            //assert
            Assert.NotNull(student);
        }

        [Fact]
        public async Task GetStudentGetsNullIfNotFound()
        {
            //Arrange
            var studentID = -1;

            //act
            var student = await _manager.GetStudent(studentID);

            //assert
            Assert.Null(student);
        }
    }
}
