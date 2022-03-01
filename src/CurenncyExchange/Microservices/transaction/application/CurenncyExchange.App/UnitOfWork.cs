using CurenncyExchange.App.Repository;
using CurenncyExchange.Data.Context;
using CurenncyExchange.Transaction.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurenncyExchange.App
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TransactionContext _context;
        public UnitOfWork(TransactionContext context)
        {
            _context = context;
            TransactionRepository = new TransactionRepository(_context);
        }
        public ITransactionRepository TransactionRepository { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

