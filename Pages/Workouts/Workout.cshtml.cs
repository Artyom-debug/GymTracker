using GymTrackerProject.Commands;
using GymTrackerProject.Data;
using GymTrackerProject.Entities;
using GymTrackerProject.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GymTrackerProject.Pages.Workouts;

public class WorkoutModel : PageModel
{
    private readonly IMediator _mediator;

    public WorkoutModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public List<WorkoutDetailsDto> Details { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public int WorkoutId { get; set; }

    [BindProperty(SupportsGet = true)]
    public string WorkoutName { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        if(WorkoutId <= 0)
            return NotFound();
        Details = await _mediator.Send(new GetWorkoutDetailsQuery(WorkoutId));
        if(Details == null)
            return NotFound();

        return Page();
    }

    //метод для отображения выпадающего списка
    public async Task<IActionResult> OnGetAllExercisesAsync()
    {
        if (!ModelState.IsValid)
            return Page();
        var exercises = await _mediator.Send(new GetExercisesQuery());
        return Partial("_AddExerciseForm", exercises);
    }

    public async Task<IActionResult> OnPostAddExerciseAsync(int exerciseId)
    {
        if(!ModelState.IsValid)
            return Page();
        await _mediator.Send(new AddProgressCommand(exerciseId, WorkoutId, 0, 0));
        return RedirectToPage("/Workout", new { WorkoutId, WorkoutName });
    }

    public async Task<IActionResult> OnPostAddProgressAsync(int exerciseId, double weight, int reps)
    {
        if(!ModelState.IsValid)
            return Page();
        await _mediator.Send(new AddProgressCommand(exerciseId, WorkoutId, weight, reps));
        return RedirectToPage(WorkoutId);
    }

    public async Task<IActionResult> OnPostUpdateProgressAsync(int trackerId, double weight, int reps)
    {
        if(!ModelState.IsValid)
            return Page();
        await _mediator.Send(new UpdateProgressCommand(trackerId, weight, reps));
        return RedirectToPage(WorkoutId);
    }

}


