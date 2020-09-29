using System;
using System.Threading.Tasks;
using CqrsFramework;
using Infrastructure.Interfaces;
using WebApi.Order.CheckOrder;

namespace UseCases.Order.CheckOrder
{
    public class CheckOrderMiddleware<TRequest, TResponse> : IMiddleware<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICheckOrderRequest
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public CheckOrderMiddleware(IDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }
        public async Task<TResponse> HandleAsync(TRequest request, HandlerDelegate<TResponse> next)
        {
            var order = await _dbContext.Orders.FindAsync(request.Id);
            if (order == null) throw new Exception("Not found");
            if (order.UserEmail != _currentUserService.Email) throw new Exception("Forbidden");

            return await next();
        }
    }
}
