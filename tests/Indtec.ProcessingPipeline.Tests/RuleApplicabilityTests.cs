using Indtec.ProcessingPipeline.Abstractions.Processing;
using Indtec.ProcessingPipeline.Abstractions.Validation;
using Indtec.ProcessingPipeline.Application.Orders.CreateOrder;
using Indtec.ProcessingPipeline.Domain.Orders;

namespace Indtec.ProcessingPipeline.Tests;

public sealed class RuleApplicabilityTests
{
    [Fact]
    public void Subscription_rule_only_applies_to_subscription()
    {
        var rule = new SubscriptionMinimumValueRule();
        var subscription = Order.Create(OrderType.Subscription, OrderSide.Buy, Guid.NewGuid());
        var standard = Order.Create(OrderType.Standard, OrderSide.Buy, Guid.NewGuid());
        var context = new ValidationContext(ProcessingIntent.Save);

        Assert.True(rule.IsApplicable(subscription, context));
        Assert.False(rule.IsApplicable(standard, context));
    }

    [Theory]
    [InlineData(ProcessingIntent.Save, false)]
    [InlineData(ProcessingIntent.Release, true)]
    [InlineData(ProcessingIntent.SaveAndRelease, true)]
    public void Composed_rule_requires_preorder_sell_and_release_intent(ProcessingIntent intent, bool expected)
    {
        var rule = new PreOrderSellReleaseRule();
        var sell = Order.Create(OrderType.PreOrder, OrderSide.Sell, Guid.NewGuid());

        Assert.Equal(expected, rule.IsApplicable(sell, new ValidationContext(intent)));
    }

    [Fact]
    public void Composed_rule_does_not_apply_to_buy_side()
    {
        var rule = new PreOrderSellReleaseRule();
        var buy = Order.Create(OrderType.PreOrder, OrderSide.Buy, Guid.NewGuid());

        Assert.False(rule.IsApplicable(buy, new ValidationContext(ProcessingIntent.Release)));
    }
}
