using ConsoleApp35.Models;

namespace ConsoleApp35.Strategies;

/// <summary>
/// FedEx shipping strategy with percentage discounts for VIP and Corporate tiers.
/// </summary>
public class FedexShippingStrategy : IShippingStrategy
{
    public string ProviderName => "FedEx";

    public decimal CalculateCost(Order order)
    {
        // Base international rate: lower fixed fee than DHL, moderate per-kg rate.
        decimal baseRate = 15.00m + (order.WeightKg * 2.50m);

        return order.Customer switch
        {
            { Type: CustomerType.Vip } => baseRate * 0.50m,        // 50% VIP discount
            { Type: CustomerType.Corporate } => baseRate * 0.80m, // 20% Corporate discount
            _ => baseRate
        };
    }
}
