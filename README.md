## Connect with Me

[![LinkedIn](https://img.shields.io/badge/LinkedIn-Profile-blue)](https://www.linkedin.com/in/spyros-ponaris-913a6937/)

# Shipping Strategy Demo

.NET 9 console app that prices shipping with the **Strategy** pattern, a strategy factory, optional DLL plugins, and a zero-allocation merchandise pricing path.

**Suggested GitHub repo name:** `shipping-strategy-demo`

Other good options:

| Name | When to use |
|------|-------------|
| `shipping-strategy-demo` | Best default — clear and searchable |
| `csharp-shipping-strategies` | Emphasize C# / .NET |
| `shipping-strategy-pattern` | Emphasize the design pattern |
| `shpping` | Only if you want to match this folder name |

Naming tips: use lowercase, hyphens, and a short purpose-focused name. Avoid `ConsoleApp35`.

## Requirements

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

## Run

```bash
cd ConsoleApp35
dotnet run
```

## Project layout

```
Models/       Customer, Order, CustomerType
Strategies/   IShippingStrategy + DHL / FedEx / UPS implementations
Pricing/      Zero-allocation FastPricingContext + FastPricingStrategy
Factory/      ShippingStrategyFactory (register, resolve, load plugins)
Program.cs    Sample customers, orders, and carrier comparison
```

## How it works

1. **Shipping strategies** — each carrier implements `IShippingStrategy.CalculateCost(Order)` with its own base rate and customer-tier discounts (VIP, Corporate, loyalty).
2. **Factory** — `ShippingStrategyFactory` registers built-in carriers and resolves them by `ProviderName`.
3. **Plugins** — `LoadExternalPlugins(path)` loads `*.dll` files that implement `IShippingStrategy` via a collectible `AssemblyLoadContext`.
4. **Fast pricing** — `FastPricingContext` (`ref struct`) + `FastPricingStrategy` apply merchandise discounts without heap allocations for the context.

## Sample output

```
=== Available Carriers: DHL Express, FedEx, UPS Ground ===

[DHL Express] Cost for Alice (Vip): $24.45
[FedEx]       Cost for Alice (Vip): $13.13

[Zero-Alloc Strategy] Corporate Subtotal: $170.00
```
