using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CqrsFramework;
using Infrastructure.Interfaces;
using WebApi.Order;

namespace UseCases.Order.GetOrder
{
    public class GetOrderQueryHandler : IRequestHandler<int, OrderDto>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetOrderQueryHandler(IDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<OrderDto> HandleAsync(int id)
        {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null) throw new Exception("Not Found");
            if (order.UserEmail != _currentUserService.Email) throw new Exception("Forbidden");

            return _mapper.Map<OrderDto>(order);
        }
    }
}
