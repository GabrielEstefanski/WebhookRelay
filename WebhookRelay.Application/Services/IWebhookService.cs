using WebhookRelay.Application.DTOs.Response;
using System.Threading.Tasks;

namespace WebhookRelay.Application.Services
{
    public interface IWebhookService
    {
        Task Handle(WebhookResponseDto payload);
    }
}
