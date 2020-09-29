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
        private readonly ICurrentUserService _currentUserService;

        public UpdateOrderRequestHandler(IDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        protected override async Task HandleAsync(UpdateOrderRequest request)
        {
            var order = await _dbContext.Orders.FindAsync(request.Id);
            if (order == null) throw new Exception("Not found");
            if (order.UserEmail != _currentUserService.Email) throw new Exception("Forbidden");

            _mapper.Map(request.Dto, order);
            await _dbContext.SaveChangesAsync();
        }
    }
}
