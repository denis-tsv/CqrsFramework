using System;
using System.Collections.Generic;
using System.Text;
using CqrsFramework;
using WebApi.Order;

namespace UseCases.Order.GetOrder
{
    public class GetOrderQuery : IRequest<OrderDto>
    {
        public int Id { get; set; }
    }
}
