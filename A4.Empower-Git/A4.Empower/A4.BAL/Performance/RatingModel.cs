using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL
{
    public class RatingModel
    {
        public RatingModel()
        {
            RatingList = new List<Ratings>();
            MidYearRating = new Rating();
            EndYearRating = new Rating();
            CheckSaveSubmit = new CheckSaveSubmit();
        }
        public string InstructionText { get; set; }
        public List<Ratings> RatingList { get; set; }
        public Rating MidYearRating;
        public Rating EndYearRating;
        public CheckSaveSubmit CheckSaveSubmit;
    }
    
    public class Rating
    {
        public string RatingId;
        public string ManagerComment;
        public string ManagerSignature;
        public string EmployeeComment;
    }

    public class Ratings
    {
        public string RatingId { get; set; }
        public string RatingName { get; set; }
        public string RatingDescription { get; set; }


    }
}
