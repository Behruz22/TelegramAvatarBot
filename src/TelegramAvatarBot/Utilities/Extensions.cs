using System.Web;

namespace TelegramAvatarBot.Utilities;

public static class Extensions
{
    public static string ToUrlSafeString(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        return HttpUtility.UrlEncode(input.Trim());
    }

    public static bool IsValidCommand(this string command)
    {
        return Constants.DicebearStyles.StyleMapping.ContainsKey(command);
    }
}