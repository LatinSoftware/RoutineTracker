using Microsoft.EntityFrameworkCore;
using RoutineTracker.Server.Database.Configurations;
using RoutineTracker.Server.Entities;

namespace RoutineTracker.Server.Database
{
    public class ApplicationContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Routine> Routines { get; set; }
        public DbSet<RoutineExercise> RoutineExercises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExerciseConfiguration).Assembly);
        }

    }
}
