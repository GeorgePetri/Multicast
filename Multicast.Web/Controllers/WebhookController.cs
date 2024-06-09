using Microsoft.AspNetCore.Mvc;
using Multicast.Domain.Models;
using Multicast.Domain.Services;

namespace Multicast.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class WebhookController : ControllerBase
{
    private readonly IWebhookService _webhookService;

    public WebhookController(IWebhookService webHookService) =>
        _webhookService = webHookService;


    [HttpPost(Name = "Subscribe")]
    public async Task<ActionResult> Subscribe([FromBody] Subscription subscription)
    {
        await _webhookService.SubscribeAsync(subscription);

        return Created();
    }
}
