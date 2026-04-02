using Microsoft.AspNetCore.Mvc;
using Shared;
using Order.API.Services;

namespace Order.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(OrderMessage order)
    {
        var rabbit = new RabbitMqService();
        await rabbit.SendMessageAsync(order);

        return Ok("Sipariş kuyruğa gönderildi");
    }
}