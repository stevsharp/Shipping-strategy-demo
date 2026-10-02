using ConsoleApp35.Factory;
using ConsoleApp35.Models;
using ConsoleApp35.Pricing;
using ConsoleApp35.Strategies;

namespace ConsoleApp35;

/// <summary>
/// Demo entry point: builds sample customers/orders, resolves carriers via the factory,
/// and shows both shipping-cost strategies and zero-allocation merchandise pricing.
/// </summary>
public static class Program
{
    public static void Main()
    {
        // 1. Sample customers at different tiers.
        var vipCustomer = new Customer("C-101", "Alice", CustomerType.Vip, LoyaltyPoints: 1200);
        var corpCustomer = new Customer("C-102", "Acme Corp", CustomerType.Corporate, LoyaltyPoints: 300);

        // 2. VIP order used for carrier cost comparisons.
        var order = new Order("ORD-9901", Subtotal: 200.00m, WeightKg: 4.5m, Customer: vipCustomer);

        // 3. Factory already registers DHL, FedEx, and UPS Ground.
        var factory = new ShippingStrategyFactory();

        Console.WriteLine($"=== Available Carriers: {string.Join(", ", factory.AvailableProviders)} ===\n");

        // 4. Resolve and execute DHL strategy for the VIP customer.
        IShippingStrategy dhl = factory.GetStrategy("DHL Express");
        decimal dhlCost = dhl.CalculateCost(order);
        Console.WriteLine($"[DHL Express] Cost for {vipCustomer.Name} ({vipCustomer.Type}): ${dhlCost:F2}");

        // 5. Same order priced with FedEx for side-by-side comparison.
        IShippingStrategy fedEx = factory.GetStrategy("FedEx");
        decimal fedExCost = fedEx.CalculateCost(order);
        Console.WriteLine($"[FedEx]       Cost for {vipCustomer.Name} ({vipCustomer.Type}): ${fedExCost:F2}");

        // 6. Zero-allocation merchandise pricing for a Corporate customer (stack-only context).
        var fastCtx = new FastPricingContext { Subtotal = order.Subtotal, Customer = corpCustomer };
        decimal finalPrice = FastPricingStrategy.Calculate(fastCtx);
        Console.WriteLine($"\n[Zero-Alloc Strategy] Corporate Subtotal: ${finalPrice:F2}");
    }
}
