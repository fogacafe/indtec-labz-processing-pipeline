using Indtec.ProcessingPipeline.Abstractions.Commands;
using Indtec.ProcessingPipeline.Abstractions.Persistence;
using Indtec.ProcessingPipeline.Abstractions.Processing;
using Indtec.ProcessingPipeline.Domain.Orders;

namespace Indtec.ProcessingPipeline.Application.Orders.CreateOrder;

public sealed class CreateOrderHandler(
    IEntityProcessor<CreateOrderCommand, Order> processor,
    IRepository<Order> repository)
    : ICommandHandler<CreateOrderCommand, ProcessingResult<Order>>
{
    public async Task<ProcessingResult<Order>> HandleAsync(
        CreateOrderCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = await processor.ProcessAsync(command, command.Intent, cancellationToken);

        if (!result.Success)
            return result;

        if (command.Intent.RequiresRelease())
            result.Entity.Release();

        await repository.SaveAsync(result.Entity, cancellationToken);
        return result;
    }
}
