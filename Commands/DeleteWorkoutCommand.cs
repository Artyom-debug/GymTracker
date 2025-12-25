using GymTrackerProject.Data;
using System.Data;

namespace GymTrackerProject.Commands;

public record DeleteWorkoutCommand(int Id) : IRequest;

public class DeleteWorkoutCommandHandler : IRequestHandler<DeleteWorkoutCommand>
{
    private readonly ApplicationDbContext _context;

    public DeleteWorkoutCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteWorkoutCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Workouts.FindAsync(command.Id, cancellationToken);
        if (entity == null)
        {
            throw new Exception("Unable to find workout.");
        }
        _context.Workouts.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

