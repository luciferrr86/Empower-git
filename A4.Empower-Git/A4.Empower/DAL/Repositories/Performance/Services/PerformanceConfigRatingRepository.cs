using A4.BAL;
using A4.DAL.Entites;
using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Repositories
{
   public class PerformanceConfigRatingRepository : Repository<PerformanceConfigRating>, IPerformanceConfigRatingRepository
    {
        public PerformanceConfigRatingRepository(DbContext context) : base(context)
        { }
            private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
        public override PerformanceConfigRating Get(Guid id)
        {
            var performanceConfigRating = _appContext.PerformanceConfigRating.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return performanceConfigRating;
        }
        public override void Add(PerformanceConfigRating entity)
        {
            base.Add(entity);
            _context.SaveChanges();
        }

        public override void Update(PerformanceConfigRating entity)
        {
            base.Update(entity);
            _context.SaveChanges();
        }

        public override void Remove(PerformanceConfigRating entity)
        {
            base.Remove(entity);
            _context.SaveChanges();
        }

        public List<Ratings> GetRatingList()
        {
            var rating = new List<Ratings>();
            var ratingObj = _appContext.PerformanceConfigRating.AsQueryable().Where(x=>x.IsActive==true);
            if (ratingObj.Count() > 0)
            {
                foreach (var item in ratingObj)
                {
                    rating.Add(new Ratings { RatingId = item.Id.ToString(), RatingName = item.RatingName, RatingDescription = item.RatingDescription });
                }
            }
            return rating;
        }
    }
    
}
