using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MusicTeacher.Models;
using MusicTeacher.Repos;
using System.Linq;
using MusicTeacher.Models.DTO;

namespace MusicTeacher.Managers
{
    public class StudentManager : IStudentManager
    {
        private readonly ILogger<StudentManager> _logger;
        private readonly IMusicTeacherRepo _repo;

        public StudentManager(ILogger<StudentManager> logger, IMusicTeacherRepo repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            _logger.LogInformation("GetStudents() method called");

            //Get the Students Raw Data
            var studentDTOs = await GetStudentDTOs();

            //Map to Model
            
            
            return new List<Student>() {
                new Student() {Id = 1}
            };
        }

        protected async Task<IEnumerable<StudentDTO>> GetStudentDTOs()
        {
            return await _repo.GetStudents();
        }

        protected Student GetStudentFromDTO(StudentDTO)
        {

        }

    }
}
