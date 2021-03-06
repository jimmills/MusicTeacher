﻿using System;
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
using Microsoft.AspNetCore.Http;

namespace MusicTeacherUnitTests.ControllerTests
{
    public class StudentControllerTests
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentManager _manager;
        private StudentController _controller;

        private List<Student> _students;

        public StudentControllerTests()
        {
            _logger = new Mock<ILogger<StudentController>>().Object;

            _students = new List<Student>() {
                new Student() { Id = 1, FirstName = "test1first"},
                new Student() { Id = 2, FirstName = "test2first"}
            };
            var mockManager = new Mock<IStudentManager>();
            mockManager
                .Setup(m => m.GetStudents())
                .Returns(() => Task.FromResult(_students.AsEnumerable()));
            _manager = mockManager.Object;

            var mockURL = new Mock<IUrlHelper>(); //Have to mock this because of the logic in the controller that creates links
            mockURL
                .Setup(m => m.Link(It.IsAny<string>(), It.IsAny<object>()))
                .Returns("/fake/link");

            _controller = new StudentController(_logger, _manager);
            _controller.Url = mockURL.Object; 
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
