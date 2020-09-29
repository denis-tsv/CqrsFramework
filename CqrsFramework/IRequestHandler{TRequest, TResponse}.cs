using System.Threading.Tasks;

namespace CqrsFramework
{
    public interface IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request);
    }
}
