using System;
namespace MusicTeacher.Models
{
    public class Assignment : APITransferClass
    {
        public int Id { get; set; }
        public int LessonID { get; set; }
        public string Description { get; set; }
        public string PracticeNotes { get; set; }

        public Assignment()
        {
        }
    }
}
