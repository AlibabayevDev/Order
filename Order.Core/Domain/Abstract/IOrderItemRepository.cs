using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentOrder.Core.Domain.Entities;

namespace PaymentOrder.Core.Domain.Abstract
{
    public interface IOrderItemRepository : ICrudRepository<OrderItem>
    {
       
    }
}
