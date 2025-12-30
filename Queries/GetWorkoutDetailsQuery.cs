using GymTrackerProject.Data;

namespace GymTrackerProject.Queries;

public record GetWorkoutDetailsQuery(int WorkoutId) : IRequest<List<WorkoutDetailsDto>>;

public record WorkoutDetailsDto(int ExerciseId, string? ExerciseName, string? ExerciseCategory, List<ExerciseSets> Progress);
public record ExerciseSets(int SetId, double Weight, int Reps, DateTime UpdatedDate);

public class GetWorkoutDetailsQueryHandler : IRequestHandler<GetWorkoutDetailsQuery, List<WorkoutDetailsDto>>
{
    private readonly ApplicationDbContext _context;

    public GetWorkoutDetailsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<WorkoutDetailsDto>> Handle(GetWorkoutDetailsQuery query, CancellationToken cancellationToken)
    {
        var id = query.WorkoutId;
        var trackers = await _context.Trackers
            .Where(t => t.WorkoutId == id)
            .Include(t => t.Exercise)
            .ToListAsync(cancellationToken);

        var workoutDetails = trackers
            .GroupBy(t => new { t.ExerciseId, t.Exercise.Name, t.Exercise.Category })
            .Select(g => new WorkoutDetailsDto(
                    ExerciseId: g.Key.ExerciseId,
                    ExerciseName: g.Key.Name,
                    ExerciseCategory: g.Key.Category,
                    Progress: g
                        .OrderByDescending(t => t.UpdatedProgress)
                        .Select(t => new ExerciseSets(
                                Weight: t.Weight,
                                Reps: t.Reps,
                                UpdatedDate: t.UpdatedProgress,
                                SetId: t.Id
                            ))
                        .ToList()
             ))
            .ToList();

        return workoutDetails;
    }
}

