using capturerservice.Data;
using capturerservice.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


[assembly: FunctionsStartup(typeof(WordsFunction.Startup))]
namespace WordsFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();

            builder.Services.AddOptions<SettingsConfig>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection("SettingsConfig").Bind(settings);
                });

            builder.Services.AddSingleton<ISettingsConfig>(s => s.GetRequiredService<IOptions<SettingsConfig>>().Value);
            builder.Services.AddSingleton<IWordsService, WordsService>();
        }
    }
}