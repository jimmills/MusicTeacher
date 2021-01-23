using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicTeacher.Models;

namespace MusicTeacher.Repos
{
    public interface IMusicTeacherRepo
    {
        Task<IEnumerable<Student>> GetStudents();
    }
}
