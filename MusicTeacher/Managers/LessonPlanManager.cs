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

        //Returns All Lesson Plans - this would not be appropriate for a PRD implementation
        public async Task<IEnumerable<LessonPlan>> GetLessonPlans()
        {
            _logger.LogInformation("GetLessonPlans() method called");

            //Get the LessonPlan Raw Data - second query waits for first query because you need the IDs from query 1
            var lessonPlanDTOs = await _repo.GetLessonPlans();
            var assignmentDTOs = await _repo.GetAssignments(lessonPlanDTOs.Select(f => f.LessonID).ToArray<int>());

            //Build lessonPlan collection
            var lessonPlans = GetLessonPlansFromDTOs(lessonPlanDTOs, assignmentDTOs);

            return lessonPlans.AsEnumerable();
        }

        //Lesson Plans for a student
        public async Task<IEnumerable<LessonPlan>> GetLessonPlans(int studentID)
        {
            //first and second query can run parallel
            var lessonTask = _repo.GetLessonPlans(studentID);
            var assignmentTask = _repo.GetAssignments(new int[] { studentID });

            //Run both queries
            var queryTasks = new Task[] { lessonTask, assignmentTask };
            await Task.WhenAll(queryTasks);

            //assign results
            var lessonPlanDTOs = lessonTask.Result;
            var assignmentDTOs = assignmentTask.Result;

            //Build LessonPlan collection
            var lessonPlans = GetLessonPlansFromDTOs(lessonPlanDTOs, assignmentDTOs);
            return lessonPlans.AsEnumerable();
        }

        //Single Lesson Plan
        public async Task<LessonPlan> GetLessonPlan(int Id)
        {
            //first and second query can run parallel
            var lessonTask = _repo.GetLessonPlan(Id);
            var assignmentTask = _repo.GetAssignments(new int[] { Id });

            //Run both queries
            var queryTasks = new Task[] { lessonTask, assignmentTask };
            await Task.WhenAll(queryTasks);

            //assign results
            var lessonPlanDTO = lessonTask.Result;
            var assignmentDTOs = assignmentTask.Result;

            //Build lessonPlan
            return GetLessonPlanFromDTO(lessonPlanDTO, assignmentDTOs);
        }

        //Assignments for a lesson
        public async Task<IEnumerable<Assignment>> GetAssignments(int lessonID)
        {
            var assignmentDTOs = await _repo.GetAssignments(lessonID);
            var assignments = MapAllAssignmentsFromDTOs(assignmentDTOs);
            return assignments.AsEnumerable();
        }

        //Single Assignment
        public async Task<Assignment> GetAssignment(int Id)
        {
            return GetAssignmentFromDTO(await _repo.GetAssignment(Id));
        }


        //Convert a lessonPlanDTO to a lessonPlan
        public LessonPlan GetLessonPlanFromDTO(LessonPlanDTO lessonPlanDTO, IEnumerable<AssignmentDTO> assignmentDTOs)
        {
            //don't map it if null
            if(lessonPlanDTO == null)
            {
                return null;
            }

            //Provide Custom Mapping Here
            var plan = new LessonPlan()
            {
                Id = lessonPlanDTO.LessonID,
                StudentID = lessonPlanDTO.StudentID,
                StartDate = lessonPlanDTO.StartDate,
                EndDate = lessonPlanDTO.EndDate,
            };
            //plan.Assignments.AddRange(MapAllAssignmentsFromDTOs(assignmentDTOs.Where(p => p.lessonID == plan.Id)));
            plan.Assignments.AddRange(MapAllAssignmentsFromDTOs(assignmentDTOs));
            return plan;
        }

        //Gets the Lesson Plans and Assignments from the DTOs
        public List<LessonPlan> GetLessonPlansFromDTOs(IEnumerable<LessonPlanDTO> lessonPlanDTOs, IEnumerable<AssignmentDTO> assignmentDTOs)
        {
            List<LessonPlan> lessonPlans = new List<LessonPlan>();
            if (lessonPlanDTOs != null)
            {
                foreach (var dto in lessonPlanDTOs)
                {
                    lessonPlans.Add(GetLessonPlanFromDTO(dto, assignmentDTOs.Where(p => p.lessonID == dto.LessonID)));
                }
            }

            return lessonPlans;
        }

        public async Task<Assignment> InsertAssignment(AssignmentDTO assignmentDTO)
        {
            var assignment = GetAssignmentFromDTO(assignmentDTO); 
            //Just a basic validation - should be made more thorough
            if (assignment.IsValid())
            {
                var returnData = await _repo.AddAssignment(GetDTOFromAssignment(assignment));
                return GetAssignmentFromDTO(returnData);
            }
            throw new HttpResponseException() { Status = 400, Value = "Assignment data is invalid. You should fix it and try again." };
        }

        public async Task DeleteAssignment(int Id) => await _repo.DeleteAssignment(Id);


        public async Task<LessonPlan> InsertLessonPlan(LessonPlanDTO lessonPlanDTO)
        {
            var lessonPlan = GetLessonPlanFromDTO(lessonPlanDTO, null);
            if (lessonPlan.IsValid())
            {
                var returnData = await _repo.AddLessonPlan(GetDTOFromLessonPlan(lessonPlan));
                return GetLessonPlanFromDTO(returnData, null);
            }
            throw new HttpResponseException() { Status = 400, Value = "LessonPlan data is invalid. You should fix it and try again." };
        }

        public async Task DeleteLessonPlan(int Id) => await _repo.DeleteLessonPlan(Id);


        //Convert AssignmentDTO to Assignment
        public Assignment GetAssignmentFromDTO(AssignmentDTO dto)
        {
            //Don't map if null
            if(dto == null)
            {
                return null;
            }

            //Provide Custom Mapping Here
            return new Assignment()
            {
                Id = dto.assignmentID,
                LessonID = dto.lessonID,
                Description = dto.description,
                PracticeNotes = dto.practiceNotes
            };
        }

        private List<Assignment> MapAllAssignmentsFromDTOs(IEnumerable<AssignmentDTO> dtos)
        {
            List<Assignment> assignments = new List<Assignment>();
            if (dtos != null)
            {
                foreach (var dto in dtos)
                {
                    assignments.Add(GetAssignmentFromDTO(dto));
                }
            }
            return assignments;
        }

        private AssignmentDTO GetDTOFromAssignment(Assignment assignment)
        {
            return new AssignmentDTO(){
                assignmentID = assignment.Id,
                lessonID = assignment.LessonID,
                description = assignment.Description,
                practiceNotes = assignment.PracticeNotes
            };
        }

        private LessonPlanDTO GetDTOFromLessonPlan(LessonPlan lessonPlan)
        {
            return new LessonPlanDTO()
            {
                LessonID = lessonPlan.Id,
                StudentID = lessonPlan.StudentID,
                StartDate = lessonPlan.StartDate,
                EndDate = lessonPlan.EndDate
            };
        }

    }
}
