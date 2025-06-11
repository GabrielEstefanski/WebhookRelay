using Abp.Webhooks;
using WebhookRelay.Application.DTOs.Response;

namespace WebhookRelay.Application.Jobs
{
    public interface IWebhookDLQJob
    {
        Task ExecuteAsync(WebhookPayload payload);
    }
}
