using System;
using System.Collections.Generic;
using System.Text;
using CqrsFramework;
using UseCases.Order.Utils;
using WebApi.Order;

namespace UseCases.Order.UpdateOrder
{
    public class UpdateOrderCommand : ICheckOrderRequest, IRequest
    {
        public int Id { get; set; }
        public OrderDto Dto { get; set; }
    }
}
