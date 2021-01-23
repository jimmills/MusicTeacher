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

namespace MusicTeacherUnitTests.RepoTests
{
    public class MusicTeacherRepoTests
    {
        //This is a known fake DB
        //TODO: Add a script to create the test DB each time
        const string testDBConn = "Data Source=../../../RepoTests/musicteacher-test.db";

        private readonly ILogger<MusicTeacherRepo> _logger;

        public MusicTeacherRepoTests()
        {
            _logger = new Mock<ILogger<MusicTeacherRepo>>().Object;
        }

        [Fact]
        public async Task getStudentsReturnsStudents()
        {
            //arrange
            var repo = new MusicTeacherRepo(_logger, testDBConn);

            //act
            var actionResult = await repo.GetStudents();
            var students = (IEnumerable<StudentDTO>)actionResult;

            //assert
            Assert.NotEmpty(students);
        }
    }
}
