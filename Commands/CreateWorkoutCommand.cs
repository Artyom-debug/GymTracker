using GymTrackerProject.Data;
using GymTrackerProject.Entities;

namespace GymTrackerProject.Commands;

public record CreateWorkoutCommand(string name, string UserId) : IRequest<int>;

public class CreateWorkoutCommandHandler : IRequestHandler<CreateWorkoutCommand, int>
{
    private readonly ApplicationDbContext _context;

    public CreateWorkoutCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateWorkoutCommand command, CancellationToken cancellationToken)
    {
        var entity = new Workout
        { 
            Name = command.name,
            UsersId = command.UserId
        };
        _context.Workouts.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}