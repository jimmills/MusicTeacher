using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MusicTeacher.Models;
using Dapper;
using Microsoft.Data.Sqlite;

namespace MusicTeacher.Repos
{
    public class MusicTeacherRepo : IMusicTeacherRepo
    {
        private readonly string _connString;

        public MusicTeacherRepo(ILogger logger, string connString)
        {
            _connString = connString;
        }

        //public IEnumerable<Student> GetStudents()
        //{
        //    using (var conn = new SqliteConnection(_connString))
        //    {
        //        conn.Open();

        //        var students = conn.Query<Student>(
        //            @"select studentid as id, firstName, lastName, instrument, lessonWindow
        //                from student");

        //        return students;

        //    }
        //}

        public async Task<IEnumerable<Student>> GetStudents()
        {
            using var conn = new SqliteConnection(_connString);
            return await conn.QueryAsync<Student>(
                    @"select studentid as id, firstName, lastName, instrument, lessonWindow
                        from student");
        }

    }
}
