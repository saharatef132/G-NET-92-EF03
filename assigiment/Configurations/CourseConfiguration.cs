using assigiment.Entities;
using assigiment.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assigiment.Configurations;

public class CourseConfiguration
    : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.Property(x => x.Title)
               .HasMaxLength(150)
               .IsRequired();

        builder.Property(x => x.Price)
               .HasPrecision(10, 2);

        builder.HasOne(x => x.Instructor)
               .WithMany(x => x.Courses)
               .HasForeignKey(x => x.InstructorId);

        builder.HasOne(x => x.Category)
               .WithMany(x => x.Courses)
               .HasForeignKey(x => x.CategoryId);

        builder.HasData(

            new Course
            {
                Id = 1,
                Title = "C# Fundamentals",
                Price = 1000,
                Level = CourseLevel.Beginner,
                IsPublished = true,
                PublishedDate = new DateTime(2024, 1, 1),
                InstructorId = 1,
                CategoryId = 3
            },

            new Course
            {
                Id = 2,
                Title = "ASP.NET Core",
                Price = 2000,
                Level = CourseLevel.Intermediate,
                IsPublished = true,
                PublishedDate = new DateTime(2024, 2, 1),
                InstructorId = 1,
                CategoryId = 3
            }

        );
    }
}
