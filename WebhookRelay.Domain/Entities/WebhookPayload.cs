namespace WebhookRelay.Domain.Entities
{
    public class WebhookPayload(string source, string eventType, string dataJson)
    {
        public string Source { get; } = source;
        public string EventType { get; } = eventType;
        public string DataJson { get; } = dataJson;
    }
}
