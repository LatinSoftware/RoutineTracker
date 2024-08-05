namespace RoutineTracker.Server.Entities
{
    public class Exercise
    {
        private Exercise() { }
        private readonly List<string> categories = [];
        public ExerciseId ExerciseId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string? DemostrationUrl { get; private set; } = string.Empty;
        public List<string> Categories => categories;

        public List<RoutineExercise> Routines { get; private set; } = [];

        public void AddCategory(string category) => categories.Add(category);
        public void RemoveCategory(string category) => categories.Remove(category);

        public static Exercise Create(string name, string description, string? demostrationUrl = null, ExerciseId? exerciseId = null)
        {

            return new Exercise
            {
                ExerciseId = exerciseId??new ExerciseId(Guid.NewGuid()),
                Name = name,
                Description = description,
                DemostrationUrl = demostrationUrl
            };
        }
    }

    public record ExerciseId(Guid Value);
}
