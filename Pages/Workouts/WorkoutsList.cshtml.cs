using GymTrackerProject.Data;
using GymTrackerProject.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GymTrackerProject.Pages.Workouts;

public class WorkoutListModel : PageModel
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;
    public List<WorkoutDto> Workouts { get; set; } = new();
    public int WorkoutCount { get; set; }

    public WorkoutListModel(IMediator mediator, UserManager<ApplicationUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    public async Task<IActionResult> OnGet()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToPage("/Account/Login");
        }
        var id = user.Id;
        Workouts = await _mediator.Send(new GetUsersWorkoutQuery(id));
        WorkoutCount = Workouts.Count();
        return Page(); 
    }
}
