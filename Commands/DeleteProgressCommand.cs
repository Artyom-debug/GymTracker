using GymTrackerProject.Data;

namespace GymTrackerProject.Commands;

public record DeleteProgressCommand(int Id) : IRequest;

public class DeleteProgressCommandHandler : IRequestHandler<DeleteProgressCommand>
{
    private readonly ApplicationDbContext _context;

    public DeleteProgressCommandHandler(ApplicationDbContext context)
    {  
        _context = context; 
    }

    public async Task Handle(DeleteProgressCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Trackers.FindAsync(command.Id, cancellationToken);
        if (entity == null)
        {
            throw new Exception("Unable to find progress.");
        }
        _context.Trackers.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

