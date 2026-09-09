using Indtec.ProcessingPipeline.Abstractions.Commands;
using Indtec.ProcessingPipeline.Abstractions.Processing;
using Indtec.ProcessingPipeline.Domain.Orders;

namespace Indtec.ProcessingPipeline.Application.Orders.CreateOrder;

public sealed record CreateOrderItemInput(Guid ProductId, int Quantity);

public sealed record CreateOrderCommand(
    Guid? OrderId,
    OrderType Type,
    OrderSide Side,
    ProcessingIntent Intent,
    Guid CustomerId,
    IReadOnlyCollection<CreateOrderItemInput> Items)
    : ICommand<ProcessingResult<Order>>;
