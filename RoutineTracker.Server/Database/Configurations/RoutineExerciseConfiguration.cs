using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoutineTracker.Server.Entities;

namespace RoutineTracker.Server.Database.Configurations
{
    public class RoutineExerciseConfiguration : IEntityTypeConfiguration<RoutineExercise>
    {
        public void Configure(EntityTypeBuilder<RoutineExercise> builder)
        {
            builder.HasKey(x => new { x.RoutineId, x.ExerciseId });
            builder.Property(x => x.RoutineId).IsRequired().HasConversion(routineId => routineId.Value, value => new RoutineId(value));
            builder.Property(x => x.ExerciseId).IsRequired().HasConversion(exerciseId => exerciseId.Value, value => new ExerciseId(value));

            builder.Property(x => x.BreakTime).IsRequired();
            builder.Property(x => x.Sets).IsRequired();
            builder.Property(x => x.Reps).IsRequired();
        }
    }
}
