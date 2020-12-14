using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework
{
    public delegate Task<TResponse> HandlerDelegate<TResponse>(); 

    public interface IMiddleware<TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request, HandlerDelegate<TResponse> next);
    }
}
