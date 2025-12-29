using GymTrackerProject.Data;

namespace GymTrackerProject.Commands;

public record DeleteExerciseCommand(int exerciseId) : IRequest;

public class DeleteExerciseCommandHandler : IRequestHandler<DeleteExerciseCommand>
{
    private readonly ApplicationDbContext _context;

    public DeleteExerciseCommandHandler(ApplicationDbContext context) =>
        _context = context;

    public async Task Handle(DeleteExerciseCommand command, CancellationToken cancellationToken)
    {
        var exId = command.exerciseId;
        var trackers = await _context.Trackers.
            Where(t => t.ExerciseId == exId)
            .ToListAsync(cancellationToken);
        _context.Trackers.RemoveRange(trackers);
        await _context.SaveChangesAsync();
    }
}


