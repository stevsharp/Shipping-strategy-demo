namespace ConsoleApp35.Models;

/// <summary>
/// Immutable customer profile passed into order pricing and shipping calculations.
/// </summary>
/// <param name="Id">Unique customer identifier.</param>
/// <param name="Name">Display name (person or company).</param>
/// <param name="Type">Loyalty / account tier that drives discount selection.</param>
/// <param name="LoyaltyPoints">Points balance; some carriers reward high loyalty.</param>
public record Customer(string Id, string Name, CustomerType Type, int LoyaltyPoints);
