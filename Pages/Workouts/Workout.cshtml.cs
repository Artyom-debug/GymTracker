using GymTrackerProject.Commands;
using GymTrackerProject.Data;
using GymTrackerProject.Entities;
using GymTrackerProject.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace GymTrackerProject.Pages.Workouts;

public class WorkoutModel : PageModel
{
    private readonly IMediator _mediator;

    public WorkoutModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public List<WorkoutDetailsDto> Details { get; set; } = new();

    public List<SelectListItem> Items { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public int WorkoutId { get; set; }

    [BindProperty(SupportsGet = true)]
    public string WorkoutName { get; set; }

    [BindProperty(SupportsGet = true)]
    [Required(ErrorMessage = "Выберите упражнение")]
    public int? SelectedExerciseId { get; set; }

    [BindProperty(SupportsGet = true)]
    [Required(ErrorMessage = "Введите повторения")]
    [Range(0, 1000000, ErrorMessage = "Повторения не могут быть меньше 0")]
    public int? Reps { get; set; }

    [BindProperty(SupportsGet = true)]
    [Required(ErrorMessage = "Введите вес")]
    [Range(0, 1000000, ErrorMessage = "Вес не может быть меньше 0")]
    public double? Weight {  get; set; }

    private async Task LoadExercises()
    { 
        var exercises = await _mediator.Send(new GetExercisesQuery());
        var loadedExercises = Details.Select(w => w.ExerciseId).ToHashSet();
        var available = exercises.Where(e => !loadedExercises.Contains(e.ExerciseId)).ToList();
        Items = available.Select(e => new SelectListItem {Value = e.ExerciseId.ToString(), Text = $"{e.ExerciseName}({e.Category})" }).ToList();
    }
    public async Task<IActionResult> OnGetAsync()
    {
        if(WorkoutId <= 0)
            return NotFound();
        Details = await _mediator.Send(new GetWorkoutDetailsQuery(WorkoutId));
        if(Details == null)
            return NotFound();

        await LoadExercises();
        return Page();
    }
    public async Task<IActionResult> OnPostAddExerciseAsync()
    {
        if(!ModelState.IsValid)
        {
            await LoadExercises();
            return Page();
        }
        await _mediator.Send(new AddProgressCommand(SelectedExerciseId!.Value, WorkoutId, 0, 0));
        return RedirectToPage(new { WorkoutId, WorkoutName });
    }

    public async Task<IActionResult> OnPostAddProgressAsync()
    {
        if(!ModelState.IsValid)
            return Page();
        await _mediator.Send(new AddProgressCommand(SelectedExerciseId!.Value, WorkoutId, Weight!.Value, Reps!.Value));
        return RedirectToPage( new { WorkoutId, WorkoutName });
    }

    //public async Task<IActionResult> OnPostUpdateProgressAsync(int trackerId, double weight, int reps)
    //{
    //    if(!ModelState.IsValid)
    //        return Page();
    //    await _mediator.Send(new UpdateProgressCommand(trackerId, weight, reps));
    //    return RedirectToPage(WorkoutId);
    //}

    public async Task<IActionResult> OnPostDeleteProgress(int id)
    {
        if(id <= 0) 
            return NotFound();
        await _mediator.Send(new DeleteProgressCommand(id));
        return RedirectToPage(new {WorkoutId, WorkoutName});
    }

    public async Task<IActionResult> OnPostDeleteExercise(int exId)
    {
        if (exId <= 0)
            return NotFound();
        await _mediator.Send(new DeleteExerciseCommand(exId));
        return RedirectToPage(new { WorkoutId, WorkoutName });
    }
}