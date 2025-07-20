using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramAvatarBot.Services.Interfaces;
using TelegramAvatarBot.Utilities;

namespace TelegramAvatarBot.Handlers;

public class MessageHandler(
    ITelegramBotClient _botClient,
    CommandHandler _commandHandler,
    ILoggingService _loggingService)
{
    public async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
    {
        try
        {
            if (update.Type != UpdateType.Message || update.Message?.Text == null)
                return;

            var message = update.Message;   
            var userId = message.From?.Id ?? 0;

            _loggingService.LogInfo($"Received message from user {userId}: {message.Text}");

            var command =Models.BotCommand.Parse(message.Text);

            if (!command.IsValid)
            {
                await _botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: Constants.Messages.UseCommand,
                    cancellationToken: cancellationToken);
                return;
            }

            var result = await _commandHandler.HandleCommandAsync(command, userId);

            if (result.IsSuccess && result.ImageStream != null)
            {
                await SendImageAsync(message.Chat.Id, result.ImageStream, cancellationToken);
            }
            else
            {
                await _botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: result.Message,
                    cancellationToken: cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _loggingService.LogError("Error handling message", ex);
            
            if (update.Message?.Chat?.Id != null)
            {
                try
                {
                    await _botClient.SendTextMessageAsync(
                        chatId: update.Message.Chat.Id,
                        text: Constants.Messages.SendingError,
                        cancellationToken: cancellationToken);
                }
                catch (Exception sendEx)
                {
                    _loggingService.LogError("Error sending error message", sendEx);
                }
            }
        }
    }

    private async Task SendImageAsync(ChatId chatId, Stream imageStream, CancellationToken cancellationToken)
    {
        try
        {
            var inputFile = InputFile.FromStream(imageStream, "avatar.png");
            
            await _botClient.SendPhotoAsync(
                chatId: chatId,
                photo: inputFile,
                cancellationToken: cancellationToken);

            _loggingService.LogInfo($"Successfully sent image to chat {chatId}");
        }
        catch (Exception ex)
        {
            _loggingService.LogError("Error sending image", ex);
            
            // Try to send error message
            await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: Constants.Messages.SendingError,
                cancellationToken: cancellationToken);
        }
        finally
        {
            imageStream?.Dispose();
        }
    }

    public Task HandleErrorAsync(Exception exception, CancellationToken cancellationToken)
    {
        _loggingService.LogError("Bot error occurred", exception);
        return Task.CompletedTask;
    }
}