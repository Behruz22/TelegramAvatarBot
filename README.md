# Telegram Avatar Bot 🤖

Bu loyiha Telegram bot yaratadi, u foydalanuvchidan matnli buyruq oladi va Dicebear API orqali mos avatar (PNG rasm) yuboradi.

## Xususiyatlar ✅

- **Turli avatar uslublari**: fun-emoji, avataaars, bottts, pixel-art
- **Oson foydalanish**: Oddiy buyruqlar orqali avatar yaratish
- **Xatoliklarni boshqarish**: To'liq xatolik qayta ishlash tizimi
- **Logging**: Har bir so'rovni konsolga log qilish
- **Clean Architecture**: Modulli va test qilinadigan kod

## Qo'llab-quvvatlanadigan buyruqlar 📜

| Buyruq | Dicebear uslubi | Misol |
|--------|----------------|-------|
| `/fun-emoji` | fun-emoji | `/fun-emoji Ali` |
| `/avataaars` | avataaars | `/avataaars John` |
| `/bottts` | bottts | `/bottts robot123` |
| `/pixel-art` | pixel-art | `/pixel-art game` |
| `/help` | - | Yordam xabari |


## Foydalanish 💬

Bot ishga tushgandan so'ng, Telegram'da quyidagi buyruqlarni yuborishingiz mumkin:

- `🎭 /fun-emoji Alisher` - Alisher so'zidan yaratilgan fun-emoji avatar
- `👤 /avataaars John Doe` - John Doe so'zidan yaratilgan avataaars avatar
- `🤖 /bottts robot` - robot so'zidan yaratilgan bottts avatar
- `🎮 /pixel-art game` - game so'zidan yaratilgan pixel-art avatar
- `ℹ️ /help` - Yordam xabari

## Loyiha strukturasi 📁

```
TelegramAvatarBot/
├── src/
│   ├── TelegramAvatarBot/
│      ├── Configuration/          # Sozlamalar
│      ├── Services/               # Biznes logika
│      ├── Models/                 # Ma'lumot modellari
│      ├── Handlers/               # Xabar va buyruqlar ishlov berish
│      ├── Utilities/              # Yordamchi sinflar
│      └── Program.cs              # Dastur kirish nuqtasi
├── appsettings.json                # Sozlamalar fayli
└── README.md
```

## Texnologiyalar 🔧

- **.NET 8**: Asosiy framework
- **Telegram.Bot**: Telegram Bot API kutubxonasi
- **Microsoft.Extensions**: Dependency Injection va Configuration
- **HttpClient**: Dicebear API bilan muloqot
- **xUnit**: Test framework
- **Moq**: Mocking kutubxonasi


## Hissa qo'shish 🤝

1. Repository ni fork qiling
2. Feature branch yarating
3. O'zgarishlarni commit qiling
4. Pull request jo'nating

## Litsenziya 📄

MIT License

---

Savol yoki takliflaringiz bo'lsa, iltimos issue yarating! 🚀