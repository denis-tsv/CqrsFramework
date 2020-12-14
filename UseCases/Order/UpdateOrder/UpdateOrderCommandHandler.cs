using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CqrsFramework;
using Infrastructure.Interfaces;

namespace UseCases.Order.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, int>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public UpdateOrderCommandHandler(IDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<int> HandleAsync(UpdateOrderCommand request)
        {
            var order = await _dbContext.Orders.FindAsync(request.Id);
            if (order == null) throw new Exception("Not found");
            if (order.UserEmail != _currentUserService.Email) throw new Exception("Forbidden");

            _mapper.Map(request.Dto, order);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
