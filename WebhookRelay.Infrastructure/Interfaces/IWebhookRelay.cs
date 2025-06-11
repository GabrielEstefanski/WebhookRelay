using Abp.Webhooks;

namespace WebhookRelay.Infrastructure.Interfaces
{
    public interface IWebhookRelay
    {
        Task ExecuteAsync(WebhookPayload payload, Exception exception);
    }
}
