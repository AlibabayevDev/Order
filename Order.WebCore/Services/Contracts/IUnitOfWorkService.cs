using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.WebCore.Services.Contracts
{
    public interface IUnitOfWorkService
    {
        IOrderItemService OrderItemService { get; }
        IOrderService OrderService { get; }
        IProviderService ProviderService { get; }
    }
}
