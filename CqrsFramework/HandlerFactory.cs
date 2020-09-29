using System;
using Microsoft.Extensions.DependencyInjection;

namespace CqrsFramework
{
    public class HandlerFactory : IHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public HandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IRequestHandler<TRequest, TResponse> CreateHandler<TRequest, TResponse>()
        {
            return _serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
        }

        public IRequestHandler<TRequest> CreateHandler<TRequest>()
        {
            return _serviceProvider.GetRequiredService<IRequestHandler<TRequest>>();
        }
    }
}