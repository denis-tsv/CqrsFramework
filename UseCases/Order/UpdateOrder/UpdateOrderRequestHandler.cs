using System;
using System.Threading.Tasks;
using AutoMapper;
using CqrsFramework;
using Infrastructure.Interfaces;
using UseCases.Order.UpdateOrder;

namespace UseCases.Order
{
    public class UpdateOrderRequestHandler : RequestHandler<UpdateOrderRequest>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateOrderRequestHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        protected override async Task HandleAsync(UpdateOrderRequest request)
        {
            var order = await _dbContext.Orders.FindAsync(request.Id);

            _mapper.Map(request.Dto, order);
            await _dbContext.SaveChangesAsync();
        }
    }
}
