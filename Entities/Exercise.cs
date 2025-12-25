using System.ComponentModel.DataAnnotations;

namespace GymTrackerProject.Entities;

public class Exercise
{
    public int Id { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
    public string? Category { get; set; }
    public List<ProgressTracker> Progress { get; set; } = new();

    public Exercise()
    { }
    public Exercise(string name, string category)
    { 
        Name = name; 
        Category = category;
    }
}
