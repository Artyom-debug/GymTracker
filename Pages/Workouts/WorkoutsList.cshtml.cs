using GymTrackerProject.Commands;
using GymTrackerProject.Data;
using GymTrackerProject.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace GymTrackerProject.Pages.Workouts;

public class WorkoutListModel : PageModel
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;
    public List<WorkoutDto> Workouts { get; set; } = new();
    public int WorkoutCount { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Укажите название")]
    public string NewWorkoutName { get; set; }

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

    public async Task<IActionResult> OnPostDeleteWorkout(int id)
    {
        await _mediator.Send(new DeleteWorkoutCommand(id));
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostRenameWorkout(int id)
    {
        if (id < 0)
            return NotFound();
        if (!ModelState.IsValid)
        {
            await OnGet();
            return Page();
        }
        await _mediator.Send(new UpdateWorkoutCommand(id, NewWorkoutName));
        return RedirectToPage();
    }
}
