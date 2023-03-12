using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.WebCore.Services.Contracts
{
    public interface IUnifOfWorkService
    {
        IOrderItemService orderItemService { get; }
        IOrderService orderService { get; }
        IProviderService providerService { get; }
    }
}
