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
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var handler = _serviceProvider.GetRequiredService(handlerType);
            var method = GetType().GetMethod(nameof(Handle), BindingFlags.Instance | BindingFlags.NonPublic).MakeGenericMethod(request.GetType(), typeof(TResponse));
            var task = method.Invoke(this, new object[] {handler, request});
            
            return (Task<TResponse>)task;
        }

        private Task<TResponse> Handle<TRequest, TResponse>(IRequestHandler<TRequest, TResponse> handler, TRequest request)
            where TRequest : IRequest<TResponse>
        {
            var middlewares = _serviceProvider.GetServices<IMiddleware<TRequest, TResponse>>();
            HandlerDelegate<TResponse> handlerDelegate = () => handler.HandleAsync(request);
            var resultDelegate = middlewares.Aggregate(handlerDelegate, (next, middleware) => () => middleware.HandleAsync(request, next));
            return resultDelegate();
        }
    }
}