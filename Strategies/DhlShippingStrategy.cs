using ConsoleApp35.Models;

namespace ConsoleApp35.Strategies;

/// <summary>
/// DHL Express international shipping strategy with tier- and loyalty-based discounts.
/// </summary>
public class DhlShippingStrategy : IShippingStrategy
{
    public string ProviderName => "DHL Express";

    public decimal CalculateCost(Order order)
    {
        // Base international rate: fixed pickup fee + per-kg charge.
        decimal baseRate = 25.00m + (order.WeightKg * 3.50m);

        // Pattern matching selects the first matching customer rule.
        return order.Customer switch
        {
            // VIPs get priority express at a 40% discount.
            { Type: CustomerType.Vip } => baseRate * 0.60m,

            // Corporate accounts get a flat $15.00 discount (floor at $5).
            { Type: CustomerType.Corporate } => Math.Max(5.00m, baseRate - 15.00m),

            // Standard customers with high loyalty points get $5 off.
            { LoyaltyPoints: > 500 } => baseRate - 5.00m,

            // Everyone else pays the full base rate.
            _ => baseRate
        };
    }
}
