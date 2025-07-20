using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;
using TelegramAvatarBot.Configuration;
using TelegramAvatarBot.Handlers;
using TelegramAvatarBot.Services.Interfaces;

namespace TelegramAvatarBot.Services;

public class BotService : IBotService
{
    private readonly ITelegramBotClient _botClient;
    private readonly MessageHandler _messageHandler;
    private readonly ILoggingService _loggingService;
    private readonly CancellationTokenSource _cancellationTokenSource;

    public BotService(IOptions<BotConfiguration> config, MessageHandler messageHandler, ILoggingService loggingService)
    {
        var botConfig = config.Value;
        
        if (string.IsNullOrEmpty(botConfig.Token))
            throw new ArgumentException("Bot token is required");

        _botClient = new TelegramBotClient(botConfig.Token);
        _messageHandler = messageHandler;
        _loggingService = loggingService;
        _cancellationTokenSource = new CancellationTokenSource();
    }

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = [] // Receive all update types
            };

            _botClient.StartReceiving(
                updateHandler: async (botClient, update, ct) => 
                    await _messageHandler.HandleUpdateAsync(update, ct),
                pollingErrorHandler: async (botClient, exception, ct) => 
                    await _messageHandler.HandleErrorAsync(exception, ct),
                receiverOptions: receiverOptions,
                cancellationToken: _cancellationTokenSource.Token
                );

            var me = await _botClient.GetMeAsync(cancellationToken);
            _loggingService.LogInfo($"Bot @{me.Username} started successfully!");

            try
            {
                await Task.Delay(Timeout.Infinite, _cancellationTokenSource.Token);
            }
            catch (TaskCanceledException)
            {
                _loggingService.LogInfo("Bot service was cancelled");
            }
        }
        catch (Exception ex)
        {
            _loggingService.LogError("Failed to start bot", ex);
            throw;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken = default)
    {
        _loggingService.LogInfo("Stopping bot service...");
        _cancellationTokenSource.Cancel();
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _cancellationTokenSource?.Dispose();
    }
}