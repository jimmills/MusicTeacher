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
            //arrange/act
            var actionResult = await _repo.GetStudents();
            var students = (IEnumerable<StudentDTO>)actionResult;

            //assert
            Assert.NotEmpty(students);
        }

        [Fact]
        public async Task getStudentReturnsStudentDTO()
        {
            //arrange
            int studentID = 1;

            //act
            var actionResult = await _repo.GetStudent(studentID);
            var student = (StudentDTO)actionResult;

            //Assert
            Assert.Equal(studentID, student.StudentID);
        }

        [Fact]
        public async Task getStudentReturnsNullWhenStudentNotFound()
        {
            //arrange/act
            var actionResult = await _repo.GetStudent(-1);
            var student = (StudentDTO)actionResult;

            //Assert
            Assert.Null(student);
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
        public async Task getLessonPlansInvalidStudentReturnsEmptyList()
        {
            //arrange
            int studentID = -1;

            //act
            var actionResult = await _repo.GetLessonPlans(studentID);
            var lessonPlans = (IEnumerable<LessonPlanDTO>)actionResult;

            //assert
            Assert.Empty(lessonPlans);
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

        [Fact]
        public async Task getLessonPlanInvalidIDReturnsNull()
        {
            //arrange
            int lessonID = -1;

            //act
            var actionResult = await _repo.GetLessonPlan(lessonID);
            var lessonPlan = (LessonPlanDTO)actionResult;

            //assert
            Assert.Null(lessonPlan);
        }

        [Fact]
        public async Task getAssignmentsReturnsDTOs()
        {
            //arrange
            //act
            var actionResult = await _repo.GetAssignments();
            var assignments = (IEnumerable<AssignmentDTO>)actionResult;

            //assert
            Assert.NotEmpty(assignments);
        }

        [Fact]
        public async Task getAssignmentByLessonIDReturnsDTOs()
        {
            //arrange
            int lessonID = 1;

            //Act
            var actionResult = await _repo.GetAssignments(lessonID);
            var assignments = (IEnumerable<AssignmentDTO>)actionResult;

            //assert
            Assert.NotEmpty(assignments);
            Assert.True(assignments.All(p => p.lessonID == lessonID));
        }

        [Fact]
        public async Task getAssignmentsNotFoundReturnsEmptyList()
        {
            //arrange
            int lessonID = -1;

            //Act
            var actionResult = await _repo.GetAssignments(lessonID);
            var assignments = (IEnumerable<AssignmentDTO>)actionResult;

            //assert
            Assert.Empty(assignments);
        }

        [Fact]
        public async Task getAssignmentReturnsAssignment()
        {
            //arrange
            int id = 1;

            //act
            var actionResult = await _repo.GetAssignment(id);
            var assignment = (AssignmentDTO)actionResult;

            //assert
            Assert.Equal(id, assignment.assignmentID);
        }

        [Fact]
        public async Task getAssignmentBadRequestReturnsNull()
        {
            //arrange
            int id = -1;

            //act
            var actionResult = await _repo.GetAssignment(id);
            var assignment = (AssignmentDTO)actionResult;

            //assert
            Assert.Null(assignment);
        }

        [Fact]
        public async Task getAssignmentByLessonIDsReturnsDTOs()
        {
            //arrange
            int[] lessonIds = new int[] { 1, 2 };

            //Act
            var actionResult = await _repo.GetAssignments(lessonIds);
            var assignments = (IEnumerable<AssignmentDTO>)actionResult;

            //assert
            Assert.NotEmpty(assignments.Where(p => p.lessonID == lessonIds[0]));
            Assert.NotEmpty(assignments.Where(p => p.lessonID == lessonIds[1]));
            Assert.True(assignments.All(p => p.lessonID == lessonIds[0] || p.lessonID == lessonIds[1]));
        }

        [Fact]
        public async Task addAssignementAddsDeleteAssignmentDeletes()
        {
            //Double Test of Insert and Delete. Not exactly a unit test, but practical. Does mutate the data if the test fails in the middle.
            //Will be less necessary if Test DB was setup and torn down after each test run.
            //arrange
            var newAssignment = new AssignmentDTO()
            {
                lessonID = -5000,
                description = "Test Assignemnt",
                practiceNotes = "Test Notes"
            };

            //Act
            var addedAssignment = await _repo.AddAssignment(newAssignment);

            //Assert
            //TODO: Build an equality function for this
            Assert.Equal(newAssignment.lessonID, addedAssignment.lessonID);
            Assert.Equal(newAssignment.description, addedAssignment.description);
            Assert.Equal(newAssignment.practiceNotes, addedAssignment.practiceNotes);

            //Act2
            await _repo.DeleteAssignment(addedAssignment.assignmentID);
            var deletedAssignemnt = await _repo.GetAssignment(addedAssignment.assignmentID);

            //Assert
            Assert.Null(deletedAssignemnt);
        }

        [Fact]
        public async Task addLessonPlanAddsDeleteAssignmentDeletes()
        {
            //Double Test of Insert and Delete. Not ideal, see above.
            //arrange
            var newLessonPlan = new LessonPlanDTO()
            {
                StudentID = 5000,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMinutes(30)
            };

            //Act
            var addedLesson = await _repo.AddLessonPlan(newLessonPlan);

            //Assert
            //TODO: Build an equality function for this
            Assert.Equal(newLessonPlan.StudentID, addedLesson.StudentID);
            Assert.Equal(newLessonPlan.StartDate, addedLesson.StartDate);
            Assert.Equal(newLessonPlan.EndDate, addedLesson.EndDate);

            //Act2
            await _repo.DeleteAssignment(addedLesson.LessonID);
            var deletedLesson = await _repo.GetAssignment(addedLesson.LessonID);

            //Assert
            Assert.Null(deletedLesson);
        }
    }
}
