using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class WebHookController : ControllerBase
{
    [HttpPost(Name = "Add")]
    public ActionResult Add()
    {
        return Created();
    }
}

