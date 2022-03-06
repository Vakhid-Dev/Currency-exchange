﻿using CurenncyExchange.Transaction.Core;

namespace CurenncyExchange.App.Service
{
    public interface ITransactionService
    {
      public Task BuyingCurrencyAsync(TransactionCurrency transactionCurrency);
      public void BuyingCurrencyAsync(ByCurrencyRequest byCurrencyRequest);
    }
}
