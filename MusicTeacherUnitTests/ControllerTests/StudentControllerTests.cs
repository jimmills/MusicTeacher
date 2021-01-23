using System;
using Xunit;
using MusicTeacher.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace MusicTeacherUnitTests.ControllerTests
{
    public class StudentControllerTests
    {
        [Fact]
        public void getStudentsReturns200()
        {
            //arrange
            Mock<ILogger<StudentController>> mockLogger = new Mock<ILogger<StudentController>>();
            var controller = new StudentController(mockLogger.Object);

            //act
            var actionResult = controller.GetStudents();

            //assert
            Assert.IsType<OkResult>(actionResult);
        }
    }
}
