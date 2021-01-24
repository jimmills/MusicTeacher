using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicTeacher.Models;

namespace MusicTeacher.Managers
{
    public interface ILessonPlanManager
    {
        Task<IEnumerable<LessonPlan>> GetLessonPlans();
        Task<IEnumerable<LessonPlan>> GetLessonPlans(int studentID);
        Task<LessonPlan> GetLessonPlan(int Id);

        Task<IEnumerable<Assignment>> GetAssignments(int lessonId);
        Task<Assignment> GetAssignment(int Id);
    }
}
