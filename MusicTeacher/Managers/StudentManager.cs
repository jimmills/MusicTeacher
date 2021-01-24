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

            //Build students collection
            var students = new List<Student>();
            foreach (var dto in studentDTOs)
            {
                //Map to model
                students.Add(GetStudentFromDTO(dto));
            }            
            
            return students.AsEnumerable<Student>();
        }

        //Get all students from the data store as DTOs
        public async Task<IEnumerable<StudentDTO>> GetStudentDTOs()
        {
            return await _repo.GetStudents();
        }

        //Convert a studentDTO to a student
        public Student GetStudentFromDTO(StudentDTO studentDTO)
        {
            //Provide Custom Mapping Here
            return new Student()
            {
                Id = studentDTO.StudentID,
                FirstName = studentDTO.FirstName,
                LastName = studentDTO.LastName,
                Instrument = studentDTO.Instrument,
                LessonWindow = studentDTO.LessonWindow
            };
        }

    }
}
