using Order.WebCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.WebCore.Services.Contracts
{
    public interface IOrderService : IService<OrderModel>
    {
        bool Check(OrderModel model);
        bool CheckId(OrderModel order);
    }
}
