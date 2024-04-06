using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;

class Program
{
    static async Task Main(string[] args)
    {
        // Set up configuration source
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
            .Build();

        // Set up DI
        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(configuration);
        services.AddFeatureManagement();
        var serviceProvider = services.BuildServiceProvider();

        // Example of using FeatureManager
        var featureManager = serviceProvider.GetRequiredService<IFeatureManager>();

        string flag_name = "first_flag";
        bool isMyFeatureEnabled = await IsFeatureEnabledAsync(featureManager, flag_name);
        Console.WriteLine($"${flag_name} enabled: {isMyFeatureEnabled}");

        flag_name = "second_flag";
        isMyFeatureEnabled = await IsFeatureEnabledAsync(featureManager, flag_name);
        Console.WriteLine($"{flag_name} enabled: {isMyFeatureEnabled}");

    }

    public static async Task<bool> IsFeatureEnabledAsync(IFeatureManager featureManager, string flag_name)
    {
        return await featureManager.IsEnabledAsync(flag_name);
    }    
}
