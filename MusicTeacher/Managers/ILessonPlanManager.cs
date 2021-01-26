using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicTeacher.Models;
using MusicTeacher.Models.DTO;

namespace MusicTeacher.Managers
{
    public interface ILessonPlanManager
    {
        Task<IEnumerable<LessonPlan>> GetLessonPlans();
        Task<IEnumerable<LessonPlan>> GetLessonPlans(int studentID);
        Task<LessonPlan> GetLessonPlan(int Id);

        Task<IEnumerable<Assignment>> GetAssignments(int lessonId);
        Task<Assignment> GetAssignment(int Id);

        Task<Assignment> InsertAssignment(AssignmentDTO assignment); //Could refactor to a save method that upserts
        Task DeleteAssignment(int Id);
    }
}
