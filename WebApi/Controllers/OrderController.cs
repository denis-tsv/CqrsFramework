using System;
using System.Threading.Tasks;
using AutoMapper;
using CqrsFramework;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using UseCases.Order.UpdateOrder;
using WebApi.Order;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IRequestHandler<int, OrderDto> _getOrderQueryHandler;

        public OrderController(IRequestHandler<int, OrderDto> getOrderQueryHandler)
        {
            _getOrderQueryHandler = getOrderQueryHandler;
        }

        [HttpGet("{id}")]
        public Task<OrderDto> Get(int id)
        {
            return _getOrderQueryHandler.HandleAsync(id);
        }

        [HttpPost("{id}")]
        public Task Update(int id, [FromBody]OrderDto dto, [FromServices]IRequestHandler<UpdateOrderCommand, int> updateOrderCommandHandler)
        {
            return updateOrderCommandHandler.HandleAsync(new UpdateOrderCommand {Id = id, Dto = dto});
        }
    }
}
