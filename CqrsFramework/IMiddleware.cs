using System.Threading.Tasks;

namespace CqrsFramework
{
    public interface IMiddleware<TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request, HandlerDelegate<TResponse> next);
    }
}
