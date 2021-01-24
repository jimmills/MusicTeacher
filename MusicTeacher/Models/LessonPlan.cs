using System;
namespace MusicTeacher.Models
{
    public class LessonPlan
    {
        public int Id { get; set; }
        public int StudentID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
