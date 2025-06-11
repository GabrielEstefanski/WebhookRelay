using Abp.Webhooks;
using WebhookRelay.Application.Jobs;
using WebhookRelay.Domain.Interfaces;

namespace WebhookRelay.Infrastructure.Jobs;

public class WebhookDLQJob(IDLQService dlqService) : IWebhookDLQJob
{
    private readonly IDLQService _dlqService = dlqService;

    public async Task ExecuteAsync(WebhookPayload payload)
    {
        await _dlqService.PersistAsync(payload);
    }
}
