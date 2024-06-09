using Microsoft.AspNetCore.Mvc;
using Multicast.Domain.Models;

[ApiController]
[Route("[controller]")]
public class WebHookController : ControllerBase
{
    [HttpPost(Name = "Subscribe")]
    public ActionResult Subscribe([FromBody] Subscription subscription)
    {
        return Created();
    }
}

