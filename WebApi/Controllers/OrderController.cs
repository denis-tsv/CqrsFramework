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
        private readonly IHandlerFactory _handlerFactory;

        public OrderController(IHandlerFactory handlerFactory)
        {
            _handlerFactory = handlerFactory;
        }

        [HttpGet("{id}")]
        public Task<OrderDto> Get(int id)
        {
            return _handlerFactory.CreateHandler<int, OrderDto>().HandleAsync(id);
        }

        [HttpPost("{id}")]
        public Task Update(int id, [FromBody]OrderDto dto)
        {
            return _handlerFactory.CreateHandler<UpdateOrderRequest>().HandleAsync(new UpdateOrderRequest {Id = id, Dto = dto});
        }
    }
}
