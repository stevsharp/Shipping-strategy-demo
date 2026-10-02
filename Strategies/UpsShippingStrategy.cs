using ConsoleApp35.Models;

namespace ConsoleApp35.Strategies;

/// <summary>
/// UPS Ground shipping strategy — cheapest base rate, VIP-only discount.
/// </summary>
public class UpsShippingStrategy : IShippingStrategy
{
    public string ProviderName => "UPS Ground";

    public decimal CalculateCost(Order order)
    {
        // Economy ground rate: lowest fixed fee and per-kg charge.
        decimal baseRate = 12.00m + (order.WeightKg * 2.00m);

        return order.Customer switch
        {
            // VIP customers receive a 30% discount on ground shipping.
            { Type: CustomerType.Vip } => baseRate * 0.70m,
            _ => baseRate
        };
    }
}
