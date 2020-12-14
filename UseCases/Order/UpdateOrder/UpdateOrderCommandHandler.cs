using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CqrsFramework;
using Infrastructure.Interfaces;

namespace UseCases.Order.UpdateOrder
{
    public class UpdateOrderCommandHandler : RequestHandler<UpdateOrderCommand>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        protected override async Task HandleAsync(UpdateOrderCommand request)
        {
            var order = await _dbContext.Orders.FindAsync(request.Id);

            _mapper.Map(request.Dto, order);
            await _dbContext.SaveChangesAsync();

        }
    }
}
