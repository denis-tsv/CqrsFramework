using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CqrsFramework
{
    public interface IRequestHandler<TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request);
    }


    public interface IRequestHandler<TRequest> : IRequestHandler<TRequest, Unit>
    {
    }

    public class Unit
    {
        public static readonly Unit Value = new Unit();
    }

    public abstract class RequestHandler<TRequest> : IRequestHandler<TRequest>
    {
        async Task<Unit> IRequestHandler<TRequest, Unit>.HandleAsync(TRequest request)
        {
            await HandleAsync(request);

            return Unit.Value;
        }

        protected abstract Task HandleAsync(TRequest request);
    }
}