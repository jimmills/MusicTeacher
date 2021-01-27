using System;
using MusicTeacher.Models;
using Xunit;

namespace MusicTeacherUnitTests.ModelTests
{
    public class LessonPlanTests
    {
        public LessonPlanTests()
        {
        }

        [Fact]
        public void IsValid_MissingStudentIDNotValid()
        {
            //Arrange
            var lp = new LessonPlan() { StudentID = 0 };

            //Act/Assert
            Assert.False(lp.IsValid());
        }

        [Fact]
        public void IsValid_EndDateNoStartDateNotValid()
        {
            //Arrange
            var lp = new LessonPlan() { StudentID = 1,  EndDate = DateTime.Now };

            //Act/Assert
            Assert.False(lp.IsValid());
        }

        [Fact]
        public void IsValid_EndDateBeforeStartDateNotValid()
        {
            //Arrange
            var lp = new LessonPlan() { StudentID = 1, EndDate = DateTime.Now, StartDate = DateTime.Now.AddMinutes(10) };

            //Act/Assert
            Assert.False(lp.IsValid());
        }

        [Fact]
        public void IsValid_ValidObjectPassesValidation()
        {
            //Arrange
            var lp = new LessonPlan() { StudentID = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMinutes(10) };

            //Act/Assert
            Assert.True(lp.IsValid());
        }

        [Fact]
        public void Duration_ReturnsDuration()
        {
            //Arrange/Act
            var lp = new LessonPlan() { StudentID = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMinutes(10) };

            //Assert
            Assert.Equal(10, lp.Duration);
        }

        [Fact]
        public void Duration_IsNullIfMissingStartDate()
        {
            //Arrange/Act
            var lp = new LessonPlan() { StudentID = 1, EndDate = DateTime.Now };

            //Assert
            Assert.Null(lp.Duration);
        }

        [Fact]
        public void Duration_IsNullIfMissingEndDate()
        {
            //Arrange/Act
            var lp = new LessonPlan() { StudentID = 1, StartDate = DateTime.Now };

            //Assert
            Assert.Null(lp.Duration);
        }
    }
}
