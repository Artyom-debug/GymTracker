using GymTrackerProject.Data;
using GymTrackerProject.Entities;

namespace GymTrackerProject.Commands;

public record AddProgressCommand(int ExerciseId, int WorkoutId, double Weight, int Reps) : IRequest<int>;

public class AddProgressCommandHandler : IRequestHandler<AddProgressCommand, int>
{
    private readonly ApplicationDbContext _context;

    public AddProgressCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(AddProgressCommand command, CancellationToken cancellationToken)
    {
        var entity = new ProgressTracker
        {
            ExerciseId = command.ExerciseId,
            WorkoutId = command.WorkoutId,
            Weight = command.Weight,
            Reps = command.Reps
        };
        _context.Trackers.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
