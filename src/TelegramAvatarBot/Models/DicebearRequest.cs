namespace TelegramAvatarBot.Models;

public class DicebearRequest
{
    public string Style { get; set; } = string.Empty;
    public string Seed { get; set; } = string.Empty;
    public string Format { get; set; } = "png";

    public string BuildUrl(string baseUrl)
    {
        var encodedSeed = Uri.EscapeDataString(Seed);
        return $"{baseUrl}/{Style}/{Format}?seed={encodedSeed}";
    }
}