using GymTrackerProject.Data;
using GymTrackerProject.Entities;

namespace GymTrackerProject.Commands;

public record UpdateProgressCommand(int Id, double Weight, int Reps) : IRequest;

public class UpdateProgressCommandHandler : IRequestHandler<UpdateProgressCommand>
{
    private readonly ApplicationDbContext _context;

    public UpdateProgressCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateProgressCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Trackers.FindAsync(new object[] { command.Id }, cancellationToken);
        if (entity == null)
        {
            throw new Exception($"Не удалось найти запись прогресса с ID {command.Id}");
        }

        entity.Weight = command.Weight;
        entity.Reps = command.Reps;
        entity.UpdatedProgress = DateTime.Now;
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}




