using System;
namespace MusicTeacher.Models
{
    public class LessonPlan
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
