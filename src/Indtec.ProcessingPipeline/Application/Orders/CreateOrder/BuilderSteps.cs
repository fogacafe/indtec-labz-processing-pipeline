using Indtec.ProcessingPipeline.Abstractions.Building;
using Indtec.ProcessingPipeline.Domain.Orders;

namespace Indtec.ProcessingPipeline.Application.Orders.CreateOrder;

public interface IProductCatalog
{
    Task<decimal> GetPriceAsync(Guid productId, CancellationToken cancellationToken = default);
}

public sealed class OrderItemsBuilderStep(IProductCatalog productCatalog)
    : IBuilderStep<CreateOrderCommand, Order>
{
    public async Task ExecuteAsync(
        CreateOrderCommand command,
        Order order,
        CancellationToken cancellationToken = default)
    {
        foreach (var input in command.Items)
        {
            var price = await productCatalog.GetPriceAsync(input.ProductId, cancellationToken);
            order.AddItem(new OrderItem(input.ProductId, input.Quantity, price));
        }
    }
}

public sealed class OrderTotalBuilderStep : IBuilderStep<CreateOrderCommand, Order>
{
    public Task ExecuteAsync(
        CreateOrderCommand command,
        Order order,
        CancellationToken cancellationToken = default)
    {
        order.SetTotal(order.Items.Sum(x => x.Total));
        return Task.CompletedTask;
    }
}
