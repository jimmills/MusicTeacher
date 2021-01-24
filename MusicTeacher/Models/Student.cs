using System;
using System.Collections.Generic;

namespace MusicTeacher.Models
{
    public class Student : APITransferClass
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Instrument { get; set; } //could be an enum
        public string LessonWindow { get; set; }

        public Student()
        {

        }

    }
}
