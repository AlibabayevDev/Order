using Order.Core.Domain.Abstract;
using Order.WebCore.Mappers;
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
        private readonly ProviderMapper providerMapper;
        public ProviderService(IUnitOfWork db)
        {
            this.db = db;
            providerMapper = new ProviderMapper();
        }

        public void Save(ProviderModel model)
        {
            if (model == null)
                return;

            var provider = providerMapper.Map(model);


            if (provider.Id != 0)
            {
                db.ProviderRepository.Update(provider);
            }
            else
            {
                db.ProviderRepository.Add(provider);
            }
        }

        public void Delete(int id)
        {
            var provider = db.ProviderRepository.Get(id);

            db.ProviderRepository.Update(provider);
        }

        public ProviderModel Get(int id)
        {
            var provider = db.ProviderRepository.Get(id);

            if (provider == null)
                return null;

            var providerModel = providerMapper.Map(provider);

            return providerModel;
        }

        public List<ProviderModel> GetAll()
        {
            var providers = db.ProviderRepository.GetAll();

            List<ProviderModel> providerModels = new List<ProviderModel>();

            for (int i = 0; i < providers.Count; i++)
            {
                var provider = providers[i];
                var providerModel = providerMapper.Map(provider);

                providerModels.Add(providerModel);
            }

            return providerModels;
        }
    }
}
