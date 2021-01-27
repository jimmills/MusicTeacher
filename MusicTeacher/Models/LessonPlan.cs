using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MusicTeacher.Models
{
    public class LessonPlan : APITransferClass
    {
        public int Id { get; set; }
        public int StudentID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<Assignment> Assignments { get; set; }

        public int? Duration
        {
            get
            {
                if(EndDate.HasValue && StartDate.HasValue)
                {
                    return (int)((DateTime)EndDate).Subtract((DateTime)StartDate).TotalMinutes;
                }
                else
                {
                    return null;
                }

            }
        }

        public LessonPlan()
        {
            Assignments = new List<Assignment>();
        }

        public bool IsValid()
        {
            //TODO: Add more validation. Probably refactor it, could return a collection of validation issues.

            //Lesson Plans must have a student ID.
            if (StudentID <= 0)
            {
                return false;
            }

            //End Date should only be populated if Start Date is populated
            if (EndDate.HasValue && !StartDate.HasValue)
            {
                return false;
            }

            //End Date should be after Start Date
            if(StartDate.HasValue && EndDate.HasValue)
            {
                if(EndDate <= StartDate)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
