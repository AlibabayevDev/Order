﻿using Order.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.Domain.Abstract
{
    public interface IOrderRepository : ICrudRepository<OrderEntity>
    {
        OrderEntity CheckOrder(int ProviderId,string OrderNumber);
        OrderEntity CheckOrderId(int OrderId, int ProviderId, string OrderNumber);
    }
}
