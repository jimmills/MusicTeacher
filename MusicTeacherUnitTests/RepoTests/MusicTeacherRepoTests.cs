using System;
using Xunit;
using MusicTeacher.Repos;
using Moq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using MusicTeacher.Models;
using System.Threading.Tasks;

namespace MusicTeacherUnitTests.RepoTests
{
    public class MusicTeacherRepoTests
    {
        const string testDBConn = "Data Source=../../../RepoTests/musicteacher-test.db";

        public MusicTeacherRepoTests()
        {
        }


        [Fact]
        public async Task getStudentsAsyncReturnsStudents()
        {
            //arrange
            Mock<ILogger<MusicTeacherRepo>> mockLogger = new Mock<ILogger<MusicTeacherRepo>>();
            var repo = new MusicTeacherRepo(mockLogger.Object, testDBConn);

            //act
            var actionResult = await repo.GetStudents();
            var students = (IEnumerable<Student>)actionResult;

            //assert
            Assert.NotEmpty(students);
        }
    }
}
