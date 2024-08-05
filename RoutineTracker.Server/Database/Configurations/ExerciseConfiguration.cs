using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoutineTracker.Server.Entities;

namespace RoutineTracker.Server.Database.Configurations
{
    public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.HasKey(x => x.ExerciseId);
            builder.Property(x => x.ExerciseId)
                .IsRequired()
                .HasConversion(
                exerciseId => exerciseId.Value,
                value => new ExerciseId(value)
                );


            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(550);

            builder.Property(x => x.DemostrationUrl)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.Categories)
                .IsRequired();

            builder.HasMany(x => x.Routines).WithOne(r => r.Exercise)
                .HasForeignKey(r => r.ExerciseId).HasPrincipalKey(x => x.ExerciseId);
        }
    }
}
