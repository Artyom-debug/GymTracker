using GymTrackerProject.Data;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GymTrackerProject.Entities;

public class Workout
{
    public int Id { get; set; }
    [Required] public string Name { get; set; } = string.Empty; 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<ProgressTracker>? ProgressTrackers { get; set; } = new();
    public string UsersId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;

    public Workout() { }

    public Workout(string name)
    {
        Name = name;
    }
}
