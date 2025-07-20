using TelegramAvatarBot.Models;
using TelegramAvatarBot.Services.Interfaces;
using TelegramAvatarBot.Utilities;

namespace TelegramAvatarBot.Handlers;

public class CommandHandler(
    IDicebearService _dicebearService,
    ILoggingService _loggingService)
{

    public async Task<CommandResult> HandleCommandAsync(BotCommand command, long userId)
    {
        try
        {
            if (command.Command == Constants.Commands.Start)
            {
                _loggingService.LogRequest(userId, command.Command, "/", 200);
                return CommandResult.Failure(Constants.Messages.StartCommand);
            }
            if (command.Command == Constants.Commands.Help)
            {
                _loggingService.LogRequest(userId, command.Command, "/", 200);
                return CommandResult.Failure(Constants.Messages.HelpMessage);
            }

            if (!command.Command.IsValidCommand())
            {
                _loggingService.LogRequest(userId, command.Command, command.Seed, 400);
                return CommandResult.Failure(Constants.Messages.UnknownCommand);
            }

            if (string.IsNullOrWhiteSpace(command.Seed))
            {
                _loggingService.LogRequest(userId, command.Command, "", 400);
                return CommandResult.Failure(Constants.Messages.SeedRequired);
            }

            var style = Constants.DicebearStyles.StyleMapping[command.Command];
            
            var result = await _dicebearService.GetAvatarAsync(style, command.Seed);
            
            var statusCode = result.IsSuccess ? 200 : 500;
            _loggingService.LogRequest(userId, command.Command, command.Seed, statusCode);

            return result;
        }
        catch (Exception ex)
        {
            _loggingService.LogError("Error handling command", ex);
            _loggingService.LogRequest(userId, command.Command, command.Seed, 500);
            return CommandResult.Failure(Constants.Messages.AvatarCreationError);
        }
    }
}