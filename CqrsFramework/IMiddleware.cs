using System.Threading.Tasks;

namespace CqrsFramework
{
    public interface IMiddleware<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request, HandlerDelegate<TResponse> next);
    }
}
