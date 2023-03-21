using Order.WebCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.WebCore.Services.Contracts
{
    public interface IOrderItemService : IService<OrderItemModel>
    {
        List<OrderItemModel> GetById(int id);
    }
}
