using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CqrsFramework
{
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
            var middlewares = _serviceProvider.GetServices<IMiddleware<TRequest, TResponse>>();

            HandlerDelegate<TResponse> handlerDelegate = () => handler.HandleAsync(request);
            var resultHandler = middlewares.Aggregate(handlerDelegate, (next, middleware) => () => middleware.HandleAsync(request, next));

            return resultHandler();
        }
    }
}