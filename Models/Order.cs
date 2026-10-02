namespace ConsoleApp35.Models;

/// <summary>
/// Immutable order snapshot used by shipping strategies to compute carrier cost.
/// </summary>
/// <param name="OrderId">Unique order identifier.</param>
/// <param name="Subtotal">Merchandise subtotal before shipping (used by fast pricing).</param>
/// <param name="WeightKg">Shipment weight in kilograms (drives carrier base rate).</param>
/// <param name="Customer">Customer whose tier/loyalty affects the final rate.</param>
public record Order(
    string OrderId,
    decimal Subtotal,
    decimal WeightKg,
    Customer Customer);
