namespace ConsoleApp35.Models;

/// <summary>
/// Customer tier used to select discount rules in shipping and pricing strategies.
/// </summary>
public enum CustomerType
{
    /// <summary>Default retail customer — limited or no carrier discounts.</summary>
    Standard,

    /// <summary>High-value customer — strongest percentage discounts.</summary>
    Vip,

    /// <summary>Business account — negotiated corporate rates.</summary>
    Corporate
}
