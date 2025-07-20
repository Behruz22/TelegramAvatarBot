using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using TelegramAvatarBot.Configuration;
using TelegramAvatarBot.Handlers;
using TelegramAvatarBot.Services;
using TelegramAvatarBot.Services.Interfaces;

namespace TelegramAvatarBot;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            var host = CreateHostBuilder(args).Build();
            
            using var scope = host.Services.CreateScope();
            var botService = scope.ServiceProvider.GetRequiredService<IBotService>();
            
            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
                Console.WriteLine("\nShutting down...");
            };

            await botService.StartAsync(cts.Token);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Application failed to start: {ex.Message}");
            Environment.Exit(1);
        }
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../"));
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                config.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", 
                    optional: true, reloadOnChange: true);
                config.AddEnvironmentVariables();
            })
            .ConfigureServices((context, services) =>
            {
                services.Configure<BotConfiguration>(
                    context.Configuration.GetSection("BotConfiguration"));

                services.AddHttpClient<IDicebearService, DicebearService>();
                services.AddSingleton<ITelegramBotClient>(provider =>
                {
                    var configuration = provider.GetRequiredService<IConfiguration>();
                    var botConfig = configuration.GetSection("BotConfiguration").Get<BotConfiguration>();
    
                    if (string.IsNullOrEmpty(botConfig?.Token))
                        throw new ArgumentException("Bot token is required in BotConfiguration");
        
                    return new TelegramBotClient(botConfig.Token);
                });
                services.AddSingleton<ILoggingService, LoggingService>();
                services.AddSingleton<CommandHandler>();
                services.AddSingleton<MessageHandler>();
                services.AddSingleton<IBotService, BotService>();

                services.AddLogging(builder =>
                {
                    builder.AddConsole();
                    builder.AddConfiguration(context.Configuration.GetSection("Logging"));
                });
            });
}