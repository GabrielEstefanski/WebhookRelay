using WebhookRelay.Application.DTOs.Response;

namespace WebhookRelay.Domain.Interfaces
{
    public interface IDLQService
    {
        Task PersistAsync(WebhookResponseDto payload);
    }
}
