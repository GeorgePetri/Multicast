using Microsoft.AspNetCore.Mvc;
using Multicast.Domain.Models;

[ApiController]
[Route("[controller]")]
public class WebHookController : ControllerBase
{
    [HttpPost(Name = "Add")]
    public ActionResult Add([FromBody] Subscription subscription)
    {
        return Created();
    }
}

