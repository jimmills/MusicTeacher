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

        public bool IsValid()
        {
            //TODO: Add more validation. Probably refactor it.
            //Lesson ID is "valid" if > 0
            if (LessonID <= 0)
            {
                return false;                
            }
            return true;
        }
    }
}
