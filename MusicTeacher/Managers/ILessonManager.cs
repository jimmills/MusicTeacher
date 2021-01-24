using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicTeacher.Models;

namespace MusicTeacher.Managers
{
    public interface ILessonManager
    {
        Task<IEnumerable<LessonPlan>> GetLessonPlans();
        Task<IEnumerable<LessonPlan>> GetLessonPlans(string studentID);
        Task<IEnumerable<LessonPlan>> GetLessonPlan(string lessonID);
    }
}
