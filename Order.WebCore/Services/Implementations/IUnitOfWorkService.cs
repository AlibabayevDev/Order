using Order.Core.Domain.Abstract;
using Order.WebCore.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.WebCore.Services.Implementations
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly IUnitOfWork db;
        public UnitOfWorkService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IOrderItemService OrderItemService => new OrderItemService(db);

        public IOrderService OrderService => new OrderService(db);

        public IProviderService ProviderService => new ProviderService(db);
    }
}
