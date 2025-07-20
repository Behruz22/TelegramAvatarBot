using Microsoft.Extensions.Options;
using TelegramAvatarBot.Configuration;
using TelegramAvatarBot.Models;
using TelegramAvatarBot.Services.Interfaces;
using TelegramAvatarBot.Utilities;

namespace TelegramAvatarBot.Services;

public class DicebearService : IDicebearService
{
    private readonly HttpClient _httpClient;
    private readonly BotConfiguration _config;
    private readonly ILoggingService _loggingService;

    public DicebearService(HttpClient httpClient, IOptions<BotConfiguration> config, ILoggingService loggingService)
    {
        _httpClient = httpClient;
        _config = config.Value;
        _loggingService = loggingService;
    }

    public async Task<CommandResult> GetAvatarAsync(string style, string seed, CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new DicebearRequest
            {
                Style = style,
                Seed = seed,
                Format = "png"
            };

            var url = request.BuildUrl(_config.DicebearApiUrl);
            _loggingService.LogInfo($"Making request to Dicebear API: {url}");

            var response = await _httpClient.GetAsync(url, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var imageStream = await response.Content.ReadAsStreamAsync(cancellationToken);
                _loggingService.LogInfo($"Successfully retrieved avatar for style: {style}, seed: {seed}");
                return CommandResult.Success(imageStream, url);
            }

            _loggingService.LogError($"Dicebear API returned error: {response.StatusCode}");
            return CommandResult.Failure(Constants.Messages.AvatarCreationError);
        }
        catch (Exception ex)
        {
            _loggingService.LogError("Error calling Dicebear API", ex);
            return CommandResult.Failure(Constants.Messages.AvatarCreationError);
        }
    }
}