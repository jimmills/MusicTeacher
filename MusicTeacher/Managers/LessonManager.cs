using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MusicTeacher.Models;
using MusicTeacher.Models.DTO;
using MusicTeacher.Repos;

namespace MusicTeacher.Managers
{
    public class LessonManager : ILessonManager
    {
        private readonly ILogger<LessonManager> _logger;
        private readonly IMusicTeacherRepo _repo;

        public LessonManager(ILogger<LessonManager> logger, IMusicTeacherRepo repo)
        {
            _logger = logger;
            _repo = repo;
        }

        //Returns All Lesson Plans
        public async Task<IEnumerable<LessonPlan>> GetLessonPlans()
        {
            _logger.LogInformation("GetLessonPlans() method called");

            //Get the LessonPlan Raw Data
            var lessonPlanDTOs = await _repo.GetLessonPlans();

            //Build lessonPlan collection
            var lessonPlans = new List<LessonPlan>();
            foreach (var dto in lessonPlanDTOs)
            {
                //Map to model
                lessonPlans.Add(GetLessonPlanFromDTO(dto));
            }

            return lessonPlans.AsEnumerable();
        }

        public Task<IEnumerable<LessonPlan>> GetLessonPlan(string lessonID)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LessonPlan>> GetLessonPlans(string studentID)
        {
            throw new NotImplementedException();
        }

        //Convert a lessonPlanDTO to a lessonPlan
        public LessonPlan GetLessonPlanFromDTO(LessonPlanDTO lessonPlanDTO)
        {
            //Provide Custom Mapping Here
            return new LessonPlan()
            {
                ID = lessonPlanDTO.LessonID,
                StudentID = lessonPlanDTO.StudentID,
                StartDate = lessonPlanDTO.StartDate,
                EndDate = lessonPlanDTO.EndDate
            };
        }
    }
}
