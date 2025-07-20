namespace TelegramAvatarBot.Models;

public class CommandResult
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public Stream? ImageStream { get; set; }
    public string? ImageUrl { get; set; }

    public static CommandResult Success(Stream imageStream, string imageUrl)
    {
        return new CommandResult
        {
            IsSuccess = true,
            ImageStream = imageStream,
            ImageUrl = imageUrl
        };
    }

    public static CommandResult Failure(string message)
    {
        return new CommandResult
        {
            IsSuccess = false,
            Message = message
        };
    }
}