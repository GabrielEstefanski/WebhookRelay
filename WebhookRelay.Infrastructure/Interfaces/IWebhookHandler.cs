using Abp.Webhooks;

namespace WebhookRelay.Infrastructure.Interfaces
{
    public interface IWebhookHandler
    {
        Task HandleAsync(WebhookPayload payload);
    }
}
