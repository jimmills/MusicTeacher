using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicTeacher.Models.DTO;

namespace MusicTeacher.Repos
{
    public interface IMusicTeacherRepo
    {
        Task<IEnumerable<StudentDTO>> GetStudents();
        Task<StudentDTO> GetStudent(int id);

        Task<LessonPlanDTO> GetLessonPlan(int id);
        Task<IEnumerable<LessonPlanDTO>> GetLessonPlans();
        Task<IEnumerable<LessonPlanDTO>> GetLessonPlans(int studentID);

        Task<LessonPlanDTO> AddLessonPlan(LessonPlanDTO lesson);
        Task DeleteLessonPlan(int id);

        Task<AssignmentDTO> GetAssignment(int id);
        Task<IEnumerable<AssignmentDTO>> GetAssignments();
        Task<IEnumerable<AssignmentDTO>> GetAssignments(int lessonID);
        Task<IEnumerable<AssignmentDTO>> GetAssignments(int[] lessonIDs);


        Task<AssignmentDTO> AddAssignment(AssignmentDTO assignment);
        Task DeleteAssignment(int id);
    }
}
