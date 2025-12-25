using GymTrackerProject.Entities;
using Microsoft.AspNetCore.Identity;

namespace GymTrackerProject.Data;

public class ApplicationUser : IdentityUser
{
    public List<Workout> Workouts { get; set; } = new();
}
