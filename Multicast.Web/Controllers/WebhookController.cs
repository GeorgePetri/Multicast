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
    private readonly ISubscriptionService _subscriptionService;
    private readonly IEventService _eventService;

    public WebhookController(ISubscriptionService subscriptionService, IEventService eventService)
    {
        _subscriptionService = subscriptionService;
        _eventService = eventService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<Subscription>> Get([FromQuery, Required] string url)
    {
        var found = await _subscriptionService.GetAsync(url);

        if (found is null)
            return NotFound();

        return found;
    }

    // This method is idempotent, an argument can be made that it should be a PUT request, I chose POST for simplicity
    [HttpPost("Subscribe")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Subscribe([FromBody] Subscription subscription)
    {
        await _subscriptionService.SubscribeAsync(subscription);

        return CreatedAtAction(nameof(Get), new { url = subscription.Url }, subscription);
    }

    [HttpPost("SendEventsToAll")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SendEventsToAll([FromBody] Event @event)
    {
        await _eventService.PublishToAllAsync(@event);

        return NoContent();
    }
}
