using Abp.Webhooks;
using Microsoft.Extensions.Logging;
using WebhookRelay.Infrastructure.Interfaces;

namespace WebhookRelay.Infrastructure.Services
{
    public class WebhookHandler(ILogger<WebhookHandler> logger) : IWebhookHandler
    {
        private readonly ILogger<WebhookHandler> _logger = logger;

        public Task HandleAsync(WebhookPayload payload)
        {
            _logger.LogInformation(
                "Received webhook from {Id} for event {WebhookEvent} with data: {Data}",
                payload.Id,
                payload.WebhookEvent,
                (object)payload.Data);

            return Task.CompletedTask;
        }
    }
}
