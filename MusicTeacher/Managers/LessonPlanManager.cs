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
    public class LessonPlanManager : ILessonPlanManager
    {
        private readonly ILogger<LessonPlanManager> _logger;
        private readonly IMusicTeacherRepo _repo;

        public LessonPlanManager(ILogger<LessonPlanManager> logger, IMusicTeacherRepo repo)
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
            var lessonPlans = MapAllFromDTOs(lessonPlanDTOs);

            return lessonPlans.AsEnumerable();
        }

        public async Task<IEnumerable<LessonPlan>> GetLessonPlans(int studentID)
        {
            var lessonPlanDTOs = await _repo.GetLessonPlans(studentID);
            var lessonPlans = MapAllFromDTOs(lessonPlanDTOs);
            return lessonPlans.AsEnumerable();
        }

        public async Task<LessonPlan> GetLessonPlan(int lessonID)
        {
            return GetLessonPlanFromDTO(await _repo.GetLessonPlan(lessonID));
        }


        //Convert a lessonPlanDTO to a lessonPlan
        public LessonPlan GetLessonPlanFromDTO(LessonPlanDTO lessonPlanDTO)
        {
            //don't map it if null
            if(lessonPlanDTO == null)
            {
                return null;
            }

            //Provide Custom Mapping Here
            return new LessonPlan()
            {
                Id = lessonPlanDTO.LessonID,
                StudentID = lessonPlanDTO.StudentID,
                StartDate = lessonPlanDTO.StartDate,
                EndDate = lessonPlanDTO.EndDate
            };
        }

        private List<LessonPlan> MapAllFromDTOs(IEnumerable<LessonPlanDTO> dtos)
        {
            List<LessonPlan> lessonPlans = new List<LessonPlan>();
            foreach (var dto in dtos)
            {
                lessonPlans.Add(GetLessonPlanFromDTO(dto));
            }
            return lessonPlans;
        }
    }
}
