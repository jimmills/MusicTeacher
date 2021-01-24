using System;
using System.Collections.Generic;

namespace MusicTeacher.Models
{
    public class LessonPlan : APITransferClass
    {
        public int Id { get; set; }
        public int StudentID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<Assignment> Assignments { get; set; }

        public LessonPlan()
        {
            Assignments = new List<Assignment>();
        }
    }
}
