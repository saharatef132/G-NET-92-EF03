using assigiment.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assigiment.Configurations;

public class CategoryConfiguration
    : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Name)
               .HasMaxLength(100)
               .IsRequired();

        builder.HasOne(x => x.ParentCategory)
               .WithMany(x => x.SubCategories)
               .HasForeignKey(x => x.ParentCategoryId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(

            new Category
            {
                Id = 1,
                Name = "Programming"
            },

            new Category
            {
                Id = 2,
                Name = "Backend",
                ParentCategoryId = 1
            },

            new Category
            {
                Id = 3,
                Name = "ASP.NET",
                ParentCategoryId = 2
            },

            new Category
            {
                Id = 4,
                Name = "Design"
            },

            new Category
            {
                Id = 5,
                Name = "UI/UX",
                ParentCategoryId = 4
            }

        );
    }
}