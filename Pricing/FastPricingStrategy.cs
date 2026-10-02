using ConsoleApp35.Models;

namespace ConsoleApp35.Pricing;

/// <summary>
/// Stack-friendly pricing strategy that adjusts the order subtotal by customer tier.
/// Separate from carrier shipping strategies — this discounts merchandise, not freight.
/// </summary>
public static class FastPricingStrategy
{
    /// <summary>
    /// Applies VIP / Corporate percentage discounts using pattern matching on the context.
    /// </summary>
    public static decimal Calculate(in FastPricingContext ctx) => ctx switch
    {
        // High-spend VIPs get the deepest merchandise cut (30%).
        { Customer.Type: CustomerType.Vip, Subtotal: > 500m } => ctx.Subtotal * 0.70m,

        // Standard VIP merchandise discount (20%).
        { Customer.Type: CustomerType.Vip } => ctx.Subtotal * 0.80m,

        // Corporate negotiated merchandise rate (15% off).
        { Customer.Type: CustomerType.Corporate } => ctx.Subtotal * 0.85m,

        // No discount for Standard (or unmatched) customers.
        _ => ctx.Subtotal
    };
}
