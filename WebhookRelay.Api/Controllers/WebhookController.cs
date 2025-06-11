using Microsoft.AspNetCore.Mvc;
using Abp.Webhooks;
using Hangfire;
using WebhookRelay.Application.Jobs;
using WebhookRelay.Application.DTOs.Response;

namespace WebhookRelay.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WebhookController(IBackgroundJobClient backgroundJobClient) : ControllerBase
{
    private readonly IBackgroundJobClient _backgroundJobClient = backgroundJobClient;

    [HttpPost("receive")]
    public IActionResult ReceiveWebhook([FromBody] WebhookResponseDto payload)
    {
        if (payload == null)
        {
            return BadRequest("Invalid payload.");
        }

        _backgroundJobClient.Enqueue<IWebhookDLQJob>(job => job.ExecuteAsync(payload));

        return Accepted(new { Message = "Webhook received. Processing in background." });
    }
}
