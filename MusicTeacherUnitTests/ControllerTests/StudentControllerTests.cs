using System;
using Xunit;
using MusicTeacher.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using MusicTeacher.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicTeacher.Managers;
using System.Linq;

namespace MusicTeacherUnitTests.ControllerTests
{
    public class StudentControllerTests
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentManager _manager;
        private StudentController _controller;

        private List<Student> _students = new List<Student>() {
            new Student() { Id = 1, FirstName = "test1first"},
            new Student() { Id = 2, FirstName = "test2first"}
        };

        public StudentControllerTests()
        {
            _logger = new Mock<ILogger<StudentController>>().Object;

            var mockManager = new Mock<IStudentManager>();
            mockManager
                .Setup(m => m.GetStudents())
                .Returns(() => Task.FromResult(_students.AsEnumerable()));
            _manager = mockManager.Object;

            _controller = new StudentController(_logger, _manager);
        }

        [Fact]
        public async Task getStudentsReturns200()
        {
            //arrange
            //act
            var actionResult = await _controller.GetStudents();

            //assert
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public async Task getStudentsReturnsStudents()
        {
            //Arrange
            //act
            var result = await _controller.GetStudents() as OkObjectResult;
            var students = result.Value as IEnumerable<Student>;

            //assert
            Assert.NotEmpty(students);
        }
    }
}
