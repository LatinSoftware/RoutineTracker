namespace RoutineTracker.Server.Entities
{
    public class Routine
    {
        private Routine() { }
        public RoutineId RoutineId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public int Duration { get; init; }

        public ICollection<DayOfWeek> Days { get; private set; } = new HashSet<DayOfWeek>();
        public List<RoutineExercise> Exercises { get; private set; } = [];

        public Routine Create(string name, string description, int duration)
        {
            return new Routine
            {
                RoutineId = new RoutineId(Guid.NewGuid()),
                Name = name,
                Description = description,
                Duration = duration
            };
        }

        public void AddExercise(RoutineExercise exercise) => Exercises.Add(exercise);
        public void RemoveExercise(RoutineExercise exercise) => Exercises.Remove(exercise);
        public void AddDay(DayOfWeek day) => Days.Add(day);
        public void RemoveDay(DayOfWeek day) => Days.Remove(day);

    }

    public record RoutineId(Guid Value);
}
