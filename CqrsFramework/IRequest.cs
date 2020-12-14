using System;
using System.Collections.Generic;
using System.Text;

namespace CqrsFramework
{
    public interface IRequest : IRequest<Unit>
    {
    }
}
