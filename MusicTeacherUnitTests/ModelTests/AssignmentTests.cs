using System;
using Xunit;
using MusicTeacher.Models;

namespace MusicTeacherUnitTests.ModelTests
{
    public class AssignmentTests
    {
        public AssignmentTests()
        {
        }

        [Fact]
        public void IsValid_MissingLessonIDNotValid()
        {
            //Arrange
            var assignment = new Assignment() { Id = 1, LessonID = 0 };

            //Act
            //Assert
            Assert.False(assignment.IsValid());
        }

        [Fact]
        public void IsValid_ValidReturnsTrue()
        {
            //Arrange
            var assignment = new Assignment() { Id = 1, LessonID = 1 };

            //Act
            //Assert
            Assert.True(assignment.IsValid());
        }
    }
}
