using TrainTicketBookingSystem.Application.Architecture;
using TrainTicketBookingSystem.Domain.Models;

namespace TrainTicketBookingSystem.Application.Command.RegisterTrain;

public class RegisterTrainCommandHandler : ICommandHandler<RegisterTrainCommand, Guid>
{
    private readonly ITrainRepository _trainRepository;
    private readonly IMediator _mediator;

    public RegisterTrainCommandHandler(ITrainRepository trainRepository, IMediator mediator)
    {
        _trainRepository = trainRepository;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(RegisterTrainCommand command, CancellationToken cancellationToken)
    {
        var tranId = Guid.NewGuid();
        var train = Train.Register(tranId, command.Seats, command.Locations, command.Date);
        await _trainRepository.AddAsync(train, cancellationToken);
        await _mediator.PublishAndClearDomainEvents(train, cancellationToken);
        return tranId;
    }
}