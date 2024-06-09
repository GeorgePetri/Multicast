namespace Multicast.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Multicast.Domain.Models;
using Multicast.Domain.Services;

[ApiController]
[Route("[controller]")]
public class WebHookController : ControllerBase
{
    private readonly IWebHookService _webHookService;

    public WebHookController(IWebHookService webHookService) =>
        _webHookService = webHookService;


    [HttpPost(Name = "Subscribe")]
    public async Task<ActionResult> Subscribe([FromBody] Subscription subscription)
    {
        await _webHookService.SubscribeAsync(subscription);

        return Created();
    }
}
