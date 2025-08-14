using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MySecureWebApi.Services;

namespace MySecureWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController(IHubContext<ChatHub> hubContext) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SendGlobalMessage([FromBody] string message)
    {
        await hubContext.Clients.All.SendAsync("ReceiveMessage", message);
        return Ok();
    }
}
