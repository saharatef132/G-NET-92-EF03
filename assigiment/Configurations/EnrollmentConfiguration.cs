using assigiment.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assigiment.Configurations;

public class EnrollmentConfiguration
    : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.HasKey(e => new
        {
            e.StudentId,
            e.CourseId
        });

        builder.Property(e => e.FinalGrade)
               .HasPrecision(5, 2);

        builder.HasOne(e => e.Student)
               .WithMany(s => s.Enrollments)
               .HasForeignKey(e => e.StudentId);

        builder.HasOne(e => e.Course)
               .WithMany(c => c.Enrollments)
               .HasForeignKey(e => e.CourseId);
    }
}