using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CqrsFramework
{
    public interface IHandlerDispatcher
    {
        Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request);
    }

    public class HandlerDispatcher : IHandlerDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public HandlerDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            var method = this.GetType().GetMethod(nameof(HandleAsync), BindingFlags.Instance | BindingFlags.NonPublic)
                .MakeGenericMethod(request.GetType(), typeof(TResponse));
            var result = method.Invoke(this, new [] {request});
            return (Task<TResponse>)result;
        }

        protected Task<TResponse> HandleAsync<TRequest, TResponse>(TRequest request)
        {
            var handler = _serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
            return handler.HandleAsync(request);
        }
    }
}
