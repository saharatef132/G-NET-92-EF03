using assigiment.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assigiment.Configurations;

public class InstructorConfiguration
    : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.Property(x => x.FullName)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(x => x.Email)
               .HasMaxLength(100)
               .IsRequired();

        builder.HasData(

            new Instructor
            {
                Id = 1,
                FullName = "Ahmed Ali",
                Email = "ahmed@gmail.com",
                JoinDate = new DateTime(2024, 1, 1)
            },

            new Instructor
            {
                Id = 2,
                FullName = "Mohamed Hassan",
                Email = "mohamed@gmail.com",
                JoinDate = new DateTime(2024, 2, 1)
            },

            new Instructor
            {
                Id = 3,
                FullName = "Sara Mahmoud",
                Email = "sara@gmail.com",
                JoinDate = new DateTime(2024, 3, 1)
            }

        );
    }
}