using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PaymentOrder.Core.Domain.Abstract;
using PaymentOrder.Core.Domain.Entities;
using PaymentOrder.WebCore.Mappers;
using PaymentOrder.WebCore.Models;
using PaymentOrder.WebCore.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentOrder.WebCore.Services.Implementations
{
    public class BankService : IBankService
    {
        private readonly IUnitOfWork db;
        private readonly UserManager<User> userManager;
        private readonly BankMapper bankMapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public BankService(IUnitOfWork db, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;

            bankMapper = new BankMapper();
        }

        public void Save(BankModel model)
        {
            if (model == null)
                return;

            var bank = bankMapper.Map(model);

            bank.ModifiedDate = DateTime.Now;
            bank.CreatorId = Convert.ToInt32(userManager.GetUserId(httpContextAccessor.HttpContext.User)); 

            if(bank.Id != 0)
            {
                db.BankRepository.Update(bank);
            }
            else
            {
                db.BankRepository.Add(bank);
            }
        }

        public void Delete(int id)
        {
            var bank = db.BankRepository.Get(id);

            bank.IsDeleted = true;
            bank.ModifiedDate = DateTime.Now;
            bank.CreatorId = Convert.ToInt32(userManager.GetUserId(httpContextAccessor.HttpContext.User));
            
            db.BankRepository.Update(bank);
        }

        public BankModel Get(int id)
        {
            var bank = db.BankRepository.Get(id);

            if(bank == null)
                return null;
            
            var bankModel = bankMapper.Map(bank);   

            return bankModel;
        }

        public List<BankModel> GetAll()
        {
            var banks = db.BankRepository.GetAll();

            List<BankModel> bankModels = new List<BankModel>();

            for (int i = 0; i < banks.Count; i++)
            {
                var bank = banks[i];
                var bankModel = bankMapper.Map(bank);

                bankModel.No = i + 1;
                bankModels.Add(bankModel);
            }

            return bankModels;
        }
    }
}