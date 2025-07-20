using TelegramAvatarBot.Models;

namespace TelegramAvatarBot.Services.Interfaces;

public interface IDicebearService
{
    Task<CommandResult> GetAvatarAsync(string style, string seed, CancellationToken cancellationToken = default);
}