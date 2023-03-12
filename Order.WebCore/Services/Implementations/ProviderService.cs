using Order.Core.Domain.Abstract;
using Order.WebCore.Models;
using Order.WebCore.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.WebCore.Services.Implementations
{
    public class ProviderService : IProviderService
    {
        private readonly IUnitOfWork db;
        public ProviderService(IUnitOfWork db)
        {
            this.db = db;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ProviderModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProviderModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save(ProviderModel model)
        {
            throw new NotImplementedException();
        }
    }
}
