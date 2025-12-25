using GymTrackerProject.Data;
using GymTrackerProject.Entities;
using GymTrackerProject.Services;

namespace GymTrackerProject.Queries;

public record GetUsersWorkoutQuery(string UserId) : IRequest<List<WorkoutDto>>;

public record WorkoutDto(int WorkoutId, string Name, DateTime CreatedDate);

public class GetUsersWorkoutQueryHandler : IRequestHandler<GetUsersWorkoutQuery, List<WorkoutDto>>
{
    private readonly ApplicationDbContext _context;

    public GetUsersWorkoutQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<WorkoutDto>> Handle(GetUsersWorkoutQuery query, CancellationToken cancellationToken)
    {
        var id = query.UserId;
        var workouts = await _context.Workouts
            .Where(w => w.UsersId == id)
            .OrderByDescending(w => w.CreatedAt)
            .Select(w => new WorkoutDto(w.Id, w.Name, w.CreatedAt))
            .ToListAsync();
        return workouts;
    }
}


