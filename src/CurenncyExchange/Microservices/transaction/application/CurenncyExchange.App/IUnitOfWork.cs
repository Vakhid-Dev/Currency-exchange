using CurenncyExchange.Transaction.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurenncyExchange.App
{
    public interface IUnitOfWork :IDisposable
    {
        ITransactionRepository TransactionRepository { get; }
        int Complete();
    }
}
