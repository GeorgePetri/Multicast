using Microsoft.AspNetCore.Mvc;
using Multicast.Domain.Models;
using Multicast.Domain.Services;

namespace Multicast.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class WebhookController : ControllerBase
{
    private readonly IWebhookService _webHookService;

    public WebhookController(IWebhookService webHookService) =>
        _webHookService = webHookService;


    [HttpPost(Name = "Subscribe")]
    public async Task<ActionResult> Subscribe([FromBody] Subscription subscription)
    {
        await _webHookService.SubscribeAsync(subscription);

        return Created();
    }
}
