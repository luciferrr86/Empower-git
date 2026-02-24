using A4.DAL.Entites;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
    public class JobCandidateWorkExperienceRepository : Repository<JobCandidateWorkExperience>, IJobCandidateWorkExperienceRepository
    {
        public JobCandidateWorkExperienceRepository(DbContext context) : base(context)
        {
        }
    }
}
