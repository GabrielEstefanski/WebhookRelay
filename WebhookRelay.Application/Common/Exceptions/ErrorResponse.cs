namespace WebhookRelay.Application.Common.Exceptions
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string TraceId { get; set; } = default!;
        public string Message { get; set; } = default!;
        public object? Errors { get; set; }
        public string? Details { get; set; }
    }
}
