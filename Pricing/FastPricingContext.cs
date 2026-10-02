using ConsoleApp35.Models;

namespace ConsoleApp35.Pricing;

/// <summary>
/// Zero-allocation pricing input passed by <c>in</c> to avoid heap copies.
/// Uses a <c>ref struct</c> so it lives on the stack only.
/// </summary>
public readonly ref struct FastPricingContext
{
    /// <summary>Order merchandise subtotal before discounts.</summary>
    public required decimal Subtotal { get; init; }

    /// <summary>Customer whose tier drives the discount percentage.</summary>
    public required Customer Customer { get; init; }
}
