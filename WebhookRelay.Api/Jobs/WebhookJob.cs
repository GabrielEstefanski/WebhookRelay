using Hangfire;
using WebhookRelay.Application.DTOs.Response;
using WebhookRelay.Application.Services;

public class WebhookJob(IWebhookService webhookService, DLQService dlqService, ILogger<WebhookJob> logger)
{
    private readonly IWebhookService _webhookService = webhookService;
    private readonly DLQService _dlqService = dlqService;
    private readonly ILogger<WebhookJob> _logger = logger;

    [AutomaticRetry(Attempts = 3)]
    public async Task ExecuteAsync(WebhookResponseDto payload)
    {
        try
        {
            await _webhookService.Handle(payload);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Falha ao processar webhook. Enviando para DLQ.");
            await _dlqService.PersistAsync(payload);
            throw;
        }
    }
}
