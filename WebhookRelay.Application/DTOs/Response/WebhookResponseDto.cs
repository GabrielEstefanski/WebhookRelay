using System.Text.Json;

namespace WebhookRelay.Application.DTOs.Response
{
    public class WebhookResponseDto
    {
        public required string Source { get; set; }
        public required string Type { get; set; }
        public JsonElement Data { get; set; }
    }
}
