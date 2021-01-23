using System;
namespace MusicTeacher.Models.DTO
{
    public class StudentDTO
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Instrument { get; set; }
        public string LessonWindow { get; set; }
    }
}
