using Indtec.ProcessingPipeline.Abstractions.Persistence;
using Indtec.ProcessingPipeline.Domain.Orders;

namespace Indtec.ProcessingPipeline.Infrastructure.Persistence;

public sealed class InMemoryOrderRepository : IRepository<Order>
{
    private readonly Dictionary<Guid, Order> _orders = [];

    public Task<Order?> GetAsync(Guid id, CancellationToken cancellationToken = default) =>
        Task.FromResult(_orders.GetValueOrDefault(id));

    public Task SaveAsync(Order entity, CancellationToken cancellationToken = default)
    {
        _orders[entity.Id] = entity;
        return Task.CompletedTask;
    }
}
