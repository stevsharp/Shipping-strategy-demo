using ConsoleApp35.Models;

namespace ConsoleApp35.Strategies;

/// <summary>
/// Strategy Pattern contract: each carrier implements its own cost formula
/// while callers resolve strategies by <see cref="ProviderName"/>.
/// </summary>
public interface IShippingStrategy
{
    /// <summary>Stable key used by the factory dictionary (e.g. "DHL Express").</summary>
    string ProviderName { get; }

    /// <summary>Calculates shipping cost for the given order under this carrier's rules.</summary>
    decimal CalculateCost(Order order);
}
