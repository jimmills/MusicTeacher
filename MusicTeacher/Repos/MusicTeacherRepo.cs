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

        public Task<LessonPlanDTO> GetLessonPlan(int id)
        {
            throw new NotImplementedException();
        }
    }
}
