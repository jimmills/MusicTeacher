using System;
namespace MusicTeacher.Models.DTO
{
    public class AssignmentDTO
    {
        public int assignmentID { get; set; }
        public int lessonID { get; set; }
        public string description { get; set; }
        public string practiceNotes { get; set; }
    }
}
