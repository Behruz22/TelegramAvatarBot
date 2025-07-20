namespace TelegramAvatarBot.Services.Interfaces;

public interface IBotService
{
    Task StartAsync(CancellationToken cancellationToken = default);
    Task StopAsync(CancellationToken cancellationToken = default);
}