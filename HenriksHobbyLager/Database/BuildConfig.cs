using Microsoft.Extensions.Configuration;

namespace HenriksHobbyLager.Database;

public class BuildConfig
{
    public static IConfiguration BuildConfiguration() // IConfiguration gets or sets a configuration value.
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }
}