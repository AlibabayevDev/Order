using Order.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.Domain.Abstract
{
    public interface IProviderRepository : ICrudRepository<ProviderEntity>
    {
    }
}
