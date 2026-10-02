using System.Runtime.Loader;
using ConsoleApp35.Strategies;

namespace ConsoleApp35.Factory;

/// <summary>
/// Registers built-in shipping strategies and optionally loads external plugin DLLs.
/// Callers resolve a carrier by provider name instead of constructing strategies directly.
/// </summary>
public class ShippingStrategyFactory
{
    // ProviderName → strategy instance (last registration wins for a given name).
    private readonly Dictionary<string, IShippingStrategy> _strategies = [];

    public ShippingStrategyFactory()
    {
        // Seed with the three built-in carriers.
        Register(new DhlShippingStrategy());
        Register(new FedexShippingStrategy());
        Register(new UpsShippingStrategy());
    }

    /// <summary>Adds or replaces a strategy keyed by its <see cref="IShippingStrategy.ProviderName"/>.</summary>
    public void Register(IShippingStrategy strategy)
    {
        _strategies[strategy.ProviderName] = strategy;
    }

    /// <summary>Resolves a registered strategy or throws if the provider name is unknown.</summary>
    public IShippingStrategy GetStrategy(string providerName)
    {
        if (_strategies.TryGetValue(providerName, out var strategy))
        {
            return strategy;
        }

        throw new ArgumentException($"No shipping strategy found for provider: {providerName}");
    }

    /// <summary>
    /// Scans <paramref name="pluginsFolderPath"/> for *.dll assemblies that implement
    /// <see cref="IShippingStrategy"/> and registers each concrete instance discovered.
    /// Uses a collectible <see cref="AssemblyLoadContext"/> so plugins can be unloaded later.
    /// </summary>
    public void LoadExternalPlugins(string pluginsFolderPath)
    {
        if (!Directory.Exists(pluginsFolderPath)) return;

        foreach (var dllPath in Directory.GetFiles(pluginsFolderPath, "*.dll"))
        {
            // Isolate each plugin assembly in its own collectible load context.
            var loadContext = new AssemblyLoadContext(dllPath, isCollectible: true);
            var assembly = loadContext.LoadFromAssemblyPath(dllPath);

            foreach (Type type in assembly.GetTypes())
            {
                // Skip interfaces/abstract types; only concrete strategy classes are registered.
                if (typeof(IShippingStrategy).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface)
                {
                    if (Activator.CreateInstance(type) is IShippingStrategy pluginStrategy)
                    {
                        Register(pluginStrategy);
                        Console.WriteLine($"[Plugin Loaded]: {pluginStrategy.ProviderName}");
                    }
                }
            }
        }
    }

    /// <summary>Names of all currently registered carriers.</summary>
    public IEnumerable<string> AvailableProviders => _strategies.Keys;
}
