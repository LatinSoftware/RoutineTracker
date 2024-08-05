namespace RoutineTracker.Server.Features.Exercises
{
        public record ExerciseModel
        {
            public required string Id { get; set; }
            public required string Name { get; set; }
            public required string Description { get; set; }
            public string? DemostrationUrl { get; set; }
            public List<string> Categories { get; set; } = [];
        }
    
}
