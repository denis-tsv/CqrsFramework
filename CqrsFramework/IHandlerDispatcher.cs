using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework
{
    public interface IHandlerDispatcher
    {
        Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request);
    }
}
