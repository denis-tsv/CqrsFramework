using System;
using System.Threading.Tasks;
using AutoMapper;
using CqrsFramework;
using Infrastructure.Interfaces;
using WebApi.Order;

namespace UseCases.Order
{
    public class GetOrderRequestHandler : IRequestHandler<GetOrderRequest, OrderDto>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetOrderRequestHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<OrderDto> HandleAsync(GetOrderRequest request)
        {
            var order = await _dbContext.Orders.FindAsync(request.Id);

            return _mapper.Map<OrderDto>(order);
        }

    }
}
