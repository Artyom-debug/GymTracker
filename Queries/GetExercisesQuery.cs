using GymTrackerProject.Data;

namespace GymTrackerProject.Queries;

public record GetExercisesQuery() : IRequest<List<ExerciseDto>>;

public record ExerciseDto(int ExerciseId, string ExerciseName, string Category);

public class GetExercisesQueryHandler : IRequestHandler<GetExercisesQuery, List<ExerciseDto>>
{
    private readonly ApplicationDbContext _context;

    public GetExercisesQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ExerciseDto>> Handle(GetExercisesQuery query, CancellationToken cancellationToken)
    {
        var exercises = await _context.Exercises
            .Select(e => new ExerciseDto(e.Id, e.Name, e.Category))
            .ToListAsync();
        return exercises;
    }
}



