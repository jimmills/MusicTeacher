using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using MusicTeacher.Managers;
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

        private List<StudentDTO> _students = new List<StudentDTO>() {
            new StudentDTO() { StudentID = 1, FirstName = "test1first"},
            new StudentDTO() { StudentID = 2, FirstName = "test2first"}
        };

        public StudentManagerTests()
        {
            //Initiate 
            _logger = new Mock<ILogger<StudentManager>>().Object;
            var mockRepo = new Mock<IMusicTeacherRepo>();
            mockRepo
                .Setup(m => m.GetStudents())
                .Returns(Task.FromResult<IEnumerable<StudentDTO>>(_students));
            _repo = mockRepo.Object;

        }

        [Fact]
        public async Task GetStudentDTOsGetsDTOsFromRepo()
        {
            //Arrange
            var manager = new StudentManager(_logger, _repo);

            //act
            var student = await manager.GetStudents();

            //assert
            Assert.NotEmpty(student);
        }

        [Fact]
        public void GetStudentFromDTOReturnsStudent()
        {
            //A
        }
    }
}
