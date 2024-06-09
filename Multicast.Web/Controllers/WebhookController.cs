using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
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

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Subscription>> Get([FromQuery, Required] string url)
    {
        var found = await _webhookService.GetAsync(url);

        if (found is null)
            return NotFound();

        return found;
    }

    // This method is idempotent, an argument can be made that it should be a PUT request, I chose POST for simplicity
    [HttpPost(Name = "Subscribe")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Subscribe([FromBody] Subscription subscription)
    {
        await _webhookService.SubscribeAsync(subscription);

        return CreatedAtAction(nameof(Get), new { url = subscription.Url }, subscription);
    }
}
