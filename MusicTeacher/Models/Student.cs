using System;
namespace MusicTeacher.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Instrument { get; set; } //should probably be an enum
        public string LessonWindow { get; set; }
    }
}
