﻿using Order.Core.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.DataAccess.Sql
{
    public class SqlUnitOfWork:IUnitOfWork
    {
        private readonly string _connectionString;

        public SqlUnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IProviderRepository ProviderRepository => new SqlProviderRepository(_connectionString);
        public IOrderItemRepository OrderItemRepository => new SqlOrderItemRepository(_connectionString);
        public IOrderRepository OrderRepository => new SqlOrderRepository(_connectionString);
    }
}
