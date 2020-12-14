using System;
using System.Collections.Generic;
using System.Text;

namespace CqrsFramework
{
    public interface IRequest<TResponse>
    {
    }

    public interface IRequest : IRequest<Unit>
    {
    }
}
