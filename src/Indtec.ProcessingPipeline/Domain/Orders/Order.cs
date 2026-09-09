namespace Indtec.ProcessingPipeline.Domain.Orders;

public enum OrderType { Standard, Subscription, PreOrder }
public enum OrderSide { Buy, Sell }
public enum OrderStatus { Draft, Released, Processing, Completed, Cancelled }

public sealed class Order
{
    private readonly List<OrderItem> _items = [];

    public Guid Id { get; private set; }
    public OrderType Type { get; private set; }
    public OrderSide Side { get; private set; }
    public OrderStatus Status { get; private set; }
    public Guid CustomerId { get; private set; }
    public decimal Total { get; private set; }
    public IReadOnlyCollection<OrderItem> Items => _items;

    private Order() { }

    public static Order Create(OrderType type, OrderSide side, Guid customerId) => new()
    {
        Id = Guid.NewGuid(),
        Type = type,
        Side = side,
        CustomerId = customerId,
        Status = OrderStatus.Draft
    };

    public void AddItem(OrderItem item) => _items.Add(item);

    public void SetTotal(decimal total)
    {
        if (total < 0) throw new InvalidOperationException("Total cannot be negative.");
        Total = total;
    }

    public void Release()
    {
        if (Status != OrderStatus.Draft)
            throw new InvalidOperationException($"Order cannot be released from status {Status}.");

        Status = OrderStatus.Released;
    }
}

public sealed record OrderItem(Guid ProductId, int Quantity, decimal UnitPrice)
{
    public decimal Total => Quantity * UnitPrice;
}
