using System;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Order;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public OrderController(IDbContext dbContext, ICurrentUserService currentUserService, IMapper mapper)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<OrderDto> Get(int id)
        {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null) throw new Exception("Not Found");
            if (order.UserEmail != _currentUserService.Email) throw new Exception("Forbidden");

            return _mapper.Map<OrderDto>(order);
        }

        [HttpPost("{id}")]
        public async Task Update(int id, [FromBody]OrderDto dto)
        {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null) throw new Exception("Not found");
            if (order.UserEmail != _currentUserService.Email) throw new Exception("Forbidden");

            _mapper.Map(dto, order);
            await _dbContext.SaveChangesAsync();
        }
    }
}
