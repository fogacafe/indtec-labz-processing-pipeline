using Indtec.ProcessingPipeline.Abstractions.Processing;
using Indtec.ProcessingPipeline.Abstractions.Validation;
using Indtec.ProcessingPipeline.Domain.Orders;

namespace Indtec.ProcessingPipeline.Application.Orders.CreateOrder;

public sealed class CustomerRequiredRule : IValidationRule<Order>
{
    public bool IsApplicable(Order target, ValidationContext context) => true;

    public Task<IEnumerable<ValidationMessage>> ValidateAsync(
        Order order,
        ValidationContext context,
        CancellationToken cancellationToken = default)
    {
        IEnumerable<ValidationMessage> result = order.CustomerId == Guid.Empty
            ? [new(nameof(order.CustomerId), "Customer is required.", ValidationSeverity.Error)]
            : [];

        return Task.FromResult(result);
    }
}

public sealed class SubscriptionMinimumValueRule : IValidationRule<Order>
{
    public bool IsApplicable(Order order, ValidationContext context) =>
        order.Type == OrderType.Subscription;

    public Task<IEnumerable<ValidationMessage>> ValidateAsync(
        Order order,
        ValidationContext context,
        CancellationToken cancellationToken = default)
    {
        IEnumerable<ValidationMessage> result = order.Total < 100m
            ? [new(nameof(order.Total), "Subscription orders usually have a minimum value.", ValidationSeverity.Warning)]
            : [];

        return Task.FromResult(result);
    }
}

public sealed class SellOrderHintRule : IValidationRule<Order>
{
    public bool IsApplicable(Order order, ValidationContext context) =>
        order.Side == OrderSide.Sell;

    public Task<IEnumerable<ValidationMessage>> ValidateAsync(
        Order order,
        ValidationContext context,
        CancellationToken cancellationToken = default) =>
        Task.FromResult<IEnumerable<ValidationMessage>>(
            [new(nameof(order.Side), "Sell orders require inventory availability.", ValidationSeverity.Hint)]);
}

public sealed class PreOrderSellReleaseRule : IValidationRule<Order>
{
    public bool IsApplicable(Order order, ValidationContext context) =>
        order.Type == OrderType.PreOrder
        && order.Side == OrderSide.Sell
        && context.Intent.RequiresRelease();

    public Task<IEnumerable<ValidationMessage>> ValidateAsync(
        Order order,
        ValidationContext context,
        CancellationToken cancellationToken = default) =>
        Task.FromResult<IEnumerable<ValidationMessage>>(
            [new(nameof(order.Type), "Pre-order sell operations require additional review before release.", ValidationSeverity.Warning)]);
}
