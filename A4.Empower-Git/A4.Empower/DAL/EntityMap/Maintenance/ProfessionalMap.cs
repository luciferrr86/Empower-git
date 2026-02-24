using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace A4.DAL.EntityMap
{
   public class ProfessionalMap :IEntityTypeConfiguration<Professional>
    {
        public void Configure(EntityTypeBuilder<Professional> builder)
        {
            builder.HasOne(m => m.Employee).WithMany(m => m.Professional).HasForeignKey(m => m.EmployeeId).HasConstraintName("ForeignKey_Professional_Employee").OnDelete(DeleteBehavior.Cascade);

            //builder.HasOne(m => m.Employee).WithOne(m => m.Professional).HasForeignKey<Professional>(m => m.EmployeeId).HasConstraintName("ForeignKey_Professional_Employee").OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("EmployeeProfessionalDetail");
        }
    }
}
