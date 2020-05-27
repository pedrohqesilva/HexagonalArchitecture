using System;

namespace Core.Infrastructure.CrossCutting.Transaction.Interfaces
{
    public interface ITransactions : IDisposable
    {
        void Commit();
    }
}