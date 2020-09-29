using System;
using System.Collections.Generic;
using System.Text;
using CqrsFramework;
using WebApi.Order;
using WebApi.Order.CheckOrder;

namespace UseCases.Order.UpdateOrder
{
    public class UpdateOrderRequest : IRequest, ICheckOrderRequest
    {
        public int Id { get; set; }
        public OrderDto Dto { get; set; }
    }
}
