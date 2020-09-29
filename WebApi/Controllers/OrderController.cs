using System;
using System.Threading.Tasks;
using CqrsFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using UseCases.Order.UpdateOrder;
using WebApi.Order;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;

        public OrderController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpGet("{id}")]
        public Task<OrderDto> Get(int id)
        {
            return _serviceProvider.GetRequiredService<IRequestHandler<int, OrderDto>>().HandleAsync(id);
        }

        [HttpPost("{id}")]
        public Task Update(int id, [FromBody]OrderDto dto)
        {
            return _serviceProvider.GetRequiredService<IRequestHandler<UpdateOrderRequest>>().HandleAsync(new UpdateOrderRequest {Id = id, Dto = dto});
        }
    }
}
