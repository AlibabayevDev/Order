using Order.Core.Domain.Abstract;
using Order.WebCore.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.WebCore.Services.Implementations
{
    public class UnitOfWorkService : IUnifOfWorkService
    {
        private readonly IUnitOfWork db;
        public UnitOfWorkService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IOrderItemService orderItemService => new OrderItemService(db);

        public IOrderService orderService => new OrderService(db);

        public IProviderService providerService => new ProviderService(db);
    }
}
