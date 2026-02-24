using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class MassInterviewScheduleCandidateMap : IEntityTypeConfiguration<MassInterviewScheduleCandidate>
    {
        public void Configure(EntityTypeBuilder<MassInterviewScheduleCandidate> builder)
        {
            {
                builder.HasOne(m => m.JobCandidateProfile).WithOne().HasForeignKey<MassInterviewScheduleCandidate>(m => m.CandidateId).HasConstraintName("ForeignKey_MassInterviewScheduleCandiate_CandidateProfile");
                builder.HasOne(m => m.MassInterviewRoom).WithMany(u => u.MassInterviewScheduleCandidate).HasForeignKey(m => m.RoomId).HasConstraintName("ForeignKey_MassInterviewScheduleCandiate_MassInterviewRoom");
                builder.HasOne(m => m.MassInterviewPanel).WithMany(u => u.MassInterviewScheduleCandidate).HasForeignKey(m => m.PanelId).HasConstraintName("ForeignKey_MassInterviewScheduleCandiate_MassInterviewPanel");
            //    builder.HasOne(m => m.MassInterview).WithMany(u => u.MassInterviewScheduleCandidate).HasForeignKey(m => m.MassInterviewId).HasConstraintName("ForeignKey_MassInterviewScheduleCandiate_MassInterview");
                builder.ToTable("MassInterviewScheduleCandidate");
            }
        }
    }
}
