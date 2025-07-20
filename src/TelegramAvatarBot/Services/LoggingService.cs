using Microsoft.Extensions.Logging;
using TelegramAvatarBot.Services.Interfaces;

namespace TelegramAvatarBot.Services;

public class LoggingService : ILoggingService
{
    private readonly ILogger<LoggingService> _logger;

    public LoggingService(ILogger<LoggingService> logger)
    {
        _logger = logger;
    }

    public void LogRequest(long userId, string command, string seed, int statusCode)
    {
        _logger.LogInformation("User {UserId} executed {Command} with seed '{Seed}', Status: {StatusCode}", 
            userId, command, seed, statusCode);
    }

    public void LogError(string message, Exception? exception = null)
    {
        if (exception != null)
        {
            _logger.LogError(exception, "{Message}", message);
        }
        else
        {
            _logger.LogError("{Message}", message);
        }
    }

    public void LogInfo(string message)
    {
        _logger.LogInformation("{Message}", message);
    }
}