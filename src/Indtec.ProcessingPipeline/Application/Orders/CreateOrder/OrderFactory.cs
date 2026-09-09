using Indtec.ProcessingPipeline.Abstractions.Building;
using Indtec.ProcessingPipeline.Abstractions.Persistence;
using Indtec.ProcessingPipeline.Domain.Orders;

namespace Indtec.ProcessingPipeline.Application.Orders.CreateOrder;

public sealed class OrderFactory(IRepository<Order> repository)
    : IEntityFactory<CreateOrderCommand, Order>
{
    public async Task<Order> CreateAsync(
        CreateOrderCommand command,
        CancellationToken cancellationToken = default)
    {
        if (command.OrderId is { } id)
            return await repository.GetAsync(id, cancellationToken)
                   ?? throw new InvalidOperationException("Order not found.");

        return Order.Create(command.Type, command.Side, command.CustomerId);
    }
}
