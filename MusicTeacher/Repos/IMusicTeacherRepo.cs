﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicTeacher.Models.DTO;

namespace MusicTeacher.Repos
{
    public interface IMusicTeacherRepo
    {
        Task<IEnumerable<StudentDTO>> GetStudents();

        Task<LessonPlanDTO> GetLessonPlan(int id);
        Task<IEnumerable<LessonPlanDTO>> GetLessonPlans();
        Task<IEnumerable<LessonPlanDTO>> GetLessonPlans(int studentID);

        Task<AssignmentDTO> GetAssignment(int id);
        Task<IEnumerable<AssignmentDTO>> GetAssignments();
        Task<IEnumerable<AssignmentDTO>> GetAssignments(int lessonID);
        Task<IEnumerable<AssignmentDTO>> GetAssignments(int[] lessonIDs);
    }
}
