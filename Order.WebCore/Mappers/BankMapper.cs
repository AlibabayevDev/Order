using PaymentOrder.Core.Domain.Entities;
using PaymentOrder.WebCore.Models;

namespace PaymentOrder.WebCore.Mappers
{
    public class BankMapper : BaseMapper<Bank, BankModel>
    {
        public override Bank Map(BankModel model)
        {
            return new Bank()
            {
                Id = model.Id,
                Name = model.Name,
                VOEN = model.VOEN,
                CorrespondentAccount = model.CorrespondentAccount,
                SWIFT = model.SWIFT
            };
        }

        public override BankModel Map(Bank entity)
        {
            return new BankModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                VOEN = entity.VOEN,
                CorrespondentAccount = entity.CorrespondentAccount,
                SWIFT = entity.SWIFT
            };
        }
    }
}