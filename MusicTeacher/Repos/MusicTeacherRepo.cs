using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MusicTeacher.Models.DTO;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace MusicTeacher.Repos
{
    public class MusicTeacherRepo : IMusicTeacherRepo
    {
        private readonly string _connString;
        private readonly ILogger _logger;

        private const string _studentSelect = @"select studentID, firstName, lastName, instrument, lessonWindow";
        private const string _lessonPlanSelect = @"select lessonID, studentID, startDate, endDate";
        private const string _assignmentSelect = @"select assignmentID, lessonID, description, practiceNotes";


        public MusicTeacherRepo(ILogger<MusicTeacherRepo> logger, IConfiguration configuration) : this(logger, configuration.GetConnectionString("MusicTeacherDB")) { }

        public MusicTeacherRepo(ILogger<MusicTeacherRepo> logger, string connString)
        {
            _logger = logger;
            _connString = connString;
        }

        public async Task<IEnumerable<StudentDTO>> GetStudents()
        {
            using var conn = new SqliteConnection(_connString);
            SelectQuery studentQuery = new SelectQuery()
            {
                Select = _studentSelect,
                From = "from student"
            };

            return await conn.QueryAsync<StudentDTO>(studentQuery.ToString());
        }

        public async Task<IEnumerable<LessonPlanDTO>> GetLessonPlans()
        {
            using var conn = new SqliteConnection(_connString);
            SelectQuery lessonPlanQuery = new SelectQuery()
            {
                Select = _lessonPlanSelect,
                From = "from lessonPlan"
            };
            return await conn.QueryAsync<LessonPlanDTO>(lessonPlanQuery.ToString());
        }

        public async Task<IEnumerable<LessonPlanDTO>> GetLessonPlans(int studentID)
        {
            using var conn = new SqliteConnection(_connString);
            SelectQuery lessonPlanQuery = new SelectQuery()
            {
                Select = _lessonPlanSelect,
                From = "from lessonPlan",
                Where = "where studentID = :studentID"
            };
            lessonPlanQuery.Parms.studentID = studentID;

            return await conn.QueryAsync<LessonPlanDTO>(lessonPlanQuery.ToString(), (object)lessonPlanQuery.Parms);
        }

        public async Task<LessonPlanDTO> GetLessonPlan(int id)
        {
            using var conn = new SqliteConnection(_connString);
            SelectQuery lessonPlanQuery = new SelectQuery()
            {
                Select = _lessonPlanSelect,
                From = "from lessonPlan",
                Where = "where lessonID = :lessonID"
            };
            lessonPlanQuery.Parms.lessonID = id;

            return await conn.QuerySingleOrDefaultAsync<LessonPlanDTO>(lessonPlanQuery.ToString(), (object)lessonPlanQuery.Parms);
        }

        public async Task<AssignmentDTO> GetAssignment(int id)
        {
            using var conn = new SqliteConnection(_connString);
            var query = new SelectQuery()
            {
                Select = _assignmentSelect,
                From = "from assignment",
                Where = "where assignmentID = :id"
            };
            query.Parms.id = id;
            return await conn.QuerySingleOrDefaultAsync<AssignmentDTO>(query.ToString(), (object)query.Parms);
        }

        public async Task<IEnumerable<AssignmentDTO>> GetAssignments()
        {
            using var conn = new SqliteConnection(_connString);
            var query = new SelectQuery()
            {
                Select = _assignmentSelect,
                From = "from assignment"
            };
            return await conn.QueryAsync<AssignmentDTO>(query.ToString());
        }

        public async Task<IEnumerable<AssignmentDTO>> GetAssignments(int lessonID)
        {
            using var conn = new SqliteConnection(_connString);
            var query = new SelectQuery()
            {
                Select = _assignmentSelect,
                From = "from assignment",
                Where = "where lessonID = :lessonID"
            };
            query.Parms.lessonID = lessonID;
            return await conn.QueryAsync<AssignmentDTO>(query.ToString(), (object)query.Parms);
        }

        public async Task<IEnumerable<AssignmentDTO>> GetAssignments(int[] lessonIDs)
        {
            using var conn = new SqliteConnection(_connString);
            var query = new SelectQuery()
            {
                Select = _assignmentSelect,
                From = "from assignment",
                Where = "where lessonID in :lessonIDs"
            };
            query.Parms.lessonIDs = lessonIDs;
            return await conn.QueryAsync<AssignmentDTO>(query.ToString(), (object)query.Parms);
        }

        public async Task<AssignmentDTO> AddAssignment(AssignmentDTO assignment)
        {
            int id;
            using (var conn = new SqliteConnection(_connString))
            {
                id = conn.QuerySingle<int>(@"Insert into assignment(lessonId,description, practiceNotes)
                               values(@lessonID, @description, @practiceNotes);
                               select last_insert_rowid()", assignment);
                //var idobj = conn.QuerySingle("select last_insert_rowid() as id");
                //id = assignment.assignmentID;

            }
            return await this.GetAssignment(id);
        }

        public async Task DeleteAssignment(int id)
        {
            using var conn = new SqliteConnection(_connString);
            await conn.ExecuteAsync(@"Delete from assignment where assignmentId = :id", new { id = id });
        }
    }
}
