using System;
using System.Threading.Tasks;
using AutoMapper;
using CqrsFramework;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using UseCases.Order.GetOrder;
using UseCases.Order.UpdateOrder;
using WebApi.Order;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IHandlerDispatcher _handlerDispatcher;

        public OrderController(IHandlerDispatcher handlerDispatcher)
        {
            _handlerDispatcher = handlerDispatcher;
        }

        [HttpGet("{id}")]
        public Task<OrderDto> Get(int id)
        {
            return _handlerDispatcher.SendAsync(new GetOrderQuery {Id = id});
        }

        [HttpPost("{id}")]
        public Task Update(int id, [FromBody]OrderDto dto)
        {
            return _handlerDispatcher.SendAsync(new UpdateOrderCommand {Id = id, Dto = dto});
        }
    }
}
