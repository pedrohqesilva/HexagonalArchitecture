using Core.Infrastructure.CrossCutting.Transaction.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.Common;

namespace Core.Infrastructure.CrossCutting.Transaction
{
    public class Transactions : ITransactions
    {
        private readonly DbTransaction _dbTransaction;

        public Transactions(IServiceProvider serviceProvider)
        {
            _dbTransaction = serviceProvider.GetService<DbTransaction>();
        }

        public void Commit()
        {
            _dbTransaction.Commit();
        }

        public void Dispose()
        {
            _dbTransaction.Dispose();
        }
    }
}