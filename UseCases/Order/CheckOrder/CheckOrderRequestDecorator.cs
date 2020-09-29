using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CqrsFramework;
using Infrastructure.Interfaces;
using WebApi.Order.CheckOrder;

namespace UseCases.Order.CheckOrder
{
    public class CheckOrderRequestDecorator<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICheckOrderRequest
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IRequestHandler<TRequest, TResponse> _handler;

        public CheckOrderRequestDecorator(IDbContext dbContext, ICurrentUserService currentUserService, IRequestHandler<TRequest, TResponse> handler)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
            _handler = handler;
        }
        public async Task<TResponse> HandleAsync(TRequest request)
        {
            var order = await _dbContext.Orders.FindAsync(request.Id);
            if (order == null) throw new Exception("Not found");
            if (order.UserEmail != _currentUserService.Email) throw new Exception("Forbidden");

            return await _handler.HandleAsync(request);
        }
    }
}
