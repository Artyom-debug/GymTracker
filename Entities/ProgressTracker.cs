namespace GymTrackerProject.Entities;

public class ProgressTracker
{
    public int Id { get; set; }
    public int ExerciseId { get; set; }
    public Exercise Exercise { get; set; } = null!;
    public int WorkoutId { get; set; }
    public Workout Workout { get; set; } = null!;
    public double Weight { get; set; } = default;
    public int Reps {  get; set; } = default;
    public DateTime UpdatedProgress { get; set; } = DateTime.Now;

    public ProgressTracker()
    { }

    public ProgressTracker(double weight, int reps)
    {
        if(reps < 0 || weight < 0)
        {
            throw new InvalidDataException("Вес или количество повторений не могут быть отрицательными.");
        }
        Weight = weight;
        Reps = reps;
    }
}

public class InvalidDataException : Exception
{
    public InvalidDataException() { }
    public InvalidDataException(string message) : base(message) { }
}
