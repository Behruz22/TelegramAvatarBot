namespace TelegramAvatarBot.Services.Interfaces;

public interface ILoggingService
{
    void LogRequest(long userId, string command, string seed, int statusCode);
    void LogError(string message, Exception? exception = null);
    void LogInfo(string message);
}