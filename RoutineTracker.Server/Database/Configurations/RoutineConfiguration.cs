using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoutineTracker.Server.Entities;

namespace RoutineTracker.Server.Database.Configurations
{
    public class RoutineConfiguration : IEntityTypeConfiguration<Routine>
    {
        public void Configure(EntityTypeBuilder<Routine> builder)
        {
            builder.HasKey(x => x.RoutineId);
            builder.Property(x => x.RoutineId)
                .IsRequired()
                .HasConversion(
                routineId => routineId.Value,
                value => new RoutineId(value)
                );


            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(550);


            builder.Property(x => x.Days)
                .IsRequired();


            builder.Property(x => x.Duration)
                .IsRequired();


            builder.HasMany(x => x.Exercises).WithOne(r => r.Routine)
                .HasForeignKey(r => r.RoutineId).HasPrincipalKey(x => x.RoutineId);
        }
    }
}
