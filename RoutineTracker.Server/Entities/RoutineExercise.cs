namespace RoutineTracker.Server.Entities
{
    public record RoutineExercise
    {
        public required RoutineId RoutineId { get; set; }
        public required ExerciseId ExerciseId { get; set; }
        public int BreakTime { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }


        public Routine? Routine { get; private set; }
        public Exercise? Exercise { get; private set; }
    }
}
