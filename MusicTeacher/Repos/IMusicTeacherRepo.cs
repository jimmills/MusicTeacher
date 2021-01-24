﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicTeacher.Models.DTO;

namespace MusicTeacher.Repos
{
    public interface IMusicTeacherRepo
    {
        Task<IEnumerable<StudentDTO>> GetStudents();
        Task<IEnumerable<LessonPlanDTO>> GetLessonPlans();
        Task<IEnumerable<LessonPlanDTO>> GetLessonPlans(int studentID);
        Task<LessonPlanDTO> GetLessonPlan(int id);
    }
}
