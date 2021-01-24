using System;
using Xunit;
using MusicTeacher.Repos;
using Moq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using MusicTeacher.Models.DTO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;

namespace MusicTeacherUnitTests.RepoTests
{
    public class MusicTeacherRepoTests
    {
        //This is a known fake DB
        //TODO: Add a script to create the test DB each time
        const string testDBConn = "Data Source=../../../RepoTests/musicteacher-test.db";

        private readonly ILogger<MusicTeacherRepo> _logger;
        private MusicTeacherRepo _repo;

        public MusicTeacherRepoTests()
        {
            _logger = new Mock<ILogger<MusicTeacherRepo>>().Object;
            _repo = new MusicTeacherRepo(_logger, testDBConn);
        }

        [Fact]
        public async Task getStudentsReturnsStudentDTOs()
        {
            //arrange
            //act
            var actionResult = await _repo.GetStudents();
            var students = (IEnumerable<StudentDTO>)actionResult;

            //assert
            Assert.NotEmpty(students);
        }

        [Fact]
        public async Task getLessonPlansReturnsLessonPlanDTOs()
        {
            //arrange
            //act
            var actionResult = await _repo.GetLessonPlans();
            var lessonPlan = (IEnumerable<LessonPlanDTO>)actionResult;

            //assert
            Assert.NotEmpty(lessonPlan);
        }

        [Fact]
        public async Task getLessonPlansByStudentReturnsLessonPlanDTOs()
        {
            //arrange
            int studentID = 1;

            //act
            var actionResult = await _repo.GetLessonPlans(studentID);
            var lessonPlans = (IEnumerable<LessonPlanDTO>)actionResult;

            //assert
            Assert.NotEmpty(lessonPlans);
            Assert.True(lessonPlans.All(p => p.StudentID == studentID));
        }

        [Fact]
        public async Task getLessonPlanReturnsLessonPlanDTO()
        {
            //arrange
            int lessonID = 1;

            //act
            var actionResult = await _repo.GetLessonPlan(lessonID);
            var lessonPlan = (LessonPlanDTO)actionResult;

            //assert
            Assert.Equal(lessonID, lessonPlan.LessonID);
        }
    }
}
