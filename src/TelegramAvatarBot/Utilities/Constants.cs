namespace TelegramAvatarBot.Utilities;

public static class Constants
{
    public static class Commands
    {
        public const string FunEmoji = "/fun-emoji";
        public const string Avataaars = "/avataaars";
        public const string Bottts = "/bottts";
        public const string PixelArt = "/pixel-art";
        public const string Help = "/help";
        public const string Start = "/start";
    }

    public static class Messages
    {
        public const string SeedRequired = "Iltimos, buyruqdan keyin matn (seed) kiriting. Misol: /fun-emoji Ali";
        public const string UnknownCommand = "Noma'lum buyruq. Quyidagilardan birini ishlating: /fun-emoji, /bottts, /avataaars, /pixel-art";
        public const string UseCommand = "Iltimos, avatar olish uchun buyruqdan foydalaning.";
        public const string AvatarCreationError = "Avatar yaratishda xatolik yuz berdi. Keyinroq urinib ko'ring.";
        public const string SendingError = "Rasmni yuborishda xatolik yuz berdi.";
        public const string HelpMessage = @"🤖 Telegram Avatar Bot

Qo'llab-quvvatlanadigan buyruqlar:
• /fun-emoji [matn] - Fun Emoji uslubida avatar
• /avataaars [matn] - Avataaars uslubida avatar  
• /bottts [matn] - Bottts uslubida avatar
• /pixel-art [matn] - Pixel Art uslubida avatar
• /help - Bu yordam xabari

Misol: /fun-emoji Ali";

        public const string StartCommand = "Assalomu alaykum Botimizga xush kelibsiz!\n" + HelpMessage;
    }

    public static class DicebearStyles
    {
        public static readonly Dictionary<string, string> StyleMapping = new()
        {
            { Commands.FunEmoji, "fun-emoji" },
            { Commands.Avataaars, "avataaars" },
            { Commands.Bottts, "bottts" },
            { Commands.PixelArt, "pixel-art" }
        };
    }
}