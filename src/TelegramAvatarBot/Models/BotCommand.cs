namespace TelegramAvatarBot.Models;

public class BotCommand
{
    public string Command { get; set; } = string.Empty;
    public string Seed { get; set; } = string.Empty;
    public bool IsValid { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;

    public static BotCommand Parse(string? messageText)
    {
        if (string.IsNullOrWhiteSpace(messageText))
        {
            return new BotCommand   
            {
                IsValid = false,
                ErrorMessage = "Bo'sh xabar"
            };
        }

        var parts = messageText.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        
        if (parts.Length == 0)
        {
            return new BotCommand
            {
                IsValid = false,
                ErrorMessage = "Hech qanday buyruq topilmadi"
            };
        }

        var command = parts[0].ToLower();
        var seed = parts.Length > 1 ? string.Join(" ", parts[1..]) : string.Empty;

        return new BotCommand
        {
            Command = command,
            Seed = seed,
            IsValid = true
        };
    }
}