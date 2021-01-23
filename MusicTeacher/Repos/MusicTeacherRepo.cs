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


        public MusicTeacherRepo(ILogger<MusicTeacherRepo> logger, IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("MusicTeacherDB");
        }

        public MusicTeacherRepo(ILogger<MusicTeacherRepo> logger, string connString)
        {
            _connString = connString;
        }

        public async Task<IEnumerable<StudentDTO>> GetStudents()
        {
            using var conn = new SqliteConnection(_connString);
            return await conn.QueryAsync<StudentDTO>(
                    @"select studentID, firstName, lastName, instrument, lessonWindow
                        from student");
        }

    }
}
