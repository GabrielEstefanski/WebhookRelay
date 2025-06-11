using Abp.Webhooks;
using System.Text.Json;
using WebhookRelay.Application.DTOs.Response;
using WebhookRelay.Domain.Interfaces;

namespace WebhookRelay.Application.Services
{
    public class DLQService: IDLQService
    {
        public async Task PersistAsync(WebhookResponseDto payload)
        {
            Directory.CreateDirectory("DLQ");

            var fileName = $"DLQ/{DateTime.UtcNow:yyyyMMddHHmmssfff}.json";
            var content = JsonSerializer.Serialize(new
            {
                Payload = payload,
                Exception = "Exception",
                Timestamp = DateTime.UtcNow
            }, new JsonSerializerOptions { WriteIndented = true });

            await File.WriteAllTextAsync(Path.Combine("DLQ", fileName), content);
        }
    }
}
