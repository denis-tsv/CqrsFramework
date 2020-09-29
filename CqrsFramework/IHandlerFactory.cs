namespace CqrsFramework
{
    public interface IHandlerFactory
    {
        IRequestHandler<TRequest, TResponse> CreateHandler<TRequest, TResponse>();

        IRequestHandler<TRequest> CreateHandler<TRequest>();
    }
}