using GymTrackerProject.Data;

namespace GymTrackerProject.Commands;

public record UpdateWorkoutCommand(int Id, string Name) : IRequest;

public class UpdateWorkoutCommandHandler : IRequestHandler<UpdateWorkoutCommand>
{
    private readonly ApplicationDbContext _context;

    public UpdateWorkoutCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateWorkoutCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Workouts.FindAsync(command.Id, cancellationToken);
        if (entity == null)
        {
            throw new Exception("Unable to find workout");
        }
        entity.Name = command.Name;
        await _context.SaveChangesAsync(cancellationToken);
    }
}

