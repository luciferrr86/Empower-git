using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL
{
   public class PerformanceEnum
    {
        public enum CrudStatus
        {
            Saved = 1,
            Failed = 2
        }

        public enum PerformanceStatus
        {

            Notstarted = 1,
            AwaitingToReleaseGoal = 2,
            AwaitingCEOSignOff = 3,
            GoalReleased = 4,
            RatingSignedOff = 5,
            EmployeeSaved = 7,
            EmployeeSubmitted = 8,
            Managersavedgoals = 9,
            RatingsassignedandsavedbyManager = 10,
            EmployeeSignsoff = 11,
            ManagerSignsoff = 12,
            Presidentscouncilsignsoff = 13,
            Allmangerreleasedprocessforemployee = 14
        }

        public enum FeedbackStatus
        {
            Success = 1,
            Expired = 2
        }

        public enum PerformanceConfig
        {
            EnablePresidentCouncil,
            EnableIntialRating,
            EnableDeltaAndPluses,
            EnableMidYear,
            EnableTrainingAndClasses,
            EnableSuperAdmin,
            EnableNextYear

        }
    }
}
