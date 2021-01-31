using System;
namespace MusicTeacher.Models.DTO
{
    public class LessonPlanDTO
    {
        public int LessonID { get; set; }
        public int StudentID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
