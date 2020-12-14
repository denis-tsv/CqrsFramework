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
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDto>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetOrderQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<OrderDto> HandleAsync(GetOrderQuery request)
        {
            var order = await _dbContext.Orders.FindAsync(request.Id);

            return _mapper.Map<OrderDto>(order);
        }
    }
}
