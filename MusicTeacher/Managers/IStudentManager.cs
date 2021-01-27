using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicTeacher.Models;

namespace MusicTeacher.Managers
{
    public interface IStudentManager
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> GetStudent(int id);
    }
}
