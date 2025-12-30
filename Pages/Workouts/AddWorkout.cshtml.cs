using GymTrackerProject.Commands;
using GymTrackerProject.Data;
using GymTrackerProject.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GymTrackerProject.Pages.Workouts;

public class AddWorkoutModel : PageModel
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;

    public AddWorkoutModel(IMediator mediator, UserManager<ApplicationUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    [BindProperty]
    public string WorkoutName { get; set; } = string.Empty;

    public IActionResult OnGetAsync()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if(!ModelState.IsValid)
        {
            return Page();
        }
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToPage("/Account/Login");
        }
        var userId = user.Id;
        var command = new CreateWorkoutCommand(WorkoutName, userId);
        var workoutId = await _mediator.Send(command);
            
        return RedirectToPage("/Workouts/WorkoutsList");
    }
}
